using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Microsoft.Expression.Encoder;
using Microsoft.Expression.Encoder.ScreenCapture;
using Microsoft.Expression.Encoder.Devices;
using Microsoft.Expression.Encoder.Profiles;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Threading;
using System.IO;

using System.ComponentModel;
namespace WpfApplication1
{
    class Encoder
    {
        static MediaItem mainMedia = null;
        static ScreenCaptureJob captureJob = new ScreenCaptureJob();
        static Collection<EncoderDevice> audioDevices = EncoderDevices.FindDevices(EncoderDeviceType.Audio);
        static public bool ThreadExists= false;
        static Thread capturescreen;

        #region 추가사항

        static public double progress = 0;
        static public double progress2 = 0;
        static public double progress3 = 0;
        static public string returnFilename = null;
        static public bool isCompleted = false;
        static public string OriginalFilename = null;
        static public string recFilename = null;
        static public double startClip = 0;
        static public double endClip = 0;


        #endregion

        static public void RunbgThread()
        { 
            if (captureJob.Status == RecordStatus.NotStarted)
            {
                capturescreen = new Thread(new ThreadStart(CaptureScreenRecording));
                capturescreen.Priority = ThreadPriority.Highest;
                capturescreen.Start();
            }
            else if (captureJob.Status == RecordStatus.Paused)
            {
                captureJob.Resume();
            }
        }

        static public void KillThread()
        {
            if (capturescreen != null)
                capturescreen.Abort();
        }

        static public void CaptureScreenRecording()
        {
           
                string filename = null;
                string fileDirection;
                captureJob = new ScreenCaptureJob();
//                captureJob.CaptureRectangle = new Rectangle(0, 0,SystemInformation.VirtualScreen.Width, SystemInformation.VirtualScreen.Height);
                captureJob.CaptureRectangle = new Rectangle(0, 0, 1920, SystemInformation.VirtualScreen.Height);
                captureJob.ScreenCaptureVideoProfile.Quality = 50;
                captureJob.ScreenCaptureVideoProfile.Bitrate = new ConstantBitrate(12000);
                captureJob.ScreenCaptureVideoProfile.FrameRate = 24.0;

                try
                {
                    captureJob.AddAudioDeviceSource(audioDevices[0]);
                }
                catch
                {
                    new ScreenCaptureAudioException();
                    // job.AddAudioDeviceSource(audioDevices[2]);
                }

                //string[] arrDividedFileName = strFileName.Split('#');

                //for (int i = 0; i < arrDividedFileName.Length; ++i)
                //{
                //    filename += arrDividedFileName[i];
                //    filename += "_";

                //}

                if (UserInfo._LectureTitle != String.Empty && UserInfo._LectureModuleTitle != String.Empty && UserInfo._MemberName != String.Empty)
                {
                    DateTime dtVideoRecording = DateTime.Now;
                    filename = UserInfo._LectureTitle + "_" + UserInfo._LectureModuleTitle + "_" + UserInfo._MemberName + "_" + dtVideoRecording.ToString("yyyy-MM-dd-HH-mm") + ".wmv";
                }

                fileDirection = @"D:\UserList\" + filename;
                captureJob.CaptureMouseCursor = false;
                captureJob.OutputScreenCaptureFileName = fileDirection;
                recFilename = fileDirection;
                captureJob.Start();

        }

        #region 추가사항

        //static public void CaptureScreenRecordingThread()
        //{
        //    string filename;
        //    string fileDirection;

        //    captureJob = new ScreenCaptureJob();
        //    captureJob.CaptureRectangle = new Rectangle(0, 0, SystemInformation.VirtualScreen.Width, SystemInformation.VirtualScreen.Height);
        //    captureJob.ScreenCaptureVideoProfile.Quality = 100;
        //    captureJob.ScreenCaptureVideoProfile.Bitrate = new ConstantBitrate(6000);
        //    captureJob.ScreenCaptureVideoProfile.FrameRate = 24.0;

        //    try
        //    {

        //        captureJob.AddAudioDeviceSource(audioDevices[2]);
        //    }
        //    catch
        //    {
        //        //new ScreenCaptureException("Error");
        //        // captureJob.AddAudioDeviceSource(audioDevices[2]);
        //    }

