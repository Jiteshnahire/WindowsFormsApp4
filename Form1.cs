using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        private TimeSpan previousIdleTime = TimeSpan.Zero;
        private TimeSpan previousActiveTime = TimeSpan.Zero;

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //// for active
            //TimeSpan idleTime = GetSystemIdleTime();
            //label1.Text = idleTime.ToString();

            //TimeSpan activeTime = GetUserActiveTime();
            //label2.Text = activeTime.ToString();
        }

        [DllImport("user32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        // Structure to hold the last input information
        internal struct LASTINPUTINFO
        {
            public uint cbSize;
            public uint dwTime;
        }

        public static TimeSpan GetSystemIdleTime()
        {
            LASTINPUTINFO lastInputInfo = new LASTINPUTINFO();
            lastInputInfo.cbSize = (uint)Marshal.SizeOf(lastInputInfo);

            if (!GetLastInputInfo(ref lastInputInfo))
                throw new Exception("Failed to retrieve the last input information.");

            uint lastInputTime = lastInputInfo.dwTime;
            uint currentTickCount = (uint)Environment.TickCount;
            uint idleTime = currentTickCount - lastInputTime;

            return TimeSpan.FromMilliseconds(idleTime);
        }

        [DllImport("kernel32.dll")]
        private static extern uint GetTickCount();

        public static TimeSpan GetUserActiveTime()
        {
            LASTINPUTINFO lastInputInfo = new LASTINPUTINFO();
            lastInputInfo.cbSize = (uint)Marshal.SizeOf(lastInputInfo);

            if (!GetLastInputInfo(ref lastInputInfo))
                throw new Exception("Failed to retrieve the last input information.");

            uint lastInputTime = lastInputInfo.dwTime;
            uint currentTickCount = GetTickCount();
            uint activeTime = currentTickCount - lastInputTime;

            return TimeSpan.FromMilliseconds(activeTime);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan currentIdleTime = GetSystemIdleTime();
            TimeSpan totalIdleTime = previousIdleTime + currentIdleTime;

            // Calculate the total time by adding the current active time to the previous active time
            TimeSpan currentActiveTime = GetUserActiveTime();
            TimeSpan totalActiveTime = previousActiveTime + currentActiveTime;

            // Display the total idle time and active time
            label1.Text = totalIdleTime.ToString();
            label2.Text = totalActiveTime.ToString();

            /* TimeSpan idleTime = GetSystemIdleTime();
             label1.Text = idleTime.ToString();

             TimeSpan activeTime = GetUserActiveTime();
             label2.Text = activeTime.ToString();

             // Store the current idle time and active time for future calculations
             previousIdleTime = idleTime;
             previousActiveTime = activeTime;*/
        }
    }
}