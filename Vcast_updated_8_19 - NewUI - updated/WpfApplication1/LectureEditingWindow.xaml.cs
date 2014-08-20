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
using Microsoft.Win32;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;
using System.Windows.Forms;
using System.IO;

using WPFFileUpload.Core;
using WPFFileUpload.Controls;
using WPFFileUpload.Utils.Constants;
using WPFFileUpload.Utils.Helpers;
using System.Threading;

using System.Data;

#region 최동훈 추가

using WpfApplication1.kr.co.vcast.www;

#endregion

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for LectureEditingWindow.xaml
    /// </summary>
    public partial class LectureEditingWindow : Window
    {
        //private ObservableCollection<LectureList> _lectureList = new ObservableCollection<LectureList>();
        private ObservableCollection<LectureList> _lectureList;
        bool isDragging = false;
        bool isPlaying = false;
        Dialogs d;
        bool hasDuration = false;
        bool isStopped = false;
        bool isChanged = false;
        bool moveable = false;
        public bool getVideo = false;
        LectureSettingWindow mwSelect;

        #region 최동훈 추가
        DataSet dsLectureInfo = new DataSet();


        public LectureEditingWindow()
        {
            InitializeComponent();

            if (!Directory.Exists(DataSetting.fileDirection))
            {
                DataSetting.initialize();
            }

            Load_File(DataSetting.fileDirection);

            slider_seek.ApplyTemplate();
            Thumb thumb = (slider_seek.Template.FindName("PART_Track", slider_seek) as Track).Thumb;
            thumb.MouseEnter += new System.Windows.Input.MouseEventHandler(thumb_MouseEnter);
            thumb.DragStarted += new DragStartedEventHandler(slider_seeker_DragStarted);
            thumb.DragCompleted += new DragCompletedEventHandler(slider_seeker_DragCompleted);
            showMedia.ScrubbingEnabled = true;

            CloseAll.caMW2 = this;

            dsLectureInfo.ReadXml("../../LectureInfo.xml");
        }

        #endregion

        #region joyAdded
        private void CheckIsFirsTtime()
        {
            if (CloseAll.caMW1 == null)
            {
                //mwSelect = new LectureSettingWindow();
                mwSelect = new LectureSettingWindow();
                mwSelect.Show();
            }
            else
                CloseAll.caMW1.Visibility = Visibility.Visible;
        }
        #endregion

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                //case Key.Escape:
                //    this.Close();
                //    break;
                case Key.F11:
                    CheckIsFirsTtime();
                    this.Visibility = System.Windows.Visibility.Hidden;
                    break;

                default:
                    break;
            }
        }

        private void thumb_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                //showMedia.Position = TimeSpan.FromSeconds(slider_seek.Value); 
                MouseButtonEventArgs args = new MouseButtonEventArgs(e.MouseDevice, e.Timestamp, MouseButton.Left);

                args.RoutedEvent = MouseLeftButtonDownEvent;

                (sender as Thumb).RaiseEvent(args);
            }

        }

        private void slider_seeker_DragStarted(object sender, DragStartedEventArgs e)
        {
            if (hasDuration)
            {
                isDragging = true;
                isStopped = false;
                showMedia.Pause();
            }
        }

        private void slider_seeker_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            if (hasDuration)
            {
                isDragging = false;
                showMedia.Position = TimeSpan.FromSeconds(slider_seek.Value);
                if (showMedia.Position >= TimeSpan.FromSeconds(slider_seek.Maximum))
                {
                    showMedia.Pause();
                    showMedia.Position = TimeSpan.FromSeconds(slider_seek.Maximum);
                    image_PlayPause.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"\Image/임시재생버튼.png", UriKind.Relative));
                    isPlaying = false;
                    isStopped = true;
                }
                if (isPlaying)
                {
                    showMedia.Play();
                }
            }
        }

        private void showMedia_MediaOpened(object sender, RoutedEventArgs e)
        {
            if (showMedia.NaturalDuration.HasTimeSpan)
            {
                TimeSpan ts = TimeSpan.FromSeconds(showMedia.NaturalDuration.TimeSpan.TotalSeconds);
                slider_seek.Maximum = ts.TotalSeconds;
                DispatcherTimer dispatcherTimer = new DispatcherTimer();
                dispatcherTimer.Tick += new EventHandler(timer_Tick);
                dispatcherTimer.Interval = TimeSpan.FromMilliseconds(100);//new TimeSpan(0, 0, 1);
                dispatcherTimer.Start();
                hasDuration = true;
            }
            else
            {
                hasDuration = false;
                System.Windows.Forms.MessageBox.Show("It doesn't have duration information");
                slider_seek.Maximum = 1;
            }
        }

        private void setShowMedia(string filename)
        {
            showMedia.Source = new Uri(filename);
            Encoder.setMedia(showMedia.Source.OriginalString);
            slider_seek.Minimum = 0;
            isPlaying = false;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (!isDragging)
            {
                slider_seek.Value = showMedia.Position.TotalSeconds;
                if (isPlaying)
                {
                    if (showMedia.Position.TotalSeconds >= slider_seek.Maximum)
                    {
                        showMedia.Position = TimeSpan.FromSeconds(slider_seek.Maximum);
                        isPlaying = false;
                        showMedia.Pause();
                        image_PlayPause.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"\Image/임시재생버튼.png", UriKind.Relative));
                        isStopped = true;
                    }
                }
            }
            else
            {
                showMedia.Position = TimeSpan.FromSeconds(slider_seek.Value);
            }
            DurationLabel.Content = (showMedia.Position - TimeSpan.FromSeconds(slider_seek.Minimum)).ToString(@"hh\:mm\:ss\.ff");

        }

        private void Load_File(string FileDirection)
        {
            if(_lectureList != null) _lectureList.Clear();

            dsLectureInfo = new DataSet();
            dsLectureInfo.ReadXml("../../LectureInfo.xml");
            DataTable dt = dsLectureInfo.Tables[0];

            FileInfo[] allfiles = new DirectoryInfo(FileDirection).GetFiles("*.wmv*");
            List<LectureList> tempList = new List<LectureList>();
            string[] arrDividedFileName = null;
            
            foreach (FileInfo fileinfo in allfiles)
            {
                string strTemp = fileinfo.Name.Substring(0, fileinfo.Name.IndexOf(".wmv"));
                arrDividedFileName = strTemp.Split('_');

                if (arrDividedFileName.Length == 4) //to check whether the file format is correct or not (e.g. title_subtitle_lecturerName_DateTime)
                {
                   // DateTime datetime = new DateTime();
                    //if(DateTime.TryParse(arrDividedFileName[arrDividedFileName.Length-1], out datetime)) //to check whether the last element is datetime format or not
                    {
                        LectureList p1 = new LectureList()
                        {
                            icon = new BitmapImage(new Uri(@"\Image/미디어아이콘.png", UriKind.Relative)),
                            FileChecked = false,
                            //FileName = System.IO.Path.GetFileName(fileName),
                            LectureTitle = arrDividedFileName[0],
                            LectureModuleTitle = arrDividedFileName[1],
                            LecturerName = arrDividedFileName[2],
                            Count = "0",
                            Date = arrDividedFileName[3],
                            fileDirection = FileDirection + @"\" + fileinfo.Name,
                            isfile = true
                        };
                        tempList.Add(p1);
                    }
                   
                }
                
            }

            _lectureList = new ObservableCollection<LectureList>(tempList.OrderBy(lect=> lect.Date));
            
            DataContext = _lectureList;
            
            Directory_Text.Text = FileDirection;
        }

        #region 최동훈 추가

        public void SetFolderInfo()
        {
            string folderDirection;
            FolderBrowserDialog forderbrowser = new FolderBrowserDialog();
            forderbrowser.ShowDialog();
            if (Directory.Exists(forderbrowser.SelectedPath))
            {
                folderDirection = forderbrowser.SelectedPath;
                Load_File(folderDirection);
            }
        }

        #endregion

        private void Image_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e) // open directory
        {
            #region 최동훈 추가

            SetFolderInfo();

            #endregion
        }

        private void Image_MouseLeftButtonUp_2(object sender, MouseButtonEventArgs e) // play or pause
        {
            if (showMedia.Source != null)
            {
                if (!isPlaying)
                {
                    if (isStopped)
                    {
                        showMedia.Position = TimeSpan.FromSeconds(slider_seek.Minimum);
                        isStopped = false;
                    }
                    isPlaying = true;
                    image_PlayPause.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"\Image/일시정지버튼.png", UriKind.Relative));
                    showMedia.Play();
                }
                else
                {
                    image_PlayPause.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"\Image/임시재생버튼.png", UriKind.Relative));
                    showMedia.Pause();
                    isPlaying = false;
                }
            }
        }

        private void Image_MouseLeftButtonUp_3(object sender, MouseButtonEventArgs e) // stop
        {
            isPlaying = false;
            isStopped = false;
            showMedia.Stop();
            image_PlayPause.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"\Image/임시재생버튼.png", UriKind.Relative));
            showMedia.Position = TimeSpan.FromSeconds(slider_seek.Minimum);
            showMedia.Pause();
        }

        private void Image_MouseLeftButtonUp_4(object sender, MouseButtonEventArgs e) // PreMerge
        {
            if (showMedia.Source != null)
            {
                showMedia.Stop();
                image_PlayPause.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"\Image/임시재생버튼.png", UriKind.Relative));

                Microsoft.Win32.OpenFileDialog ofd;
                ofd = new Microsoft.Win32.OpenFileDialog();
                ofd.AddExtension = true;
                ofd.DefaultExt = "*.wmv";
                ofd.Filter = "Windows Media (*.wmv)|*.wmv";
                Nullable<bool> result = ofd.ShowDialog();

                try
                {
                    if (result == true)
                    {
                        if (File.Exists(ofd.FileName))
                        {
                            Encoder.startClip = slider_seek.Minimum;
                            Encoder.endClip = slider_seek.Maximum;
                            d = new Dialogs();
                            Dialogs.work = 3;
                            d.filename = ofd.FileName;
                            showMedia.Stop();
                            d.ShowDialog();

                            setShowMedia(Encoder.returnFilename);
                            isChanged = true;
                            moveable = true;
                        }
                    }
                }
                catch { new NullReferenceException("Error"); }

            }
        }

        private void Image_MouseLeftButtonUp_5(object sender, MouseButtonEventArgs e) // NextMerge
        {
            if (showMedia.Source != null)
            {
                showMedia.Stop();
                image_PlayPause.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"\Image/임시재생버튼.png", UriKind.Relative));

                Microsoft.Win32.OpenFileDialog ofd;
                ofd = new Microsoft.Win32.OpenFileDialog();
                ofd.AddExtension = true;
                ofd.DefaultExt = "*.wmv";
                ofd.Filter = "Windows Media (*.wmv)|*.wmv";
                Nullable<bool> result = ofd.ShowDialog();

                try
                {
                    if (result == true)
                    {
                        if (File.Exists(ofd.FileName))
                        {
                            Encoder.startClip = slider_seek.Minimum;
                            Encoder.endClip = slider_seek.Maximum;
                            d = new Dialogs();
                            Dialogs.work = 4;
                            d.filename = ofd.FileName;
                            showMedia.Stop();
                            d.ShowDialog();

                            setShowMedia(Encoder.returnFilename);
                            isChanged = true;
                            moveable = true;
                        }
                    }
                }
                catch { new NullReferenceException("Error"); }
            }
        }

        private void Image_MouseLeftButtonUp_6(object sender, MouseButtonEventArgs e) // PreCut
        {
            if (showMedia.Source != null)
            {
                if (slider_seek.Value != slider_seek.Maximum && slider_seek.Value != slider_seek.Minimum)
                {
                    slider_seek.Minimum = slider_seek.Value;
                    showMedia.Stop();
                    showMedia.Position = TimeSpan.FromSeconds(slider_seek.Minimum);
                    isPlaying = false;
                    isChanged = true;
                    image_PlayPause.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"\Image/임시재생버튼.png", UriKind.Relative));
                    moveable = false;

                }

            }
        }



        private void Image_MouseLeftButtonUp_7(object sender, MouseButtonEventArgs e) // NextCut
        {
            if (showMedia.Source != null)
            {
                if (slider_seek.Value != slider_seek.Maximum && slider_seek.Value != slider_seek.Minimum)
                {
                    slider_seek.Maximum = slider_seek.Value;
                    showMedia.Stop();
                    showMedia.Position = TimeSpan.FromSeconds(slider_seek.Minimum);
                    isPlaying = false;
                    isChanged = true;
                    image_PlayPause.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"\Image/임시재생버튼.png", UriKind.Relative));
                    moveable = false;
                }
            }
        }

        private void Image_MouseLeftButtonUp_8(object sender, MouseButtonEventArgs e) // Encode
        {
            if (showMedia.Source != null)
            {
                Encoder.makeThumbnail();
                d = new Dialogs();
                Dialogs.work = 5;
                showMedia.Stop();
                Encoder.startClip = slider_seek.Minimum;
                Encoder.endClip = slider_seek.Maximum;
                image_PlayPause.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"\Image/임시재생버튼.png", UriKind.Relative));

                #region 최동훈 수정 추가

                d.mw2 = this;

                d.ShowDialog();

                Load_File(DataSetting.fileDirection);

                #endregion
            }
        }

        private void Image_MouseLeftButtonUp_9(object sender, MouseButtonEventArgs e) // Save
        {
            if (showMedia.Source != null)
            {
                if (isChanged)
                {
                    Microsoft.Win32.SaveFileDialog sfd;
                    sfd = new Microsoft.Win32.SaveFileDialog();
                    sfd.AddExtension = true;
                    sfd.FileName = System.IO.Path.GetFileName(showMedia.Source.OriginalString);
                    sfd.InitialDirectory = DataSetting.fileDirection;
                    sfd.DefaultExt = "*.wmv";
                    sfd.Filter = "Windows Media(*.wmv)|*.wmv";
                    sfd.CheckPathExists = true;
                    Nullable<bool> result = sfd.ShowDialog();

                    if (result == true)
                    {
                        if (moveable)
                        {
                            Encoder.MoveSave(sfd.FileName);
                        }
                        else
                        {
                            if (!File.Exists(sfd.FileName))
                            {
                                d = new Dialogs();
                                Dialogs.work = 6;
                                showMedia.Stop();
                                Encoder.startClip = slider_seek.Minimum;
                                Encoder.endClip = slider_seek.Maximum;
                                d.filename = sfd.FileName;
                                d.ShowDialog();
                            }
                        }
                        Load_File(System.IO.Path.GetDirectoryName(sfd.FileName));
                        setShowMedia(sfd.FileName);
                        isChanged = false;
                        Encoder.OriginalFilename = sfd.FileName.Substring(0, sfd.FileName.LastIndexOf("."));
                    }
                }
            }
        }

        private void Image_MouseLeftButtonUp_10(object sender, MouseButtonEventArgs e) // Delete
        {
            if (showMedia.Source != null)
            {
                showMedia.Stop();
                isPlaying = false;
                showMedia.Source = null;
                Encoder.removeMedia();
                image_PlayPause.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"\Image/임시재생버튼.png", UriKind.Relative));
                Array.ForEach(Directory.GetFiles(@"d:\tmp\"), File.Delete);
                isChanged = false;
            }
        }

        private void Window_Closed_1(object sender, EventArgs e) // when closed
        {
            if (showMedia.Source != null)
            {
                showMedia.Source = null;
                Encoder.removeMedia();
            }
            Array.ForEach(Directory.GetFiles(@"d:\tmp\"), File.Delete);
        }

        private void lectureSaveList_MouseDoubleClick(object sender, MouseButtonEventArgs e) // list Dclick
        {
            System.Windows.Controls.ListView item = sender as System.Windows.Controls.ListView;
            object obj = item.SelectedItem;
            LectureList ll = obj as LectureList;

            if (ll.isfile)
            {
                showMedia.Stop();
                isPlaying = false;
                isChanged = false;
                moveable = false;
                isStopped = false;

                try
                {
                    setShowMedia(ll.fileDirection);
                    Encoder.OriginalFilename = ll.fileDirection.Substring(0, ll.fileDirection.LastIndexOf("."));
                    showMedia.Pause();
                    showMedia.Stop();
                    image_PlayPause.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"\Image/임시재생버튼.png", UriKind.Relative));
                    Array.ForEach(Directory.GetFiles(@"d:\tmp\"), File.Delete);
                }
                catch { new NullReferenceException("Error"); }
            }
            else
            {
                Load_File(ll.fileDirection);
            }
        }

        private void Image_MouseLeftButtonUp_11(object sender, MouseButtonEventArgs e) // Upload
        {
            if (showMedia.Source != null)
            {
                try
                {
                    showMedia.Stop();

                    int CompanySeq = 0;
                    int MemberSeq = 0;
                    int LectureSeq = 0;
                    int LectureModuleSeq = 0;

                    DataSet dsLectureInfo = new DataSet();

                    //Upload u = new Upload();
                    //u.ShowDialog();

                    #region 최동훈 수정 및 추가

                    int PlayTime = showMedia.NaturalDuration.TimeSpan.Seconds;
                    //int sec = showMedia.NaturalDuration.TimeSpan.Seconds;

                    VCastService vs = new VCastService();
                    CompanySeq = int.Parse(UserInfo._CompanySeq);
                    LectureSeq = int.Parse(UserInfo._SelectedLectureSeq);
                    MemberSeq = int.Parse(UserInfo._MemberSeq);

                    LectureModuleSeq = vs.LectureModuleAdd(LectureSeq, MemberSeq, UserInfo._LectureModuleTitle, PlayTime);

                    Upload u = new Upload(CompanySeq, MemberSeq, LectureSeq, LectureModuleSeq);
                    u.ShowDialog();

                    vs.LectureModuleFinish(LectureSeq, LectureModuleSeq);

                    dsLectureInfo.ReadXml("../../LectureInfo.xml");

                    DataTable dt = dsLectureInfo.Tables[0];
                    DataRow row;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        row = dt.Rows[i];
                        if (row["LectureSeq"] == LectureSeq.ToString() && row["LectureModuleTitle"] == UserInfo._LectureModuleTitle)
                        {
                            row["LectureModuleSeq"] = LectureModuleSeq;
                        }
                    }

                    dt.WriteXml("../../LectureInfo.xml");

                    #endregion

                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("서버로 업로드 중에 오류가 발생 하였습니다.");
                }
            }
        }

        private void Image_MouseLeftButtonUp_12(object sender, MouseButtonEventArgs e) // Parent Directory
        {
            DirectoryInfo parent = Directory.GetParent(Directory_Text.Text);

            if (parent != null)
                Load_File(parent.FullName);
        }


        private void Window_ContentRendered_1(object sender, EventArgs e)
        {
            if (getVideo)
            {
                setShowMedia(Encoder.recFilename);
                Encoder.OriginalFilename = Encoder.recFilename.Substring(0, Encoder.recFilename.LastIndexOf("."));
                showMedia.Pause();
                showMedia.Stop();
                getVideo = false;
            }
        }

    }

    public class LectureList : INotifyPropertyChanged
    {
        public BitmapImage icon { get; set; }
        public string FileName { get; set; }
        public string LectureTitle { get; set; }
        public string LectureModuleTitle { get; set; }
        public string LecturerName { get; set; }
        public string Count { get; set; }
        public string Date { get; set; }
        public string fileDirection { get; set; }
        public bool isfile { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private bool _fileChecked = false;
        public bool FileChecked
        {
            get { return _fileChecked; }
            set
            {
                if (value != _fileChecked)
                {
                    _fileChecked = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("FileChecked"));
                    }
                }
            }
        }



    }





}
