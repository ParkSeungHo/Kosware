using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace WpfApplication1
{
    /// <summary>
    /// 소켓통신을 위해 필요한 data를 관리하기 위해 작성
    /// </summary>
    class soket_data
    {
        public int avata_Num = -1;
        public int background_Num = -1;
        public int ppt_Page = -1;
        public string video_Path = "";

        //void soket_data(int an, int bn, int pp, string vp)
        //{
        //    avta_Num = an;
        //    background_Num = bn;
        //    ppt_Page = pp;
        //    video_Path = vp;
        //}
        /// <summary>
        /// 저장된 데이터를 전송하기 쉽게 byte[]로 형변환 시켜주는 함수
        /// </summary>
        /// <returns></returns>
        public Byte[] Get_baye()
        {
            string return_data = null;
            return_data = avata_Num.ToString() + '`';
            return_data += background_Num.ToString() + '`';
            return_data += ppt_Page.ToString() + '`';
            return_data += video_Path + '`';
            Byte[] rB = Encoding.UTF8.GetBytes(return_data);
            return rB;
        }
    }

    class soket
    {
        private IPEndPoint ipep = null;
        private Socket server = null;
        private Socket client = null;
        private Byte[] sendByte = null;
        private Byte[] receiveByte = new Byte[512];
        Thread mThread = null;
        Thread _mThread = null;
        public SocketPolicyServer _policyServer = null;
        public string d;

        /// <summary>
        /// <mafer>김혁주</mafer>
        /// Server_Thread라는 함수를 스레드로 Start 시킴
        /// _policyServer는 유니티에서 재공하는 소스로 소켓통신에 필요하다는 팁을 보고 추가함
        /// </summary>
        public void Start_Sokec()
        {
            // 유니티와 소켓 통신을 하기 위해서는 해당 소스를 추가하고 start 해줘야 한다고 함.
            _policyServer = new SocketPolicyServer(SocketPolicyServer.AllPolicy);
            _policyServer.Start();


            mThread = new Thread(new ThreadStart(Server_Thread));
            mThread.Start();
        }
        /// <summary>
        /// 서버를 만드는 함수 ip는 any, 포트 번호는 19870으로 접속 제한을 둠
        /// server 소켓을 new 시킨 후 클라이언트 접속을 무한 대기함(프로그램이 켜져 있는한)
        /// client 변수에 접속한 클라이언트를 저장 후 "서버접속 됨" 이라는 string 값을 클라이언트에 전송(주석처리함)
        /// 클라이언트에 정보를 받기위해 Receive_Thead 함수를 스레드로 Start 시킴
        /// </summary>
        void Server_Thread()
        {
            ipep = new IPEndPoint(IPAddress.Any, 19870);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


            server.Bind(ipep);
            server.Listen(20);

            client = server.Accept();
            IPEndPoint ip = (IPEndPoint)client.RemoteEndPoint;

            string _buf = "서버접속 됨";
            sendByte = Encoding.Default.GetBytes(_buf);
            //          client.Send(sendByte);

            _mThread = new Thread(new ThreadStart(Receive_Thead));
            _mThread.Start();
        }

        /// <summary>
        /// 클라이언트에게 Byte[]형의 데이터를 받기위해 무한 대기하는 함수
        /// 현재 클라이언트에 받는 데이터가 없어 Byte[]형을 string형으로 변환 후 d 변수에 저장하고 다시 대기함
        /// </summary>
        void Receive_Thead()
        {
            while (true)
            {
                client.Receive(receiveByte);
                d = Encoding.Default.GetString(receiveByte);
            }
        }
        /// <summary>
        /// 클라이언트에 데이터를 전송하는 함수
        /// soket_data 클래스 형태로 받았을때 Baye[]형으로 형변환 후 클라이언트에 전송
        /// 오버로딩 하여 Byte[]형을 인자로 받아 클라이언트에 전송 할 수 있음
        /// </summary>
        /// <param name="_sB"> soket_data class형으로 soket에 필요한 데이터 정보</param>
        public void Send_Message(soket_data _sB)
        {
            sendByte = _sB.Get_baye();
            client.Send(sendByte);
        }
        public void Send_Message(Byte[] _sB)
        {
            sendByte = _sB;
            client.Send(sendByte);
        }



        public void Close_Socket()
        {
            _policyServer.Stop();
            _mThread.Abort();
            mThread.Abort();
            server.Close();
            client.Close();
        }

         


    }
}
