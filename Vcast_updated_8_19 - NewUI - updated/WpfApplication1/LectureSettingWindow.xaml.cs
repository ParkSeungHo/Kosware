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

#region 최동훈 추가

using System.Data;
using System.IO;

using WpfApplication1.kr.co.vcast.www;

#endregion

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for LectureSettingWindow.xaml
    /// </summary>
    /// 

    #region 최동훈 추가

    
    public enum EncodingResolution { R1920x1080, R1024x768, R800x600, R640x480 };

    #endregion

    public partial class LectureSettingWindow : Window
    {
        enum MenuIndex
        {
            CLASS,      // = 0
            FILE,       // = 1
            BACKGROUND, // = 2
            AVATAR,     // = 3
            NONE,      // = 4
        }

        LectureRecordWindow mw = null;
        RealModeWindow rm = null;
        ucLecture ucLect = null;
        ucMaterial ucMat = null;
        ucAvatar ucAvat = null;
        ucBackground ucBg = null;


        private int avata_Count = 1;
        string NowLectureSeq = string.Empty;
        string NowLectureModuleTitle = string.Empty;

        public bool isFirstTime { get; set; }
        private int curSelectedMenuIndex = 0;
        public const string strMainImagePath = @"Image/UIImage/Main/";
        
        #region 최동훈 추가 사항

        //UserInfo _Info = null;

        List<ComboBoxItem> liCbItem { get; set; }
        DataSet dsLectureInfo = null;

        /// <summary>
        ///  생성자 오버로드
        /// </summary>
        /// <param name="Info">로그인 정보가 저장되는 UserInfo Type의 변수</param>
        public LectureSettingWindow()
        {
            InitializeComponent();

            LoadAllMenuRelatedUcControls();
            curSelectedMenuIndex = (int) MenuIndex.CLASS;
            GetLectureList();
            CloseAll.caMW1 = this;
            isFirstTime = false;
            //strBg = @"Image\scBg.png";
            //strAvatarBg = "avatarBg";
            dsLectureInfo = new DataSet();
            dsLectureInfo.ReadXml("../../LectureInfo.xml");
        }

        private void GetLectureList()
        {
     
            cbLectureList.Items.Clear();

            using (VCastService VCS = new VCastService())
            {
                int CompanySeq = int.Parse(UserInfo._CompanySeq);
                int MemberSeq = int.Parse(UserInfo._MemberSeq);

                string[] arrLectureList = VCS.LectureList(CompanySeq, MemberSeq).Split(',');
                string[] arrItemList = null;
                ComboBoxItem cbi;

                for (int i = 0; i < arrLectureList.Length; i++)
                {
                    cbi = new ComboBoxItem();
                    arrItemList = arrLectureList[i].Split('|');
                    if (arrItemList.Length > 1)
                    {
                        cbi.Content = arrItemList[1];
                        cbi.Tag = arrItemList[0];

                        cbLectureList.Items.Add(cbi);
                    }
                }

                if (cbLectureList.Items.Count > 0)
                {
                    cbLectureList.SelectedIndex = 0;
                    UserInfo._LectureTitle = cbLectureList.Text;
                }

                txt_MemberName.Text = UserInfo._MemberName;
            }
        }

        private void LoadAllMenuRelatedUcControls()
        {
            ucLect = new ucLecture();
            ucLect.DisplayInfo += DisplayInfo;
            grid_class.Children.Add(ucLect);
            ucLect.Visibility = Visibility.Collapsed;

            ucMat = new ucMaterial();
            ucMat.DisplayInfo += DisplayInfo;
            grid_class.Children.Add(ucMat);
            ucMat.Visibility = Visibility.Collapsed;

            ucAvat = new ucAvatar();
            ucAvat.DisplayInfo += DisplayInfo;
            grid_class.Children.Add(ucAvat);
            ucAvat.Visibility = Visibility.Collapsed;

            ucBg = new ucBackground();
            ucBg.DisplayInfo += DisplayInfo;
            grid_class.Children.Add(ucBg);
            ucBg.Visibility = Visibility.Collapsed;
        }

        void DisplayInfo(object sender, EventArgs e)
        {
            if (sender.Equals((object)ucLect))
                lblMode.Content = (EnumCollection.PlayerMode)UserInfo._AvatarMode;
            else if (sender.Equals((object)ucMat))
            {
                lblMaterial.Content = (EnumCollection.MaterialMode)UserInfo._MaterialMode + "\n" + (EnumCollection.ContentAlignment)UserInfo._ContentAlignmentMode;
            }

            else if (sender.Equals((object)ucAvat))
            {
                if (UserInfo._AvatarMode != (int)EnumCollection.PlayerMode.ONE_PLAYER)
                    lblAvatar.Content = "1P: " + UserInfo._FirstAvatarName.ToUpper() + "\n2P: " + UserInfo._SecondAvatarName.ToUpper();
                else
                    lblAvatar.Content = "1P: " + UserInfo._FirstAvatarName.ToUpper();
            }
            else
                lblBackground.Content = UserInfo._BackgroundName.ToUpper();

                
        }

        //private void 




        #endregion

        /*public LectureSettingWindow()
        {
            InitializeComponent();
            CloseAll.caMW1 = this;
            isFirstTime = false;
            

            #region initialize MenuImg

            MenuImg = new CMenuImage();
            MenuImg.iCurrMenu = (int)MenuIndex.CLASS;
            MenuImg.imgTitle = image_title;

            MenuImg.imgArr[(int)MenuIndex.CLASS] = image_class;
            MenuImg.imgArr[(int)MenuIndex.FILE] = image_file;
            MenuImg.imgArr[(int)MenuIndex.BACKGROUND] = image_background;
            MenuImg.imgArr[(int)MenuIndex.AVATAR] = image_avatar;
            MenuImg.imgArr[(int)MenuIndex.VIDEO] = image_video;

            MenuImg.gridArr[(int)MenuIndex.CLASS] = grid_class;
            MenuImg.gridArr[(int)MenuIndex.FILE] = grid_file;
            MenuImg.gridArr[(int)MenuIndex.BACKGROUND] = grid_background;
            MenuImg.gridArr[(int)MenuIndex.AVATAR] = grid_avatar;
            MenuImg.gridArr[(int)MenuIndex.VIDEO] = grid_video;

            MenuImg.biTitleSourceArr[(int)MenuIndex.CLASS] = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"\Image/타이틀1.png", UriKind.Relative));
            MenuImg.biTitleSourceArr[(int)MenuIndex.FILE] = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"\Image/타이틀2.png", UriKind.Relative));
            MenuImg.biTitleSourceArr[(int)MenuIndex.BACKGROUND] = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"\Image/타이틀3.png", UriKind.Relative));
            MenuImg.biTitleSourceArr[(int)MenuIndex.AVATAR] = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"\Image/타이틀4.png", UriKind.Relative));
            MenuImg.biTitleSourceArr[(int)MenuIndex.VIDEO] = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"\Image/타이틀5.png", UriKind.Relative));

            //MenuImg.biMouseOverSourceArr[(int)MenuIndex.CLASS] = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"\Image/1_마우스오버.png", UriKind.Relative));
            //MenuImg.biMouseOverSourceArr[(int)MenuIndex.FILE] = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"\Image/2_마우스오버.png", UriKind.Relative));
            //MenuImg.biMouseOverSourceArr[(int)MenuIndex.BACKGROUND] = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"\Image/3_마우스오버.png", UriKind.Relative));
            //MenuImg.biMouseOverSourceArr[(int)MenuIndex.AVATAR] = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"\Image/4_마우스오버.png", UriKind.Relative));
            //MenuImg.biMouseOverSourceArr[(int)MenuIndex.VIDEO] = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"\Image/5_마우스오버.png", UriKind.Relative));

            //MenuImg.biMouseLeaveSourceArr[(int)MenuIndex.CLASS] = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"\Image/1.png", UriKind.Relative));
            //MenuImg.biMouseLeaveSourceArr[(int)MenuIndex.FILE] = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"\Image/2.png", UriKind.Relative));
            //MenuImg.biMouseLeaveSourceArr[(int)MenuIndex.BACKGROUND] = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"\Image/3.png", UriKind.Relative));
            //MenuImg.biMouseLeaveSourceArr[(int)MenuIndex.AVATAR] = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"\Image/4.png", UriKind.Relative));
            //MenuImg.biMouseLeaveSourceArr[(int)MenuIndex.VIDEO] = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"\Image/5.png", UriKind.Relative));

            

            #endregion
        }*/

        private void LectureSettingWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //초기화
            //MenuImg.ChangeMenu(image_class);

            #region 최동훈 추가

            //this.GetLectureList();
            //webviewList.SelectedIndex = 0;
            //SetMenuList("image_class");
            //ResetEvent("image_class");

            #endregion
        }

        #region Unnecessary Methods
        //private void ResetEvent(string Name)
        //{
        //    image_class.MouseEnter -= image_MouseEnter;
        //    image_file.MouseEnter -= image_MouseEnter;
        //    image_background.MouseEnter -= image_MouseEnter;
        //    image_avatar.MouseEnter -= image_MouseEnter;
        //    //image_video.MouseEnter -= image_MouseEnter;

        //    image_class.MouseLeave -= image_MouseLeave;
        //    image_file.MouseLeave -= image_MouseLeave;
        //    image_background.MouseLeave -= image_MouseLeave;
        //    image_avatar.MouseLeave -= image_MouseLeave;
        //    //image_video.MouseLeave -= image_MouseLeave;

        //    image_class.MouseEnter += image_MouseEnter;
        //    image_file.MouseEnter += image_MouseEnter;
        //    image_background.MouseEnter += image_MouseEnter;
        //    image_avatar.MouseEnter += image_MouseEnter;
        //    //image_video.MouseEnter += image_MouseEnter;

        //    image_class.MouseLeave += image_MouseLeave;
        //    image_file.MouseLeave += image_MouseLeave;
        //    image_background.MouseLeave += image_MouseLeave;
        //    image_avatar.MouseLeave += image_MouseLeave;
        //    //image_video.MouseLeave += image_MouseLeave;

        //    switch (Name)
        //    {
        //        case "image_class":
        //            image_class.MouseEnter -= image_MouseEnter;
        //            image_class.MouseLeave -= image_MouseLeave;
        //            break;

        //        case "image_file":
        //            image_file.MouseEnter -= image_MouseEnter;
        //            image_file.MouseLeave -= image_MouseLeave;
        //            break;

        //        case "image_background":
        //            image_background.MouseEnter -= image_MouseEnter;
        //            image_background.MouseLeave -= image_MouseLeave;
        //            break;

        //        case "image_avatar":
        //            image_avatar.MouseEnter -= image_MouseEnter;
        //            image_avatar.MouseLeave -= image_MouseLeave;
        //            break;


        //    }
        //}

        //private void SetMenuList(string Name)
        //{
        //    string ImageFileName = string.Empty;

        //    image_class.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"\Image/UIImage/Main/modename.png", UriKind.RelativeOrAbsolute));
        //    image_file.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"\Image/UIImage/Main/lecturename.png", UriKind.RelativeOrAbsolute));
        //    image_background.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"\Image/UIImage/Main/avatarname.png", UriKind.RelativeOrAbsolute));
        //    image_avatar.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"\Image/UIImage/Main/bgname.png", UriKind.RelativeOrAbsolute));

            
        //    switch (Name)
        //    {
        //        case "image_class":
        //            image_class.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"\Image/UIImage/Main/modename.png", UriKind.RelativeOrAbsolute));
        //            grid_class.Visibility = System.Windows.Visibility.Visible;
        //            break;

        //        case "image_file":
        //            image_file.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"\Image/2_Over.png", UriKind.RelativeOrAbsolute));
        //            //grid_file.Visibility = System.Windows.Visibility.Visible;
        //            break;

        //        case "image_background":
        //            image_background.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"\Image/3_Over.png", UriKind.RelativeOrAbsolute));
        //            //grid_background.Visibility = System.Windows.Visibility.Visible;
        //            break;

        //        case "image_avatar":
        //            image_avatar.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"\Image/4_Over.png", UriKind.RelativeOrAbsolute));
        //            //grid_avatar.Visibility = System.Windows.Visibility.Visible;
        //            break;

        //    }
        //}
        #endregion

        private void image_MouseEnter(object sender, MouseEventArgs e)
        {
            //MenuImg.MouseOverImage(sender);

            string Name = ((Image)sender).Name;
            string ImageFileName = string.Empty;

            switch (Name)
            {
                case "image_class":
                    ImageFileName = "modename2.png";                    
                    break;

                case "image_file":
                    ImageFileName = "lecturename2.png";                    
                    break;
                case "image_avatar":
                    ImageFileName = "avatarname2.png";
                    break;
                case "image_background":
                    ImageFileName = "bgname2.png";
                    break;

            }

            ((Image)sender).Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(strMainImagePath + ImageFileName, UriKind.RelativeOrAbsolute));
        }

        private void image_MouseLeave(object sender, MouseEventArgs e)
        {
            string Name = ((Image)sender).Name;
            string ImageFileName = string.Empty;

            switch (Name)
            {
                case "image_class":
                    ImageFileName = "modename.png";
                    break;

                case "image_file":
                    ImageFileName = "lecturename.png";
                    break;
                case "image_avatar":
                    ImageFileName = "avatarname.png";
                    break;
                case "image_background":
                    ImageFileName = "bgname.png";
                    break;

            }

            ((Image)sender).Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(strMainImagePath +ImageFileName, UriKind.RelativeOrAbsolute));
        }


        private void image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //MenuImg.ChangeMenu(sender);

            string Name = ((Image)sender).Name;

            switch ((MenuIndex)curSelectedMenuIndex)
            {
                case MenuIndex.CLASS: ucLect.Visibility = Visibility.Collapsed; break;
                case MenuIndex.FILE: ucMat.Visibility = Visibility.Collapsed; break;
                case MenuIndex.AVATAR: ucAvat.Visibility = Visibility.Collapsed; break;
                case MenuIndex.BACKGROUND: ucBg.Visibility = Visibility.Collapsed; break;
            }

            switch (Name)
            {
                case "image_class":
                    ucLect.Visibility = Visibility.Visible;
                    curSelectedMenuIndex = (int)MenuIndex.CLASS;
                    break;

                case "image_file":
                    ucMat.Visibility = Visibility.Visible;
                    curSelectedMenuIndex = (int)MenuIndex.FILE;
                    break;

                case "image_avatar":
                    ucAvat.Visibility = Visibility.Visible;
                    curSelectedMenuIndex = (int)MenuIndex.AVATAR;
                    break;
                case "image_background":
                    ucBg.Visibility = Visibility.Visible;
                    curSelectedMenuIndex = (int)MenuIndex.BACKGROUND;
                    break; 
            }
        }


        private void image_close_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            //MessageBox.Show(e.Key.ToString());

            switch (e.Key)
            {
                case Key.Escape:
                    this.Close();
                    break;
            }
        }

        public class CMenuImage
        {
            public int iCurrMenu { get; set; }
            public Image imgTitle = new Image();
            public BitmapImage[] biTitleSourceArr = new BitmapImage[5];
            public Image[] imgArr = new Image[5];
            public Grid[] gridArr = new Grid[5];
            public BitmapImage[] biMouseOverSourceArr = new BitmapImage[5];
            public BitmapImage[] biMouseLeaveSourceArr = new BitmapImage[5];

            public void ChangeMenu(object sender)
            {
                for (int i = 0; i < imgArr.GetLength(0); ++i)
                {
                    if (imgArr[i] == (Image)sender)
                    {
                        iCurrMenu = i;
                        imgTitle.Source = biTitleSourceArr[iCurrMenu];
                        gridArr[iCurrMenu].Visibility = Visibility.Visible;
                        imgArr[iCurrMenu].Source = biMouseOverSourceArr[iCurrMenu];
                    }
                    else
                    {
                        gridArr[i].Visibility = Visibility.Hidden;
                        imgArr[i].Source = biMouseLeaveSourceArr[i];
                        imgArr[i].Visibility = Visibility.Visible;
                    }
                }
            }

            public void MouseOverImage(object sender)
            {
                for (int i = 0; i < imgArr.GetLength(0); ++i)
                {
                    if (imgArr[i] == (Image)sender && iCurrMenu != i)
                    {
                        imgArr[i].Source = biMouseOverSourceArr[i];
                        break;
                    }
                }
            }

            public void MouseLeaveImage(object sender)
            {
                for (int i = 0; i < imgArr.GetLength(0); ++i)
                {
                    if (imgArr[i] == (Image)sender && iCurrMenu != i)
                    {
                        imgArr[i].Source = biMouseLeaveSourceArr[i];
                        break;
                    }
                }
            }
        }


        #region 최동훈 추가 또는 수정 사항

        #region 이벤트 핸들러

        private void imgSave_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //if (string.IsNullOrEmpty(_Info.SelectedLectureSeq))
            //    MessageBox.Show("강좌 리스트를 선택해 주세요.");
            if (CheckIfTheNeccessaryLectureInformationIsProvided())
            {
                if (string.IsNullOrEmpty(UserInfo._LectureModuleTitle))
                {
                    //MessageBox.Show("강좌 차시를 기입해 주세요.");

                    string ErrorKey = "LectureModuleTitleError";
                    ErrorWindow ew = new ErrorWindow(ErrorKey);

                    ew.Left = 700;
                    ew.Top = 400;

                    ew.ShowDialog();
                }
                //else if (UserInfo._string.IsNullOrEmpty(txt_path.Text) && !web_check_on)
                else if (UserInfo._MaterialMode != 2 && UserInfo._MaterialFileName == String.Empty)
                {
                    //MessageBox.Show("강좌 파일 정보를 기입해 주세요.");

                    string ErrorKey = "LectureFileNameError";
                    ErrorWindow ew = new ErrorWindow(ErrorKey);

                    ew.Left = 700;
                    ew.Top = 400;

                    ew.ShowDialog();
                }
                else
                    SaveLectureInfo();
            }
            
        }

        //Joy_Modified
        private void image_record_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //this.Visibility = Visibility.Hidden;
            
            //string strfileName = cbLectureList.Text + strFileDivCharacter + txt_LectureModuleTitle.Text + strFileDivCharacter + txt_MemberName.Text;

            //string LectureSeq = ((ComboBoxItem)cbLectureList.SelectedItem).Tag.ToString();
            //string LectureModuleTitle = txt_LectureModuleTitle.Text;

           // if (string.IsNullOrEmpty(LectureSeq) || string.IsNullOrEmpty(LectureModuleTitle))
            if (!UserInfo.isSaved)
            {
                //MessageBox.Show("저장이 안되었습니다. 저장 처리 해 주십시요.");

                string MessageKey = "NotSavedErrorMessage";
                ErrorWindow ew = new ErrorWindow(MessageKey);

                ew.Left = 700;
                ew.Top = 400;

                ew.ShowDialog();

                return;
            }
            else
            {
                DataSetting.initialize();   //수정해야
                this.Visibility = Visibility.Hidden;

                if (UserInfo._AvatarMode == 0) //1: RealMode
                {
                    LecyureDataSaveTxt lecyureData = new LecyureDataSaveTxt();
                    avata_Count = UserInfo._AvatarMode;
                    lecyureData.Set_Avata_count(UserInfo._AvatarMode);
                    if (avata_Count == 1)
                        lecyureData.Add_Avata_Play(UserInfo._FirstAvatar);
                    if (avata_Count > 1)
                        for (int i = 0; i < avata_Count; i++)
                            lecyureData.Add_Avata_Play(UserInfo._FirstAvatar + i);
                    lecyureData.Set_BackGround_Num(UserInfo._Background);
                    lecyureData.Set_lecyure_type(UserInfo._ContentAlignmentMode);

                    lecyureData.Save_txt();
                }

                if (UserInfo._AvatarMode == 1)
                {
                    if (rm == null)
                        rm = new RealModeWindow();

                    rm.Show();
                }
                else
                {
                    #region 최동훈 수정 및 추가

                    mw = new LectureRecordWindow();
                    //mw = new LectureRecordWindow(strAvatar, txt_path.Text);
                    //lw.Close();
                    mw.Show();

                    #endregion


                }

            }

            #region ToBeDeleted
            //if ((LectureSeq != NowLectureSeq || LectureModuleTitle != NowLectureModuleTitle))
            //{
            //    //MessageBox.Show("저장이 안되었습니다. 저장 처리 해 주십시요.");

            //    string MessageKey = "NotSavedErrorMessage";
            //    ErrorWindow ew = new ErrorWindow(MessageKey);

            //    ew.Left = 700;
            //    ew.Top = 400;

            //    ew.ShowDialog();

            //    return;
            //}

           

            //if (UserInfo._MaterialMode != 2 && UserInfo._MaterialFileName == string.Empty ) //AuthoringMode 0: Avatar    _MaterialMode 2:web
            //{
            //    MessageBox.Show("Lecture material is not included");
            //    return;
            //}
            //else
            //{
            //    string[] extension;
            //    extension = UserInfo._MaterialFileName.Split('.');
            //    switch (extension[extension.Length - 1])
            //    {
            //        case "pptx":
            //            strAVI = String.Empty;
            //            strPPT = UserInfo._MaterialFileName;
            //            break;
            //        case "wmv":
            //        case "mp4":
            //        case "avi":
            //            strAVI = UserInfo._MaterialFileName;
            //            strPPT = String.Empty;
            //            break;
            //    }
            //}
            //else
            //{
            //    strPPT = String.Empty;
            //    strAVI = String.Empty;
            //}

            //if (webviewList.SelectedItem.ToString() == "사용안함")
            //{
            //    strWEB = String.Empty;
            //}
            //else
            //{
            //    strWEB = webviewList.SelectedItem.ToString();
            //}


            #endregion

        #endregion
            //}


           


        }
        #endregion

        //Needs to do!!!!!
        private bool CheckIfTheNeccessaryLectureInformationIsProvided()
        {
            string MessageKey = string.Empty;
            if ((UserInfo._LectureTitle != string.Empty) && (UserInfo._LectureModuleTitle != string.Empty) && (UserInfo._MemberName != string.Empty))
            {
                if (UserInfo._MaterialFileName != string.Empty) //IF MaterialFileName is not empty
                {
                    return true;
                }
            }

            return false;
        }

        #region 일반메소드

        private bool SaveLectureInfo()
        {
            bool bDuplicate = false;
            dsLectureInfo = new DataSet();
            dsLectureInfo.ReadXml("../../LectureInfo.xml");

            DataTable dt = dsLectureInfo.Tables[0];

            // 강의정보 등록
            if (!string.IsNullOrEmpty(UserInfo._LectureSeq))
            {
                int RowCnt = dt.Rows.Count;

                if (RowCnt > 0)
                {
                    for (int i = 0; i < RowCnt; i++)
                    {
                        if (string.IsNullOrEmpty(dt.Rows[0]["MemberSeq"].ToString()))
                            dt.Rows.RemoveAt(0);
                    }

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["LectureSeq"].ToString() == UserInfo._LectureSeq && dt.Rows[i]["LectureModuleTitle"].ToString() == UserInfo._LectureModuleTitle)
                        {
                            bDuplicate = true;
                            break;
                        }
                    }
                }


                if (bDuplicate)
                {
                    //MessageBox.Show("이미 저장된 정보가 있어.");
                    string MessageKey = "DuplicateSaveErrorMessage";
                    ErrorWindow ew = new ErrorWindow(MessageKey);

                    ew.ShowDialog();
                }
                else
                {
                    DataRow row = dt.NewRow();
                    row["MemberSeq"] = UserInfo._MemberSeq;
                    row["MemberName"] = UserInfo._MemberName;
                    row["CompanySeq"] = UserInfo._CompanySeq;

                    row["LectureSeq"] = UserInfo._LectureSeq;
                    row["LectureTitle"] = UserInfo._LectureTitle;
                    row["LectureModuleSeq"] = UserInfo._LectureModuleSeq;
                    row["LectureModuleTitle"] = UserInfo._LectureModuleTitle;

                    row["LectureFileName"] = UserInfo._MaterialFileName;
                    row["BackgroundFileName"] = UserInfo._Background;
                    row["AvatarFileName"] = UserInfo._FirstAvatar;
                    row["PlayTime"] = UserInfo._PlayTime;

                    row["EncodingResolution"] = UserInfo._EncodingResolution;
                    //row["WmvFileName"] = UserInfo._MaterialFileName;

                    dt.Rows.Add(row);

                    if (File.Exists("../../LectureInfo.xml"))
                        File.Delete("../../LectureInfo.xml");


                    dt.WriteXml("../../LectureInfo.xml");


                    string MessageKey = "SaveSuccessMessage";
                    ErrorWindow ew = new ErrorWindow(MessageKey);

                    ew.Left = 700;
                    ew.Top = 400;

                    ew.ShowDialog();
                    UserInfo.isSaved = true;
                }

                NowLectureSeq = UserInfo._LectureSeq;
                NowLectureModuleTitle = UserInfo._LectureModuleTitle;

                return true;
            }

            return false;

        }
        #region TobeDeleted
        /*public void SaveLectureInfo()
        {
            try
            {
                #region 강사 관련 정보

                string MemberSeq = _Info.MemberSeq;
                string MemberName = _Info.MemberName;
                string CompanySeq = _Info.CompanySeq;
                                
                #endregion

                #region 강좌 관련 정보

                #region xml 구조
                //<MemberSeq></MemberSeq>
                //<MemberName></MemberName>                
                //<CompanySeq></CompanySeq>
                //<LectureIdx></LectureIdx>
                //<LectureTitle></LectureTitle>
                //<LectureSeq></LectureSeq>
                //<LectureModuleTitle></LectureModuleTitle>
                //<LectureModuleSeq></LectureModuleSeq>
                //<PlayTime></PlayTime>  
                //<Resolution></Resolution>
                #endregion

                string LectureSeq = ((ComboBoxItem)cbLectureList.SelectedItem).Tag.ToString();
                _Info.SelectedLectureSeq = LectureSeq;
                _Info.LectureModuleTitle = txt_LectureModuleTitle.Text;

                string LectureTitle = ((ComboBoxItem)cbLectureList.SelectedItem).Content.ToString();
                string LectureModuleTitle = txt_LectureModuleTitle.Text;
                string LectureModuleSeq = string.Empty;
                string LectureFileName = string.Empty;
                string BackgroundFileName = "Image/녹화_배경1.png";
                string AvatarFileName = string.Empty;
                string PlayTime = "3600";
                EncodingResolution Er = EncodingResolution.R1920x1080;

                #endregion

                SaveLectureInfo(MemberSeq, MemberName, CompanySeq, LectureSeq, LectureTitle, LectureModuleSeq, LectureModuleTitle, LectureFileName, BackgroundFileName, AvatarFileName, PlayTime, Er);
                                
            }
            catch (Exception ex)
            {
                MessageBox.Show("오류가 발생하였습니다.");
            }

        }*/



        /// <summary>
        /// 강의목록 불러오기
        /// </summary>
        /*private void GetLectureList()
        {
            if (_Info != null)
            {
                cbLectureList.Items.Clear();

                using (VCastService VCS = new VCastService())
                {
                    int CompanySeq = int.Parse(_Info.CompanySeq);
                    int MemberSeq = int.Parse(_Info.MemberSeq);

                    string[] arrLectureList = VCS.LectureList(CompanySeq, MemberSeq).Split(',');
                    string[] arrItemList = null;
                    ComboBoxItem cbi;

                    for (int i = 0; i < arrLectureList.Length; i++)
                    {
                        cbi = new ComboBoxItem();
                        arrItemList = arrLectureList[i].Split('|');
                        if (arrItemList.Length > 1)
                        {
                            cbi.Content = arrItemList[1];
                            cbi.Tag = arrItemList[0];

                            cbLectureList.Items.Add(cbi);
                        }
                    }

                    if (cbLectureList.Items.Count > 0)
                        cbLectureList.SelectedIndex = 0;

                    txt_MemberName.Text = _Info.MemberName;
                }
            }
        }/*



       /* private void  SaveLectureInfo(
            string MemberSeq,
            string MemberName,
            string CompanySeq,
            string LectureSeq,
            string LectureTitle,
            string LectureModuleSeq,
            string LectureModuleTitle,
            string LectureFileName,
            string BackgroundFileName,
            string AvatarFileName,
            string PlayTime,
            EncodingResolution Er
            )
        {
            bool bDuplicate = false;
            dsLectureInfo = new DataSet();
            dsLectureInfo.ReadXml("../../LectureInfo.xml");

            DataTable dt = dsLectureInfo.Tables[0];

            // 강의정보 등록
            if (!string.IsNullOrEmpty(LectureSeq))
            {
                int RowCnt = dt.Rows.Count;

                if (RowCnt > 0)
                {
                    for (int i = 0; i < RowCnt; i++)
                    {
                        if(string.IsNullOrEmpty(dt.Rows[0]["MemberSeq"].ToString()))
                            dt.Rows.RemoveAt(0);
                    }

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["LectureSeq"].ToString() == LectureSeq && dt.Rows[i]["LectureModuleTitle"].ToString() == LectureModuleTitle)
                        {
                            bDuplicate = true;
                            break;
                        }
                    }
                }

                if (bDuplicate)
                {
                    //MessageBox.Show("이미 저장된 정보가 있어.");
                    string MessageKey = "DuplicateSaveErrorMessage";
                    ErrorWindow ew = new ErrorWindow(MessageKey);

                    ew.ShowDialog();
                }
                else
                {
                    DataRow row = dt.NewRow();
                    row["MemberSeq"] = MemberSeq;
                    row["MemberName"] = MemberName;
                    row["CompanySeq"] = CompanySeq;

                    row["LectureSeq"] = LectureSeq;
                    row["LectureTitle"] = LectureTitle;
                    row["LectureModuleSeq"] = LectureModuleSeq;
                    row["LectureModuleTitle"] = LectureModuleTitle;

                    row["LectureFileName"] = LectureFileName;
                    row["BackgroundFileName"] = BackgroundFileName;
                    row["AvatarFileName"] = AvatarFileName;
                    row["PlayTime"] = PlayTime;

                    row["EncodingResolution"] = Er;
                    row["WmvFileName"] = string.Empty;

                    dt.Rows.Add(row);

                    if(File.Exists("../../LectureInfo.xml"))
                        File.Delete("../../LectureInfo.xml");

                    //FileStream fs = new FileStream("../../LectureInfo.xml", FileMode.Create, FileAccess.Write);
                    //StreamWriter sw = new StreamWriter(fs);

                    //sw.Write(dsLectureInfo.GetXml());
                    //sw.Flush();

                    //sw.Close();
                    //fs.Close();

                    dt.WriteXml("../../LectureInfo.xml");

                      
                    string MessageKey = "SaveSuccessMessage";
                    ErrorWindow ew = new ErrorWindow(MessageKey);

                    ew.Left = 700;
                    ew.Top = 400;

                    ew.ShowDialog();

                    //MessageBox.Show("저장 되었습니다.");
                }

                NowLectureSeq = LectureSeq;
                NowLectureModuleTitle = LectureModuleTitle;

                
            }
            else
            {
                // 강의정보 수정
            }

            
        }*/
        #endregion

        private int GetRowIndex(DataSet ds, string LectureSeq, string LectureModuleSeq)
        {
            int Idx = -1;

            DataTable dt = ds.Tables[0];

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][LectureSeq].ToString() == LectureSeq && dt.Rows[i][LectureModuleSeq].ToString() == LectureModuleSeq)
                        Idx = i;
                }
            }

            return Idx;
        }

        #endregion
        

        //private void lecture_mode1_Click(object sender, RoutedEventArgs e)
        //{
        //    lecyure_type = 2; // 교안이 왼쪽에 위치
        //}

        //private void lecture_mode2_Click(object sender, RoutedEventArgs e)
        //{
        //    lecyure_type = 1; // 교안이 중앙에 위치
        //}

        //private void lecture_mode3_Click(object sender, RoutedEventArgs e)
        //{
        //    lecyure_type = 3; // 교안이 오른쪽에 위치
        //}

        //private void Button_Click_1(object sender, RoutedEventArgs e)
        //{
        //    avata_Count = 1;
        //}

        //private void Button_Click_2(object sender, RoutedEventArgs e)
        //{
        //    avata_Count = 2;
        //}

        // //Joy_Modified (Need to add two radio buttons to set the Bg Mode)
        //private void rdbRealMode_Click(object sender, RoutedEventArgs e)
        //{
        //    isRealMode = true;
        //}

        //private void rdbAvatarMode_Click(object sender, RoutedEventArgs e)
        //{
        //    isRealMode = false;
        //}

        private void image_class_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void image_class_MouseLeave(object sender, MouseEventArgs e)
        {

        }

        private void SaveLectureInformationWithValidation()
        {
            if (CheckIfTheNeccessaryLectureInformationIsProvided())
            {
                if (string.IsNullOrEmpty(UserInfo._LectureModuleTitle))
                {
                    string ErrorKey = "LectureModuleTitleError";
                    ErrorWindow ew = new ErrorWindow(ErrorKey);

                    ew.Left = 700;
                    ew.Top = 400;

                    ew.ShowDialog();
                }
                //else if (UserInfo._string.IsNullOrEmpty(txt_path.Text) && !web_check_on)
                else if (UserInfo._MaterialMode != 2 && UserInfo._MaterialFileName == String.Empty)
                {
                    string ErrorKey = "LectureFileNameError";
                    ErrorWindow ew = new ErrorWindow(ErrorKey);

                    ew.Left = 700;
                    ew.Top = 400;

                    ew.ShowDialog();
                }
                else
                   SaveLectureInfo();
            }
        }

        private void image_start_recording_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            UserInfo._LectureModuleTitle = txt_LectureModuleTitle.Text;

            SaveLectureInformationWithValidation();

            if (!UserInfo.isSaved)
            {
                //MessageBox.Show("저장이 안되었습니다. 저장 처리 해 주십시요.");

                string MessageKey = "NotSavedErrorMessage";
                ErrorWindow ew = new ErrorWindow(MessageKey);

                ew.Left = 700;
                ew.Top = 400;

                ew.ShowDialog();

                return;
            }
            else
            {
                DataSetting.initialize();   //수정해야
                this.Visibility = Visibility.Hidden;

                if (UserInfo._AvatarMode != 0) //0:RealMode 1:One Player 2:Two Players
                {
                    LecyureDataSaveTxt lecyureData = new LecyureDataSaveTxt();
                    avata_Count = UserInfo._AvatarMode + 1;
                    lecyureData.Set_Avata_count(UserInfo._AvatarMode);
                    if (avata_Count == 1)
                        lecyureData.Add_Avata_Play(UserInfo._FirstAvatar);
                    if (avata_Count > 1)
                        for (int i = 0; i < avata_Count; i++)
                            lecyureData.Add_Avata_Play(UserInfo._FirstAvatar + i);
                    lecyureData.Set_BackGround_Num(UserInfo._Background);
                    lecyureData.Set_lecyure_type(UserInfo._ContentAlignmentMode);

                    lecyureData.Save_txt();
                }

                if (UserInfo._AvatarMode == 0)
                {
                    if (rm == null)
                        rm = new RealModeWindow();

                    rm.Show();
                }
                else
                {
                    mw = new LectureRecordWindow();
                    mw.Show();
                }
            }
        }

        private void img_ExitBtn_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (CloseAll.caLW != null)
                CloseAll.caLW.Close();
            if (CloseAll.caCW != null)
                CloseAll.caCW.Close();
            if (CloseAll.caMW != null)
                CloseAll.caMW.Close();
            if (CloseAll.caMW1 != null)
                CloseAll.caMW1.Close();
            if (CloseAll.caMW2 != null)
                CloseAll.caMW2.Close();
            if (CloseAll.caRW != null)
                CloseAll.caRW.Close();
            if (CloseAll.caLoWin != null)
                CloseAll.caLoWin.Close();
            if (CloseAll.caRM != null)
                CloseAll.caRM.Close();
            this.Close();
        }

        private bool checkKeyInput(Key key)
        {
            if (key == Key.OemQuotes || key == Key.OemSemicolon || key == Key.OemQuestion || key == Key.OemCloseBrackets || key == Key.OemOpenBrackets
                || key == Key.OemPeriod || key == Key.OemBackslash || key == Key.Separator || key == Key.OemComma || key == Key.OemPipe)
            {
                return true;

            }
            return false;
        }

        private void txt_LectureModuleTitle_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = checkKeyInput(e.Key);        
        }

        private void txt_LectureModuleTitle_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text.Equals("_"))
            {

                e.Handled = true;

            }
        }

    }
   
}
