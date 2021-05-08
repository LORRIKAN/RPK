using Autofac;
using RPK.Presenter;
using RPK.Administrator.Presenter;
using RPK.Administrator.View;
using RPK.Login.Presenter;
using RPK.Login.View;
using RPK.Repository.MathModel;
using RPK.Repository.Users;
using RPK.Researcher.Presenter;
using RPK.Researcher.View;
using RPK.Model.Users;

namespace ApplicationController
{
    public class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ResearcherForm>();
            builder.RegisterType<MathModelContext>();
            builder.RegisterType<MathModel>();
            builder.RegisterType<FileExportService>();
            builder.RegisterType<ResearcherPresenter>()
                .WithParameter(new TypedParameter(typeof(Role), new Role { RoleId = 2 }))
                .AsSelf()
                .As<RolePresenterBase>();

            builder.RegisterType<AdministratorForm>();
            builder.RegisterType<UsersContext>();
            builder.RegisterType<AdministratorPresenter>()
                .WithParameter( new TypedParameter(typeof(Role), new Role { RoleId = 1 }))
                .AsSelf()
                .As<RolePresenterBase>();

            builder.RegisterType<LoginForm>();
            builder.RegisterType<LoginPresenter>().AsSelf().As<LoginPresenterBase>();

            return builder.Build();
        }
    }
}
