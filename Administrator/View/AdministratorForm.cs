using RPK.Administrator.Presenter;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPK.Administrator.View
{
    public partial class AdministratorForm : Form
    {
        public event Func<IList<string>> GetContextsNames;

        public event Func<string, IList<string>> GetContextEntitiesNames;

        public event Func<(Form_PresenterMessage message, DataGridView dataGridView), DataGridView> BindDataGridView;

        public event Func<(Form_PresenterMessage message, object value, string columnName, int rowIndex),
            IAsyncEnumerable<ValidationResult>> ValidateValueAsync;

        public event Func<Form_PresenterMessage, Task<bool>> AnyChangesForTableAsync;

        public event Func<string, Task<bool>> AnyChangesForDbAsync;

        public event Func<Form_PresenterMessage, Task<bool>> AnyChangesToUndoAsync;

        public event Func<Form_PresenterMessage, Task<bool>> AnyChangesToRedoAsync;

        public event Func<Form_PresenterMessage, Task<(bool result, string errorMessage)>> TryAddRowAsync;

        public event Func<Form_PresenterMessage, Task<(bool result, string errorMessage)>> TryUndo;

        public event Func<Form_PresenterMessage, Task<(bool result, string errorMessage)>> TryRedo;

        public event Func<Form_PresenterMessage, Task<(bool result, string errorMessage)>> TrySaveChangesAsync;

        public event Func<(Form_PresenterMessage message, int rowIndex),
            Task<(bool result, string errorMessage)>> TryDeleteRowAsync;

        public event Action<Form_PresenterMessage> CancelAllChangesForTable;

        public event Action<string> CancelAllChangesForDb;

        public event Action ReloginRequired;

        public AdministratorForm()
        {
            InitializeComponent();

            this.Shown += LoadDbNamesAsync;

            dataGridView.DataError += DataGridView_DataError;

            addButt.Click += AddButt_Click;
            saveButt.Click += SaveButt_Click;
            deleteButt.Click += DeleteButt_Click;

            exitButt.Click += (sender, e) => Close();
        }

        public void SetUserDescription(string userName, string userRole)
        {
            this.Text = $"Вы вошли как {userName}: {userRole}";
        }

        public void ShowMessage(string caption, string heading, string text, MessageType messageType)
        {
            TaskDialog.ShowDialog(new TaskDialogPage
            {
                Caption = caption,
                Heading = heading,
                Text = text,
                Icon = (messageType) switch
                {
                    MessageType.Success => TaskDialogIcon.ShieldSuccessGreenBar,
                    MessageType.Error => TaskDialogIcon.Error,
                    MessageType.Warning => TaskDialogIcon.Warning,
                    _ => TaskDialogIcon.Error
                }
            });
        }

        private async void RedoButt_Click(object sender, EventArgs e)
        {
            await ExecuteEditActionAsync(async () =>
            {
                (bool redoResult, string errorMessage) = await TryRedo(new Form_PresenterMessage
                { ContextName = CurrDb, EntityName = CurrTable });

                if (!redoResult)
                {
                    TaskDialog.ShowDialog(new TaskDialogPage
                    {
                        Caption = "Возврат изменения",
                        Heading = "Возвратить изменение не удалось",
                        Text = errorMessage,
                        Buttons = { TaskDialogButton.Close },
                        Icon = TaskDialogIcon.Error
                    });
                }
            });
        }

        private async void UndoButt_Click(object sender, EventArgs e)
        {
            await ExecuteEditActionAsync(async () =>
            {
                (bool undoResult, string errorMessage) = await TryUndo(new Form_PresenterMessage
                { ContextName = CurrDb, EntityName = CurrTable });

                if (!undoResult)
                {
                    TaskDialog.ShowDialog(new TaskDialogPage
                    {
                        Caption = "Отмена изменения",
                        Heading = "Отменить изменение не удалось",
                        Text = errorMessage,
                        Buttons = { TaskDialogButton.Close },
                        Icon = TaskDialogIcon.Error
                    });
                }
            });
        }

        private async void DeleteButt_Click(object sender, EventArgs e)
        {
            await ExecuteEditActionAsync(async () =>
            {
                foreach (DataGridViewRow row in dataGridView.SelectedRows)
                {
                    int rowIndex = row.Index;
                    (bool deletionResult, string errorMessage) = await TryDeleteRowAsync((new Form_PresenterMessage
                    { ContextName = CurrDb, EntityName = CurrTable }, row.Index));

                    if (!deletionResult)
                    {
                        TaskDialog.ShowDialog(new TaskDialogPage
                        {
                            Caption = "Удаление строки",
                            Heading = $"Удалить строку {rowIndex + 1} не удалось",
                            Text = errorMessage,
                            Buttons = { TaskDialogButton.Close },
                            Icon = TaskDialogIcon.Error
                        });
                    }
                }
            });
        }

        private async void SaveButt_Click(object sender, EventArgs e)
        {
            await ExecuteEditActionAsync(async () =>
            {
                if (DataGridViewCellsHaveErrors.Values.Any(e => e is true))
                {
                    TaskDialog.ShowDialog(new TaskDialogPage
                    {
                        Caption = "Сохранение изменений",
                        Heading = "Сохранить изменения не удалось",
                        Text = "Сохранить изменения не удалось, так как в таблице есть неверно заполненные ячейки.",
                        Buttons = { TaskDialogButton.Close },
                        Icon = TaskDialogIcon.Error
                    });
                }

                (bool saveResult, string errorMessage) = await TrySaveChangesAsync(new Form_PresenterMessage
                { ContextName = CurrDb, EntityName = CurrTable });

                if (saveResult)
                {
                    TaskDialog.ShowDialog(new TaskDialogPage
                    {
                        Caption = "Сохранение изменений",
                        Heading = "Сохранение изменений прошло успешно",
                        Text = errorMessage,
                        Buttons = { TaskDialogButton.OK },
                        Icon = TaskDialogIcon.ShieldSuccessGreenBar
                    });
                }
                else
                    TaskDialog.ShowDialog(new TaskDialogPage
                    {
                        Caption = "Сохранение изменений",
                        Heading = "Сохранить изменения не удалось",
                        Buttons = { TaskDialogButton.Close },
                        Icon = TaskDialogIcon.Error
                    });
            });
        }

        private async void AddButt_Click(object sender, EventArgs e)
        {
            (bool addResult, string errorMessage) = await TryAddRowAsync(new Form_PresenterMessage
            { ContextName = CurrDb, EntityName = CurrTable });

            if (!addResult)
                TaskDialog.ShowDialog(new TaskDialogPage
                {
                    Caption = "Добавление строки",
                    Heading = "Добавить строку не удалось",
                    Text = errorMessage,
                    Buttons = { TaskDialogButton.Close },
                    Icon = TaskDialogIcon.Error
                });
        }

        private void DataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //e.Cancel = true;
        }

        private void LoadDbNamesAsync(object sender, EventArgs e)
        {
            dbChooseComboBox.DataSource = GetContextsNames();
        }

        private async void DbChooseComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            await ExecuteEditActionAsync(async () =>
            {
                PreviousDb ??= dbChooseComboBox.Items[0].ToString();

                bool close = await AskToSaveChangesAsync(
                    new Form_PresenterMessage { ContextName = PreviousDb, EntityName = null });

                PreviousDb = CurrDb;

                if (!close)
                    return;

                DataGridViewCellsHaveErrors.Clear();

                tableChooseComboBox.DataSource = GetContextEntitiesNames(CurrDb);
            });
        }

        private async void TableChooseComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            await ExecuteEditActionAsync(async () =>
            {
                dbChooseComboBox.Enabled = false;
                tableChooseComboBox.Enabled = false;

                bool close = await AskToSaveChangesAsync(
                    new Form_PresenterMessage { ContextName = PreviousDb, EntityName = null });

                PreviousTable = CurrTable;
                PreviousDb = CurrDb;

                if (!close)
                {
                    dbChooseComboBox.Enabled = true;
                    tableChooseComboBox.Enabled = true;
                    return;
                }

                DataGridViewCellsHaveErrors.Clear();

                dataGridView = BindDataGridView((new Form_PresenterMessage
                {
                    ContextName = CurrDb,
                    EntityName = CurrTable,
                }, dataGridView));

                dbChooseComboBox.Enabled = true;
                tableChooseComboBox.Enabled = true;

                deleteButt.Enabled = dataGridView.Rows.Count > 0;
            });
        }

        public void SetInitialData()
        {
            reloginButt.Click += async (sender, e) =>
            {
                bool close = await AskToSaveChangesAsync(
                    new Form_PresenterMessage { ContextName = CurrDb, EntityName = null });

                if (close)
                    ReloginRequired();
            };
        }

        private async Task ExecuteEditActionAsync(Action editAction)
        {
            saveButt.Enabled = false;
            deleteButt.Enabled = false;
            addButt.Enabled = false;

            foreach (Control control in this.Controls)
                control.Enabled = false;

            editAction();

            foreach (Control control in this.Controls)
                control.Enabled = true;

            saveButt.Enabled = DataGridViewCellsHaveErrors.Values.All(error => error is false) &&
                await AnyChangesForDbAsync(CurrDb);
            deleteButt.Enabled = dataGridView.Rows.Count > 0;
            addButt.Enabled = true;
        }

        private async void DataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell currentCell = dataGridView[e.ColumnIndex, e.RowIndex];
            try
            {
                await ExecuteEditActionAsync(async () =>
                {
                    try
                    {
                        DataGridViewCell currentCell = dataGridView[e.ColumnIndex, e.RowIndex];

                        currentCell.ErrorText = null;

                        var validationResults = ValidateValueAsync((new Form_PresenterMessage
                        {
                            ContextName = CurrDb,
                            EntityName = CurrTable,
                        },
                            currentCell.Value, dataGridView.Columns[e.ColumnIndex].DataPropertyName, e.RowIndex));

                        await foreach (ValidationResult validationResult in validationResults)
                        {
                            currentCell.ErrorText += validationResult;
                        }

                        DataGridViewCellsHaveErrors[currentCell] = !string.IsNullOrEmpty(currentCell.ErrorText);
                    }
                    catch
                    {
                        currentCell.ErrorText = $"В этой записи данный параметр изменить нельзя.";
                    }
                });
            }
            catch
            {
                currentCell.ErrorText = $"В этой записи данный параметр изменить нельзя.";
            }
        }

        private async Task<bool> AskToSaveChangesAsync(Form_PresenterMessage message)
        {
            bool anyChanges;
            if (string.IsNullOrEmpty(message.EntityName))
                anyChanges = await AnyChangesForDbAsync(message.ContextName);
            else
                anyChanges = await AnyChangesForTableAsync(message);

            if (anyChanges)
            {
                TaskDialogButton taskDialogResult = TaskDialog.ShowDialog(new TaskDialogPage
                {
                    Caption = "Сохранение изменений",
                    Heading = "У вас есть несохранённые изменения",
                    Text = "У вас есть несохранённые изменения. Хотите сохранить?",
                    Buttons = { TaskDialogButton.Yes, TaskDialogButton.No, TaskDialogButton.Cancel },
                    Icon = TaskDialogIcon.Information
                });

                if (taskDialogResult == TaskDialogButton.Yes)
                    SaveButt_Click(saveButt, null);
                else if (taskDialogResult == TaskDialogButton.No)
                {
                    if (string.IsNullOrEmpty(message.EntityName))
                        CancelAllChangesForDb(message.ContextName);
                    else
                        CancelAllChangesForTable(message);
                }
                else if (taskDialogResult == TaskDialogButton.Cancel)
                    return false;
            }

            return true;
        }

        protected override async void OnFormClosing(FormClosingEventArgs e)
        {
            bool close = await AskToSaveChangesAsync(
                new Form_PresenterMessage { ContextName = CurrDb, EntityName = null });

            e.Cancel = !close;

            base.OnFormClosing(e);
        }

        private Dictionary<DataGridViewCell, bool> DataGridViewCellsHaveErrors { get; set; } = new();

        private string CurrDb => dbChooseComboBox.Text;

        private string CurrTable => tableChooseComboBox.Text;

        private string PreviousDb { get; set; }

        private string PreviousTable { get; set; }
    }

    public enum MessageType
    {
        Success,
        Warning,
        Error
    }
}