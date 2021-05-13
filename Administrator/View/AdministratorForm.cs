using Microsoft.EntityFrameworkCore;
using Repository;
using System;
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

        public AdministratorForm()
        {
            InitializeComponent();

            this.Shown += LoadData;

            dataGridView.DataError += DataGridView_DataError;
        }

        private void DataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            
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

            await foreach (ValidationResult error in ValidateEntity((dbChooseComboBox.Text, tableChooseComboBox.Text)))
            {
                dataGridView.Rows[e.RowIndex].ErrorText += error.ErrorMessage;
            }

            if (!string.IsNullOrEmpty(dataGridView.Rows[e.RowIndex].ErrorText))
                e.Cancel = true;
        }
    }
}