using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    public static class UserInfo
    {

        public static string _CompanySeq = string.Empty;
        public static string _MemberSeq = string.Empty;
        public static string _MemberID = string.Empty;
        public static string _MemberName = string.Empty;
        public static string _SelectedLectureSeq = "5000002";
        public static string _LectureSeq = "5000002";
        public static string _LectureTitle = string.Empty;
        public static string _LectureModuleTitle = string.Empty;
        public static int _Background = 1;
        public static string _BackgroundName = string.Empty;
        public static int _FirstAvatar = 0;
        public static string _FirstAvatarName = string.Empty;
        public static int _SecondAvatar = 0;
        public static string _SecondAvatarName = string.Empty;
        public static string _PlayTime = string.Empty;
        public static string _EncodingResolution = string.Empty;
        public static string _MaterialFileName = string.Empty;
        public static string _LectureModuleSeq = string.Empty;
        public static string _WebLink = string.Empty;
        //public static int _AuthoringMode = 0; //0: AvatarMode 1:RealMode
        public static int _MaterialMode = 0; //0: PPT 1:video 2:Web
        public static int _AvatarMode = 0; //0:realMode 1:1p  2:2p
        public static int _ContentAlignmentMode = 0; //0:Left   1:Center   2:Right
        public static bool isSaved = false;

        /*public UserInfo(string CompanySeq, string MemberSeq, string MemberID, string MemberName)
        {
            this._CompanySeq = CompanySeq;
            this._MemberSeq = MemberSeq;
            this._MemberID = MemberID;
            this._MemberName = MemberName;
        }

        public string CompanySeq
        {
            get { return _CompanySeq; }
        }

        public string MemberSeq
        {
            get { return _MemberSeq; }
        }

        public string MemberID
        {
            get { return _MemberID; }
        }

        public string MemberName
        {
            get { return _MemberName; }
        }

        public string SelectedLectureSeq
        {
            get { return _SelectedLectureSeq; }
            set { _SelectedLectureSeq = value; }
        }

        public string LectureModuleTitle
        {
            get { return _LectureModuleTitle; }
            set { _LectureModuleTitle = value; }
        }*/
    }

}
