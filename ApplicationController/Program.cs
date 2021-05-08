using Autofac;
using RPK.Presenter;
using RPK.Login.Presenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using RPK.Model.Users;
using RPK.InterfaceElements.SharedElements;

namespace ApplicationController
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            IContainer container = ContainerConfig.Configure();

            using ILifetimeScope scope = container.BeginLifetimeScope();

            LoginPresenterBase loginPresenter = scope.Resolve<LoginPresenterBase>();

            loginPresenter.UserLoggedIn += UserLoggedIn;
            
            void UserLoggedIn(User user)
            {
                IEnumerable<RolePresenterBase> rolePresenters = scope.Resolve<IEnumerable<RolePresenterBase>>();

                RolePresenterBase chosenRolePresenter = null;
                foreach (RolePresenterBase rolePresenter in rolePresenters)
                {
                    if (rolePresenter.Role == user.Role)
                    {
                        chosenRolePresenter = rolePresenter;
                        break;
                    }
                }

                if (chosenRolePresenter is null)
                {
                    new InnerErrorReportDialog().Show("Отсутствует необходимая часть программы. Пока что " +
                        "функция недоступна.");

                    return;
                }

                chosenRolePresenter.ReloginRequired += () =>
                {
                    loginPresenter = scope.Resolve<LoginPresenterBase>();
                    loginPresenter.UserLoggedIn += UserLoggedIn;
                    loginPresenter.Form.FormClosed += static (sender, e) => Application.Exit();
                    chosenRolePresenter.Form.Hide();
                    loginPresenter.Run().Show();
                };

                chosenRolePresenter.Form.FormClosed += static (sender, e) => Application.Exit();

                loginPresenter.Form.Hide();

                chosenRolePresenter.Run().Show();
            };

            Application.Run(loginPresenter.Run());
        }
    }
}
