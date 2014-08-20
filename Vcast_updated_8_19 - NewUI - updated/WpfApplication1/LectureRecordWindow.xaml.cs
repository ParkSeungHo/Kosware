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


using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;

using Microsoft.Win32;


namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for LectureRecordWindow.xaml
    /// </summary>
    public partial class LectureRecordWindow : Window
    {
        #region 김혁주 추가

        static UnityControl2 Unity;
//        string _pptPath;
//        Presentation PPT;
        #endregion

//        AvateeringXNA game;

//        soket s;
//        soket_data sd;
        private ucPPTWinForm ppt;
        private ucAVIWinForm avi;
        private ucWebForm web;

        RecordWindow rw = null;
        LectureEditingWindow mw_vedit;
        private bool isFirstTime = true;
        LoadingWindow lowin = null;
        private string pptPath;
        private string aviPath;
        private string strweb;   //구글 스트리트뷰를 교안으로 사용시 이용

        private string fileName;//비디오 파일명 설정에 사용

        #region 최동훈 추가

        #endregion

        //rw_UnloadMainForm

        //public LectureRecordWindow()
        //{
        //    InitializeComponent();
        //    CloseAll.caMW = this;
        //    this.Closing += new System.ComponentModel.CancelEventHandler(Window_Closing);
        //    // 윈도우 생성후 바로 보여준다.
        //    isFirstTime = false;
        //}

        public LectureRecordWindow()
        {

            InitializeComponent();
            CloseAll.caMW = this;

            Unity = new UnityControl2();

            if (UserInfo._MaterialMode == 0 && UserInfo._MaterialFileName != string.Empty)
                ppt = new ucPPTWinForm();
            else if (UserInfo._MaterialMode == 1 && UserInfo._MaterialFileName != string.Empty)
                avi = new ucAVIWinForm();
            if (UserInfo._MaterialMode == 2 && UserInfo._MaterialFileName != string.Empty)
                web = new ucWebForm();

            //switch (UserInfo._ContentAlignmentMode)
            //{
            //    case 0:
            //        windowsFormsHost2.Margin = new Thickness() { Left = 40, Top = 130 };
            //        break;
            //    case 1:
            //        windowsFormsHost2.Margin = new Thickness() { Left = 480, Top = 130 };
            //        break;
            //    case 2:
            //        windowsFormsHost2.Margin = new Thickness() { Left = 920, Top = 130 };
            //        break;
            //}

            

            this.Closing += new System.ComponentModel.CancelEventHandler(Window_Closing);
            // 윈도우 생성후 바로 보여준다.
            isFirstTime = false;


        }

        /*public LectureRecordWindow(string _pptPath, string _aviPath, UserInfo info, int lecyure_type, string _fileName, string _strweb)
        {

            InitializeComponent();
            Unity = new UnityControl2();
            this._info = info;

            pptPath = _pptPath;
            aviPath = _aviPath;
            fileName = _fileName; //비디오 파일명 설정에 사용
            strweb = _strweb;

            if(pptPath != "")
                SetPPT(pptPath);
            if (aviPath != "")
                avi = new ucAVIWinForm();
            if (strweb != "")
                web = new ucWebForm();

            switch (lecyure_type)
            {
                case 1:
                    windowsFormsHost2.Margin = new Thickness() { Left = 480, Top = 130 };
                    break;
                case 2:
                    windowsFormsHost2.Margin = new Thickness() { Left = 40, Top = 130 };
                    break;
                case 3:
                    windowsFormsHost2.Margin = new Thickness() { Left = 920, Top = 130 };
                    break;
            }

            CloseAll.caMW = this;

            this.Closing += new System.ComponentModel.CancelEventHandler(Window_Closing);
            // 윈도우 생성후 바로 보여준다.
            isFirstTime = false;


        }*/

        private void grid_ppt_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

       
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }

    #region JoyModified

        public bool getIsFirstTime()
        {
            return isFirstTime;
        }

        #region 최동훈 추가

        public void ShowRecordWindow()
        {
            if (rw == null)
            {
                rw = new RecordWindow(this, null);
                rw.ShowDialog();
            }
            else
            {
                rw.Visibility = Visibility.Visible;
                rw.Focus();
            }
        }

        #endregion
        
        //public void SetPPT(string pptPathAndTitle)
        //{
        //    ppt = new ucPPTWinForm();
        //    ppt.pptAction = pptPathAndTitle;
        //}

        //public void Setinfo(UserInfo info)
        //{
        //    this._info = info;
        //}

        public void ShowAvatar(string avatarName, string avatarBg)
        {
//            game.MyLoadContent();
//            game.isUpdatingFrame = true;
            
        }

        public void RestartStream()
        {
//            game.isUpdatingFrame = true;
        }

        public void ResetAll()
        {
            this.Visibility = Visibility.Hidden;
            //ppt.Dispose();
            if(ppt != null)
                ppt.Reset();
 //           game.isUpdatingFrame = false;
            //game.HideStream();
        }

    #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            //imgBrushBg.ImageSource = new BitmapImage(new Uri(CloseAll.caMW1.strBg, UriKind.RelativeOrAbsolute));
            lowin = new LoadingWindow();
            CloseAll.caLoWin = lowin;
            //lowin.ShowDialog();
            lowin.Visibility = Visibility.Hidden;
            lowin.LoadPPTAndAvatar += lowin_LoadPPTAndAvatar;
            ShowRecordWindow();
            //rw.LoadPPTAndAvatar += rw_LoadPPTAndAvatar;
           // rw.UnloadMainForm += rw_UnloadMainForm;
            //rw.ShowDialog();
        }

        void lowin_LoadPPTAndAvatar(object sender, EventArgs e)
        {
            this.Show();

            switch (UserInfo._ContentAlignmentMode)
            {
                case 0:
                    windowsFormsHost2.Margin = new Thickness() { Left = 40, Top = 130 };
                    break;
                case 1:
                    windowsFormsHost2.Margin = new Thickness() { Left = 480, Top = 130 };
                    break;
                case 2:
                    windowsFormsHost2.Margin = new Thickness() { Left = 920, Top = 130 };
                    break;
            }

            switch (UserInfo._MaterialMode)
            {
                case 0: //ppt
                    ppt = new ucPPTWinForm();
                    windowsFormsHost2.Child = ppt;
                    break;
                case 1: //video
                    avi = new ucAVIWinForm();
                    windowsFormsHost2.Child = avi;
                    break;
                case 2: web = new ucWebForm();
                    windowsFormsHost2.Child = web;
                    break;
            }

            #region 김혁주 추가

            //            ReadPPTfile(_pptPath);
            //            MakePPTIamge(_pptPath);

            windowsFormsHost1.Child = Unity;
            //            Unity.get_unity().SendMessage("file_print", "play", "ppt");

            #endregion

            CloseAll.caRW.ticktok.Start();
            CloseAll.caLoWin.Visibility = Visibility.Hidden;
            Encoder.RunbgThread();

            //            game.Run();

            this.Focus();

            //            MessageBox.Show(this.IsFocused.ToString());
            //            s.Send_Message(sd.Get_baye());
        }

        //original code
