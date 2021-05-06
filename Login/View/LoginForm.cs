#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPK.Login.View
{
    public partial class LoginForm : Form
    {
        public event Func<string, string, string?>? LoginAttempt;

        //public event 

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
                await CheckTextBoxes_AndTryToLogIn_Async();
            }
        }

        private async Task CheckTextBoxes_AndTryToLogIn_Async()
        {
            if (!string.IsNullOrEmpty(loginTextBox.Text) && !string.IsNullOrWhiteSpace(loginTextBox.Text) &&
                !string.IsNullOrEmpty(passwordTextBox.Text) && !string.IsNullOrWhiteSpace(passwordTextBox.Text))
            {
                loginTextBox.Enabled = false;
                passwordTextBox.Enabled = false;
                loginButt.Enabled = false;

                string? errorMessage = await Task.Run(() => 
                    LoginAttempt?.Invoke(loginTextBox.Text, passwordTextBox.Text));


            }
        }

        private async void LoginButt_Click(object sender, EventArgs e)
        {
            if (sender is not Button)
                return;

            await CheckTextBoxes_AndTryToLogIn_Async();
        }
    }
}
