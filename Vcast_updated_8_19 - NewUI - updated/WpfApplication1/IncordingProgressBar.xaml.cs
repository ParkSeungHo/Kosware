using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Dialogs.xaml에 대한 상호 작용 논리
    /// </summary>

    delegate void MyDeli();

    public partial class Dialogs : Window
    {
        public static int work = 0;
        public string filename = null;
        public LectureEditingWindow mw2 { get; set; }

        Thread th = null;
        ThreadStart ts = null;

        public Dialogs()
        {
            InitializeComponent();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {          
            if (work == 3)
            {
                StatusMsg.Content = @"병합중..";

                ts = new ThreadStart(progressChange);
                th = new Thread(ts);

                Encoder.PreMerge(filename);
                th.Start();
            }

            else if (work == 4)
            {
                StatusMsg.Content = @"병합중..";

                ts = new ThreadStart(progressChange);
                th = new Thread(ts);

                Encoder.NextMerge(filename);
                th.Start();
            }

            else if (work == 5)
            {
                StatusMsg.Content = @"인코딩..";

                ts = new ThreadStart(progressChange);
                th = new Thread(ts);

                Encoder.Encode();
                th.Start();
            }

            else if (work == 6)
            {
                StatusMsg.Content = @"저장중..";

                ts = new ThreadStart(progressChange);
                th = new Thread(ts);

                Encoder.Save(filename);
                th.Start();
            }

        }


        private void progressChange()
        {
            if (work == 5)
            {

                while ((Encoder.progress + Encoder.progress2) < 100)
                {
                    encodeProgress.Dispatcher.Invoke(new MyDeli(delegate { encodeProgress.Value = (Encoder.progress + Encoder.progress2) / 2; }));
                    Thread.Sleep(100);
                }
                while ((Encoder.progress3 < 100))
                {
                    encodeProgress.Dispatcher.Invoke(new MyDeli(delegate { encodeProgress.Value = Encoder.progress3 / 2 + 50; }));
                    Thread.Sleep(100);
                }

            }
            else
            {
                while ((Encoder.progress + Encoder.progress2) / 2 < 100)
                {
                    encodeProgress.Dispatcher.Invoke(new MyDeli(delegate { encodeProgress.Value = (Encoder.progress + Encoder.progress2) / 2; }));
                    Thread.Sleep(100);
                }
            }
            encodeProgress.Dispatcher.Invoke(new MyDeli(delegate { encodeProgress.Value = 100; }));

            Encoder.progress = 0;
            Encoder.progress2 = 0;
            Encoder.progress3 = 0;

            while (!Encoder.isCompleted) {
                Thread.Sleep(100);
            }

            Encoder.isCompleted = false;
           // System.Windows.MessageBox.Show("완료");

            this.Dispatcher.Invoke(new MyDeli(delegate { this.Close();}));                
                 
        }

        private void Window_Closed_1(object sender, EventArgs e)
        {
            //encodeProgress.Value = 0;
            //work = 0;
            if (th != null)
                    th.Abort();          
        }

    }
}
