using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Handle = System.IntPtr;
using System.IO;

namespace Client.Functions
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SECURITY_ATTRIBUTES
    {
        public int nLength;
        public IntPtr lpSecurityDescriptor;
        public int bInheritHandle;
    }

    /// <summary>
    /// Allows the Launcher to fool the 8.1+ SFrame into believing a 'Retail' Launcher has launched it.
    /// Special thanks to xXExiledXx for making me aware that I should work on this for the Portal project
    /// also many thanks to Glandu2 for letting me pummel him with questions and even providing a great example to work 
    /// with
    /// </summary>
    public class SFrameBypass
    {
        private static IntPtr _Handle;
        private static IntPtr _Attributes = IntPtr.Zero;

        [DllImport("Kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Auto)]
        static extern Handle CreateEvent(ref SECURITY_ATTRIBUTES lpEventAttributes, [In, MarshalAs(UnmanagedType.Bool)] bool bManualReset, [In, MarshalAs(UnmanagedType.Bool)] bool bIntialState, [In, MarshalAs(UnmanagedType.BStr)] string lpName);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool CloseHandle(IntPtr hObject);

        [DllImport("kernel32", SetLastError = true, ExactSpelling = true)]
        internal static extern Int32 WaitForSingleObject(IntPtr handle, Int32 milliseconds);

        /// <summary>
        /// Creates a windows event and returns the EventID (IntPtr) in string for
        /// </summary>
        public static string EventHandle
        {
            get { return CreateEventHandle().ToString(); }
        }

        /// <summary>
        /// Creates an event that the SFrame will be listening for and returns it's specific Event-ID to be attached to the SFrame.exe as
        /// an environmental variable
        /// </summary>
        /// <returns>IntPtr representing the EventID</returns>
        static Handle CreateEventHandle()
        {
            SECURITY_ATTRIBUTES securityAttrib = new SECURITY_ATTRIBUTES();

            securityAttrib.bInheritHandle = 1;
            securityAttrib.lpSecurityDescriptor = IntPtr.Zero;
            securityAttrib.nLength = Marshal.SizeOf(typeof(SECURITY_ATTRIBUTES));

            _Handle = CreateEvent(ref securityAttrib, false, false, String.Empty);
            return _Handle;
        }

        /// <summary>
        /// Wait for the event to signal to a maximum period of TimeoutInSecs total seconds.
        /// Returns true if the event signaled, false if timeout occurred.
        /// </summary>
        static bool Wait(int TimeoutInSecs)
        {
            int rc = WaitForSingleObject(_Handle, TimeoutInSecs * 1000);
            CloseHandle(_Handle);
            return rc == 0;
        }

        /// <summary>
        /// Launches the SFrame without the need of outside exe or libraries by Creating a Windows-Event and the proper EnvironmentVariables
        /// that the SFrame will check for.
        /// </summary>
        /// <param name="waitTime">Time the Launcher should wait for SFrame to trigger the Windows-Event</param>
        /// <param name="arguments">Start arguments needed to launch the SFrame</param>
        /// <returns>bool value indicating success or failure</returns>
        static public bool Start(int waitTime, string arguments)
        {
            try
            {
                var p = new ProcessStartInfo();
                p.FileName = string.Concat(OPT.Instance.GetString("clientdirectory"), @"\SFrame.exe");
                p.WorkingDirectory = OPT.Instance.GetString("clientdirectory");
                p.Arguments = arguments;
                p.EnvironmentVariables["SFrame.exe_PARENT"] = "Launcher.exe";
                p.EnvironmentVariables["SFrame.exe_RUNNER"] = EventHandle;
                p.UseShellExecute = false;
                Process.Start(p);

                return (Wait(waitTime)) ? true : false;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Launch Exception", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);    
            }

            return false;
        }
    }
}
