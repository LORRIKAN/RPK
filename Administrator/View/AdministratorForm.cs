using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Windows.Forms;

namespace RPK.Administrator.View
{
    public partial class AdministratorForm : Form
    {
        public event Func<IList<string>> GetContextsNames;

        public event Func<string, IList<string>> GetContextEntitiesNames;

        public event Func<(string contextName, string entityName, DataGridView dataGridView), DataGridView> BindDataGridView;

        public event Func<(string contextName, string entityName), IAsyncEnumerable<ValidationResult>> ValidateEntity;

        public event Func<bool> AddRow;

        public AdministratorForm()
        {
            InitializeComponent();

            this.Shown += LoadData;

            dataGridView.DataError += DataGridView_DataError;

            addButt.Click += AddButt_Click;
        }

        private void AddButt_Click(object sender, EventArgs e)
        {
            AddRow();

            CheckControlsAndEnableButts();
        }

        private void DataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void LoadData(object sender, EventArgs e)
        {
            dbChooseComboBox.DataSource = GetContextsNames();
        }

        private void DbChooseComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            tableChooseComboBox.DataSource = GetContextEntitiesNames(dbChooseComboBox.SelectedItem as string);
        }

        private void TableChooseComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            dataGridView = BindDataGridView((dbChooseComboBox.Text, tableChooseComboBox.Text, dataGridView));
        }

        private async void DataGridView_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            dataGridView.Rows[e.RowIndex].ErrorText = null;

            try
            {
                await foreach (ValidationResult error in ValidateEntity((dbChooseComboBox.Text, tableChooseComboBox.Text)))
                {
                    dataGridView.Rows[e.RowIndex].ErrorText += error?.ErrorMessage;
                }
            }
            catch (InvalidOperationException)
            {
                dataGridView.Rows[e.RowIndex].ErrorText += "В этой записи данный параметр изменить нельзя.";
            }

            if (!string.IsNullOrEmpty(dataGridView.Rows[e.RowIndex].ErrorText))
            {
                e.Cancel = true;
            }

            CheckControlsAndEnableButts();
        }

        private void CheckControlsAndEnableButts()
        {
            bool allRowsAreValidated = dataGridView.Rows.Cast<DataGridViewRow>().All(r => string.IsNullOrEmpty(r.ErrorText));

            addButt.Enabled = allRowsAreValidated;

            saveButt.Enabled = allRowsAreValidated;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = false;
            base.OnClosing(e);
        }
    }
}