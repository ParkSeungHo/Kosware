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

#region 추가

using WpfApplication1.kr.co.vcast;

#endregion

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private bool isFirstTime = true;
        //ChoiceWindow cw;
       
        public bool GetIsFristTime()
        {
            return isFirstTime;

        }

        public LoginWindow()
        {

            InitializeComponent();
            isFirstTime = false;
            CloseAll.caLW = this;
            txt_id.Text = "dhchoi";
            txt_pw.Password = "111111";
        }

        private void image_login_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            #region 기존 처리

            //this.Close();
            //ChoiceWindow cw = new ChoiceWindow();
            //cw.ShowDialog();

            #endregion

            #region 신규처리

            try
            {
                string MemberID = txt_id.Text.Trim();
                string Password = txt_pw.Password.Trim();

                //MemberID = "dhchoi";
                //Password = "111111";

                using (kr.co.vcast.www.VCastService VCS = new kr.co.vcast.www.VCastService())
                {
                    string sResult = VCS.Login(MemberID, Password);
                    string[] arrResult = sResult.Split('|');
                    string MessageKey = string.Empty;
                    if (arrResult.Length < 2)
                    {
                        switch (sResult)
                        {
                            case "-1":

                                MessageKey = "LoginIDFailErrorMessage";                                
                                
                                //MessageBox.Show("해당 아이디가 존재하지 않습니다.");
                                break;

                            case "-2":
                                MessageKey = "LoginPasswordErrorMessage";
                                
                                //MessageBox.Show("비밀번호가 올바르지 않습니다.");
                                break;

                            case "-9":
                                MessageKey = "LoginEtcErrorMessage";
                                break;

                            default:
                                MessageKey = "LoginDefaultError";
                                break;
                        }

                        ErrorWindow ew = new ErrorWindow(MessageKey);

                        ew.Left = 700;
                        ew.Top = 400;

                        ew.ShowDialog();
                    }
                    else
                    {
                        //UserInfo _Info = new UserInfo(arrResult[0], arrResult[1], arrResult[2], arrResult[3]);       
                        UserInfo._CompanySeq = arrResult[0];
                        UserInfo._MemberSeq = arrResult[1];
                        UserInfo._MemberID = arrResult[2];
                        UserInfo._MemberName = arrResult[3];
                        //ChoiceWindow cw = new ChoiceWindow(_Info);
                        this.Visibility = Visibility.Hidden;
                        ChoiceWindow cw = new ChoiceWindow();
                        cw.ShowDialog();

                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("시스템에 오류가 발생 하였습니다.");
            }

            #endregion
            
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    this.Close();
                    break;
            }
        }
    }
}
