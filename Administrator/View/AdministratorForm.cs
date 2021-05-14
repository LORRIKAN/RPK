using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Windows.Forms;
using RPK.Administrator.Presenter;
using System.Threading.Tasks;
using System.Threading;

namespace RPK.Administrator.View
{
    public partial class AdministratorForm : Form
    {
        public event Func<IAsyncEnumerable<string>> GetContextsNamesAsync;

        public event Func<string, IAsyncEnumerable<string>> GetContextEntitiesNamesAsync;

        public event Func<(Form_PresenterMessage message, DataGridView dataGridView), Task<DataGridView>> BindDataGridViewAsync;

        public event Func<(Form_PresenterMessage message, object value, string columnName), IAsyncEnumerable<ValidationResult>> ValidateValueAsync;

        public event Func<Form_PresenterMessage, Task<bool>> AnyChangesAsync;

        public event Func<Form_PresenterMessage, Task<bool>> AnyChangesToUndoAsync;

        public event Func<Form_PresenterMessage, Task<bool>> AnyChangesToRedoAsync;

        public event Func<Form_PresenterMessage, Task<(bool result, string errorMessage)>> AddRowAsync;

        public event Func<Form_PresenterMessage, Task<(bool result, string errorMessage)>> TryUndoLastChangeAsync;

        public event Func<Form_PresenterMessage, Task<(bool result, string errorMessage)>> TryRedoLastChangeAsync;

        public event Func<Form_PresenterMessage, Task<(bool result, string errorMessage)>> TrySaveChangesAsync;

        public event Func<(Form_PresenterMessage message, int rowIndex), 
            Task<(bool result, string errorMessage)>> TryDeleteRowAsync;

        public AdministratorForm()
        {
            InitializeComponent();

            this.Shown += LoadDbNamesAsync;

            dataGridView.DataError += DataGridView_DataError;

            addButt.Click += AddButt_Click;
            saveButt.Click += SaveButt_Click;
            deleteButt.Click += DeleteButt_Click;
            undoButt.Click += UndoButt_Click;
            redoButt.Click += RedoButt_Click;
        }

        private async void RedoButt_Click(object sender, EventArgs e)
        {
            (bool redoResult, string errorMessage) = await TryRedoLastChangeAsync(new Form_PresenterMessage
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
        }

        private async void UndoButt_Click(object sender, EventArgs e)
        {
            (bool undoResult, string errorMessage) = await TryUndoLastChangeAsync(new Form_PresenterMessage
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
        }

        private async void DeleteButt_Click(object sender, EventArgs e)
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
        }

        private async void SaveButt_Click(object sender, EventArgs e)
        {
            foreach (Control control in this.Controls)
                control.Enabled = false;

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
        }

        private async void AddButt_Click(object sender, EventArgs e)
        {
            CancellationTokenSourceForCellsChecks.Cancel();

            (bool addResult, string errorMessage) = await AddRowAsync(new Form_PresenterMessage
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

        private async void LoadDbNamesAsync(object sender, EventArgs e)
        {
            dbChooseComboBox.DataSource = null;

            await foreach (string item in GetContextsNamesAsync())
            {
                dbChooseComboBox.Items.Add(item);
            }
        }

        private async void LoadTablesNamesAsync(object sender, EventArgs e)
        {
            tableChooseComboBox.DataSource = null;

            await foreach (string item in GetContextEntitiesNamesAsync(CurrDb))
            {
                tableChooseComboBox.Items.Add(item);
            }
        }

        private async void TableChooseComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            dbChooseComboBox.Enabled = false;
            tableChooseComboBox.Enabled = false;

            dataGridView = await BindDataGridViewAsync((new Form_PresenterMessage
            {
                ContextName = CurrDb,
                EntityName = CurrTable,
            }, dataGridView));

            dbChooseComboBox.Enabled = true;
            tableChooseComboBox.Enabled = true;

            deleteButt.Enabled = dataGridView.Rows.Cast<DataGridViewRow>().Any();
        }

        private async Task CheckControlsAndEnableButtsAsync(CancellationToken cancellationToken)
        {
            saveButt.Enabled = false;
            undoButt.Enabled = await AnyChangesToUndoAsync(new Form_PresenterMessage { ContextName = CurrDb, EntityName = CurrTable });
            redoButt.Enabled = await AnyChangesToRedoAsync(new Form_PresenterMessage { ContextName = CurrDb, EntityName = CurrTable });
            deleteButt.Enabled = dataGridView.Rows.Cast<DataGridViewRow>().Any();

            progressBar.Visible = true;

            bool allCellsAreValidated = true;

            await Task.Run(() =>
            {
                try
                {
                    int cellsChecked = 0;
                    int cellsCount = dataGridView.Rows.Cast<DataGridViewRow>().Select(r => r.Cells.Count).Sum();

                    foreach (DataGridViewRow row in dataGridView.Rows.Cast<DataGridViewRow>())
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            cellsChecked++;
                            if (!string.IsNullOrEmpty(cell.ErrorText))
                            {
                                allCellsAreValidated = false;
                                break;
                            }
                        }

                        if (!allCellsAreValidated)
                            break;

                        this.Invoke(new MethodInvoker(() => progressBar.Value = cellsChecked / cellsCount));
                    }
                }
                catch (OperationCanceledException)
                {
                    return;
                }
            }, cancellationToken);

            progressBar.Visible = false;

            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            saveButt.Enabled = allCellsAreValidated && 
                await AnyChangesAsync(new Form_PresenterMessage { ContextName = CurrDb, EntityName = CurrTable });
        }

        private async void DataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView[e.ColumnIndex, e.RowIndex].ErrorText = null;

            var validationResults = ValidateValueAsync((new Form_PresenterMessage
            {
                ContextName = CurrDb,
                EntityName = CurrTable,
            },
                dataGridView[e.ColumnIndex, e.RowIndex].Value, dataGridView.Columns[e.ColumnIndex].DataPropertyName));

            try
            {
                await foreach (ValidationResult error in validationResults)
                {
                    dataGridView[e.ColumnIndex, e.RowIndex].ErrorText += error?.ErrorMessage;
                }
            }
            catch
            {
                dataGridView[e.ColumnIndex, e.RowIndex].ErrorText += $"В этой записи данный параметр изменить нельзя.";
            }

            CancellationTokenSourceForCellsChecks?.Cancel();

            CancellationTokenSourceForCellsChecks = new CancellationTokenSource();

            await CheckControlsAndEnableButtsAsync(CancellationTokenSourceForCellsChecks.Token);
        }

        private string CurrDb => dbChooseComboBox.Text;

        private string CurrTable => tableChooseComboBox.Text;

        private CancellationTokenSource CancellationTokenSourceForCellsChecks { get; set; }
    }
}