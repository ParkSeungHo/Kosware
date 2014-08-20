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
    /// Interaction logic for ucBackground.xaml
    /// </summary>
    public partial class ucBackground : UserControl
    {
        List<string> liBgImages = null;
        int iCurBgIndex = 0;
        public const string bgImagePath = @"Image/BgImages/";
        public event EventHandler DisplayInfo;

        public ucBackground()
        {
            InitializeComponent();
            liBgImages = new List<string>();
            AddBgImagesToTheList();
            //settingWindow.UpdateBackgroundInfo += settingWindow_UpdateBackgroundInfo;
        }

        private void AddBgImagesToTheList()
        {
            liBgImages.Add("BackGround1.png");
            liBgImages.Add("BackGround2.png");
            liBgImages.Add("BackGround3.jpg");
            liBgImages.Add("BackGround4.jpg");
            liBgImages.Add("BackGround5.jpg");

        }

        /// <summary>
        /// add or subtract 1 from current iCurAvatarIndex
        /// Check 
        /// </summary>
        /// <param name="nav"></param>
        private void NavigateAvatars(EnumCollection.Navigate nav)
        {
            int iNext = 0;
            int iPrev = 0;

            switch (nav)
            {
                case EnumCollection.Navigate.NEXT:
                    {
                        if (++iCurBgIndex > liBgImages.Count - 1)
                        {
                            iCurBgIndex = 0;
                            iPrev = liBgImages.Count - 1;
                        }
                        else
                            iPrev = iCurBgIndex - 1;
                    }
                    break;
                case EnumCollection.Navigate.PREV:
                    {
                        if (--iCurBgIndex < 0)
                        {
                            iCurBgIndex = liBgImages.Count - 1;
                            iNext = 0;
                        }
                        else
                            iNext = iCurBgIndex + 1;
                    }
                    break;
            }

            img_left.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(bgImagePath + liBgImages[iNext], UriKind.RelativeOrAbsolute));
            img_selected.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(bgImagePath + liBgImages[iCurBgIndex], UriKind.RelativeOrAbsolute));
            img_right.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(bgImagePath + liBgImages[iPrev], UriKind.RelativeOrAbsolute));
        }

        private void imgNavi_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            switch (((Image)sender).Name)
            {
                case "img_prev":
                    NavigateAvatars(EnumCollection.Navigate.PREV); break;
                case "img_next":
                    NavigateAvatars(EnumCollection.Navigate.NEXT); break;
            }
        }

        private void img_ok_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //settingWindow.grid_class.Children.Remove(this);

            //Need to add the code that assign the selected avatar. The index will be based on the list index
            UserInfo._Background = (iCurBgIndex % 2);
            UserInfo._BackgroundName = liBgImages[UserInfo._Background].Substring(0, liBgImages[UserInfo._Background].IndexOf('.'));

            if (DisplayInfo != null)
                DisplayInfo(this, null);

            this.Visibility = Visibility.Collapsed;
        }

        /*private void bgImages_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;

            switch (img.Name)
            {
                case "imgBg1": strAvatarBg = "avatarBg";
                    {
                        bgSelect.Margin = img.Margin;
                    }
                    break;
                case "imgBg2": strAvatarBg = "avatarBg2";
                    {
                        bgSelect.Margin = img.Margin;
                    } break;
                case "imgBg3": strAvatarBg = "avatarBg3";
                    {
                        bgSelect.Margin = img.Margin;
                    } break;

            }
            bgSelect.Visibility = Visibility.Visible;
        }*/

    }


}
