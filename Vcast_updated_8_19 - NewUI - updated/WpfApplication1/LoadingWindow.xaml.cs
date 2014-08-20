using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// LoadingWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LoadingWindow : Window
    {
        public event EventHandler LoadPPTAndAvatar;
        LectureRecordWindow mw;
        RecordWindow rw;
        bool blGamePPTLoaded = false;


        public LoadingWindow(bool doNothing)
        {
            InitializeComponent();
        }

        public LoadingWindow()
        {
            InitializeComponent();
            mw = CloseAll.caMW;
            rw = CloseAll.caRW;

        }

        //public void LoadForms()
        //{
        //    if (LoadPPTAndAvatar != null && !blGamePPTLoaded)
        //    {
                
        //        this.Topmost = true;
        //        LoadPPTAndAvatar(this, null);
        //    }
        //}


        #region 최동훈 추가 및 수정

        public void LoadForms(LectureRecordWindow wm, RealModeWindow rm)
        {
            if (LoadPPTAndAvatar != null && !blGamePPTLoaded)
            {

                this.Topmost = true;
                LoadPPTAndAvatar(this, null);
            }

            if (wm != null) wm.Focus();
            else rm.Focus();
        }

        #endregion
    }
}
