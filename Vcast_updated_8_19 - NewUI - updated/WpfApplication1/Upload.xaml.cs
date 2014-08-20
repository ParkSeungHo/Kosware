using System;
using System.Collections.Generic;
using System.IO;
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
using WPFFileUpload.Core;

namespace WpfApplication1
{
    /// <summary>
    /// Upload.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Upload : Window
    {
        private FileCollection _files;
        public string uploadFilename = null;

        int _CompanySeq = 0;
        int _MemberSeq = 0;
        int _LectureSeq = 0;
        int _LectureModuleSeq = 0;


        public Upload()
        {
            Configuration.Instance.UploadHandlerName = "http://www.vcast.co.kr/service/HttpUploadHandler.ashx";//V
            InitializeComponent();

            _files = new FileCollection(Configuration.Instance.CustomParams, Configuration.Instance.MaxUploads, this.Dispatcher);//V

            UploadProgress.DataContext = _files;

            _files.TotalPercentageChanged += _files_TotalPercentageChanged;
            _files.AllFilesFinished += _files_AllFilesFinished;
        }

        public Upload(int CompanySeq, int MemberSeq, int LectureSeq, int LectureModuleSeq)
        {
            this._CompanySeq = CompanySeq;
            this._MemberSeq = MemberSeq;
            this._LectureSeq = LectureSeq;
            this._LectureModuleSeq = LectureModuleSeq;

            Configuration.Instance.UploadHandlerName = "http://www.vcast.co.kr/service/HttpUploadHandler.ashx";//V
            InitializeComponent();

            _files = new FileCollection(Configuration.Instance.CustomParams, Configuration.Instance.MaxUploads, this.Dispatcher);//V

            UploadProgress.DataContext = _files;

            _files.TotalPercentageChanged += _files_TotalPercentageChanged;
            _files.AllFilesFinished += _files_AllFilesFinished;
        }


        private string AddFile(FileInfo file, string filetype)
        {

            if (file.Exists)
            {
                string fileName = file.Name;
                string ext = fileName.Substring(fileName.LastIndexOf('.')).ToLower();

                //Create a new UserFile object
                UserFile userFile = new UserFile();

                // 파일구분
                userFile.FileType = filetype; //L:PC용, M:모바일, T:썸네일
                // 사용자 및 강의정보 설정

                userFile.CompanySeq = _CompanySeq;
                userFile.MemberSeq = _MemberSeq;
                userFile.LectureSeq = _LectureSeq;
                userFile.LectureModuleSeq = _LectureModuleSeq;

                //userFile.CompanySeq = 1000000;
                //userFile.MemberSeq = 2000000;
                //userFile.LectureSeq = 5000000;
                //userFile.LectureModuleSeq = 7000000;

                userFile.OriginalFileName = fileName;
                //userFile.FileName = string.Format("PC_{0}_{1}{2}", DateTime.Now.ToString(),fileName, ext);
                userFile.FileName = string.Format("{0}_{1}_{2}_{3}{4}", userFile.LectureSeq, userFile.LectureModuleSeq, filetype.ToLower(), DateTime.Now.ToFileTime(), ext);
                userFile.FileStream = file.OpenRead();


                //Check for the file size limit (configurable)
                if (userFile.FileStream.Length <= Configuration.Instance.MaxFileSize)
                {
                    //Add to the list
                    _files.Add(userFile);
                    return file.Name;
                    //UploadFiles.UploadFiles();
                    // UploadFiles.Clear();
                }
                else
                {
                    System.Windows.MessageBox.Show("제한된 용량보다 큽니다.");
                    return null;
                }
            }
            else
            {
                return "cannot find file";
            }
        }

        void _files_TotalPercentageChanged(object sender, EventArgs e)
        {
          //  if (_files.Percentage < UploadProgress.Value)
           // {
                UploadProgress.Value = _files.Percentage*100;
          //  }
          //  else
          //  {
                //Storyboard sbProgress = this.Resources["sbProgress"] as Storyboard;
                //(sbProgress.Children[0] as DoubleAnimationUsingKeyFrames).
                //sbProgressFrame.Value = _files.Percentage;
                //sbProgress.Begin();
           // }
        }

        void _files_AllFilesFinished(object sender, EventArgs e)
        {
            _files.Clear();

            string MessageKey = "UploadSuccessMessage";
            ErrorWindow ew = new ErrorWindow(MessageKey);

            ew.Left = 700;
            ew.Top = 400;

            ew.ShowDialog();

            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (_files.Count == 0)
            {
                MessageBox.Show("선택된 파일이 없습니다.");
            }
            else
            {
                _files.UploadFiles();
                
            }
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            string fileName = DataSetting.fileDirection + @"\" + System.IO.Path.GetFileName(Encoder.OriginalFilename) + "P.mp4";
            FileInfo file = new FileInfo(fileName);
            fileNameL.Content = AddFile(file, "L");
            fileName = DataSetting.fileDirection + @"\" + System.IO.Path.GetFileName(Encoder.OriginalFilename) + "M.mp4";
            file = new FileInfo(fileName);
            fileNameM.Content = AddFile(file, "M");
            fileName = DataSetting.fileDirection + @"\" + System.IO.Path.GetFileName(Encoder.OriginalFilename) + "Thumb.jpg";
            file = new FileInfo(fileName);
            fileNameT.Content = AddFile(file, "T");
                        
            #region 최동훈 추가
            
            //bool bSuccess = false;

            //if (_files.Count > 0)
            //    _files.UploadFiles();

            //while (!bSuccess)
            //{
            //    if (UploadProgress.Value == 100)
            //        bSuccess = true;

            //    System.Threading.Thread.Sleep(100);
            //}

            //if (bSuccess)
            //{
            //    string MessageKey = "UploadErrorMessage";
            //    ErrorWindow ew = new ErrorWindow(MessageKey);

            //    ew.Left = 700;
            //    ew.Top = 400;

            //    ew.ShowDialog();

            //    this.Close();
            //}

            #endregion
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            _files.Clear();
            this.Close();
        }
    }
}
