using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsInput;

namespace WKS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var button = new Button
            {
                Text = "พิมพ์ไปยังหน้าต่างชื่อ 'abc'",
                Dock = DockStyle.Fill
            };
            button.Click += OnClickSendText;
            Controls.Add(button);
        } // Win32 API สำหรับควบคุมหน้าต่าง
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private const int SW_RESTORE = 9;



        private void OnClickSendText(object sender, EventArgs e)
        {
            // หา process ที่ชื่อหน้าต่างมีคำว่า "abc"
            var abcWindows = Process.GetProcesses()
                .Where(p => !string.IsNullOrEmpty(p.MainWindowTitle) && p.MainWindowTitle.Contains("Rag"))
                .ToList();

            var ScreenWindows = Process.GetProcesses()
                .Where(p => !string.IsNullOrEmpty(p.MainWindowTitle) && p.MainWindowTitle.Contains("On-Screen"))
                .ToList();

            if (abcWindows.Count >= 1)
            {
                var targetProcess = abcWindows[0]; // ตัวที่ 2 (index เริ่มจาก 0)

                IntPtr hWnd = targetProcess.MainWindowHandle;

                if (IsIconic(hWnd))
                {
                    ShowWindow(hWnd, SW_RESTORE);
                }

                SetForegroundWindow(hWnd);

                //hWnd = ScreenWindows[0].MainWindowHandle;
                //if (IsIconic(hWnd))
                //{
                //    ShowWindow(hWnd, SW_RESTORE);
                //}
                //SetForegroundWindow(hWnd);

                //System.Windows.Forms.SendKeys.SendWait("H");

                // ส่งข้อความโดยใช้ InputSimulator
                var sim = new InputSimulator();
                sim.Keyboard.TextEntry("H");
            }
            else
            {
                MessageBox.Show("ไม่พบหน้าต่างชื่อ 'abc' ครบ 5 อัน", "แจ้งเตือน");
            }
        }
    }
}