        //    filename = DateTime.Now.ToString("yyyy.MM.dd-HH.mm.ss") + ".wmv";
        //    fileDirection = DataSetting.fileDirection + @"\" + filename;
        //    captureJob.CaptureMouseCursor = false;
        //    captureJob.OutputScreenCaptureFileName = fileDirection;
        //    captureJob.Start();

        //}

        #endregion
        
        static public void pauseRecording() 
        {
            captureJob.Pause();
        }

        static public void CaptureCompleted()
        {
            if (captureJob.Status != RecordStatus.NotStarted)
            {
                captureJob.Stop();
                //captureJob = new ScreenCaptureJob();
            }
        }

        static public void setMedia(string fileDirection)
        {
            mainMedia = new MediaItem(fileDirection);
        }

        static public void removeMedia()
        {
            mainMedia = null;
        }

 

        static public void PreMerge(string fileDirection)
        {
            Thread thread = new Thread(() => PreMergeThread(fileDirection));
            thread.Start();
        }

        #region 추가사항

        static public void PreMergeThread(string fileDirection)
        {
            Job mergePreJob = new Job();
            string mergeFilename = @"mP" + Path.GetFileName(mainMedia.SourceFileName);
            MediaItem merged = new MediaItem(fileDirection);
            Source mainSource = new Source(mainMedia.MainMediaFile);
            mainSource.Clips[0].StartTime = TimeSpan.FromSeconds(startClip);
            mainSource.Clips[0].EndTime = TimeSpan.FromSeconds(endClip);
            merged.Sources.Add(mainSource);

            mergePreJob.MediaItems.Add(merged);
            mergePreJob.OutputDirectory = @"d:\tmp\";
            mergePreJob.DefaultMediaOutputFileName = mergeFilename;
            mergePreJob.CreateSubfolder = false;
            mergePreJob.ApplyPreset(Presets.VC1HD1080pVBR);

            mergePreJob.EncodeProgress += new EventHandler<EncodeProgressEventArgs>(OnProgress);
            mergePreJob.EncodeCompleted += new EventHandler<EncodeCompletedEventArgs>(OnComleted);

            returnFilename = @"d:\tmp\" + mergeFilename;

            mergePreJob.Encode();

        }

        #endregion

        static public void NextMerge(string fileDirection)
        {
            Thread thread = new Thread(() => NextMergeThread(fileDirection));
            thread.Start();
        }

        #region 추가사항

        static public void NextMergeThread(string fileDirection)
        {
            Job mergeNextJob = new Job();
            string mergeFilename = @"mN" + Path.GetFileName(mainMedia.SourceFileName);
            MediaItem merged = new MediaItem(mainMedia.MainMediaFile);
            Source mergeSource = new Source(fileDirection);
            merged.Sources[0].Clips[0].StartTime = TimeSpan.FromSeconds(startClip);
            merged.Sources[0].Clips[0].EndTime = TimeSpan.FromSeconds(endClip);
            merged.Sources.Add(mergeSource);

            mergeNextJob.MediaItems.Add(merged);
            mergeNextJob.OutputDirectory = @"d:\tmp\";
            mergeNextJob.DefaultMediaOutputFileName = mergeFilename;
            mergeNextJob.CreateSubfolder = false;
            mergeNextJob.ApplyPreset(Presets.VC1HD1080pVBR);

            mergeNextJob.EncodeProgress += new EventHandler<EncodeProgressEventArgs>(OnProgress);
            mergeNextJob.EncodeCompleted += new EventHandler<EncodeCompletedEventArgs>(OnComleted);

            returnFilename = @"d:\tmp\" + mergeFilename;

            mergeNextJob.Encode();


        }

        #endregion

        static public void MoveSave(string fileName)
        {
            File.Copy(mainMedia.SourceFileName, fileName, true);
        }

        static public void Save(string fileDirection)
        {
            Thread thread = new Thread(() => SaveThread(fileDirection));
            thread.Start();
        }

