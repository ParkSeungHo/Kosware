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
    public partial class ucWebForm : UserControl
    {
        public ucWebForm()
        {
            InitializeComponent();
        }

        private void ucWebForm_Load(object sender, EventArgs e)
        {
            string url = UserInfo._MaterialFileName;
           // string url2 = "http://map.naver.com/";
            this.webBrowser1.Navigate(url);
        }

        public void Web_Close()
        {
        }

        public void Web_Url(string url)
        {
            this.webBrowser1.Navigate(url);
        }
    }
}
