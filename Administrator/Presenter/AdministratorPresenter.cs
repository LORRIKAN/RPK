using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Repository;
using RPK.Administrator.View;
using RPK.Model.Users;
using RPK.Presenter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            AdministratorForm.GetContextsNames += AdministratorForm_GetContextsNames;
            AdministratorForm.GetContextEntitiesNames += AdministratorForm_GetContextEntitiesNames;
            AdministratorForm.BindDataGridView += AdministratorForm_BindDataGridViewAsync;
            AdministratorForm.ValidateValueAsync += AdministratorForm_ValidateValueAsync;

            AdministratorForm.TryAddRowAsync += AdministratorForm_AddRowAsync;
            AdministratorForm.TryDeleteRowAsync += AdministratorForm_TryDeleteRowAsync;

            AdministratorForm.AnyChangesForDbAsync += AdministratorForm_AnyChangesForContextAsync;
            AdministratorForm.AnyChangesForTableAsync += AdministratorForm_AnyChangesForEntityAsync;
            AdministratorForm.AnyChangesToUndoAsync += AdministratorForm_AnyChangesToUndoAsync;
            AdministratorForm.AnyChangesToRedoAsync += AdministratorForm_AnyChangesToRedoAsync;

            AdministratorForm.TryUndo += AdministratorForm_TryUndoAsync;
            AdministratorForm.TryRedo += AdministratorForm_TryRedoAsync;

            AdministratorForm.TrySaveChangesAsync += AdministratorForm_TrySaveChangesAsync;

            AdministratorForm.CancelAllChangesForDb += AdministratorForm_CancelAllChangesForDb;
            AdministratorForm.CancelAllChangesForTable += AdministratorForm_CancelAllChangesForTable;
        }

        private void AdministratorForm_CancelAllChangesForTable(Form_PresenterMessage arg)
        {
            foreach (EntityEntry entityEntry in FindChangingEntityEntries(arg))
            {
                CancelChange(entityEntry);
            }
        }

        private IEnumerable<EntityEntry> FindChangingEntityEntries(Form_PresenterMessage arg)
        {
            DbContext dbContext = DbContexts[arg.ContextName];

            IBindingList entity = Entities[(arg.ContextName, arg.EntityName)];

            return dbContext.ChangeTracker.Entries().Where(e => e.Entity.GetType().IsAssignableTo(entity.GetDataType())
                && e.State is not (EntityState.Detached or EntityState.Unchanged));
        }

        private void AdministratorForm_CancelAllChangesForDb(string arg)
        {
            foreach (EntityEntry entityEntry in DbContexts[arg].ChangeTracker.Entries())
            {
                CancelChange(entityEntry);
            }
        }

        private async Task<bool> AdministratorForm_AnyChangesForEntityAsync(Form_PresenterMessage message)
        {
            return await Task.Run(() => FindChangingEntityEntries(message).Any());
        }

        private async Task<(bool result, string errorMessage)> AdministratorForm_TrySaveChangesAsync(Form_PresenterMessage arg)
        {
            try
            {
                await DbContexts[arg.ContextName].SaveChangesAsync();
                return await Task.FromResult((true, string.Empty));
            }
            catch (Exception ex)
            {
                return await Task.FromResult((false, ex.Message));
            }
        }

        private async Task<(bool result, string errorMessage)> AdministratorForm_TryRedoAsync(Form_PresenterMessage arg)
        {
            return await Task.FromResult((true, string.Empty));
        }

        private async Task<(bool result, string errorMessage)> AdministratorForm_TryUndoAsync(Form_PresenterMessage arg)
        {
            return await Task.FromResult((true, string.Empty));
        }

        private async Task<(bool result, string errorMessage)> AdministratorForm_TryDeleteRowAsync((Form_PresenterMessage message, int rowIndex) arg)
        {
            IBindingList entity = Entities[(arg.message.ContextName, arg.message.EntityName)];

            if (await DbContexts[arg.message.ContextName].RowCanBeChangedAsync(entity, arg.rowIndex) is false)
                return (false, "Данную строку удалить нельзя, так как она является базовой частью программы.");

            try
            {
                entity.RemoveAt(arg.rowIndex);
                return await Task.FromResult((true, string.Empty));
            }
            catch (Exception ex)
            {
                return await Task.FromResult((false, ex.Message));
            }
        }

        private async Task<bool> AdministratorForm_AnyChangesToUndoAsync(Form_PresenterMessage arg)
        {
            return await Task.FromResult(true);
        }

        private async Task<bool> AdministratorForm_AnyChangesToRedoAsync(Form_PresenterMessage arg)
        {
            return await Task.FromResult(true);
        }

        private async Task<bool> AdministratorForm_AnyChangesForContextAsync(string contextName)
        {
            return await Task.Run(() => DbContexts[contextName].ChangeTracker.HasChanges());
        }

        private async IAsyncEnumerable<ValidationResult> AdministratorForm_ValidateValueAsync(
            (Form_PresenterMessage message, object value, string columnName, int rowIndex) arg)
        {
            IAsyncEnumerable<ValidationResult> validationResults = DbContexts[arg.message.ContextName]
                    .ValidateAsync(arg.value, Entities[(arg.message.ContextName, arg.message.EntityName)], arg.columnName,
                    arg.rowIndex);

            await foreach (ValidationResult validationResult in validationResults)
            {
                if (validationResult is RowIndexIsInvalid)
                {
                    CancelChange(DbContexts[arg.message.ContextName].ChangeTracker.Entries().First());
                    AdministratorForm.ShowMessage("Операция не удалась", "Ваша последняя операция была безуспешной.",
                        validationResult.ErrorMessage, MessageType.Error);
                    yield break;
                }

                yield return validationResult;
            }
        }

        private async Task<(bool result, string errorMessage)> AdministratorForm_AddRowAsync(Form_PresenterMessage message)
        {
            try
            {
                IBindingList entity = Entities[(message.ContextName, message.EntityName)];
                entity.AddNew();
                return await Task.FromResult((true, string.Empty));
            }
            catch (Exception ex)
            {
                return await Task.FromResult((false, ex.Message));
            }
        }

        private DataGridView AdministratorForm_BindDataGridViewAsync(
            (Form_PresenterMessage message, DataGridView dataGridView) arg)
        {
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
                    Type dataType = set.GetDataType();

                    DisplayAttribute displayAttribute = dataType.GetCustomAttributes(true)
                        .FirstOrDefault(attr => attr is DisplayAttribute) as DisplayAttribute;

                    string dbSetName = displayAttribute?.Name ?? set.GetType().Name;

                    Entities.Add((context.Key, dbSetName), set);
                }
            }
        }

        void CancelChange(EntityEntry entityEntry)
        {
            switch (entityEntry.State)
            {
                case EntityState.Modified:
                    entityEntry.State = EntityState.Unchanged;
                    break;
                case EntityState.Added:
                    entityEntry.State = EntityState.Detached;
                    break;
                case EntityState.Deleted:
                    entityEntry.Reload();
                    break;
                default: break;
            }
        }

        public override Form Run(User user)
        {
            AdministratorForm.SetInitialData();

            AdministratorForm.ReloginRequired += ReloginRequired;

            AdministratorForm.SetUserDescription(user.Login, user.Role.RoleName);

            return AdministratorForm;
        }

        private Dictionary<string, ExtendedDbContext> DbContexts { get; set; }

        private Dictionary<(string contextName, string entityName), IBindingList> Entities { get; set; } = new();

        private AdministratorForm AdministratorForm { get; set; }

        public override event Action ReloginRequired;
    }
}