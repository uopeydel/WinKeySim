using System.Diagnostics;

namespace WKSConsole
{
    internal class Program
    {
        static void Main()
        {
            // เปิด osk ถ้ายังไม่เปิด
            var osk = Process.GetProcessesByName("osk").FirstOrDefault();
            if (osk == null)
            {
                Process.Start("osk.exe");
                Thread.Sleep(2000);
                osk = Process.GetProcessesByName("osk").FirstOrDefault();
            }

            if (osk == null)
            {
                Console.WriteLine("ไม่พบ osk.exe");
                return;
            }

            // เริ่มจาก root element ของ osk
            var root = AutomationElement.FromHandle(osk.MainWindowHandle);
            Console.WriteLine("=== เริ่มสำรวจ Tree ของ OSK ===");
            ExploreAutomationTree(root, 0);
        }

        static void ExploreAutomationTree(AutomationElement element, int depth)
        {
            if (element == null) return;

            string indent = new string(' ', depth * 2);
            string name = element.Current.Name;
            string controlType = element.Current.ControlType.ProgrammaticName;
            string automationId = element.Current.AutomationId;

            Console.WriteLine($"{indent}- Name: '{name}', Type: {controlType}, ID: '{automationId}'");

            TreeWalker walker = TreeWalker.ControlViewWalker;
            var child = walker.GetFirstChild(element);
            while (child != null)
            {
                ExploreAutomationTree(child, depth + 1);
                child = walker.GetNextSibling(child);
            }
        }

    }
}