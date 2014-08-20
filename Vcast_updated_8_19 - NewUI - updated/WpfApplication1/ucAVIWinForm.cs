using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WpfApplication1
{
    public partial class ucAVIWinForm : UserControl
    {
        private string aviUrl = "";

        public ucAVIWinForm()
        {
            InitializeComponent();
        }


//        public void PlayFile(string filePath)
//        {
            
////            this.axWindowsMediaPlayer1.Ctlcontrols.play();
//        }

        public void PauseVideo()
        {
            this.axWindowsMediaPlayer1.Ctlcontrols.pause();
            
        }

        public void CloseVideo()
        {
            this.axWindowsMediaPlayer1.close();
        }
        private void ucAVIWinForm_Load(object sender, EventArgs e)
        {
            if (UserInfo._MaterialFileName != string.Empty)
            {
                this.axWindowsMediaPlayer1.URL = UserInfo._MaterialFileName;
                this.axWindowsMediaPlayer1.Ctlcontrols.stop();
            }
        }
    }
}
