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

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for ucMaterial.xaml
    /// </summary>
    public partial class ucMaterial : UserControl
    {

        //bool web_check_on = false;
        public const string strMainImagePath = @"Image/UIImage/Material/";
        private string strMaterialPath = @"D:\SWMaterial\";
        public const string GOOGLE_STREETVIEW = @"https://www.google.com/maps/views/view/116301864423028290717/gphoto/5976405169727045986?gl=kr&hl=ko&heading=30&pitch=99&fovy=75";
        public const string NAVER_STREETVIEW = @"https://www.google.com/maps/views/view/116301864423028290717/gphoto/5976405169727045986?gl=kr&hl=ko&heading=30&pitch=99&fovy=75";
        public const string DAUM_ROADMAP  = @"http://map.daum.net/?panoid=1020650859&pan=196.2&tilt=-15.3&zoom=0&map_type=TYPE_MAP&map_attribute=ROADVIEW&urlX=443096&urlY=1157693&urlLevel=3";
        private string contentCurSelImage = string.Empty;
        private string alignmentCurSelImage = string.Empty;
        
        List<string> liWebLinkes =  null;
        private string strWebContent = string.Empty;
        private int iContentAlignment = 0;
        private int iMaterialMode = 0;

        public event EventHandler DisplayInfo;

        public ucMaterial()
        {         
            InitializeComponent();
            liWebLinkes = new List<string>();
            AddWebLinksToTheList();
            //rdbAvatarMode.IsChecked = true;
        }

        private void AddWebLinksToTheList()
        {
            liWebLinkes.Add(GOOGLE_STREETVIEW);
            liWebLinkes.Add(NAVER_STREETVIEW);
            liWebLinkes.Add(DAUM_ROADMAP);

        }

        void settingWindow_UpdateMaterialInfo(object sender, EventArgs e)
        {

            //저작 모드 셋팅
            //UserInfo._AuthoringMode = rdbRealMode.IsChecked == true? 1 : 0; // 0: AvatarMode 1:RealMode

            //Material 셋팅
            //0: PPT 1:VIDEO 2:WEB
            //if (Check.IsChecked == false && !string.IsNullOrEmpty(txt_path.Text))
            //    UserInfo._MaterialMode = DetermineTheMaterialFormat(txt_path.Text) == MaterialMode.PPT ? (int) MaterialMode.PPT : (int) MaterialMode.VIDEO;
            //else if (Check.IsChecked == true)
            //    UserInfo._MaterialMode = (int) MaterialMode.WEB;
             

            //if (UserInfo._MaterialMode == (int)MaterialMode.WEB)
            //{
            //    switch (webviewList.SelectedIndex)
            //    {
            //        //Google Street View
            //        case 0: UserInfo._MaterialFileName = "https://www.google.com/maps/views/view/116301864423028290717/gphoto/5976405169727045986?gl=kr&hl=ko&heading=30&pitch=99&fovy=75"; break;
            //        //Naver Street View
            //        case 1: UserInfo._MaterialFileName = "http://map.naver.com/?menu=location&mapMode=0&lat=37.5726755&lng=126.9770196&dlevel=12&searchCoord=126.9772544%3B37.5706746&query=7IS47KKF64yA7JmV64%2BZ7IOB&mpx=09110615%3A37.5706746%2C126.9772544%3AZ13%3A0.0013298%2C0.0007017&tab=1&vrpanotype=3&vrpanoid=3kwQCmzE7k1Xp0d7GPLjYg%3D%3D&vrpanopan=-3.7&vrpanotilt=4.18&vrpanofov=120&vrpanolat=37.5726756&vrpanolng=126.9770201&street=on&vrpanosky=off&vrpanopoi=off&enc=b64"; break;
            //        //Naver Road View
            //        case 2: UserInfo._MaterialFileName = "http://map.daum.net/?panoid=1020650859&pan=196.2&tilt=-15.3&zoom=0&map_type=TYPE_MAP&map_attribute=ROADVIEW&urlX=443096&urlY=1157693&urlLevel=3"; break;
            //    }
            //}
            //else
            //    UserInfo._MaterialFileName = txt_path.Text;

            ////Content Alignment Setting
            //if (lecture_mode1.IsChecked == true)
            //    UserInfo._ContentAlignmentMode = (int)ContentAlignment.LEFT;
            //else if (lecture_mode2.IsChecked == true)
            //    UserInfo._ContentAlignmentMode = (int)ContentAlignment.CENTRE;
            //else
            //    UserInfo._ContentAlignmentMode = (int)ContentAlignment.RIGHT;

            ////Player Mode setting
            //if (rdb1Player.IsChecked == true)
            //    UserInfo._AvatarMode = (int)PlayerMode.ONE;
            //else if (rdb2Players.IsChecked == true)
            //    UserInfo._AvatarMode = (int)PlayerMode.TWO;

        }

        private WpfApplication1.EnumCollection.MaterialMode DetermineTheMaterialFormat(string strPath)
        {
            string strFormat = strPath.Substring(strPath.IndexOf('.') + 1);

            if (strFormat == "pptx")
                return WpfApplication1.EnumCollection.MaterialMode.PPT;
            else
                return WpfApplication1.EnumCollection.MaterialMode.VIDEO;
        }


        private void img_MouseEnter(object sender, MouseEventArgs e)
        {
            Image tempImg = (Image)sender;
            string strFileName = string.Empty;

            switch (tempImg.Name)
            {
                case "img_contentType":
                    strFileName = "materialname2.png";
                    break;
                case "img_googleMap":
                    strFileName = "googlename2.png";
                    break;
                case "img_naverStView":
                    strFileName = "navername2.png";
                    break;
                case "img_DaumRdView":
                    strFileName = "daumname2.png";
                    break;
                case "img_contLeft":
                    strFileName = "leftname2.png"; 
                    break;
                case "img_contCentre":
                    strFileName = "centername2.png";
                    break;
                case "img_contRight":
                    strFileName = "rightname2.png";
                    break;
            }

            ((Image)sender).Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(strMainImagePath + strFileName, UriKind.RelativeOrAbsolute));
        }

        private void img_MouseLeave(object sender, MouseEventArgs e)
        {
            Image tempImg = (Image)sender;
            string strFileName = string.Empty;

            switch (tempImg.Name)
            {
                case "img_contentType":
                    strFileName = "materialname.png";
                    break;
                case "img_googleMap":
                    strFileName = "googlename.png";
                    break;
                case "img_naverStView":
                    strFileName = "navername.png";
                    break;
                case "img_DaumRdView":
                    strFileName = "daumname.png";
                    break;
                case "img_contLeft":
                    strFileName = "leftname.png";
                    break;
                case "img_contCentre":
                    strFileName = "centername.png";
                    break;
                case "img_contRight":
                    strFileName = "rightname.png";
                    break;
            }

            ((Image)sender).Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(strMainImagePath + strFileName, UriKind.RelativeOrAbsolute));
        }
        
        /// <summary>
        /// showing the dialog box to choose a file either ppt or video, and then set the material mode base on the selected file format
        /// </summary>
        private void SelectLectureMaterial()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".pptx";
            //            dlg.Filter = "PowerPoint 프레젠테이션 (*.pptx)|*.pptx|*.pptm|*.ppsx|*.pps|*.ppsm|*.potx|*.pot|*.potm|*.odp";
            dlg.Filter = "PowerPoint 프레젠테이션 (*.pptx)| *.pptx| 동영상 파일 (*.wmv,*.avi,*.mp4)| *.wmv;*.avi;*.mp4";

            dlg.InitialDirectory = strMaterialPath;
            Nullable<bool> result = dlg.ShowDialog();
            
            SetPathText(dlg.FileName);

            iMaterialMode = DetermineTheMaterialFormat(txt_path.Text) == WpfApplication1.EnumCollection.MaterialMode.PPT ?
                (int)WpfApplication1.EnumCollection.MaterialMode.PPT : (int)WpfApplication1.EnumCollection.MaterialMode.VIDEO;
        }


        private void SetPathText(string str)
        {
            txt_path.Text = str;
        }

        private void SetNeonImage(object sender, Image imgNeon, string strFileName, Image imgNormal, string strNormalNameImage)
        {
            if (imgNormal != null)
                imgNormal.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(strMainImagePath + strNormalNameImage, UriKind.RelativeOrAbsolute));//set to normal name image
            imgNeon.Margin = ((Image)sender).Margin;
            imgNeon.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(strMainImagePath + strFileName, UriKind.RelativeOrAbsolute)); //set the image to neon image
        }

        private void img_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Image tempImg = null;

            string strFileName = string.Empty;
            string strNormalNameImage = string.Empty;

            if (alignmentCurSelImage == string.Empty)
                alignmentCurSelImage = ((Image)sender).Name;

            switch (contentCurSelImage)
            {
                case "img_contLeft":
                    strNormalNameImage = "leftname.png";
                    tempImg = img_contLeft; break;
                case "img_contCentre":
                    strNormalNameImage = "centername.png";
                    tempImg = img_contCentre; break;
                case "img_contRight":
                    strNormalNameImage = "rightname.png";
                    tempImg = img_contRight; break;
            }

            strFileName = "lectureglow10.png";

            switch (((Image)sender).Name)
            {
                case "img_contLeft":
                    iContentAlignment = (int)EnumCollection.ContentAlignment.LEFT;break;
                case "img_contCentre":
                    iContentAlignment = (int)EnumCollection.ContentAlignment.CENTRE;break;
                case "img_contRight":
                   iContentAlignment = (int) EnumCollection.ContentAlignment.RIGHT;break;
                    
            }

            SetNeonImage(sender, img_BigNeon, strFileName, tempImg, strNormalNameImage);
            
        }

        

        private void ContentsImg_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Image tempImg = null;

            string strFileName = string.Empty;
            string strNormalNameImage = string.Empty;

            if (contentCurSelImage == string.Empty)
                contentCurSelImage = ((Image)sender).Name;

            switch (contentCurSelImage)
            {
                case "img_contentType":
                    strNormalNameImage = "materialname.png";
                    tempImg = img_contentType; break;
                case "img_googleMap":
                    strNormalNameImage = "googlename.png";
                    tempImg = img_googleMap; break;
                case "img_naverStView":
                    strNormalNameImage = "navername.png";
                    tempImg = img_naverStView; break;
                case "img_DaumRdView":
                    strNormalNameImage = "daumname.png";
                    tempImg = img_naverStView; break;
            }

            strFileName = "smallglow10.png";

            iMaterialMode = (int)WpfApplication1.EnumCollection.MaterialMode.WEB;
            SetNeonImage(sender, img_SmallNeon, strFileName, tempImg, strNormalNameImage);

            switch (((Image)sender).Name)
            {
                case "img_contentType":
                    SelectLectureMaterial();
                    break;
                case "img_googleMap":
                    SetPathText("Google StreetView");
                    strWebContent = liWebLinkes[(int)EnumCollection.WebContents.GOOGLE];
                    break;
                case "img_naverStView":
                    SetPathText("Naver StreetView");
                    strWebContent = liWebLinkes[(int)EnumCollection.WebContents.NAVER]; 
                    break;
                case "img_DaumRdView":
                    SetPathText("Daum RoadView");
                    strWebContent = liWebLinkes[(int)EnumCollection.WebContents.DAUM]; 
                    break;
            }

           

        }

        private void img_ok_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //settingWindow.grid_class.Children.Remove(this);
            UserInfo._ContentAlignmentMode = iContentAlignment;
            if (iMaterialMode == (int)EnumCollection.MaterialMode.WEB)
            {
                UserInfo._MaterialMode = (int)EnumCollection.MaterialMode.WEB;
                UserInfo._MaterialFileName = strWebContent;
            }
            else
            {
                UserInfo._MaterialMode = iMaterialMode;
                UserInfo._MaterialFileName = txt_path.Text;
            }

            UserInfo._ContentAlignmentMode = iContentAlignment;

            if (DisplayInfo != null)
                DisplayInfo(this, null);
            this.Visibility = Visibility.Collapsed;
        }

    }
}
