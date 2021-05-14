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
    public struct Form_PresenterMessage
    {
        public string ContextName { get; set; }

        public string EntityName { get; set; }
    }
    public class AdministratorPresenter : RolePresenterBase
    {
        public override Form Form { get => AdministratorForm; }

        public AdministratorPresenter(AdministratorForm administratorForm, Role role, params ExtendedDbContext[] dbContexts)
        {
            DbContexts = dbContexts.ToDictionary(d => d.ToString());
            SetDbSets();
            Role = role;

            AdministratorForm = administratorForm;
            AdministratorForm.GetContextsNamesAsync += AdministratorForm_GetContextsNames;
            AdministratorForm.GetContextEntitiesNamesAsync += AdministratorForm_GetContextEntitiesNames;
            AdministratorForm.BindDataGridViewAsync += AdministratorForm_BindDataGridView;
            AdministratorForm.ValidateValueAsync += AdministratorForm_ValidateValue;
            AdministratorForm.AddRowAsync += AdministratorForm_AddRow;
            AdministratorForm.AnyChangesAsync += AdministratorForm_AnyChanges;
            AdministratorForm.AnyChangesToUndoAsync += AdministratorForm_AnyChanges;
            AdministratorForm.AnyChangesToRedoAsync += AdministratorForm_AnyChangesToRedo;
        }

        private bool AdministratorForm_AnyChangesToRedo(Form_PresenterMessage arg)
        {
            throw new NotImplementedException();
        }

        private bool AdministratorForm_AnyChanges(Form_PresenterMessage message)
        {
            return DbContexts[message.ContextName].ChangeTracker.HasChanges();
        }

        private IAsyncEnumerable<ValidationResult> AdministratorForm_ValidateValue(
            (Form_PresenterMessage message, object value, string columnName) arg)
        {
            return DbContexts[arg.message.ContextName]
                .ValidateAsync(arg.value, Entities[(arg.message.ContextName, arg.message.EntityName)], arg.columnName);
        }

        private bool AdministratorForm_AddRow(Form_PresenterMessage message)
        {
            try
            {
                IBindingList entity = Entities[(message.ContextName, message.EntityName)];
                entity.AddNew();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private DataGridView AdministratorForm_BindDataGridView((Form_PresenterMessage message, DataGridView dataGridView) arg)
        {
            DbContexts[arg.message.ContextName].ChangeTracker.Entries();
            foreach (var item1 in DbContexts[arg.message.ContextName].ChangeTracker.Entries())
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


            ExtendedDbContext dbContext = DbContexts[arg.message.ContextName];

            IBindingList entity = Entities[(arg.message.ContextName, arg.message.EntityName)];

            return arg.dataGridView.Bind(entity, dbContext);
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

        private Dictionary<string, ExtendedDbContext> DbContexts { get; set; }

        private Dictionary<(string contextName, string entityName), IBindingList> Entities { get; set; } = new();

        private AdministratorForm AdministratorForm { get; set; }

        public override event Action ReloginRequired;
    }
}