using System;
using System.Collections.Generic;
using Server.Structures;

namespace Server.Functions
{
    public static class Output
    {
        private static bool locked;
        private static bool Locked
        {
            get { return locked; }
            set
            {
                locked = value;
                if (locked && !value) { OnUnlock(); }
            }
        }

        private static void OnUnlock() { if (messageQueue.Count > 0) { foreach (Message queuedMsg in messageQueue) { print(queuedMsg); } } }

        private static List<Message> messageQueue = new List<Message>();

        public static void Write(Message msg)
        {
            if (Locked)
                messageQueue.Add(msg);
            else
                print(msg);
        }

        public static void WriteAndLock(Message msg)
        {
            Locked = true;
            print(msg);
        }

        public static void WriteAndUnlock(Message msg)
        {
            
            print(msg);
            Locked = false;
        }

        private static void print(Message msg)
        {
            string outputText = msg.Text;

            if (msg.AddBreak) { for (int i = 0; i < msg.BreakCount; i++) { outputText += Environment.NewLine; } }
                      
            GUI.Instance.Invoke(new System.Windows.Forms.MethodInvoker(delegate { GUI.Instance.OutputText.AppendText(outputText); }));
        }
    }
}
