using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Diagnostics;
using System.Runtime.InteropServices;

namespace WpfApplication1
{
    public partial class UnityControl2 : UserControl
    {
        ProcessStartInfo psi;
        Process PR;
        //EmailUC ucEmail;

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hwnd, int nindex, int dwNewLong);

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hwnd, int nindex);

        [DllImport("user32")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern long SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        private const int SWP_NOZORDER = 0x0004;
        private const int SWP_NOACTIVATE = 0x0010;
        private const int GWL_STYLE = -16;
        private const int WS_CAPTION = 0x00C00000;
        private const int WS_THICKFRAME = 0x00040000;
        private const string UnityPach = @"\twoPlayers.exe";

        public UnityControl2()
        {
            InitializeComponent();
        }

        private void load(object sender, EventArgs e)
        {
            Unity_open();
        }

        private void Unity_open()
        {
            psi = new ProcessStartInfo(AppDomain.CurrentDomain.BaseDirectory + UnityPach);
            //psi = new ProcessStartInfo(Directory.GetCurrentDirectory() + @"\IronManKinect.exe");
            PR = new Process();
            //PR.StartInfo.RedirectStandardOutput = true;
            //PR.StartInfo.RedirectStandardInput = true;
            //PR.StartInfo.UseShellExecute = false;

            PR = Process.Start(psi);


            PR.WaitForInputIdle();
            System.Threading.Thread.Sleep(7000);

            SetParent(PR.MainWindowHandle, this.panel1.Handle);

            ResizeEmbeddedApp();

            int style = GetWindowLong(PR.MainWindowHandle, GWL_STYLE);
            style = style & ~WS_CAPTION & ~WS_THICKFRAME;
            SetWindowLong(PR.MainWindowHandle, GWL_STYLE, style);
            

            //int newStyle = GetWindowLong(PR.MainWindowHandle, GWL_STYLE);
            //newStyle = newStyle & ~WS_CAPTION & ~WS_THICKFRAME;

            //if (newStyle != style)
            //{
            //  // 적용 안된것! 다시 한번 돌림
            //    System.Threading.Thread.Sleep(1000);
            //    style = GetWindowLong(PR.MainWindowHandle, GWL_STYLE);
            //    style = style & ~WS_CAPTION & ~WS_THICKFRAME;
            //    SetWindowLong(PR.MainWindowHandle, GWL_STYLE, style);

            //    ResizeEmbeddedApp();
            //}
        }

        private void ResizeEmbeddedApp()
        {
            if (PR == null)
                return;

//            SetWindowPos(PR.MainWindowHandle, IntPtr.Zero, -8, -40, (int)this.panel1.ClientSize.Width+16, (int)this.panel1.ClientSize.Height-155, SWP_NOZORDER | SWP_NOACTIVATE);
            while (!SetWindowPos(PR.MainWindowHandle, IntPtr.Zero, 0, 0, (int)this.panel1.ClientSize.Width, (int)this.panel1.ClientSize.Height - 250, SWP_NOZORDER | SWP_NOACTIVATE))
            {
            }
        }

        public void Unity_Close()
        {
            PR.CloseMainWindow();
            //            PR.Close();
            //            PR.Kill();
        }
    }
}
