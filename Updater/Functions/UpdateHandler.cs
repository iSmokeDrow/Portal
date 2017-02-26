using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Updater.Functions
{
    public static class UpdateHandler
    {
        internal static string remoteHash = null;
        internal static byte[] buffer = null;

        public static void Initialize(long length, string hash)
        {
            buffer = new byte[length];
            remoteHash = hash;
        }

        public static void WriteToBuffer(int chunkSize, int offset, byte[] fileBytes) { Buffer.BlockCopy(fileBytes, 0, buffer, offset, chunkSize); }

        public static void WriteFile()
        {
            Console.Write("[OK]\n\tWriting Launcher to disk...");

            string launcherPath = string.Format(@"{0}\{1}", Directory.GetCurrentDirectory(), "Launcher.exe");
            string backupPath = string.Format(@"{0}\{1}", Directory.GetCurrentDirectory(), "Launcher.exe.old");

            if (File.Exists(launcherPath)) { File.Move(launcherPath, backupPath); }

            using (FileStream fs = File.Create(launcherPath))
            {
                fs.Write(buffer, 0, buffer.Length);
                Console.WriteLine("[OK]");
            }

            bool verified = verify(launcherPath);

            Console.WriteLine("\tVerifying Launcher...{0}", verified ? "[Verified]" : "[Fail]");

            if (!verified)
            {
                Console.WriteLine("\tReverting to previous Launcher...");
                if (File.Exists(launcherPath)) { File.Delete(launcherPath); }
                File.Move(backupPath, launcherPath);
                Console.WriteLine("[OK]");
            }
            else
            {
                Console.WriteLine("\tLauncher update completed!");
                Program.OnLauncherVerified();
            }
        }

        internal static bool verify(string path)
        {
            string localHash = Hash.GetSHA512Hash(path);
            return (string.Equals(@localHash, @remoteHash)) ? true : false;
        }

    }
}
