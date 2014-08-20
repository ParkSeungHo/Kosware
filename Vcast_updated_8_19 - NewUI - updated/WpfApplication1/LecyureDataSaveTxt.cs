using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

using System.Diagnostics;

namespace WpfApplication1
{
    class LecyureDataSaveTxt
    {

        private const string absolute_pach = @"\twoPlayers_Data\setting.txt";

        private int Avata_count = -1;
        private int[] Avata_Play = null;
        private int background_Num = -1;
        private int lecyure_type = 1; 
        private int ppt_Page = -1;
        private string video_Path = "";

        private int index = 0;
        /// <summary>
        /// 김혁주
        /// setting된 데이터를 text로 저장하기 위해 string으로 반환함
        /// </summary>
        /// <returns></returns>
        private string AllData_String()
        {
            string return_data = null;

            return_data = Avata_count.ToString() + '`';
            for (int i = 0; i < Avata_count; i++)
            {
                return_data += Avata_Play[i].ToString() + '`';
            }
            return_data += background_Num.ToString() + '`';
            return_data += lecyure_type.ToString() + '`';
            return_data += ppt_Page.ToString() + '`';
            return_data += video_Path + '`';
            return return_data;
        }
        /// <summary>
        /// 스트링 값을 받아 txt파일로 absolute_pach 위치에 저장
        /// </summary>
        /// <param name="save_text"></param>
        private void Lecyure_WritingText(string save_text)
        {
            System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + absolute_pach, save_text);
        }

       

        public void Save_txt()
        {
            Lecyure_WritingText(AllData_String());
        }

        public void Reset()
        {
              Avata_count = -1;
              Avata_Play = null;
              background_Num = -1;
              lecyure_type = 1; 
              ppt_Page = -1;
              video_Path = "";

              index = 0;
        }

        public int[] Get_Avata_Plays()
        {
            return Avata_Play;
        }
        public int Get_Avata_count()
        {
            return Avata_count;
        }
        public int Get_BackGround_Num()
        {
            return background_Num;
        }
        public int Get_PPTpage_Num()
        {
            return ppt_Page;
        }

        public void Set_Avata_count(int ac)
        {
            Avata_count = ac;
            Avata_Play = new int[Avata_count+1];
        }
        public void Add_Avata_Play(int an)
        {
                Avata_Play[index++] = an;
        }
        public void Set_BackGround_Num(int bn)
        {
            background_Num = bn;
        }
        public void Set_PPTpage_Num(int pn)
        {
            ppt_Page = pn;
        }
        public void Set_Video_Pach(string vp)
        {
            video_Path.Remove(0);
            video_Path = vp;
        }
        public void Set_lecyure_type(int lt)
        {
            lecyure_type = lt;
        }
    }

    
}