//        void lowin_LoadPPTAndAvatar(object sender, EventArgs e)
//        {
//            this.Show();
//            if (strweb != "")
//            {
//                if (strweb == "구글 스트리트뷰")
//                    web.Web_Url("https://www.google.com/maps/views/view/116301864423028290717/gphoto/5976405169727045986?gl=kr&hl=ko&heading=30&pitch=99&fovy=75");
//                if (strweb == "네이버 거리뷰")
//                    web.Web_Url("http://map.naver.com/?menu=location&mapMode=0&lat=37.5726755&lng=126.9770196&dlevel=12&searchCoord=126.9772544%3B37.5706746&query=7IS47KKF64yA7JmV64%2BZ7IOB&mpx=09110615%3A37.5706746%2C126.9772544%3AZ13%3A0.0013298%2C0.0007017&tab=1&vrpanotype=3&vrpanoid=3kwQCmzE7k1Xp0d7GPLjYg%3D%3D&vrpanopan=-3.7&vrpanotilt=4.18&vrpanofov=120&vrpanolat=37.5726756&vrpanolng=126.9770201&street=on&vrpanosky=off&vrpanopoi=off&enc=b64");
//                if (strweb == "다음 로드뷰")
//                    web.Web_Url("http://map.daum.net/?panoid=1020650859&pan=196.2&tilt=-15.3&zoom=0&map_type=TYPE_MAP&map_attribute=ROADVIEW&urlX=443096&urlY=1157693&urlLevel=3");
//                windowsFormsHost2.Child = web;
//            }
//            if(pptPath != "")
//                windowsFormsHost2.Child = ppt;
//            else if (aviPath != "")
//            {
//                windowsFormsHost2.Child = avi;
//                avi.PlayFile(aviPath);
//            }
//            #region 김혁주 추가

////            ReadPPTfile(_pptPath);
////            MakePPTIamge(_pptPath);

//            windowsFormsHost1.Child = Unity;
////            Unity.get_unity().SendMessage("file_print", "play", "ppt");

//            #endregion

//            CloseAll.caRW.ticktok.Start();
//            CloseAll.caLoWin.Visibility = Visibility.Hidden;
//            Encoder.RunbgThread();
            
////            game.Run();

//            this.Focus();

////            MessageBox.Show(this.IsFocused.ToString());
////            s.Send_Message(sd.Get_baye());
//        }

        
        void rw_UnloadMainForm(object sender, EventArgs e)
        {
//            if (game != null)
//            {
//               game.StopKinect();
//                game.Exit();
//            }
            mw_vedit = new LectureEditingWindow();
            mw_vedit.Show();
            
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            //this.Visibility = Visibility.Hidden;
            switch (e.Key)
            {
                //case Key.F5:
                    //rw.SetiCount(2);


                //case Key.Escape:
                //    this.Close();
                //    break;

                case Key.Escape:
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
                    this.Close();
                    Unity.Unity_Close();
            
                    if (ppt != null)
                        ppt.Reset();
                    if (avi != null)
                        avi.PauseVideo();
                    if (web != null)
                        web.Web_Close();
                    rw.ticktok.Stop();
                    break;
            }
        }


        private void imgClose_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            rw.Visibility = Visibility.Visible;
            rw.ticktok.Stop();
            rw.image_play.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"\Image/일시정지버튼.png", UriKind.Relative));
            Encoder.pauseRecording();
            rw.isRecording = false;
            Unity.Unity_Close();

            if (ppt != null)
                ppt.Reset();
            if (avi != null)
                avi.PauseVideo();
            if (web != null)
                web.Web_Close();
//            s.Close_Socket();
        }

        private void Window_Unloaded_1(object sender, RoutedEventArgs e)
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
            if (Unity != null)
                Unity.Unity_Close();
            if (ppt != null)
                ppt.Reset();
            if (avi != null)
                avi.CloseVideo();
            if (web != null)
                web.Web_Close();
            Encoder.Encoder_Close();
        }





    }
}
