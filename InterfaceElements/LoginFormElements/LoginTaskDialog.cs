#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPK.InterfaceElements.LoginFormElements
{
    public class LoginTaskDialog
    {
#nullable disable
        public event Func<IAsyncEnumerable<bool>> GetLoginProgress;

        TaskDialogPage ProgressDialogPage { get; set; }

        TaskDialogPage SuccessfullLoginPage { get; set; }
        
        TaskDialogPage UnsuccessfullLoginPage { get; set; }

#nullable enable
        public event Func<string?>? GetLoginResult;

        public LoginTaskDialog()
        {
            InitializeProgressPage();
            InitializeSuccessfullLoginPage();
            InitializeUnsuccessfulLoginPage();
        }


        private void InitializeUnsuccessfulLoginPage()
        {
            ProgressDialogPage = new TaskDialogPage
            {
                Caption = "Процесс входа",
                Heading = "Вход в процессе...",
                Text = "Пожалуйста подождите, пока идёт вход в систему.",
                Icon = TaskDialogIcon.Information,
                AllowCancel = false,
                AllowMinimize = false,

                ProgressBar = new TaskDialogProgressBar()
                {
                    State = TaskDialogProgressBarState.Marquee
                },
            };

            ProgressDialogPage.Created += async (s, e) =>
            {
                var progressBar = ProgressDialogPage.ProgressBar;

                await foreach (bool isLoginFinished in GetLoginProgress())
                {
                }

                string? errorMessage = GetLoginResult?.Invoke();

                if (errorMessage is null)
                    ProgressDialogPage.Navigate(SuccessfullLoginPage);
                else
                {
                    UnsuccessfullLoginPage.Text = errorMessage;
                    ProgressDialogPage.Navigate(UnsuccessfullLoginPage);
                }
            };
        }

        private void InitializeSuccessfullLoginPage()
        {
            SuccessfullLoginPage = new TaskDialogPage
            {
                Caption = "Процесс входа",
                Heading = "Вход в систему произошёл успешно",
                Icon = TaskDialogIcon.ShieldSuccessGreenBar,

                Buttons = { TaskDialogButton.OK }
            };
        }

        private void InitializeProgressPage()
        {
            SuccessfullLoginPage = new TaskDialogPage
            {
                Caption = "Процесс входа",
                Heading = "Вход в систему не был произведён",
                Icon = TaskDialogIcon.ShieldErrorRedBar,

                Buttons = { TaskDialogButton.Close }
            };
        }
    }
}