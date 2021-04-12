﻿namespace RPK.View
{
    partial class ResearcherForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResearcherForm));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.inputParametersPage = new System.Windows.Forms.TabPage();
            this.inputParametersLayout = new System.Windows.Forms.TableLayoutPanel();
            this.materialPropertiesGroupBox = new System.Windows.Forms.GroupBox();
            this.materialPropertiesLayout = new System.Windows.Forms.TableLayoutPanel();
            this.canalChooseGroupBox = new System.Windows.Forms.GroupBox();
            this.canalChooseComboBox = new RPK.InterfaceElements.CustomComboBox();
            this.materialChooseGroupBox = new System.Windows.Forms.GroupBox();
            this.materialChooseComboBox = new RPK.InterfaceElements.CustomComboBox();
            this.canalGeometryParametersGroupBox = new System.Windows.Forms.GroupBox();
            this.canalGeometryParametersLayout = new System.Windows.Forms.TableLayoutPanel();
            this.variableParametersPage = new System.Windows.Forms.TabPage();
            this.variableParametersLayout = new System.Windows.Forms.TableLayoutPanel();
            this.mathModelParametersPage = new System.Windows.Forms.TabPage();
            this.mathModelLayout = new System.Windows.Forms.TableLayoutPanel();
            this.empiricalCoefficientsOfMathModelGroupBox = new System.Windows.Forms.GroupBox();
            this.empiricalCoefficientsOfMathModelLayout = new System.Windows.Forms.TableLayoutPanel();
            this.solvingMethodParametersGroupBox = new System.Windows.Forms.GroupBox();
            this.solvingMethodParametersLayout = new System.Windows.Forms.TableLayoutPanel();
            this.resultsPage = new System.Windows.Forms.TabPage();
            this.resultsLayout = new System.Windows.Forms.TableLayoutPanel();
            this.resultsTableGroupBox = new System.Windows.Forms.GroupBox();
            this.resultsTable = new System.Windows.Forms.DataGridView();
            this.coordinateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.temperatureColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.viscosityColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.temperaturePlot = new ScottPlot.FormsPlot();
            this.viscosityPlot = new ScottPlot.FormsPlot();
            this.discreteResultsLayout = new System.Windows.Forms.TableLayoutPanel();
            this.qualityIndicatorsGroupBox = new System.Windows.Forms.GroupBox();
            this.qualityIndicatorsLayout = new System.Windows.Forms.TableLayoutPanel();
            this.productTemperatureOutput = new RPK.InterfaceElements.ParameterOutput();
            this.productViscosityOutput = new RPK.InterfaceElements.ParameterOutput();
            this.productivityAndPerfomanceLayout = new System.Windows.Forms.TableLayoutPanel();
            this.canalProductivityOutput = new RPK.InterfaceElements.ParameterOutput();
            this.programPerfomanceGroupBox = new System.Windows.Forms.GroupBox();
            this.programPerfomanceLayout = new System.Windows.Forms.TableLayoutPanel();
            this.programWorkTimeOutput = new RPK.InterfaceElements.ParameterOutput();
            this.programOccupiedRAMOutput = new RPK.InterfaceElements.ParameterOutput();
            this.tabPagesImageList = new System.Windows.Forms.ImageList(this.components);
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportResultsStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeAccountStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.calculateStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.backgroundInputControlsFiller = new System.ComponentModel.BackgroundWorker();
            this.calculationBackgroundProcessor = new System.ComponentModel.BackgroundWorker();
            this.tabControl.SuspendLayout();
            this.inputParametersPage.SuspendLayout();
            this.inputParametersLayout.SuspendLayout();
            this.materialPropertiesGroupBox.SuspendLayout();
            this.canalChooseGroupBox.SuspendLayout();
            this.materialChooseGroupBox.SuspendLayout();
            this.canalGeometryParametersGroupBox.SuspendLayout();
            this.variableParametersPage.SuspendLayout();
            this.mathModelParametersPage.SuspendLayout();
            this.mathModelLayout.SuspendLayout();
            this.empiricalCoefficientsOfMathModelGroupBox.SuspendLayout();
            this.solvingMethodParametersGroupBox.SuspendLayout();
            this.resultsPage.SuspendLayout();
            this.resultsLayout.SuspendLayout();
            this.resultsTableGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resultsTable)).BeginInit();
            this.discreteResultsLayout.SuspendLayout();
            this.qualityIndicatorsGroupBox.SuspendLayout();
            this.qualityIndicatorsLayout.SuspendLayout();
            this.productivityAndPerfomanceLayout.SuspendLayout();
            this.programPerfomanceGroupBox.SuspendLayout();
            this.programPerfomanceLayout.SuspendLayout();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.inputParametersPage);
            this.tabControl.Controls.Add(this.variableParametersPage);
            this.tabControl.Controls.Add(this.mathModelParametersPage);
            this.tabControl.Controls.Add(this.resultsPage);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.ImageList = this.tabPagesImageList;
            this.tabControl.Location = new System.Drawing.Point(0, 33);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1390, 603);
            this.tabControl.TabIndex = 0;
            // 
            // inputParametersPage
            // 
            this.inputParametersPage.Controls.Add(this.inputParametersLayout);
            this.inputParametersPage.Location = new System.Drawing.Point(4, 34);
            this.inputParametersPage.Name = "inputParametersPage";
            this.inputParametersPage.Padding = new System.Windows.Forms.Padding(3);
            this.inputParametersPage.Size = new System.Drawing.Size(1382, 565);
            this.inputParametersPage.TabIndex = 0;
            this.inputParametersPage.Text = "Входные параметры";
            this.inputParametersPage.UseVisualStyleBackColor = true;
            // 
            // inputParametersLayout
            // 
            this.inputParametersLayout.ColumnCount = 2;
            this.inputParametersLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.inputParametersLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.inputParametersLayout.Controls.Add(this.materialPropertiesGroupBox, 1, 1);
            this.inputParametersLayout.Controls.Add(this.canalChooseGroupBox, 0, 0);
            this.inputParametersLayout.Controls.Add(this.materialChooseGroupBox, 1, 0);
            this.inputParametersLayout.Controls.Add(this.canalGeometryParametersGroupBox, 0, 1);
            this.inputParametersLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputParametersLayout.Location = new System.Drawing.Point(3, 3);
            this.inputParametersLayout.Name = "inputParametersLayout";
            this.inputParametersLayout.RowCount = 2;
            this.inputParametersLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.95349F));
            this.inputParametersLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 86.04651F));
            this.inputParametersLayout.Size = new System.Drawing.Size(1376, 559);
            this.inputParametersLayout.TabIndex = 0;
            // 
            // materialPropertiesGroupBox
            // 
            this.materialPropertiesGroupBox.Controls.Add(this.materialPropertiesLayout);
            this.materialPropertiesGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialPropertiesGroupBox.Location = new System.Drawing.Point(691, 81);
            this.materialPropertiesGroupBox.Name = "materialPropertiesGroupBox";
            this.materialPropertiesGroupBox.Size = new System.Drawing.Size(682, 475);
            this.materialPropertiesGroupBox.TabIndex = 3;
            this.materialPropertiesGroupBox.TabStop = false;
            this.materialPropertiesGroupBox.Text = "Параметры свойств материала";
            // 
            // materialPropertiesLayout
            // 
            this.materialPropertiesLayout.ColumnCount = 1;
            this.materialPropertiesLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.materialPropertiesLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialPropertiesLayout.Location = new System.Drawing.Point(3, 27);
            this.materialPropertiesLayout.Name = "materialPropertiesLayout";
            this.materialPropertiesLayout.RowCount = 1;
            this.materialPropertiesLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.materialPropertiesLayout.Size = new System.Drawing.Size(676, 445);
            this.materialPropertiesLayout.TabIndex = 0;
            // 
            // canalChooseGroupBox
            // 
            this.canalChooseGroupBox.Controls.Add(this.canalChooseComboBox);
            this.canalChooseGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canalChooseGroupBox.Location = new System.Drawing.Point(3, 3);
            this.canalChooseGroupBox.Name = "canalChooseGroupBox";
            this.canalChooseGroupBox.Size = new System.Drawing.Size(682, 72);
            this.canalChooseGroupBox.TabIndex = 0;
            this.canalChooseGroupBox.TabStop = false;
            this.canalChooseGroupBox.Text = "Выберите тип канала";
            // 
            // canalChooseComboBox
            // 
            this.canalChooseComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canalChooseComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.canalChooseComboBox.FormattingEnabled = true;
            this.canalChooseComboBox.Location = new System.Drawing.Point(3, 27);
            this.canalChooseComboBox.Name = "canalChooseComboBox";
            this.canalChooseComboBox.Size = new System.Drawing.Size(676, 33);
            this.canalChooseComboBox.TabIndex = 0;
            // 
            // materialChooseGroupBox
            // 
            this.materialChooseGroupBox.Controls.Add(this.materialChooseComboBox);
            this.materialChooseGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialChooseGroupBox.Location = new System.Drawing.Point(691, 3);
            this.materialChooseGroupBox.Name = "materialChooseGroupBox";
            this.materialChooseGroupBox.Size = new System.Drawing.Size(682, 72);
            this.materialChooseGroupBox.TabIndex = 1;
            this.materialChooseGroupBox.TabStop = false;
            this.materialChooseGroupBox.Text = "Выберите тип материала";
            // 
            // materialChooseComboBox
            // 
            this.materialChooseComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialChooseComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.materialChooseComboBox.FormattingEnabled = true;
            this.materialChooseComboBox.Location = new System.Drawing.Point(3, 27);
            this.materialChooseComboBox.Name = "materialChooseComboBox";
            this.materialChooseComboBox.Size = new System.Drawing.Size(676, 33);
            this.materialChooseComboBox.TabIndex = 0;
            // 
            // canalGeometryParametersGroupBox
            // 
            this.canalGeometryParametersGroupBox.Controls.Add(this.canalGeometryParametersLayout);
            this.canalGeometryParametersGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canalGeometryParametersGroupBox.Location = new System.Drawing.Point(3, 81);
            this.canalGeometryParametersGroupBox.Name = "canalGeometryParametersGroupBox";
            this.canalGeometryParametersGroupBox.Size = new System.Drawing.Size(682, 475);
            this.canalGeometryParametersGroupBox.TabIndex = 2;
            this.canalGeometryParametersGroupBox.TabStop = false;
            this.canalGeometryParametersGroupBox.Text = "Геометрические параметры канала";
            // 
            // canalGeometryParametersLayout
            // 
            this.canalGeometryParametersLayout.ColumnCount = 1;
            this.canalGeometryParametersLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.canalGeometryParametersLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canalGeometryParametersLayout.Location = new System.Drawing.Point(3, 27);
            this.canalGeometryParametersLayout.Name = "canalGeometryParametersLayout";
            this.canalGeometryParametersLayout.RowCount = 1;
            this.canalGeometryParametersLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.canalGeometryParametersLayout.Size = new System.Drawing.Size(676, 445);
            this.canalGeometryParametersLayout.TabIndex = 0;
            // 
            // variableParametersPage
            // 
            this.variableParametersPage.Controls.Add(this.variableParametersLayout);
            this.variableParametersPage.Location = new System.Drawing.Point(4, 34);
            this.variableParametersPage.Name = "variableParametersPage";
            this.variableParametersPage.Padding = new System.Windows.Forms.Padding(3);
            this.variableParametersPage.Size = new System.Drawing.Size(1382, 565);
            this.variableParametersPage.TabIndex = 1;
            this.variableParametersPage.Text = "Варьируемые параметры";
            this.variableParametersPage.UseVisualStyleBackColor = true;
            // 
            // variableParametersLayout
            // 
            this.variableParametersLayout.ColumnCount = 1;
            this.variableParametersLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.variableParametersLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.variableParametersLayout.Location = new System.Drawing.Point(3, 3);
            this.variableParametersLayout.Name = "variableParametersLayout";
            this.variableParametersLayout.RowCount = 1;
            this.variableParametersLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.variableParametersLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 559F));
            this.variableParametersLayout.Size = new System.Drawing.Size(1376, 559);
            this.variableParametersLayout.TabIndex = 0;
            // 
            // mathModelParametersPage
            // 
            this.mathModelParametersPage.Controls.Add(this.mathModelLayout);
            this.mathModelParametersPage.Location = new System.Drawing.Point(4, 34);
            this.mathModelParametersPage.Name = "mathModelParametersPage";
            this.mathModelParametersPage.Padding = new System.Windows.Forms.Padding(3);
            this.mathModelParametersPage.Size = new System.Drawing.Size(1382, 565);
            this.mathModelParametersPage.TabIndex = 2;
            this.mathModelParametersPage.Text = "Параметры математической модели";
            this.mathModelParametersPage.UseVisualStyleBackColor = true;
            // 
            // mathModelLayout
            // 
            this.mathModelLayout.ColumnCount = 2;
            this.mathModelLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.mathModelLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.mathModelLayout.Controls.Add(this.empiricalCoefficientsOfMathModelGroupBox, 0, 0);
            this.mathModelLayout.Controls.Add(this.solvingMethodParametersGroupBox, 1, 0);
            this.mathModelLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mathModelLayout.Location = new System.Drawing.Point(3, 3);
            this.mathModelLayout.Name = "mathModelLayout";
            this.mathModelLayout.RowCount = 1;
            this.mathModelLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mathModelLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 559F));
            this.mathModelLayout.Size = new System.Drawing.Size(1376, 559);
            this.mathModelLayout.TabIndex = 0;
            // 
            // empiricalCoefficientsOfMathModelGroupBox
            // 
            this.empiricalCoefficientsOfMathModelGroupBox.Controls.Add(this.empiricalCoefficientsOfMathModelLayout);
            this.empiricalCoefficientsOfMathModelGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.empiricalCoefficientsOfMathModelGroupBox.Location = new System.Drawing.Point(3, 3);
            this.empiricalCoefficientsOfMathModelGroupBox.Name = "empiricalCoefficientsOfMathModelGroupBox";
            this.empiricalCoefficientsOfMathModelGroupBox.Size = new System.Drawing.Size(682, 553);
            this.empiricalCoefficientsOfMathModelGroupBox.TabIndex = 0;
            this.empiricalCoefficientsOfMathModelGroupBox.TabStop = false;
            this.empiricalCoefficientsOfMathModelGroupBox.Text = "Эмпирические коэффициенты математической модели";
            // 
            // empiricalCoefficientsOfMathModelLayout
            // 
            this.empiricalCoefficientsOfMathModelLayout.ColumnCount = 1;
            this.empiricalCoefficientsOfMathModelLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.empiricalCoefficientsOfMathModelLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.empiricalCoefficientsOfMathModelLayout.Location = new System.Drawing.Point(3, 27);
            this.empiricalCoefficientsOfMathModelLayout.Name = "empiricalCoefficientsOfMathModelLayout";
            this.empiricalCoefficientsOfMathModelLayout.RowCount = 1;
            this.empiricalCoefficientsOfMathModelLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.empiricalCoefficientsOfMathModelLayout.Size = new System.Drawing.Size(676, 523);
            this.empiricalCoefficientsOfMathModelLayout.TabIndex = 0;
            // 
            // solvingMethodParametersGroupBox
            // 
            this.solvingMethodParametersGroupBox.Controls.Add(this.solvingMethodParametersLayout);
            this.solvingMethodParametersGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.solvingMethodParametersGroupBox.Location = new System.Drawing.Point(691, 3);
            this.solvingMethodParametersGroupBox.Name = "solvingMethodParametersGroupBox";
            this.solvingMethodParametersGroupBox.Size = new System.Drawing.Size(682, 553);
            this.solvingMethodParametersGroupBox.TabIndex = 1;
            this.solvingMethodParametersGroupBox.TabStop = false;
            this.solvingMethodParametersGroupBox.Text = "Параметры метода решения";
            // 
            // solvingMethodParametersLayout
            // 
            this.solvingMethodParametersLayout.ColumnCount = 1;
            this.solvingMethodParametersLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.solvingMethodParametersLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.solvingMethodParametersLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.solvingMethodParametersLayout.Location = new System.Drawing.Point(3, 27);
            this.solvingMethodParametersLayout.Name = "solvingMethodParametersLayout";
            this.solvingMethodParametersLayout.RowCount = 1;
            this.solvingMethodParametersLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.solvingMethodParametersLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 523F));
            this.solvingMethodParametersLayout.Size = new System.Drawing.Size(676, 523);
            this.solvingMethodParametersLayout.TabIndex = 0;
            // 
            // resultsPage
            // 
            this.resultsPage.Controls.Add(this.resultsLayout);
            this.resultsPage.Location = new System.Drawing.Point(4, 34);
            this.resultsPage.Name = "resultsPage";
            this.resultsPage.Padding = new System.Windows.Forms.Padding(3);
            this.resultsPage.Size = new System.Drawing.Size(1382, 565);
            this.resultsPage.TabIndex = 3;
            this.resultsPage.Text = "Результаты";
            this.resultsPage.UseVisualStyleBackColor = true;
            // 
            // resultsLayout
            // 
            this.resultsLayout.ColumnCount = 2;
            this.resultsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.resultsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.resultsLayout.Controls.Add(this.resultsTableGroupBox, 0, 0);
            this.resultsLayout.Controls.Add(this.temperaturePlot, 1, 0);
            this.resultsLayout.Controls.Add(this.viscosityPlot, 1, 1);
            this.resultsLayout.Controls.Add(this.discreteResultsLayout, 0, 1);
            this.resultsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultsLayout.Location = new System.Drawing.Point(3, 3);
            this.resultsLayout.Name = "resultsLayout";
            this.resultsLayout.RowCount = 2;
            this.resultsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.resultsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.resultsLayout.Size = new System.Drawing.Size(1376, 559);
            this.resultsLayout.TabIndex = 0;
            // 
            // resultsTableGroupBox
            // 
            this.resultsTableGroupBox.Controls.Add(this.resultsTable);
            this.resultsTableGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultsTableGroupBox.Location = new System.Drawing.Point(3, 3);
            this.resultsTableGroupBox.Name = "resultsTableGroupBox";
            this.resultsTableGroupBox.Size = new System.Drawing.Size(682, 273);
            this.resultsTableGroupBox.TabIndex = 0;
            this.resultsTableGroupBox.TabStop = false;
            this.resultsTableGroupBox.Text = "Таблица результатов";
            // 
            // resultsTable
            // 
            this.resultsTable.AllowUserToAddRows = false;
            this.resultsTable.AllowUserToDeleteRows = false;
            this.resultsTable.AllowUserToOrderColumns = true;
            this.resultsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.resultsTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.coordinateColumn,
            this.temperatureColumn,
            this.viscosityColumn});
            this.resultsTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultsTable.Location = new System.Drawing.Point(3, 27);
            this.resultsTable.Name = "resultsTable";
            this.resultsTable.ReadOnly = true;
            this.resultsTable.RowHeadersWidth = 62;
            this.resultsTable.RowTemplate.Height = 33;
            this.resultsTable.Size = new System.Drawing.Size(676, 243);
            this.resultsTable.TabIndex = 0;
            // 
            // coordinateColumn
            // 
            this.coordinateColumn.HeaderText = "Координата по длине канала, м";
            this.coordinateColumn.MinimumWidth = 8;
            this.coordinateColumn.Name = "coordinateColumn";
            this.coordinateColumn.ReadOnly = true;
            this.coordinateColumn.Width = 150;
            // 
            // temperatureColumn
            // 
            this.temperatureColumn.HeaderText = "Температура, °C";
            this.temperatureColumn.MinimumWidth = 8;
            this.temperatureColumn.Name = "temperatureColumn";
            this.temperatureColumn.ReadOnly = true;
            this.temperatureColumn.Width = 150;
            // 
            // viscosityColumn
            // 
            this.viscosityColumn.HeaderText = "Вязкость, Па⋅с";
            this.viscosityColumn.MinimumWidth = 8;
            this.viscosityColumn.Name = "viscosityColumn";
            this.viscosityColumn.ReadOnly = true;
            this.viscosityColumn.Width = 150;
            // 
            // temperaturePlot
            // 
            this.temperaturePlot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.temperaturePlot.Location = new System.Drawing.Point(693, 6);
            this.temperaturePlot.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.temperaturePlot.Name = "temperaturePlot";
            this.temperaturePlot.Size = new System.Drawing.Size(678, 267);
            this.temperaturePlot.TabIndex = 1;
            // 
            // viscosityPlot
            // 
            this.viscosityPlot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viscosityPlot.Location = new System.Drawing.Point(693, 285);
            this.viscosityPlot.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.viscosityPlot.Name = "viscosityPlot";
            this.viscosityPlot.Size = new System.Drawing.Size(678, 268);
            this.viscosityPlot.TabIndex = 2;
            // 
            // discreteResultsLayout
            // 
            this.discreteResultsLayout.ColumnCount = 2;
            this.discreteResultsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.discreteResultsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.discreteResultsLayout.Controls.Add(this.qualityIndicatorsGroupBox, 0, 0);
            this.discreteResultsLayout.Controls.Add(this.productivityAndPerfomanceLayout, 1, 0);
            this.discreteResultsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.discreteResultsLayout.Location = new System.Drawing.Point(3, 282);
            this.discreteResultsLayout.Name = "discreteResultsLayout";
            this.discreteResultsLayout.RowCount = 1;
            this.discreteResultsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.discreteResultsLayout.Size = new System.Drawing.Size(682, 274);
            this.discreteResultsLayout.TabIndex = 3;
            // 
            // qualityIndicatorsGroupBox
            // 
            this.qualityIndicatorsGroupBox.Controls.Add(this.qualityIndicatorsLayout);
            this.qualityIndicatorsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.qualityIndicatorsGroupBox.Location = new System.Drawing.Point(3, 3);
            this.qualityIndicatorsGroupBox.Name = "qualityIndicatorsGroupBox";
            this.qualityIndicatorsGroupBox.Size = new System.Drawing.Size(335, 268);
            this.qualityIndicatorsGroupBox.TabIndex = 6;
            this.qualityIndicatorsGroupBox.TabStop = false;
            this.qualityIndicatorsGroupBox.Text = "Показатели качества";
            // 
            // qualityIndicatorsLayout
            // 
            this.qualityIndicatorsLayout.ColumnCount = 1;
            this.qualityIndicatorsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.qualityIndicatorsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.qualityIndicatorsLayout.Controls.Add(this.productTemperatureOutput, 0, 0);
            this.qualityIndicatorsLayout.Controls.Add(this.productViscosityOutput, 0, 1);
            this.qualityIndicatorsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.qualityIndicatorsLayout.Location = new System.Drawing.Point(3, 27);
            this.qualityIndicatorsLayout.Name = "qualityIndicatorsLayout";
            this.qualityIndicatorsLayout.RowCount = 2;
            this.qualityIndicatorsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.qualityIndicatorsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.qualityIndicatorsLayout.Size = new System.Drawing.Size(329, 238);
            this.qualityIndicatorsLayout.TabIndex = 0;
            // 
            // productTemperatureOutput
            // 
            this.productTemperatureOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.productTemperatureOutput.Location = new System.Drawing.Point(3, 3);
            this.productTemperatureOutput.MeasureUnit = "°C";
            this.productTemperatureOutput.Name = "productTemperatureOutput";
            this.productTemperatureOutput.ParameterName = "Температура продукта";
            this.productTemperatureOutput.Size = new System.Drawing.Size(323, 113);
            this.productTemperatureOutput.TabIndex = 0;
            this.productTemperatureOutput.Value = null;
            // 
            // productViscosityOutput
            // 
            this.productViscosityOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.productViscosityOutput.Location = new System.Drawing.Point(3, 122);
            this.productViscosityOutput.MeasureUnit = "Па⋅с";
            this.productViscosityOutput.Name = "productViscosityOutput";
            this.productViscosityOutput.ParameterName = "Вязкость продукта";
            this.productViscosityOutput.Size = new System.Drawing.Size(323, 113);
            this.productViscosityOutput.TabIndex = 1;
            this.productViscosityOutput.Value = null;
            // 
            // productivityAndPerfomanceLayout
            // 
            this.productivityAndPerfomanceLayout.ColumnCount = 1;
            this.productivityAndPerfomanceLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.productivityAndPerfomanceLayout.Controls.Add(this.canalProductivityOutput, 0, 0);
            this.productivityAndPerfomanceLayout.Controls.Add(this.programPerfomanceGroupBox, 0, 1);
            this.productivityAndPerfomanceLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.productivityAndPerfomanceLayout.Location = new System.Drawing.Point(344, 3);
            this.productivityAndPerfomanceLayout.Name = "productivityAndPerfomanceLayout";
            this.productivityAndPerfomanceLayout.RowCount = 2;
            this.productivityAndPerfomanceLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.productivityAndPerfomanceLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 66.66666F));
            this.productivityAndPerfomanceLayout.Size = new System.Drawing.Size(335, 268);
            this.productivityAndPerfomanceLayout.TabIndex = 7;
            // 
            // canalProductivityOutput
            // 
            this.canalProductivityOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canalProductivityOutput.Location = new System.Drawing.Point(3, 3);
            this.canalProductivityOutput.MeasureUnit = "кг/ч";
            this.canalProductivityOutput.Name = "canalProductivityOutput";
            this.canalProductivityOutput.ParameterName = "Производительность канала";
            this.canalProductivityOutput.Size = new System.Drawing.Size(329, 83);
            this.canalProductivityOutput.TabIndex = 0;
            this.canalProductivityOutput.Value = null;
            // 
            // programPerfomanceGroupBox
            // 
            this.programPerfomanceGroupBox.Controls.Add(this.programPerfomanceLayout);
            this.programPerfomanceGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.programPerfomanceGroupBox.Location = new System.Drawing.Point(3, 92);
            this.programPerfomanceGroupBox.Name = "programPerfomanceGroupBox";
            this.programPerfomanceGroupBox.Size = new System.Drawing.Size(329, 173);
            this.programPerfomanceGroupBox.TabIndex = 1;
            this.programPerfomanceGroupBox.TabStop = false;
            this.programPerfomanceGroupBox.Text = "Производительность программы";
            // 
            // programPerfomanceLayout
            // 
            this.programPerfomanceLayout.ColumnCount = 1;
            this.programPerfomanceLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.programPerfomanceLayout.Controls.Add(this.programWorkTimeOutput, 0, 0);
            this.programPerfomanceLayout.Controls.Add(this.programOccupiedRAMOutput, 0, 1);
            this.programPerfomanceLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.programPerfomanceLayout.Location = new System.Drawing.Point(3, 27);
            this.programPerfomanceLayout.Name = "programPerfomanceLayout";
            this.programPerfomanceLayout.RowCount = 2;
            this.programPerfomanceLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.programPerfomanceLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.programPerfomanceLayout.Size = new System.Drawing.Size(323, 143);
            this.programPerfomanceLayout.TabIndex = 0;
            // 
            // programWorkTimeOutput
            // 
            this.programWorkTimeOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.programWorkTimeOutput.Location = new System.Drawing.Point(3, 3);
            this.programWorkTimeOutput.MeasureUnit = "мс";
            this.programWorkTimeOutput.Name = "programWorkTimeOutput";
            this.programWorkTimeOutput.ParameterName = "Время расчётов";
            this.programWorkTimeOutput.Size = new System.Drawing.Size(317, 65);
            this.programWorkTimeOutput.TabIndex = 0;
            this.programWorkTimeOutput.Value = null;
            // 
            // programOccupiedRAMOutput
            // 
            this.programOccupiedRAMOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.programOccupiedRAMOutput.Location = new System.Drawing.Point(3, 74);
            this.programOccupiedRAMOutput.MeasureUnit = "мб";
            this.programOccupiedRAMOutput.Name = "programOccupiedRAMOutput";
            this.programOccupiedRAMOutput.ParameterName = "Занимаемая оперативная память";
            this.programOccupiedRAMOutput.Size = new System.Drawing.Size(317, 66);
            this.programOccupiedRAMOutput.TabIndex = 1;
            this.programOccupiedRAMOutput.Value = null;
            // 
            // tabPagesImageList
            // 
            this.tabPagesImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.tabPagesImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("tabPagesImageList.ImageStream")));
            this.tabPagesImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.tabPagesImageList.Images.SetKeyName(0, "ok.png");
            this.tabPagesImageList.Images.SetKeyName(1, "editing.png");
            this.tabPagesImageList.Images.SetKeyName(2, "error.png");
            this.tabPagesImageList.Images.SetKeyName(3, "incomplete.png");
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileStripMenuItem,
            this.calculateStripMenuItem,
            this.helpStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1390, 33);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileStripMenuItem
            // 
            this.fileStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportResultsStripMenuItem,
            this.changeAccountStripMenuItem,
            this.exitStripMenuItem});
            this.fileStripMenuItem.Name = "fileStripMenuItem";
            this.fileStripMenuItem.Size = new System.Drawing.Size(69, 29);
            this.fileStripMenuItem.Text = "Файл";
            // 
            // exportResultsStripMenuItem
            // 
            this.exportResultsStripMenuItem.Name = "exportResultsStripMenuItem";
            this.exportResultsStripMenuItem.Size = new System.Drawing.Size(344, 34);
            this.exportResultsStripMenuItem.Text = "Экспортировать результаты";
            // 
            // changeAccountStripMenuItem
            // 
            this.changeAccountStripMenuItem.Name = "changeAccountStripMenuItem";
            this.changeAccountStripMenuItem.Size = new System.Drawing.Size(344, 34);
            this.changeAccountStripMenuItem.Text = "Сменить учётную запись";
            // 
            // exitStripMenuItem
            // 
            this.exitStripMenuItem.Name = "exitStripMenuItem";
            this.exitStripMenuItem.Size = new System.Drawing.Size(344, 34);
            this.exitStripMenuItem.Text = "Выход";
            // 
            // calculateStripMenuItem
            // 
            this.calculateStripMenuItem.Enabled = false;
            this.calculateStripMenuItem.Name = "calculateStripMenuItem";
            this.calculateStripMenuItem.Size = new System.Drawing.Size(115, 29);
            this.calculateStripMenuItem.Text = "Рассчитать";
            // 
            // helpStripMenuItem
            // 
            this.helpStripMenuItem.Name = "helpStripMenuItem";
            this.helpStripMenuItem.Size = new System.Drawing.Size(141, 29);
            this.helpStripMenuItem.Text = "О программе";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // backgroundInputControlsFiller
            // 
            this.backgroundInputControlsFiller.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundInputControlsFiller_DoWork);
            this.backgroundInputControlsFiller.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundInputControlsFiller_RunWorkerCompleted);
            // 
            // calculationBackgroundProcessor
            // 
            this.calculationBackgroundProcessor.WorkerReportsProgress = true;
            this.calculationBackgroundProcessor.WorkerSupportsCancellation = true;
            this.calculationBackgroundProcessor.DoWork += new System.ComponentModel.DoWorkEventHandler(this.CalculationBackgroundProcessor_DoWork);
            this.calculationBackgroundProcessor.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.CalculationBackgroundProcessor_ProgressChanged);
            this.calculationBackgroundProcessor.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.CalculationBackgroundProcessor_RunWorkerCompleted);
            // 
            // ResearcherForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1390, 636);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "ResearcherForm";
            this.Text = "Главное окно";
            this.tabControl.ResumeLayout(false);
            this.inputParametersPage.ResumeLayout(false);
            this.inputParametersLayout.ResumeLayout(false);
            this.materialPropertiesGroupBox.ResumeLayout(false);
            this.canalChooseGroupBox.ResumeLayout(false);
            this.materialChooseGroupBox.ResumeLayout(false);
            this.canalGeometryParametersGroupBox.ResumeLayout(false);
            this.variableParametersPage.ResumeLayout(false);
            this.mathModelParametersPage.ResumeLayout(false);
            this.mathModelLayout.ResumeLayout(false);
            this.empiricalCoefficientsOfMathModelGroupBox.ResumeLayout(false);
            this.solvingMethodParametersGroupBox.ResumeLayout(false);
            this.resultsPage.ResumeLayout(false);
            this.resultsLayout.ResumeLayout(false);
            this.resultsTableGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.resultsTable)).EndInit();
            this.discreteResultsLayout.ResumeLayout(false);
            this.qualityIndicatorsGroupBox.ResumeLayout(false);
            this.qualityIndicatorsLayout.ResumeLayout(false);
            this.productivityAndPerfomanceLayout.ResumeLayout(false);
            this.programPerfomanceGroupBox.ResumeLayout(false);
            this.programPerfomanceLayout.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage inputParametersPage;
        private System.Windows.Forms.TabPage variableParametersPage;
        private System.Windows.Forms.TabPage mathModelParametersPage;
        private System.Windows.Forms.TabPage resultsPage;
        private System.Windows.Forms.TableLayoutPanel inputParametersLayout;
        private System.Windows.Forms.TableLayoutPanel mathModelLayout;
        private System.Windows.Forms.GroupBox empiricalCoefficientsOfMathModelGroupBox;
        private System.Windows.Forms.GroupBox solvingMethodParametersGroupBox;
        private System.Windows.Forms.TableLayoutPanel solvingMethodParametersLayout;
        private System.Windows.Forms.TableLayoutPanel resultsLayout;
        private System.Windows.Forms.GroupBox resultsTableGroupBox;
        private System.Windows.Forms.DataGridView resultsTable;
        private ScottPlot.FormsPlot temperaturePlot;
        private ScottPlot.FormsPlot viscosityPlot;
        private System.Windows.Forms.TableLayoutPanel discreteResultsLayout;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportResultsStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeAccountStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem calculateStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn coordinateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn temperatureColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn viscosityColumn;
        private InterfaceElements.ParameterOutput productTemperatureOutput;
        private InterfaceElements.ParameterOutput productViscosityOutput;
        private System.Windows.Forms.GroupBox qualityIndicatorsGroupBox;
        private System.Windows.Forms.TableLayoutPanel qualityIndicatorsLayout;
        private System.Windows.Forms.TableLayoutPanel productivityAndPerfomanceLayout;
        private InterfaceElements.ParameterOutput canalProductivityOutput;
        private System.Windows.Forms.GroupBox programPerfomanceGroupBox;
        private System.Windows.Forms.TableLayoutPanel programPerfomanceLayout;
        private InterfaceElements.ParameterOutput programWorkTimeOutput;
        private InterfaceElements.ParameterOutput programOccupiedRAMOutput;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.ImageList tabPagesImageList;
        private System.Windows.Forms.GroupBox canalChooseGroupBox;
        private System.Windows.Forms.GroupBox materialChooseGroupBox;
        private System.Windows.Forms.GroupBox canalGeometryParametersGroupBox;
        private System.Windows.Forms.TableLayoutPanel canalGeometryParametersLayout;
        private System.Windows.Forms.GroupBox materialPropertiesGroupBox;
        private System.Windows.Forms.TableLayoutPanel materialPropertiesLayout;
        private System.Windows.Forms.TableLayoutPanel variableParametersLayout;
        private System.Windows.Forms.TableLayoutPanel empiricalCoefficientsOfMathModelLayout;
        private System.ComponentModel.BackgroundWorker backgroundInputControlsFiller;
        private InterfaceElements.CustomComboBox canalChooseComboBox;
        private InterfaceElements.CustomComboBox materialChooseComboBox;
        private System.ComponentModel.BackgroundWorker calculationBackgroundProcessor;
    }
}