        static public void SaveThread(string fileDirection)
        {
            Job SaveJob = new Job();
            string fileName = Path.GetFileName(fileDirection);
            MediaItem media = new MediaItem(mainMedia.SourceFileName);
            media.Sources[0].Clips[0].StartTime = TimeSpan.FromSeconds(startClip);
            media.Sources[0].Clips[0].EndTime = TimeSpan.FromSeconds(endClip);
            SaveJob.MediaItems.Add(media);
            SaveJob.ApplyPreset(Presets.VC1HD1080pVBR);
            SaveJob.OutputDirectory = Path.GetDirectoryName(fileDirection);
            SaveJob.DefaultMediaOutputFileName = fileName;
            SaveJob.CreateSubfolder = false;

            SaveJob.EncodeProgress += new EventHandler<EncodeProgressEventArgs>(OnProgress);
            SaveJob.EncodeCompleted += new EventHandler<EncodeCompletedEventArgs>(OnComleted);

            SaveJob.Encode();

        }

        static public void makeThumbnail()
        {
            using (Bitmap bitmap = mainMedia.MainMediaFile.GetThumbnail(new TimeSpan(0, 0, 5), new System.Drawing.Size(640, 480)))
            {
                bitmap.Save(DataSetting.fileDirection + @"\" + Path.GetFileName(OriginalFilename) + "Thumb.jpg");
            }
        }

        static public void Encode() 
        {
            Thread thread = new Thread(() => EncodeThread());
            thread.Start();

        }

        static private void EncodeThread()
        {
            Job EncodeJobP = new Job();
            Job EncodeJobM = new Job();

            string PfileName = Path.GetFileName(OriginalFilename) + "P.mp4";
            string MfileName = Path.GetFileName(OriginalFilename) + "M.mp4";

            MediaItem mediaP = new MediaItem(mainMedia.SourceFileName);
            MediaItem mediaM = new MediaItem(mainMedia.SourceFileName);
            mediaP.Sources[0].Clips[0].StartTime = TimeSpan.FromSeconds(startClip);
            mediaP.Sources[0].Clips[0].EndTime = TimeSpan.FromSeconds(endClip);
            mediaM.Sources[0].Clips[0].StartTime = TimeSpan.FromSeconds(startClip);
            mediaM.Sources[0].Clips[0].EndTime = TimeSpan.FromSeconds(endClip);

            EncodeJobP.MediaItems.Add(mediaP);
            EncodeJobM.MediaItems.Add(mediaM);

            //EncodeJobP.ApplyPreset(Presets.H264HD1080pVBR);
            EncodeJobP.ApplyPreset(Presets.H264HD1080pVBR);
            EncodeJobM.ApplyPreset(Presets.H264iPhoneiPodTouch);

            EncodeJobP.OutputDirectory = DataSetting.fileDirection;
            EncodeJobM.OutputDirectory = DataSetting.fileDirection;

            EncodeJobP.DefaultMediaOutputFileName = PfileName;
            EncodeJobM.DefaultMediaOutputFileName = MfileName;

            EncodeJobP.CreateSubfolder = false;
            EncodeJobM.CreateSubfolder = false;

            EncodeJobP.EncodeProgress += new EventHandler<EncodeProgressEventArgs>(OnProgressForP);
            EncodeJobP.EncodeCompleted += new EventHandler<EncodeCompletedEventArgs>(OnComletedForP);

            EncodeJobM.EncodeProgress += new EventHandler<EncodeProgressEventArgs>(OnProgressForM);
            EncodeJobM.EncodeCompleted += new EventHandler<EncodeCompletedEventArgs>(OnComletedForM);


            EncodeJobP.Encode();
            EncodeJobM.Encode();
        }


        static void OnProgress(object sender, EncodeProgressEventArgs e)
        {
            if (progress < 100)
                progress = e.Progress;
            else
                progress2 = e.Progress;
        }
        static void OnProgressForP(object sender, EncodeProgressEventArgs e)
        {
            if (progress < 50)
                progress = e.Progress / 2;
            else
                progress2 = e.Progress / 2;
        }

        static void OnProgressForM(object sender, EncodeProgressEventArgs e)
        {
            progress3 = e.Progress;
        }

        static void OnComletedForP(object sender, EncodeCompletedEventArgs e)
        {
            // isCompleted = true;
        }

        static void OnComletedForM(object sender, EncodeCompletedEventArgs e)
        {
            isCompleted = true;
        }

        static void OnComleted(object sender, EncodeCompletedEventArgs e)
        {
            isCompleted = true;
        }
        static public void Encoder_Dispose()
        {

            captureJob.Dispose();
        }
    }
}
