using System;
using System.Diagnostics;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server.Functions
{
    public static class Statistics
    {
        private static Stopwatch upTime = new Stopwatch();
        private static Timer upTimer = new Timer() { Enabled = true, Interval = 100 };

        private static Timer updatetimer = new Timer() { Enabled = true, Interval = 1000 };

        private static int clientCount = 0;

        private static int updaterCount = 0;

        private static int disconnectCount = 0;

        private static int usersAuthenticated = 0;

        private static int usersRejected = 0;

        private static int usersBanned = 0;

        public static void UpdateClientCount(bool add)
        {
            if (add) { clientCount++; }
            else { clientCount--; disconnectCount++; }
        }

        public static void UpdateUpdaterCount(bool add)
        {
            if (add) { updaterCount++; }
            else { updaterCount--; disconnectCount++; }
        }

        internal static void UpdateAuthenticatedCount(bool add)
        {
            if (add) { usersAuthenticated++; }
            else { usersAuthenticated--; }
        }

        internal static void UpdateRejectCount(bool add)
        {
            if (add) { usersRejected++; }
            else { usersRejected--; }
        }

        internal static void UpdateBannedCount(bool add)
        {
            if (add) { usersBanned++; }
            else { usersBanned--; }
        }

        private static void update()
        {
            Task.Run(() => SetIO());
            Task.Run(() => setUpdates());
            Task.Run(() => setConnections());
            Task.Run(() => setLogins());
        }

        public static void StartUptime()
        {
            upTime.Start();
            upTimer.Tick += upTimerTick;
        }

        private static void upTimerTick(object sender, EventArgs e) { Task.Run(() => SetUptime()); }

        private static void SetUptime() { GUI.Instance.Invoke(new MethodInvoker(delegate { GUI.Instance.Uptime.Text = upTime.ElapsedMilliseconds.ToString(); })); }

        public static void StartUpdating()
        {
            updatetimer.Tick += updateTimerTick;
            updatetimer.Start();
        }

        private static void updateTimerTick(object sender, EventArgs e) { update(); }

        public static void SetIO()
        {
            GUI.Instance.Invoke(new MethodInvoker(delegate
            {
                GUI.Instance.IP.Text = OPT.GetString("io.ip");
                GUI.Instance.Port.Text = OPT.GetString("io.port");
                GUI.Instance.Maintenance.Text = (OPT.GetBool("maintenance")) ? "ON" : "OFF";
            }));
        }

        private static void setUpdates()
        {
            GUI.Instance.Invoke(new MethodInvoker(delegate {
                GUI.Instance.DataCount.Text = IndexManager.DataCount.ToString();
                GUI.Instance.ResourceCount.Text = IndexManager.ResourceCount.ToString();
                GUI.Instance.DeleteCount.Text = OPT.DeleteCount.ToString();
            }));       
        }

        private static void setConnections()
        {
            GUI.Instance.Invoke(new MethodInvoker(delegate
            {
                GUI.Instance.LaunchersCount.Text = clientCount.ToString();
                GUI.Instance.UpdatersCount.Text = updaterCount.ToString();
                GUI.Instance.DisconnectCount.Text = disconnectCount.ToString();
            }));
        }

        private static void setLogins()
        {
            GUI.Instance.Invoke(new MethodInvoker(delegate
            {
                GUI.Instance.authenticatedCount.Text = usersAuthenticated.ToString();
                GUI.Instance.rejectedCount.Text = usersRejected.ToString();
                GUI.Instance.bannedCount.Text = usersBanned.ToString();
            }));
        }
    }
}
