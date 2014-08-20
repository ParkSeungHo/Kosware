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
    /// Interaction logic for ChoiceWindow.xaml
    /// </summary>
    public partial class ChoiceWindow : Window
    {
        private bool isFirstTime = true;
        //LectureSettingWindow mw1;
        //LectureEditingWindow mw2;

        #region 최동훈

        //UserInfo _Info = null;

        //public ChoiceWindow(UserInfo Info)
        //{            
        //    InitializeComponent();

        //    this._Info = Info;
        //    CloseAll.caCW = this;
        //}

        public ChoiceWindow()
        {
            InitializeComponent();
            CloseAll.caCW = this;
        }

        #endregion


        private void image_record_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            if (CloseAll.caMW1 == null)
            {
                //mw1 = new LectureSettingWindow();
                //LectureSettingWindow mw1 = new LectureSettingWindow(_Info);
                LectureSettingWindow mw1 = new LectureSettingWindow();
                mw1.Show();
            }
            else
                CloseAll.caMW1.Visibility = Visibility.Visible;
            
        }

        private void image_edit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            if (CloseAll.caMW2 == null)
            {
                //mw2 = new LectureEditingWindow();
                //LectureEditingWindow mw2 = new LectureEditingWindow(_Info);
                LectureEditingWindow mw2 = new LectureEditingWindow();
                mw2.Show();
            }
            else
                CloseAll.caMW2.Visibility = Visibility.Visible;

            
        }
    }
}
