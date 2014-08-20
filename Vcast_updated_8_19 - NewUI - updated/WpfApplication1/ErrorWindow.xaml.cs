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

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for ErrorWindow.xaml
    /// </summary>
    public partial class ErrorWindow : Window
    {
        public ErrorWindow()
        {
            InitializeComponent();
            string ErrorName = string.Empty;

            ShowErrorMessage(ErrorName);
        }

        public ErrorWindow(string ErrorName)
        {
            InitializeComponent();
            ShowErrorMessage(ErrorName);
        }

        private void spClose_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void spClose_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void ShowErrorMessage(string ErrorName)
        {
            string ErrorMessage = string.Empty;

            switch (ErrorName)
            {
                case "AfterDeleteErrorMessage":
                    ErrorMessage = Properties.Resources.AfterDeleteErrorMessage;
                    break;

                case "BeforeDeleteErrorMessage":
                    ErrorMessage = Properties.Resources.BeforeDeleteErrorMessage;
                    break;

                case "BehindMergeErrorMessage":
                    ErrorMessage = Properties.Resources.BehindMergeErrorMessage;
                    break;

                case "DeleteErrorMessage":
                    ErrorMessage = Properties.Resources.DeleteErrorMessage;
                    break;

                case "EditScreenChangeErrorMessage":
                    ErrorMessage = Properties.Resources.EditScreenChangeErrorMessage;
                    break;

                case "FrontMergeErrorMessage":
                    ErrorMessage = Properties.Resources.FrontMergeErrorMessage;
                    break;

                case "PauseErrorMessage":
                    ErrorMessage = Properties.Resources.PauseErrorMessage;
                    break;

                case "PlayErrorMessage":
                    ErrorMessage = Properties.Resources.PlayErrorMessage;
                    break;

                case "RecordingErrorMessage":
                    ErrorMessage = Properties.Resources.RecordingErrorMessage;
                    break;

                case "SavingErrorMessage":
                    ErrorMessage = Properties.Resources.SavingErrorMessage;
                    break;

                case "EncodingErrorMessage":
                    ErrorMessage = Properties.Resources.EncodingErrorMessage;
                    break;

                case "StopErrorMessage":
                    ErrorMessage = Properties.Resources.StopErrorMessage;
                    break;

                case "TakeMediaErrorMessage":
                    ErrorMessage = Properties.Resources.TakeMediaErrorMessage;
                    break;

                case "MediaWindowInitialiizingErrorMessage":
                    ErrorMessage = Properties.Resources.MediaWindowInitialiizingErrorMessage;
                    break;

                case "UpdateErrorMessage":
                    ErrorMessage = Properties.Resources.UploadErrorMessage;
                    break;

                case "GetFileListErrorMessage":
                    ErrorMessage = Properties.Resources.GetFileListErrorMessage;
                    break;

                case "LectureModuleTitleError":
                    ErrorMessage = Properties.Resources.LectureModuleTitleError;
                    break;

                case "LectureFileNameError":
                    ErrorMessage = Properties.Resources.LectureFileNameError;
                    break;

                case "DuplicateSaveErrorMessage":
                    ErrorMessage = Properties.Resources.DuplicateSaveErrorMessage;
                    break;

                case "SaveSuccessMessage":
                    ErrorMessage = Properties.Resources.SaveSuccessMessage;
                    break;

                case "NotSavedErrorMessage":
                    ErrorMessage = Properties.Resources.NotSavedErrorMessage;
                    break;

                case "UploadErrorMessage":
                    ErrorMessage = Properties.Resources.UploadErrorMessage;
                    break;

                case "UploadSuccessMessage":
                    ErrorMessage = Properties.Resources.UploadSuccessMessage;
                    break;

                case "LoginIDFailErrorMessage":
                    ErrorMessage = Properties.Resources.LoginIDFailErrorMessage;
                    break;

                case "LoginPasswordErrorMessage":
                    ErrorMessage = Properties.Resources.LoginPasswordErrorMessage;
                    break;

                case "LoginEtcErrorMessage":
                    ErrorMessage = Properties.Resources.LoginEtcErrorMessage;
                    break;

                case "LoginDefaultError":
                    ErrorMessage = Properties.Resources.LoginDefaultError;
                    break;

                case "EncodingSuccessMessage":
                    ErrorMessage = Properties.Resources.EncodingSuccessMessage;
                    break;

                default:
                    ErrorMessage = Properties.Resources.GetFileListErrorMessage;
                    break;
            }

            rtbMessage.AppendText(ErrorMessage);
        }

        private void spClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }





    }
}
