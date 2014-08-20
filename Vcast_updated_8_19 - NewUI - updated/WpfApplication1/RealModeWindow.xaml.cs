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
using System.IO;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using Microsoft.Kinect;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for RealModeWindow.xaml
    /// </summary>
    public partial class RealModeWindow : Window, INotifyPropertyChanged
    {
        /// <summary>
        /// Size of the RGB pixel in the bitmap
        /// </summary>
        private readonly uint bytesPerPixel = 0;

        /// <summary>
        /// Active Kinect sensor
        /// </summary>
        private KinectSensor kinectSensor = null;

        /// <summary>
        /// Reader for color frames
        /// </summary>
        private ColorFrameReader colorFrameReader = null;

        /// <summary>
        /// Bitmap to display
        /// </summary>
        private WriteableBitmap colorBitmap = null;

        /// <summary>
        /// Intermediate storage for receiving frame data from the sensor
        /// </summary>
        private byte[] colorPixels = null;

        /// <summary>
        /// Current status text to display
        /// </summary>
        private string statusText = null;

        private ucPPTWinForm ppt = null;
        private ucAVIWinForm avi = null;
        private RecordWindow rw = null;
        private LectureEditingWindow mw_vedit;
        private LoadingWindow lowin = null;
        private ucWebForm web = null;

        private bool isFirstTime = true;


        public RealModeWindow()
        {
            // get the kinectSensor object
            this.kinectSensor = KinectSensor.GetDefault();

            // open the reader for the color frames
            this.colorFrameReader = this.kinectSensor.ColorFrameSource.OpenReader();

            // wire handler for frame arrival
            this.colorFrameReader.FrameArrived += this.Reader_ColorFrameArrived;

            // create the colorFrameDescription from the ColorFrameSource using Bgra format
            FrameDescription colorFrameDescription = this.kinectSensor.ColorFrameSource.CreateFrameDescription(ColorImageFormat.Bgra);

            // rgba is 4 bytes per pixel
            this.bytesPerPixel = colorFrameDescription.BytesPerPixel;

            // allocate space to put the pixels to be rendered
            this.colorPixels = new byte[colorFrameDescription.Width * colorFrameDescription.Height * this.bytesPerPixel];

            // create the bitmap to display
            this.colorBitmap = new WriteableBitmap(colorFrameDescription.Width, colorFrameDescription.Height, 96.0, 96.0, PixelFormats.Bgr32, null);

            // set IsAvailableChanged event notifier
            //this.kinectSensor.IsAvailableChanged += this.Sensor_IsAvailableChanged;

            // open the sensor
            this.kinectSensor.Open();

            // set the status text
            //this.StatusText = this.kinectSensor.IsAvailable ? Properties.Resources.RunningStatusText
            //: Properties.Resources.NoSensorStatusText;

            // use the window object as the view model in this simple example
            this.DataContext = this;

            // initialize the components (controls) of the window
            InitializeComponent();

            CloseAll.caRM = this;

            //if (pptPath != "")
            //    SetPPT(pptPath);
            //if (aviPath != "")
            //    avi = new ucAVIWinForm();

            //switch (UserInfo._MaterialMode)
            //{
            //    case 0: ppt = new ucPPTWinForm(); break;
            //    case 1: avi = new ucAVIWinForm(); break;
            //    case 2: web = new ucWebForm(); break;
            //}

            //switch (UserInfo._ContentAlignmentMode)
            //{
            //    case 1:
            //        windowsFormsHost2.Margin = new Thickness() { Left = 480, Top = 130 };
            //        break;
            //    case 2:
            //        windowsFormsHost2.Margin = new Thickness() { Left = 40, Top = 130 };
            //        break;
            //    case 3:
            //        windowsFormsHost2.Margin = new Thickness() { Left = 920, Top = 130 };
            //        break;
            //}

        }

        public void ShowRecordWindow()
        {
            if (rw == null)
            {
                rw = new RecordWindow(null, this);
                rw.ShowDialog();
            }
            else
            {
                rw.Visibility = Visibility.Visible;
                rw.Focus();
            }
        }

        //public void SetPPT(string pptPathAndTitle)
        //{
        //    ppt = new ucPPTWinForm();
        //    ppt.pptAction = pptPathAndTitle;
        //}

        public void ResetAll()
        {
            this.Visibility = Visibility.Hidden;
            if (ppt != null)
                ppt.Reset();

        }

        /// <summary>
        /// INotifyPropertyChangedPropertyChanged event to allow window controls to bind to changeable data
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets the bitmap to display
        /// </summary>
        public ImageSource ImageSource
        {
            get
            {
                return this.colorBitmap;
            }
        }

        /// <summary>
        /// Execute shutdown tasks
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {

        }



        /// <summary>
        /// Handles the color frame data arriving from the sensor
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void Reader_ColorFrameArrived(object sender, ColorFrameArrivedEventArgs e)
        {
            bool colorFrameProcessed = false;

            // ColorFrame is IDisposable
            using (ColorFrame colorFrame = e.FrameReference.AcquireFrame())
            {
                if (colorFrame != null)
                {
                    FrameDescription colorFrameDescription = colorFrame.FrameDescription;

                    // verify data and write the new color frame data to the display bitmap
                    if ((colorFrameDescription.Width == this.colorBitmap.PixelWidth) && (colorFrameDescription.Height == this.colorBitmap.PixelHeight))
                    {
                        if (colorFrame.RawColorImageFormat == ColorImageFormat.Bgra)
                        {
                            colorFrame.CopyRawFrameDataToArray(this.colorPixels);
                        }
                        else
                        {
                            colorFrame.CopyConvertedFrameDataToArray(this.colorPixels, ColorImageFormat.Bgra);
                        }

                        colorFrameProcessed = true;
                    }
                }
            }

            // we got a frame, render
            if (colorFrameProcessed)
            {
                this.RenderColorPixels();
            }
        }

        /// <summary>
        /// Renders color pixels into the writeableBitmap.
        /// </summary>
        private void RenderColorPixels()
        {
            this.colorBitmap.WritePixels(
                new Int32Rect(0, 0, this.colorBitmap.PixelWidth, this.colorBitmap.PixelHeight),
                this.colorPixels,
                this.colorBitmap.PixelWidth * (int)this.bytesPerPixel,
                0);
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            lowin = new LoadingWindow();
            CloseAll.caLoWin = lowin;
            lowin.Visibility = Visibility.Hidden;
            lowin.LoadPPTAndAvatar += lowin_LoadPPTAndAvatar;
            ShowRecordWindow();
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

            
            //if (pptPath != "")
            //    windowsFormsHost2.Child = ppt;
            //else if (aviPath != "")
            //{
            //    windowsFormsHost2.Child = avi;
            //    avi.PlayFile(aviPath);
            //}


            CloseAll.caRW.ticktok.Start();
            CloseAll.caLoWin.Visibility = Visibility.Hidden;
            Encoder.RunbgThread();


            this.Focus();

        }

        void rw_UnloadMainForm(object sender, EventArgs e)
        {
            mw_vedit = new LectureEditingWindow();
            mw_vedit.Show();
        }

        private void Window_KeyDown_1(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //this.Visibility = Visibility.Hidden;
            switch (e.Key)
            {

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
                    if (CloseAll.caRM != null)
                        CloseAll.caRM.Close();

                    this.Close();

                    if (ppt != null)
                        ppt.Reset();
                    rw.ticktok.Stop();
                    break;
            }
        }

        private void imgClose_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            rw.Visibility = Visibility.Visible;
            rw.ticktok.Stop();
            rw.image_play.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"\Image/일시정지버튼.png", UriKind.Relative));

            if (this.colorFrameReader != null)
            {
                // ColorFrameReder is IDisposable
                this.colorFrameReader.Dispose();
                this.colorFrameReader = null;
            }

            if (this.kinectSensor != null)
            {
                this.kinectSensor.Close();
                this.kinectSensor = null;
            }

            Encoder.pauseRecording();
            rw.isRecording = false;
        }
    }
}
