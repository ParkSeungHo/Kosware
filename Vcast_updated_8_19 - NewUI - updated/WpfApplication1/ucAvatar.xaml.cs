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
    /// Interaction logic for ucAvatar.xaml
    /// </summary>
    public partial class ucAvatar : UserControl
    {

        public event EventHandler DisplayInfo;

        List<string> liAvatarImages = null;
        List<string> liAvatarDescription = null;
        //int avatar_Num = 0;
        int iCurAvatarIndex = 0;
        public const string strMainImagePath = @"Image/UIImage/AvatarBackground/";
        public const string avatarImagePath = @"Image/Avatars/"
            ;
        public ucAvatar()
        {
            InitializeComponent();
            liAvatarImages = new List<string>();
            liAvatarDescription = new List<string>();
            AddAvatarImagesAndDescriptions();

        }

        private void AddAvatarImagesAndDescriptions()
        {
            //TODO: read avatar file naames from the avatar folder and add to the list

            liAvatarImages.Add("yuni.png");
            liAvatarImages.Add("wind.png");
            liAvatarImages.Add("mark.png");
            liAvatarImages.Add("iron.png");
            liAvatarImages.Add("man.png");


            liAvatarDescription.Add("avatarbg2.png");
            liAvatarDescription.Add("avatarbgwind.png");
            liAvatarDescription.Add("avatarbg1.png");
            liAvatarDescription.Add("avatarbgiron.png");
            liAvatarDescription.Add("avatarbg1.png");
        }


        /// <summary>
        /// add or subtract 1 from current iCurAvatarIndex
        /// Check 
        /// </summary>
        /// <param name="nav"></param>
        private void NavigateAvatars(EnumCollection.Navigate nav)
        {
            switch(nav)
            {
                case EnumCollection.Navigate.NEXT:
                    {
                        if(++iCurAvatarIndex > liAvatarImages.Count - 1)
                            iCurAvatarIndex = 0;
                    }
                    break;
                case EnumCollection.Navigate.PREV:
                    {
                        if(--iCurAvatarIndex < 0)
                            iCurAvatarIndex = liAvatarImages.Count -1;
                    }
                    break;
            }
            img_Avatar.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(avatarImagePath + liAvatarImages[iCurAvatarIndex], UriKind.RelativeOrAbsolute));
            img_description.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(avatarImagePath + liAvatarDescription[iCurAvatarIndex], UriKind.RelativeOrAbsolute));
        }

        private void imgNavi_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            switch(((Image)sender).Name)
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

            //TODO: change the type of _FirstAvatar & _SecondAvatar to "string"
            if (UserInfo._AvatarMode == (int)EnumCollection.PlayerMode.TWO_PLAYERS) //2: two players
            {
                UserInfo._FirstAvatar = 0;
                UserInfo._FirstAvatarName = liAvatarImages[UserInfo._FirstAvatar].Substring(0, liAvatarImages[UserInfo._FirstAvatar].IndexOf('.'));
                UserInfo._SecondAvatar = 1;
                UserInfo._SecondAvatarName = liAvatarImages[UserInfo._SecondAvatar].Substring(0, liAvatarImages[UserInfo._SecondAvatar].IndexOf('.'));
            }
            else
            {
                if(iCurAvatarIndex < 3)
                    UserInfo._FirstAvatar = iCurAvatarIndex; //two avatars are available at the moment
                else
                    UserInfo._FirstAvatar = iCurAvatarIndex % 2; //two avatars are available at the moment

                UserInfo._FirstAvatarName = liAvatarImages[UserInfo._FirstAvatar].Substring(0, liAvatarImages[UserInfo._FirstAvatar].IndexOf('.'));
            }

            if (DisplayInfo != null)
                DisplayInfo(this, null);
          
            this.Visibility = Visibility.Collapsed;

        }
        
#region ToBeDeleted
        //void settingWindow_UpdateAvatarInfo(object sender, EventArgs e)
        //{
        //    if (UserInfo._AvatarMode == 0) //PlayerMode = 1p
        //        UserInfo._FirstAvatar = avatar_Num;
        //    else if (UserInfo._AvatarMode == 1) //PlayerMode = 2p
        //    {
        //        UserInfo._FirstAvatar = avatar_Num;
        //        UserInfo._SecondAvatar = avatar_Num + 1;
        //    }
        //}

        //private void avatarImg1_MouseEnter(object sender, MouseEventArgs e)
        //{
        //    if (avatarImg1.LoadedBehavior != MediaState.Manual)
        //        avatarImg1.LoadedBehavior = MediaState.Manual;

        //    avatarImg1.Play();
        //}

        //private void avatarImg1_MouseLeave(object sender, MouseEventArgs e)
        //{
        //    avatarImg1.Stop();
        //}

        //private void avatarImg1_MediaEnded(object sender, RoutedEventArgs e)
        //{
        //    if (avatarImg1.LoadedBehavior != MediaState.Manual)
        //        avatarImg1.LoadedBehavior = MediaState.Manual;

        //    avatarImg1.Stop();
        //    avatarImg1.Play();
        //}

        //private void avatarImg1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    avatarSelect.Margin = avatarImg1.Margin;
        //    avatar_Num = 1; // 여자아바타
        //}

        //private void avatarImg2_MouseEnter(object sender, MouseEventArgs e)
        //{
        //    if (avatarImg2.LoadedBehavior != MediaState.Manual)
        //        avatarImg2.LoadedBehavior = MediaState.Manual;

        //    avatarImg2.Play();
        //}

        //private void avatarImg2_MouseLeave(object sender, MouseEventArgs e)
        //{
        //    avatarImg2.Stop();
        //}

        //private void avatarImg2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    avatarSelect.Margin = avatarImg2.Margin;
        //    avatar_Num = 2; // 남자아바타
        //}

        //private void avatarImg2_MediaEnded(object sender, RoutedEventArgs e)
        //{
        //    if (avatarImg2.LoadedBehavior != MediaState.Manual)
        //        avatarImg2.LoadedBehavior = MediaState.Manual;

        //    avatarImg2.Stop();
        //    avatarImg2.Play();
        //}

        /*private void avatarImg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;

            switch (img.Name)
            {
                case "avatarImg1": avatar_Num = 1;
                    {
                        avatarSelect.Margin = img.Margin;
                    }
                    break;
                case "avatarImg2": avatar_Num = 2;
                    {
                        avatarSelect.Margin = img.Margin;
                    } break;
                case "avatarImg3": avatar_Num = 3;
                    {
                        avatarSelect.Margin = img.Margin;
                    } break;
                case "avatarImg5": avatar_Num = 5;
                    {
                        avatarSelect.Margin = img.Margin;
                    } break;
                case "avatarImg6": avatar_Num = 6;
                    {
                        avatarSelect.Margin = img.Margin;
                    }
                    break;
            }
            avatarSelect.Visibility = Visibility.Visible;
        }*/
#endregion

        
    }
}
