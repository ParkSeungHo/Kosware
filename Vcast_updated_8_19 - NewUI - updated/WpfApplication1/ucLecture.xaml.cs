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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApplication1.kr.co.vcast.www;


namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for ucLecture.xaml
    /// </summary>
    public partial class ucLecture : UserControl
    {
        public event EventHandler DisplayInfo;

        private string strCurSelectedMode = string.Empty;
        //private bool isRealMode = false;
        public const string strMainImagePath = @"Image/UIImage/Mode/";
        private int iPlayerMode = 0;

        public ucLecture()
        {
            InitializeComponent();
        }

        void settingWindow_UpdateSubjectInfo(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(cbLectureList.Text))
            //    UserInfo._LectureTitle = cbLectureList.Text;
            //if (!string.IsNullOrEmpty(txt_LectureModuleTitle.Text))
            //    UserInfo._LectureModuleTitle = txt_LectureModuleTitle.Text;
            //if (!string.IsNullOrEmpty(txt_MemberName.Text))
            //    UserInfo._MemberName = txt_MemberName.Text;
        }

        /// <summary>
        /// 강의목록 불러오기
        /// </summary>
        private void GetLectureList()
        {
            //if (_Info != null)
            //{
            //cbLectureList.Items.Clear();

            //using (VCastService VCS = new VCastService())
            //{
            //    int CompanySeq = int.Parse(UserInfo._CompanySeq);
            //    int MemberSeq = int.Parse(UserInfo._MemberSeq);

            //    string[] arrLectureList = VCS.LectureList(CompanySeq, MemberSeq).Split(',');
            //    string[] arrItemList = null;
            //    ComboBoxItem cbi;

            //    for (int i = 0; i < arrLectureList.Length; i++)
            //    {
            //        cbi = new ComboBoxItem();
            //        arrItemList = arrLectureList[i].Split('|');
            //        if (arrItemList.Length > 1)
            //        {
            //            cbi.Content = arrItemList[1];
            //            cbi.Tag = arrItemList[0];

            //            cbLectureList.Items.Add(cbi);
            //        }
            //    }

            //    if (cbLectureList.Items.Count > 0)
            //    {
            //        cbLectureList.SelectedIndex = 0;
            //        UserInfo._LectureTitle = cbLectureList.Text;
            //    }

            //    txt_MemberName.Text = UserInfo._MemberName;
            //}
            //}
        }

        private void img_MouseEnter(object sender, MouseEventArgs e)
        {
            Image tempImg = (Image)sender;
            string strFileName = string.Empty;

            switch (tempImg.Name)
            {
                case "img_realMode":
                    strFileName = "realmodename2.png";
                    break;
                case "img_oneAvatar":
                    strFileName = "oneavatarname2.png";
                    break;
                case "img_twoAvatars":
                    strFileName = "twoavatarname2.png";
                    break;
            }

            ((Image)sender).Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(strMainImagePath + strFileName, UriKind.RelativeOrAbsolute));
        }

        private void ChangeToNeonImage()
        {
            
        }

        private void img_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Image tempImg = null;

            string strFileName = string.Empty;
            string strNormalNameImage = string.Empty;

            if (strCurSelectedMode == string.Empty)
                strCurSelectedMode = ((Image)sender).Name;
        
            strFileName = "modeglow10.png";


            switch (strCurSelectedMode)
            {
                case "img_realMode":
                    strNormalNameImage = "realmodename.png";
                    iPlayerMode = (int)EnumCollection.PlayerMode.REAL_MODE;
                    tempImg = img_realMode;break;
                case "img_oneAvatar":
                    strNormalNameImage = "oneavatarname.png";
                    iPlayerMode = (int)EnumCollection.PlayerMode.ONE_PLAYER;
                    tempImg = img_oneAvatar; break;
                case "img_twoAvatars":
                    strNormalNameImage = "twoavatarname.png";
                    iPlayerMode = (int)EnumCollection.PlayerMode.TWO_PLAYERS;
                    tempImg = img_twoAvatars; break;

            }

            tempImg.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(strMainImagePath + strNormalNameImage, UriKind.RelativeOrAbsolute));
            img_neon.Margin = ((Image)sender).Margin;
            img_neon.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(strMainImagePath + strFileName, UriKind.RelativeOrAbsolute));
        }


        private void img_MouseLeave(object sender, MouseEventArgs e)
        {
            Image tempImg = (Image)sender;
            string strFileName = string.Empty;

            switch (tempImg.Name)
            {
                case "img_realMode":
                    strFileName = "realmodename.png";
                    break;
                case "img_oneAvatar":
                    strFileName = "oneavatarname.png";
                    break;
                case "img_twoAvatars":
                    strFileName = "twoavatarname.png";
                    break;
            }

            ((Image)sender).Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(strMainImagePath + strFileName, UriKind.RelativeOrAbsolute));
        }


        private void img_ok_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //settingWindow.grid_class.Children.Remove(this);
            UserInfo._AvatarMode = iPlayerMode;
            if (DisplayInfo != null)
                DisplayInfo(this, null);
            this.Visibility = Visibility.Collapsed;
        }
    }
}
