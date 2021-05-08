#nullable enable
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPK.InterfaceElements.LoginFormElements
{
    public class LoginDialog
    {
#nullable disable
        public event Func<Task<string>> GetLoginProgress;

        private TaskDialogPage ProgressDialogPage { get; set; }

        private TaskDialogPage UnsuccessfullLoginPage { get; set; }

#nullable enable

        public void Show()
        {
            TaskDialog.ShowDialog(ProgressDialogPage);
        }

        public LoginDialog()
        {
            InitializeProgressPage();
            InitializeUnsuccessfullLoginPage();
        }

        private void InitializeProgressPage()
        {
            TaskDialogButton cancelButt = TaskDialogButton.Cancel;

            cancelButt.Enabled = false;

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

                Buttons = { cancelButt }
            };

            ProgressDialogPage.Created += async (s, e) =>
            {
                string? errorMessage = "Попытка входа не была совершена. Проблема в программном коде.";

                errorMessage = await GetLoginProgress();

                if (errorMessage is not null)
                {
                    UnsuccessfullLoginPage.Text = errorMessage;
                    ProgressDialogPage.Navigate(UnsuccessfullLoginPage);
                    return;
                }

                ProgressDialogPage.BoundDialog?.Close();
            };
        }

        private void InitializeUnsuccessfullLoginPage()
        {
            UnsuccessfullLoginPage = new TaskDialogPage
            {
                Caption = "Процесс входа",
                Heading = "Вход в систему не был произведён",
                Icon = TaskDialogIcon.ShieldErrorRedBar,

                Buttons = { TaskDialogButton.Close }
            };
        }
    }
}