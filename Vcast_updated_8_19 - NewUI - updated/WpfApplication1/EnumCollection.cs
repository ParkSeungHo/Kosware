using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    public static class EnumCollection
    {
        public enum PlayerMode
        {
            REAL_MODE = 0, ONE_PLAYER = 1, TWO_PLAYERS = 2
        }

        public enum MaterialMode
        {
            PPT = 0, VIDEO = 1, WEB = 2
        }

        public enum ContentAlignment
        {
            LEFT = 0, CENTRE = 1, RIGHT = 2
        }

        public enum WebContents
        {
            GOOGLE = 0, NAVER = 1, DAUM = 2
        }

        public enum Navigate
        {
            PREV = 0, NEXT = 1
        }
    }
}
