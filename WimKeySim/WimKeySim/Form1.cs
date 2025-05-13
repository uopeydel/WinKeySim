using InputInterceptorNS;
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Windows.Automation;
using System.Windows.Forms;
using WindowsInput;
using System;
using System.Drawing;

namespace WimKeySim
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            AllocConsole();



            bool isAdmin = new
                WindowsPrincipal(WindowsIdentity.GetCurrent())
                .IsInRole(WindowsBuiltInRole.Administrator);

            Console.WriteLine("Is Admin: " + isAdmin);



            if (InitializeDriver())
            {
                //MouseHook mouseHook = new MouseHook(MouseCallback);
                //KeyboardHook keyboardHook = new KeyboardHook(KeyboardCallback);
                //Console.WriteLine("Hooks enabled. Press any key to release.");
                //Console.ReadKey();
                //keyboardHook.Dispose();
                //mouseHook.Dispose();
            }
            else
            {
                InstallDriver();
            }

        } // Win32 API สำหรับควบคุมหน้าต่าง

        #region input intercep




        void MouseCallback(ref MouseStroke mouseStroke)
        {
            Console.WriteLine($"{mouseStroke.X} {mouseStroke.Y} {mouseStroke.Flags} {mouseStroke.State} {mouseStroke.Information}"); // Mouse XY coordinates are raw
                                                                                                                                     // Invert mouse X
                                                                                                                                     //mouseStroke.X = -mouseStroke.X;
                                                                                                                                     // Invert mouse Y
                                                                                                                                     //mouseStroke.Y = -mouseStroke.Y;
        }

        void KeyboardCallback(ref KeyStroke keyStroke)
        {
            Console.WriteLine($"{keyStroke.Code} {keyStroke.State} {keyStroke.Information}");
            // Button swap
            //keyStroke.Code = keyStroke.Code switch {
            //    KeyCode.A => KeyCode.B,
            //    KeyCode.B => KeyCode.A,
            //    _ => keyStroke.Code,
            //};
        }

        Boolean InitializeDriver()
        {
            if (InputInterceptor.CheckDriverInstalled())
            {
                Console.WriteLine("Input interceptor seems to be installed.");
                if (InputInterceptor.Initialize())
                {
                    Console.WriteLine("Input interceptor successfully initialized.");
                    return true;
                }
            }
            Console.WriteLine("Input interceptor initialization failed.");
            return false;
        }

        void InstallDriver()
        {
            Console.WriteLine("Input interceptor not installed.");
            if (InputInterceptor.CheckAdministratorRights())
            {
                Console.WriteLine("Installing...");
                if (InputInterceptor.InstallDriver())
                {
                    Console.WriteLine("Done! Restart your computer.");
                }
                else
                {
                    Console.WriteLine("Something... gone... wrong... :(");
                }
            }
            else
            {
                Console.WriteLine("Restart program with administrator rights so it will be installed.");
            }
        }
        #endregion


        #region screen capture

        public Bitmap CaptureScreen(int x, int y, int wid = 32, int hei = 32)
        {

            try
            {
                Rectangle captureArea = new Rectangle(x, y, wid, hei);
                CaptureScreen(captureArea);
            }
            catch
            {

            }

            return _croppedBitmap;
        }

        private static Bitmap _croppedBitmap;
        private static Bitmap _bmSmall;
        private void CaptureScreen(Rectangle captureArea)
        {
            int screenCaptureWidth = (int)Screen.PrimaryScreen.Bounds.Width;
            screenCaptureWidth = 600;
            //Bitmap result;
            using (Bitmap bitmap = new Bitmap(screenCaptureWidth, Screen.PrimaryScreen.Bounds.Height))
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    graphics.CopyFromScreen(Point.Empty, Point.Empty, bitmap.Size);
                }
                //Bitmap croppedBitmap = bitmap.Clone(captureArea, bitmap.PixelFormat);
                //result = croppedBitmap; 
                _croppedBitmap = CropBitmap(bitmap, captureArea);
            }
            //return result;
        }
        private Bitmap CropBitmap(Bitmap source, Rectangle cropArea)
        {
            // Check if the crop area is within the bounds of the source bitmap
            if (!cropArea.IntersectsWith(GetBitmapBounds(source)))
            {
                throw new ArgumentException("Crop area is outside the bounds of the source bitmap");
            }

            // Create a new bitmap with the same dimensions as the crop area
            Bitmap croppedBitmap = new Bitmap(cropArea.Width, cropArea.Height);

            // Use Graphics to draw the cropped portion of the source bitmap onto the new bitmap
            using (Graphics g = Graphics.FromImage(croppedBitmap))
            {
                g.DrawImage(source, new Rectangle(0, 0, cropArea.Width, cropArea.Height), cropArea, GraphicsUnit.Pixel);
            }

            // Return the cropped bitmap
            return croppedBitmap;
        }
        private Rectangle GetBitmapBounds(Bitmap bitmap)
        {
            Rectangle bounds = Rectangle.Empty;
            bounds = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            return bounds;
        }
        #endregion


        #region get cursor image
        [StructLayout(LayoutKind.Sequential)]
        struct CURSORINFO
        {
            public int cbSize;
            public int flags;
            public IntPtr hCursor;
            public Point ptScreenPos;
        }

        [DllImport("user32.dll")]
        static extern bool GetCursorInfo(out CURSORINFO pci);

        [DllImport("user32.dll")]
        static extern bool DrawIcon(IntPtr hDC, int X, int Y, IntPtr hIcon);

        [DllImport("user32.dll")]
        static extern IntPtr CopyIcon(IntPtr hIcon);
        #endregion

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        private const int SW_RESTORE = 9;

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool AllocConsole();





        private void MouseClick(int x, int y)
        {
            void callback(ref MouseStroke stroke)
            {
                Console.WriteLine($"Mouse event: {stroke.State}");
            }
            //CallbackAction callback = (device, stroke) =>
            //{
            //    Console.WriteLine($"Mouse event: {stroke.State}");
            //};
            // สร้าง MouseHook โดยกำหนด MouseFilter และ CallbackAction
            using (var mouseHook = new MouseHook(MouseFilter.All, callback))
            {


                //// ย้ายเคอร์เซอร์ไปยังตำแหน่ง (500, 500)
                //mouseHook.SetCursorPosition(500, 500);

                //// คลิกซ้าย
                //mouseHook.SimulateLeftButtonClick();
                // สร้าง MouseHook
                //var mouseHook = new MouseHook();

                // ย้ายเคอร์เซอร์ไปยังตำแหน่ง (500, 500)
                mouseHook.SetCursorPosition(x, y);

                // คลิกซ้าย
                mouseHook.SimulateLeftButtonClick(); Thread.Sleep(300);
                mouseHook.SimulateLeftButtonClick(); Thread.Sleep(500);
                mouseHook.SimulateLeftButtonClick();
                // เลื่อนล้อเมาส์ลง
                mouseHook.SimulateScrollDown();

            }

            Bitmap cursorImage = CaptureScreen(x, y);
            var imageDir = Path.Combine(Directory.GetCurrentDirectory(), "imageDir");
            if (!Directory.Exists(imageDir))
            {
                Directory.CreateDirectory(imageDir);
            }
            cursorImage?.Save(Path.Combine(imageDir, $"cursor_{DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")}.png"), System.Drawing.Imaging.ImageFormat.Png);

        }



        private void OnClickSendText(object sender, EventArgs e)
        {
            var textProgramName = tbxReadAllBtn.Text;
            if (string.IsNullOrEmpty(textProgramName))
            {
                return;
            }
            if (string.IsNullOrEmpty(tbxTextToType.Text))
            {
                return;
            }

           




            // หา process ที่ชื่อหน้าต่างมีคำว่า "textProgramName"
            var abcWindows = Process.GetProcesses()
                .Where(p => !string.IsNullOrEmpty(p.MainWindowTitle) && p.MainWindowTitle.Contains(textProgramName))
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

                #region hardware hook
                using (KeyboardHook keyboardHook = new KeyboardHook(KeyboardCallback))
                {
                    keyboardHook.SimulateKeyDown(KeyCode.A);
                    keyboardHook.SimulateKeyDown(KeyCode.B);
                    keyboardHook.SimulateKeyDown(KeyCode.C);
                }
                #endregion


                ////Thread.Sleep(3000);


                //Thread.Sleep(3000);

                OskControl(targetProcess);


                //System.Windows.Forms.SendKeys.SendWait("H");

                // ส่งข้อความโดยใช้ InputSimulator
                //var sim = new InputSimulator();
                //sim.Keyboard.TextEntry("H");
            }
            else
            {
                MessageBox.Show("ไม่พบหน้าต่างชื่อ 'abc' ครบ 5 อัน", "แจ้งเตือน");
            }
        }

        static void TreeWalkX(AutomationElement root)
        {
            var walker = TreeWalker.ContentViewWalker;
            var child = walker.GetFirstChild(root);

            while (child != null)
            {
                Console.WriteLine($"Name: {child.Current.Name}, Type: {child.Current.ControlType.ProgrammaticName}");
                child = walker.GetNextSibling(child);
            }


        }


        static void ExploreAutomationTree(AutomationElement element, int depth)
        {
            if (element == null) return;

            string indent = new string(' ', depth * 2);
            string name = element.Current.Name;
            string controlType = element.Current.ControlType.ProgrammaticName;
            string automationId = element.Current.AutomationId;

            Console.WriteLine($"{indent}- Name: '{name}', Type: {controlType}, ID: '{automationId}'");


            System.Windows.Point clickablePoint = element.GetClickablePoint();

            // 2. กำหนดเงื่อนไขการค้นหา: หา elements ที่เป็นปุ่มและมีชื่อ "ตกลง"
            //Condition buttonCondition = new PropertyCondition(AutomationElement.ClickablePointProperty, ControlType.);
            //Condition nameCondition = new PropertyCondition(AutomationElement.NameProperty, "ตกลง");
            //Condition andCondition = new AndCondition(buttonCondition, nameCondition);

            // 3. เรียกใช้ FindAll(): ค้นหา descendants ทั้งหมดของหน้าต่าง
            //AutomationElementCollection okButtons = element.FindAll(TreeScope.Descendants, buttonCondition);



            TreeWalker walker = TreeWalker.ControlViewWalker;

            var child = walker.GetFirstChild(element);
            while (child != null)
            {
                ExploreAutomationTree(child, depth + 1);
                child = walker.GetNextSibling(child);
            }
        }


        void OskControl(Process processOnTop)
        {
            // หา process OSK
            var oskProc = Process.GetProcessesByName("osk").FirstOrDefault();
            if (oskProc == null)
            {
                //Process.Start("C:\\Windows\\System32\\osk.exe");
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.UseShellExecute = true;

                // process.StartInfo.RedirectStandardOutput = true;
                // process.StartInfo.RedirectStandardError = true;
                process.StartInfo.CreateNoWindow = true;
                //process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.FileName = "c:\\windows\\system32\\osk.exe";
                process.StartInfo.Arguments = "";
                process.StartInfo.WorkingDirectory = "c:\\";

                process.Start(); // **ERROR WAS HERE**

                Thread.Sleep(2000); // รอ OSK เปิดขึ้นมา
                oskProc = Process.GetProcessesByName("osk").FirstOrDefault();
            }

            if (oskProc == null)
            {
                Console.WriteLine("ไม่สามารถเปิด OSK ได้");
                return;
            }


            //ShowWindow(hWnd, SW_SHOW);
            if (IsIconic(processOnTop.MainWindowHandle))
            {
                ShowWindow(processOnTop.MainWindowHandle, SW_SHOW);
            }
            SetForegroundWindow(processOnTop.MainWindowHandle);
            Thread.Sleep(300);
            System.Windows.Forms.SendKeys.SendWait(tbxTextToType.Text);

            Thread.Sleep(300);





            //// ใช้ UIAutomation ค้นหาปุ่ม A
            //var root = AutomationElement.FromHandle(oskProc.MainWindowHandle);
            ////TreeWalkX(root);
            ////ExploreAutomationTree(root, 0);

            //var aButton = root.FindFirst(TreeScope.Descendants,
            //    new PropertyCondition(AutomationElement.NameProperty, "a"));

            //if (aButton != null)
            //{

            //    var invokePattern = aButton.GetCurrentPattern(InvokePattern.Pattern) as InvokePattern;
            //    Console.WriteLine(invokePattern?.ToString());


            //    //-
            //    IntPtr hWnd = processOnTop.MainWindowHandle;
            //    //ShowWindow(hWnd, SW_SHOW);
            //    if (IsIconic(hWnd))
            //    {
            //        ShowWindow(hWnd, SW_SHOW);
            //    }
            //    SetForegroundWindow(hWnd);
            //    Thread.Sleep(1000);
            //    //-

            //    ShowWindow(oskProc.MainWindowHandle, SW_HIDE);
            //    invokePattern?.Invoke();
            //    //ShowWindow(oskProc.MainWindowHandle, SW_SHOW);
            //    Console.WriteLine("กดปุ่ม 'A' ผ่าน OSK แล้ว");
            //}
            //else
            //{
            //    Console.WriteLine("ไม่พบปุ่ม A บน OSK");
            //}
        }

        private void btnClick_Click(object sender, EventArgs e)
        {
            MouseClick((int)(numX.Value), (int)(numX.Value));

        }

        private void btnType_Click(object sender, EventArgs e)
        {

            var textProgramName = tbxReadAllBtn.Text;
            if (string.IsNullOrEmpty(textProgramName))
            {
                return;
            }
            var ragWindows = Process.GetProcesses()
             .Where(p => !string.IsNullOrEmpty(p.MainWindowTitle) && p.MainWindowTitle.Contains(textProgramName))
             .ToList();

            var ragProc = ragWindows[0];
            var root = AutomationElement.FromHandle(ragProc.MainWindowHandle);


            OnClickSendText(sender, e);


        }



        private void btnClickBtnName_Click(object sender, EventArgs e)
        {
            var textProgramName = tbxReadAllBtn.Text;
            if (string.IsNullOrEmpty(textProgramName))
            {
                return;
            }

            var textBtnNameClick = tbxBtnNameClick.Text;
            if (string.IsNullOrEmpty(textBtnNameClick))
            {
                return;
            }

            var ragWindows = Process.GetProcesses()
              .Where(p => !string.IsNullOrEmpty(p.MainWindowTitle) && p.MainWindowTitle.Contains(textProgramName))
              .ToList();

            var ragProc = ragWindows[0];


            // ใช้ UIAutomation ค้นหาปุ่ม A
            var root = AutomationElement.FromHandle(ragProc.MainWindowHandle);
            //TreeWalkX(root);
            //ExploreAutomationTree(root, 0);

            var aButton = root.FindFirst(TreeScope.Descendants,
                new PropertyCondition(AutomationElement.NameProperty, textBtnNameClick));

            if (aButton != null)
            {

                var invokePattern = aButton.GetCurrentPattern(InvokePattern.Pattern) as InvokePattern;

                invokePattern?.Invoke();
                //ShowWindow(oskProc.MainWindowHandle, SW_SHOW);
                Console.WriteLine($"กดปุ่ม '{textBtnNameClick}' ผ่าน {textProgramName} แล้ว");
            }
            else
            {
                Console.WriteLine($"ไม่พบปุ่ม '{textBtnNameClick}' บน {textProgramName}");
            }
        }

        private void btnReadAllBtn_Click(object sender, EventArgs e)
        {
            var textProgramName = tbxReadAllBtn.Text;
            if (string.IsNullOrEmpty(textProgramName))
            {
                return;
            }

            var ragWindows = Process.GetProcesses()
               .Where(p => !string.IsNullOrEmpty(p.MainWindowTitle) && p.MainWindowTitle.Contains(textProgramName))
               .ToList();

            var ragProc = ragWindows[0];

            // ใช้ UIAutomation ค้นหาปุ่ม A
            var root = AutomationElement.FromHandle(ragProc.MainWindowHandle);

            ExploreAutomationTree(root, 0);

        }
    }
}
