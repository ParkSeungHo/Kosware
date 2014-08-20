using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Office.Core;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Matrix = System.Drawing.Drawing2D.Matrix;

namespace WpfApplication1
{
    public partial class ucPPTWinForm : UserControl
    {
        

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


        //public String pptAction = string.Empty;

        PowerPoint.Application objApp;
        PowerPoint.Presentations objPresSet;
        PowerPoint._Presentation objPres;
        PowerPoint.Slides objSlides;
        PowerPoint.SlideShowSettings objSSS;
        PowerPoint.SlideShowWindow objSSW;
        PowerPoint.SlideShowView objSSV;

        bool blQuit = false;

        public ucPPTWinForm()
        {
            InitializeComponent();
        }

        public void open()
        {
            try
            {
                panel1.Visible = false;
                objApp = new PowerPoint.ApplicationClass();
                objPresSet = objApp.Presentations;
                objPres = objPresSet.Open(UserInfo._MaterialFileName, MsoTriState.msoTrue, MsoTriState.msoTrue, MsoTriState.msoFalse);
                //objPres.PageSetup.SlideWidth = panel1.Width;
                //objPres.PageSetup.SlideHeight = panel1.Height;

                objPres.PageSetup.SlideSize = PowerPoint.PpSlideSizeType.ppSlideSizeOnScreen;

                panel1.Controls.Add(objApp as Control);

                objSSS = objPres.SlideShowSettings;

                objSSS.LoopUntilStopped = MsoTriState.msoTrue;
                
                objSlides = objPres.Slides;

                objSSS.StartingSlide = 1;
                objSSS.EndingSlide = objSlides.Count - 1;

                objSSS.ShowType = PowerPoint.PpSlideShowType.ppShowTypeSpeaker;

                objSSW = objSSS.Run();
         
                objSSV = objPres.SlideShowWindow.View;
//                objPres.SlideShowWindow.Height = panel1.Height-110;
//                objPres.SlideShowWindow.Width = panel1.Width-180;

//                objPres.SlideShowWindow.Top     = 1;
//                objPres.SlideShowWindow.Left    = 3;
                SetParent((IntPtr)objSSW.HWND, panel1.Handle);

            int style = GetWindowLong((IntPtr)objSSW.HWND, GWL_STYLE);
            style = style & ~WS_CAPTION & ~WS_THICKFRAME;
            SetWindowLong((IntPtr)objSSW.HWND, GWL_STYLE, style);
            ResizeEmbeddedApp();

                
                panel1.Visible = true;
            }
            catch (Exception)
            {
                throw;
            }

        }

        private void ResizeEmbeddedApp()
        {
            if (objSSW == null)
                return;

            SetWindowPos((IntPtr)objSSW.HWND, IntPtr.Zero, 0, -90, (int)this.panel1.ClientSize.Width , (int)this.panel1.ClientSize.Height , SWP_NOZORDER | SWP_NOACTIVATE);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            open();
        }

        public void Reset()
        {
            if (objApp != null && blQuit == false)
            {
                objApp.Quit();
                blQuit = true;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //objPres.Close();
            //objApp.Quit();
            //Reset();
        }

        private void panel1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Right:
                    objSSV.Next();
                    break;
                case Keys.Left:
                    objSSV.Previous();
                    break;

            }

        }

    }
}
