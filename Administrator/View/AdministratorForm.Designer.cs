
namespace RPK.Administrator.View
{
    partial class AdministratorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.dbChooseGroupBox = new System.Windows.Forms.GroupBox();
            this.dbChooseComboBox = new RPK.InterfaceElements.SharedElements.CustomComboBox();
            this.tableChooseGroupBox = new System.Windows.Forms.GroupBox();
            this.tableChooseComboBox = new System.Windows.Forms.ComboBox();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeAccountMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abortLastChangeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel.SuspendLayout();
            this.dbChooseGroupBox.SuspendLayout();
            this.tableChooseGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.dbChooseGroupBox, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.tableChooseGroupBox, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.dataGridView, 0, 2);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 33);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 4;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(940, 477);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // dbChooseGroupBox
            // 
            this.dbChooseGroupBox.Controls.Add(this.dbChooseComboBox);
            this.dbChooseGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dbChooseGroupBox.Location = new System.Drawing.Point(3, 3);
            this.dbChooseGroupBox.Name = "dbChooseGroupBox";
            this.dbChooseGroupBox.Size = new System.Drawing.Size(934, 74);
            this.dbChooseGroupBox.TabIndex = 0;
            this.dbChooseGroupBox.TabStop = false;
            this.dbChooseGroupBox.Text = "Выберите базу данных:";
            // 
            // dbChooseComboBox
            // 
            this.dbChooseComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dbChooseComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dbChooseComboBox.FormattingEnabled = true;
            this.dbChooseComboBox.Location = new System.Drawing.Point(3, 27);
            this.dbChooseComboBox.Name = "dbChooseComboBox";
            this.dbChooseComboBox.Size = new System.Drawing.Size(928, 33);
            this.dbChooseComboBox.TabIndex = 0;
            this.dbChooseComboBox.SelectedValueChanged += new System.EventHandler(this.DbChooseComboBox_SelectedValueChanged);
            // 
            // tableChooseGroupBox
            // 
            this.tableChooseGroupBox.Controls.Add(this.tableChooseComboBox);
            this.tableChooseGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableChooseGroupBox.Location = new System.Drawing.Point(3, 83);
            this.tableChooseGroupBox.Name = "tableChooseGroupBox";
            this.tableChooseGroupBox.Size = new System.Drawing.Size(934, 74);
            this.tableChooseGroupBox.TabIndex = 1;
            this.tableChooseGroupBox.TabStop = false;
            this.tableChooseGroupBox.Text = "Выберите таблицу:";
            // 
            // tableChooseComboBox
            // 
            this.tableChooseComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableChooseComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tableChooseComboBox.FormattingEnabled = true;
            this.tableChooseComboBox.Location = new System.Drawing.Point(3, 27);
            this.tableChooseComboBox.Name = "tableChooseComboBox";
            this.tableChooseComboBox.Size = new System.Drawing.Size(928, 33);
            this.tableChooseComboBox.TabIndex = 0;
            this.tableChooseComboBox.SelectedValueChanged += new System.EventHandler(this.TableChooseComboBox_SelectedValueChanged);
            // 
            // dataGridView
            // 
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(3, 163);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersWidth = 62;
            this.dataGridView.RowTemplate.Height = 33;
            this.dataGridView.Size = new System.Drawing.Size(934, 291);
            this.dataGridView.TabIndex = 2;
            this.dataGridView.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.DataGridView_RowValidating);
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuItem,
            this.abortLastChangeMenuItem,
            this.saveMenuItem,
            this.aboutMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(940, 33);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileMenuItem
            // 
            this.fileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeAccountMenuItem,
            this.exitMenuItem});
            this.fileMenuItem.Name = "fileMenuItem";
            this.fileMenuItem.Size = new System.Drawing.Size(69, 29);
            this.fileMenuItem.Text = "Файл";
            // 
            // changeAccountMenuItem
            // 
            this.changeAccountMenuItem.Name = "changeAccountMenuItem";
            this.changeAccountMenuItem.Size = new System.Drawing.Size(316, 34);
            this.changeAccountMenuItem.Text = "Сменить учётную запись";
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Name = "exitMenuItem";
            this.exitMenuItem.Size = new System.Drawing.Size(316, 34);
            this.exitMenuItem.Text = "Выход";
            // 
            // abortLastChangeMenuItem
            // 
            this.abortLastChangeMenuItem.Name = "abortLastChangeMenuItem";
            this.abortLastChangeMenuItem.Size = new System.Drawing.Size(200, 29);
            this.abortLastChangeMenuItem.Text = "Отменить изменение";
            // 
            // saveMenuItem
            // 
            this.saveMenuItem.Name = "saveMenuItem";
            this.saveMenuItem.Size = new System.Drawing.Size(114, 29);
            this.saveMenuItem.Text = "Сохранить";
            // 
            // aboutMenuItem
            // 
            this.aboutMenuItem.Name = "aboutMenuItem";
            this.aboutMenuItem.Size = new System.Drawing.Size(141, 29);
            this.aboutMenuItem.Text = "О программе";
            // 
            // AdministratorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(940, 510);
            this.Controls.Add(this.tableLayoutPanel);
            this.Controls.Add(this.menuStrip);
            this.Name = "AdministratorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Главное окно";
            this.tableLayoutPanel.ResumeLayout(false);
            this.dbChooseGroupBox.ResumeLayout(false);
            this.tableChooseGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.GroupBox dbChooseGroupBox;
        private System.Windows.Forms.GroupBox tableChooseGroupBox;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeAccountMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abortLastChangeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutMenuItem;
        private InterfaceElements.SharedElements.CustomComboBox dbChooseComboBox;
        private System.Windows.Forms.ComboBox tableChooseComboBox;
    }
}