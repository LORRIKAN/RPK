
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdministratorForm));
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.dbChooseGroupBox = new System.Windows.Forms.GroupBox();
            this.dbChooseComboBox = new RPK.InterfaceElements.SharedElements.CustomComboBox();
            this.tableChooseGroupBox = new System.Windows.Forms.GroupBox();
            this.tableChooseComboBox = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.addButt = new System.Windows.Forms.ToolStripButton();
            this.deleteButt = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.saveButt = new System.Windows.Forms.ToolStripButton();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileButt = new System.Windows.Forms.ToolStripMenuItem();
            this.reloginButt = new System.Windows.Forms.ToolStripMenuItem();
            this.exitButt = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutButt = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel.SuspendLayout();
            this.dbChooseGroupBox.SuspendLayout();
            this.tableChooseGroupBox.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.toolStrip.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.dbChooseGroupBox, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.tableChooseGroupBox, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.panel1, 0, 2);
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
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGridView);
            this.panel1.Controls.Add(this.toolStrip);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 163);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(934, 291);
            this.panel1.TabIndex = 2;
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridView.Location = new System.Drawing.Point(0, 50);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersWidth = 62;
            this.dataGridView.RowTemplate.Height = 33;
            this.dataGridView.Size = new System.Drawing.Size(934, 241);
            this.dataGridView.TabIndex = 1;
            this.dataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_CellValueChanged);
            // 
            // toolStrip
            // 
            this.toolStrip.AutoSize = false;
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addButt,
            this.deleteButt,
            this.toolStripSeparator2,
            this.saveButt,
            this.progressBar});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(934, 50);
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Text = "toolStrip1";
            // 
            // addButt
            // 
            this.addButt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addButt.Image = ((System.Drawing.Image)(resources.GetObject("addButt.Image")));
            this.addButt.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addButt.Name = "addButt";
            this.addButt.Size = new System.Drawing.Size(44, 45);
            this.addButt.Text = "toolStripButton1";
            this.addButt.ToolTipText = "Добавить запись";
            // 
            // deleteButt
            // 
            this.deleteButt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deleteButt.Image = ((System.Drawing.Image)(resources.GetObject("deleteButt.Image")));
            this.deleteButt.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteButt.Name = "deleteButt";
            this.deleteButt.Size = new System.Drawing.Size(44, 45);
            this.deleteButt.Text = "toolStripButton1";
            this.deleteButt.ToolTipText = "Удалить запись";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 50);
            // 
            // saveButt
            // 
            this.saveButt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveButt.Enabled = false;
            this.saveButt.Image = ((System.Drawing.Image)(resources.GetObject("saveButt.Image")));
            this.saveButt.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveButt.Name = "saveButt";
            this.saveButt.Size = new System.Drawing.Size(44, 45);
            this.saveButt.Text = "toolStripButton1";
            this.saveButt.ToolTipText = "Сохранить изменения";
            // 
            // progressBar
            // 
            this.progressBar.AutoSize = false;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(50, 10);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.Visible = false;
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileButt,
            this.aboutButt});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(940, 33);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileButt
            // 
            this.fileButt.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reloginButt,
            this.exitButt});
            this.fileButt.Name = "fileButt";
            this.fileButt.Size = new System.Drawing.Size(69, 29);
            this.fileButt.Text = "Файл";
            // 
            // reloginButt
            // 
            this.reloginButt.Name = "reloginButt";
            this.reloginButt.Size = new System.Drawing.Size(316, 34);
            this.reloginButt.Text = "Сменить учётную запись";
            // 
            // exitButt
            // 
            this.exitButt.Name = "exitButt";
            this.exitButt.Size = new System.Drawing.Size(316, 34);
            this.exitButt.Text = "Выход";
            // 
            // aboutButt
            // 
            this.aboutButt.Name = "aboutButt";
            this.aboutButt.Size = new System.Drawing.Size(141, 29);
            this.aboutButt.Text = "О программе";
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
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tableLayoutPanel.ResumeLayout(false);
            this.dbChooseGroupBox.ResumeLayout(false);
            this.tableChooseGroupBox.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.GroupBox dbChooseGroupBox;
        private System.Windows.Forms.GroupBox tableChooseGroupBox;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem reloginButt;
        private InterfaceElements.SharedElements.CustomComboBox dbChooseComboBox;
        private System.Windows.Forms.ComboBox tableChooseComboBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton addButt;
        private System.Windows.Forms.ToolStripButton deleteButt;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton saveButt;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.ToolStripMenuItem fileButt;
        private System.Windows.Forms.ToolStripMenuItem exitButt;
        private System.Windows.Forms.ToolStripMenuItem aboutButt;
    }
}