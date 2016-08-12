using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Google.Apis.Drive.v2;
using Google.Apis.Drive.v2.Data;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive;
using Google.Apis.Services;
using System.Security.Cryptography.X509Certificates;

namespace Client.Functions
{
    public class Drive
    {
        GUI guiInstance = GUI.Instance;

        internal DriveService driveService;
        internal ServiceAccountCredential credential;

        protected void setKeyFile()
        {
            string[] scopes = new string[] { DriveService.Scope.Drive };

            var serviceAccountEmail = "horizon-service@portal-140102.iam.gserviceaccount.com";
            var certificate = new X509Certificate2(Properties.Resources.Portal_b80398deda00, "notasecret", X509KeyStorageFlags.Exportable);
            credential = new ServiceAccountCredential(new ServiceAccountCredential.Initializer(serviceAccountEmail) { Scopes = scopes }.FromCertificate(certificate));
        }

        protected void startDrive()
        {
            driveService = new DriveService(new BaseClientService.Initializer() { ApiKey = "AIzaSyAJqagk9gOiQRoK-Y3Z9R6KpSMa4vdZwzk", ApplicationName = "client", HttpClientInitializer = credential });
        }
           
        public void Start()
        {
            guiInstance.UpdateStatus(1, "Starting Google Drive interface...");
            setKeyFile();
            startDrive();
        }

        public string GetFile(string name)
        {
            FilesResource.ListRequest request = driveService.Files.List();
            string decodedName = Path.GetFileNameWithoutExtension(UpdateHandler.Instance.Core.DecodeName(name));
            request.Q = string.Format(@"title = '{0}.zip' and trashed=false", decodedName);
            FileList fileList = request.Execute();
            if (fileList.Items.Count > 0)
            {
                string fileDirectory = string.Concat(Directory.GetCurrentDirectory(), @"/Downloads");
                string filePath = string.Format(@"{0}/{1}.zip", fileDirectory, name);

                if (!Directory.Exists(fileDirectory)) { Directory.CreateDirectory(fileDirectory); }

                if (System.IO.File.Exists(filePath)) { System.IO.File.Delete(filePath); }

                byte[] fileBytes = driveService.HttpClient.GetByteArrayAsync(fileList.Items[0].DownloadUrl).Result;

                guiInstance.UpdateProgressMaximum(1, fileBytes.Length);

                using (FileStream fs = System.IO.File.Create(filePath))
                {
                    for (int i = 0; i < fileBytes.Length; i = i + Math.Min(64000, fileBytes.Length - i))
                    {
                        guiInstance.UpdateProgressValue(1, i);
                        fs.Write(fileBytes, i, Math.Min(64000, fileBytes.Length - i));
                    }

                    return filePath;
                }
            }

            return null;
        }
    }
}
