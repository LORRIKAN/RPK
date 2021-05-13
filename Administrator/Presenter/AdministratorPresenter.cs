using RPK.Administrator.View;
using RPK.Model.MathModel;
using RPK.Model.Users;
using RPK.Presenter;
using RPK.Repository.MathModel;
using RPK.Repository.Users;
using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using Repository;
using System.ComponentModel.DataAnnotations;

namespace RPK.Administrator.Presenter
{
    public class AdministratorPresenter : RolePresenterBase
    {
        public override Form Form { get => AdministratorForm; }

        public AdministratorPresenter(AdministratorForm administratorForm, Role role, params ExtendedDbContext[] dbContexts)
        {
            DbContexts = dbContexts.ToDictionary(d => d.ToString());
            SetDbSets();
            Role = role;

            AdministratorForm = administratorForm;
            AdministratorForm.GetContextsNames += AdministratorForm_GetContextsNames;
            AdministratorForm.GetContextEntitiesNames += AdministratorForm_GetContextEntitiesNames;
            AdministratorForm.BindDataGridView += AdministratorForm_BindDataGridView;
            AdministratorForm.ValidateEntity += AdministratorForm_ValidateEntity;
            AdministratorForm.AddRow += AdministratorForm_AddRow;
        }

        private bool AdministratorForm_AddRow()
        {
            try
            {
                CurrEntity.AddNew();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private IAsyncEnumerable<ValidationResult> AdministratorForm_ValidateEntity((string contextName, string entityName) arg)
        {
            return DbContexts[arg.contextName].ValidateAsync();
        }

        private DataGridView AdministratorForm_BindDataGridView((string contextName, string entityName, DataGridView dataGridView) arg)
        {
            //if (DbContexts[arg.contextName].ChangeTracker.Entries().Any(e => e.))
            foreach (var item1 in DbContexts[arg.contextName].ChangeTracker.Entries())
                switch (item1.State)
                {
                    case EntityState.Modified:
                        item1.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        item1.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        item1.Reload();
                        break;
                    default: break;
                }

            CurrContext = DbContexts[arg.contextName];

            CurrEntity = Entities[(arg.contextName, arg.entityName)];

            return arg.dataGridView.Bind(Entities[(arg.contextName, arg.entityName)], DbContexts[arg.contextName]);
        }

        private IList<string> AdministratorForm_GetContextEntitiesNames(string contextName)
        {
            return Entities.Keys.Where(k => k.contextName == contextName).Select(k => k.entityName).ToList();
        }

        private IList<string> AdministratorForm_GetContextsNames()
        {
            return DbContexts.Keys.ToList();
        }

        private void SetDbSets()
        {
            foreach (KeyValuePair<string, ExtendedDbContext> context in DbContexts)
            {
                foreach (IBindingList set in context.Value.GetDbSets(true))
                {
                    Type indexerType =
                        set.GetType().GetProperties().First(x => x.GetIndexParameters().Length > 0).PropertyType;

                    DisplayAttribute displayAttribute = indexerType.GetCustomAttributes(true)
                        .FirstOrDefault(attr => attr is DisplayAttribute) as DisplayAttribute;

                    string dbSetName = displayAttribute?.Name ?? set.GetType().Name;

                    Entities.Add((context.Key, dbSetName), set);
                }
            }
        }

        private ExtendedDbContext CurrContext { get; set; }

        private IBindingList CurrEntity { get; set; }

        private Dictionary<string, ExtendedDbContext> DbContexts { get; set; }

        private Dictionary<(string contextName, string entityName), IBindingList> Entities { get; set; } = new();

        private AdministratorForm AdministratorForm { get; set; }

        public override event Action ReloginRequired;
    }
}