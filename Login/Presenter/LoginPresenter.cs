#nullable enable
using RPK.Presenter;
using RPK.Login.View;
using RPK.Model.Users;
using RPK.Repository.Users;
using System;
using System.Linq;
using System.Windows.Forms;

namespace RPK.Login.Presenter
{
    public class LoginPresenter : LoginPresenterBase
    {
        private LoginForm LoginForm { get; set; }

        private UsersContext UsersContext { get; set; }

        public override Form Form { get => LoginForm; }

        public LoginPresenter(LoginForm loginForm, UsersContext usersContext)
        {
            LoginForm = loginForm;
            LoginForm.LoginAttempt += LoginForm_LoginAttempt;
            LoginForm.ReadyToClose += () => UserLoggedIn?.Invoke(LoggedUser!);

            UsersContext = usersContext;
        }

        public override event Action<User>? UserLoggedIn;

        private string? LoginForm_LoginAttempt(string login, string password)
        {
            if (UserLoggedIn is null)
                return "Попытка входа не была совершена. Проблема в программном коде.";

            User? foundUser = UsersContext.Users.FirstOrDefault(u => u.Login == login && u.Password == password);

            if (foundUser is not null)
            {
                LoggedUser = foundUser;
                return null;
            }
            else
                return "Неправильный логин/пароль";
        }

        private User? LoggedUser { get; set; } = null;
    }
}