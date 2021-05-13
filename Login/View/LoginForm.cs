#nullable enable
using RPK.InterfaceElements.LoginFormElements;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPK.Login.View
{
    public delegate string? LoginAttempt(string login, string password);

    public partial class LoginForm : Form
    {
        public event LoginAttempt? LoginAttempt;

        public event Action? ReadyToClose;

        public LoginForm()
        {
            InitializeComponent();
        }

        private async void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (sender is not TextBox)
                return;

            if (e.KeyCode == Keys.Enter)
            {
                await CheckTextBoxes_AndTryToLogin();
            }
        }

        private async Task CheckTextBoxes_AndTryToLogin()
        {
            if (!string.IsNullOrEmpty(loginTextBox.Text) && !string.IsNullOrWhiteSpace(loginTextBox.Text) &&
                !string.IsNullOrEmpty(passwordTextBox.Text) && !string.IsNullOrWhiteSpace(passwordTextBox.Text))
            {
                loginTextBox.Enabled = false;
                passwordTextBox.Enabled = false;
                loginButt.Enabled = false;

                Task<string?> loginTask = Task.Run(() =>
                    LoginAttempt is not null && ReadyToClose is not null ?
                    LoginAttempt(loginTextBox.Text, passwordTextBox.Text)
                    : "Попытка входа не была совершена. Проблема в программном коде.");

                string? errorMessage = null;

                var dialogPage = new LoginDialog();

                dialogPage.GetLoginProgress += ReportProgress;

                async Task<string?> ReportProgress()
                {
                    return await loginTask;
                }

                dialogPage.Show();

                errorMessage = await loginTask;

                if (errorMessage is not null)
                {
                    loginTextBox.Enabled = true;
                    passwordTextBox.Enabled = true;
                    loginButt.Enabled = true;
                }
                else
                    ReadyToClose?.Invoke();
            }
        }

        private async void LoginButt_Click(object sender, EventArgs e)
        {
            if (sender is not Button)
                return;

            await CheckTextBoxes_AndTryToLogin();
        }
    }
}
