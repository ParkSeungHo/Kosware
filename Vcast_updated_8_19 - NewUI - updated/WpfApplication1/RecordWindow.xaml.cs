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

using System.Diagnostics;
using System.Threading;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.IO;

using System.Data;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for RecordWindow.xaml
    /// </summary> 
    public partial class RecordWindow : Window
    {
        public bool isRecording = false;

        //public event EventHandler UnloadMainForm;

        LectureRecordWindow mw;
        LectureEditingWindow mw2;
        LectureSettingWindow mw1;
        RealModeWindow rm;
        bool isRepeat = false;
        public Stopwatch ticktok = new Stopwatch();

        #region 최동훈 추가

        public RecordWindow(LectureRecordWindow mw, RealModeWindow rm)
        {
            InitializeComponent();

            if (mw != null)
                this.mw = mw;

            if (rm != null)
                this.rm = rm;

            this.mw1 = CloseAll.caMW1;
            System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
            CloseAll.caRW = this;

            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Tick += timer_Tick;
            timer.Start();

            //this.mw.Focus();
        }

        #endregion


        void timer_Tick(object sender, EventArgs e)
        {
            TimeSpan ts = ticktok.Elapsed;
            label_timer.Content = string.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
        }

        
        private void UnloadForm()
        {
            if (mw != null)
                mw.ResetAll();
            else if (rm != null)
                rm.ResetAll();
        }

        private void image_play_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (!isRecording)
            {
                //ticktok.Start(); 
                this.Visibility = Visibility.Hidden;

                image_play.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"\Image/일시정지버튼.png", UriKind.Relative));
                isRecording = true;
                if (!isRepeat)
                {
                    isRepeat = true;
                    CloseAll.caLoWin.Show();
                    if (mw != null)
                        CloseAll.caLoWin.LoadForms(this.mw, null);
                    else if (rm != null)
                        CloseAll.caLoWin.LoadForms(null, this.rm);
                }

            }
        }


        #region 최동훈

        private void SetWmvFileName()
        {

            DataSet dsLectureInfo = new DataSet();
            dsLectureInfo.ReadXml("../../LectureInfo.xml");

            string FileName = Encoder.recFilename;
            FileName = System.IO.Path.GetFileName(FileName);

            DataTable dt = dsLectureInfo.Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["LectureSeq"].ToString() == UserInfo._SelectedLectureSeq && dt.Rows[i]["LectureModuleTitle"].ToString() == UserInfo._LectureModuleTitle)
                {
                    dt.Rows[i]["WmvFileName"] = FileName;
                    break;
                }
            }

            dt.WriteXml("../../LectureInfo.xml");
        }

        private void DeleteLectureInfo()
        {
            DataSet dsLectureInfo = new DataSet();
            dsLectureInfo.ReadXml("../../LectureInfo.xml");

            DataTable dt = dsLectureInfo.Tables[0];

            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                if (dt.Rows[i]["LectureSeq"].ToString() == UserInfo._SelectedLectureSeq &&
                    dt.Rows[i]["LectureModuleTitle"].ToString() == UserInfo._LectureModuleTitle)
                {
                    dt.Rows[i].Delete();
                    break;
                }
            }

            dt.WriteXml("../../LectureInfo.xml");

        }
        #endregion


        private void image_stop_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            ticktok.Reset();
            image_play.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"\Image/녹화실행버튼.png", UriKind.Relative));
            isRecording = false;

            #region 최동훈 추가

            SetWmvFileName();

            #endregion

        }

        /// <summary>
        /// 
        /// </summary>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.F5:
                    if (!isRecording)
                    {
                        this.Visibility = Visibility.Hidden;
                        image_play.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"\Image/일시정지버튼.png", UriKind.Relative));
                        CloseAll.caLoWin.ShowDialog();
                    }
                    break;

                case Key.F6:
                    ticktok.Reset();
                    image_play.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"\Image/녹화실행버튼.png", UriKind.Relative));
                    isRecording = false;
                    this.Visibility = Visibility.Hidden;
                    UnloadForm();

                    break;
                case Key.Escape:
                    this.Close();
                    break;
            }
        }

        private void image_deit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Encoder.CaptureCompleted(); 
            this.Visibility = Visibility.Hidden;
            UnloadForm();
            mw2 = new LectureEditingWindow();
            if (Encoder.recFilename != null)
                mw2.getVideo = true;
            mw2.Show();



        }

        private void image_delete_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Encoder.CaptureCompleted();
            isRecording = false;
            ticktok.Reset();
            if (File.Exists(Encoder.recFilename))
            {
                File.Delete(Encoder.recFilename);
                Encoder.recFilename = null;
                DeleteLectureInfo();
            }

            image_play.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"\Image/녹화실행버튼.png", UriKind.Relative));
            this.Visibility = Visibility.Hidden;
            UnloadForm();

            mw1.Show();
        }

        private void image_bg_KeyDown(object sender, KeyEventArgs e)
        {
            MessageBox.Show(e.Key.ToString());
        }

    }
}
