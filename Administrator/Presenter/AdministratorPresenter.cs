#nullable enable
using RPK.Presenter;
using RPK.Administrator.View;
using RPK.Repository.MathModel;
using RPK.Repository.Users;
using System;
using System.Windows.Forms;
using RPK.Model.Users;

namespace RPK.Administrator.Presenter
{
    public class AdministratorPresenter : RolePresenterBase
    {
        public override Form Form { get => AdministratorForm; }

        public AdministratorPresenter(MathModelContext mathModelContext, UsersContext usersContext, AdministratorForm administratorForm, Role role)
        {
            MathModelContext = mathModelContext;
            UsersContext = usersContext;
            AdministratorForm = administratorForm;
            Role = role;
        }

        private MathModelContext MathModelContext { get; set; }

        private UsersContext UsersContext { get; set; }

        private AdministratorForm AdministratorForm { get; set; }

        public override event Action? ReloginRequired;
    }
}
