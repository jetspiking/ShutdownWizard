using System.Runtime.InteropServices;
using System.Windows;

namespace ShutdownWizard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

        private const byte VK_MENU = 0x12;  // Alt key
        private const byte VK_F4 = 0x73;
        private const int KEYEVENTF_KEYUP = 0x02;

        public MainWindow()
        {
            InitializeComponent();
            this.Hide();
            CallClassicDialog();
            System.Threading.Thread.Sleep(10000);
            System.Windows.Application.Current.Shutdown();
        }

        private const byte VK_LWIN = 0x5B; // Left Windows key
        private const byte VK_D = 0x44; // D key

        private void CallClassicDialog()
        {
            keybd_event(VK_LWIN, 0, 0, 0);                  // Press Left Windows key
            keybd_event(VK_D, 0, 0, 0);                     // Press D key
            keybd_event(VK_D, 0, KEYEVENTF_KEYUP, 0);       // Release D key
            keybd_event(VK_LWIN, 0, KEYEVENTF_KEYUP, 0);    // Release Left Windows key

            System.Threading.Thread.Sleep(100);

            keybd_event(VK_MENU, 0, 0, 0);                  // Press Alt
            keybd_event(VK_F4, 0, 0, 0);                    // Press F4
            keybd_event(VK_F4, 0, KEYEVENTF_KEYUP, 0);      // Release F4
            keybd_event(VK_MENU, 0, KEYEVENTF_KEYUP, 0);    // Release Alt
        }
    }
}
