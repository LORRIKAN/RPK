#nullable enable
using RPK.InterfaceElements;
using RPK.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPK.View
{
    public delegate void CalculationFunc(ref CalculationParameters calculationParameters, out CalculationResults calculationResults);

    public partial class ResearcherForm : Form
    {
        public ResearcherForm()
        {
            InitializeComponent();

            exitStripMenuItem.Click += (sender, e) => this.Close();

            tabControl.Selected += (sender, e) =>
            {
                foreach (TabPage tabPage in PagesStatuses.Keys)
                    ChangeTabPageStatus(tabPage);
            };
            PagesStatuses.Add(inputParametersPage, TabPageStatus.Incomplete);
            PagesStatuses.Add(variableParametersPage, TabPageStatus.Incomplete);
            PagesStatuses.Add(mathModelParametersPage, TabPageStatus.Incomplete);

            temperaturePlot.plt.Title("График зависимости температуры материала от длины канала");
            temperaturePlot.plt.XLabel("Длина канала (м)");
            temperaturePlot.plt.YLabel("Температура материала (°C)");

            viscosityPlot.plt.Title("График зависимости вязкости материала от длины канала");
            viscosityPlot.plt.XLabel("Длина канала (м)");
            viscosityPlot.plt.YLabel("Вязкость материала (Па⋅с)");

            canalChooseComboBox.NewIndexSelected += ComboBox_NewIndexSelected;
            materialChooseComboBox.NewIndexSelected += ComboBox_NewIndexSelected;

            calculateStripMenuItem.Click += CalculateStripMenuItem_Click;
        }

        private void ComboBox_NewIndexSelected(object? sender, EventArgs e)
        {
            if (sender is ComboBox comboBox)
                try
                {
                    backgroundInputControlsFiller.RunWorkerAsync(new[] { comboBox.SelectedItem,
                        (canalChooseComboBox.SelectedItem as Canal, materialChooseComboBox.SelectedItem as Material) });
                }
                catch
                {
                    InputControlsFillerAwaiters.Enqueue((comboBox, e));
                }
        }

        private Queue<(ComboBox comboBox, EventArgs e)> InputControlsFillerAwaiters { get; set; } = new();

        public event Func<Material, Canal, IEnumerable<VariableParameter>>? SetVariableParameters;

        public event Func<Material, Canal, IEnumerable<Parameter>>? SetSolvingParameters;

        public event CalculationFunc? CalculationRequired;

        public void SetInitialData(IEnumerable<Canal> canals, IEnumerable<Material> materials)
        {
            canalChooseComboBox.Items.AddRange(canals.ToArray());
            materialChooseComboBox.Items.AddRange(materials.ToArray());

            if (canalChooseComboBox.Items?.Count != 0)
                canalChooseComboBox.SelectedIndex = 0;

            if (materialChooseComboBox.Items?.Count != 0)
                materialChooseComboBox.SelectedIndex = 0;
        }

        private void BackgroundInputControlsFiller_DoWork(object sender, DoWorkEventArgs e)
        {
            Dictionary<TableLayoutPanel, IEnumerable<Parameter>> result = new();

            if (e.Argument is not IEnumerable<object> comboBoxesSelectedItems)
                return;

            var canal = comboBoxesSelectedItems.OfType<Canal>().FirstOrDefault();
            var material = comboBoxesSelectedItems.OfType<Material>().FirstOrDefault();

            if (canal is not null)
            {
                IEnumerable<CanalGeometryParameter> geometryValues = canal.CanalGeometryParameters;

                result.Add(canalGeometryParametersLayout,
                    geometryValues.Select(geometryValue => (Parameter)geometryValue));
            }

            if (material is not null)
            {
                IEnumerable<ParameterOfMaterialProperty> propertiesValues = material.ParameterOfMaterialProperties;

                IEnumerable<EmpiricalCoefficientOfMathModel> empiricalCoefficients = material.EmpiricalCoefficientOfMathModels;

                result.Add(materialPropertiesLayout,
                    propertiesValues.Select(value => (Parameter)value));

                result.Add(empiricalCoefficientsOfMathModelLayout,
                    empiricalCoefficients.Select(coefficient => (Parameter)coefficient));
            }

            try
            {
                (canal, material) = ((Canal, Material))comboBoxesSelectedItems.First(i => i is (Canal, Material));
            }
            catch { }

            if (canal is not null && material is not null)
            {
                IEnumerable<VariableParameter> variableParameters = SetVariableParameters!.Invoke(material, canal);

                IEnumerable<Parameter> solvingParameters = SetSolvingParameters!.Invoke(material, canal);

                result.Add(variableParametersLayout, variableParameters
                    .Select(var => (ParameterWithBounds)var));

                result.Add(solvingMethodParametersLayout, solvingParameters);
            }

            e.Result = result;
        }

        private ParameterInput GetInputControl(Parameter parameter)
        {
            T InitializeInputControl<T>(Parameter parameter) where T : ParameterInput, new()
            {
                var parameterInput = new T
                {
                    ParameterName = parameter.Name,
                    MeasureUnit = parameter.MeasureUnit
                };

                parameterInput.ParsedAndValidated += InputControlAcquireResult;
                parameterInput.Invalidated += InputControlHandleError;

                return parameterInput;
            }

            if (parameter.Value is not null)
            {
                ParameterOutput parameterOutput = InitializeInputControl<ParameterOutput>(parameter);
                parameterOutput.Value = parameter.Value;

                return parameterOutput;
            }

            if (parameter is ParameterWithBounds parameterWithBounds)
            {
                ParameterInputWithBounds parameterInputWithBounds = InitializeInputControl<ParameterInputWithBounds>(parameter);
                parameterInputWithBounds.LowerBound = parameterWithBounds.LowerBound;
                parameterInputWithBounds.UpperBound = parameterWithBounds.UpperBound;

                return parameterInputWithBounds;
            }

            return InitializeInputControl<ParameterInput>(parameter);
        }

        private void InputControlHandleError(ParameterInput inputControl, string errorMessage)
        {
            errorProvider.SetError(inputControl.MeasureUnitLabel, errorMessage);

            try
            {
                Parameter parameter = InputControlsAndParameters[inputControl];

                InputControlsAndParameters[inputControl].Value = null;
            }
            catch { }

            ChangeTabPageStatus(tabControl.SelectedTab);

            TryEnableCalculateButt();
        }

        private void TryEnableCalculateButt()
        {
                calculateStripMenuItem.Enabled = 
                    InputControlsAndParameters.Keys
                    .All(parameterInput => parameterInput.ParameterInputStatus is ParameterInputStatus.Validated) &&
                    !InputControlsAndParameters.Keys
                    .All(parameterInput => parameterInput is ParameterOutput);
        }

        private void InputControlAcquireResult(ParameterInput inputControl, object result)
        {
            errorProvider.SetError(inputControl.MeasureUnitLabel, null);

            try
            {
                Parameter parameter = InputControlsAndParameters[inputControl];

                InputControlsAndParameters[inputControl].Value = result;
            }
            catch { }

            ChangeTabPageStatus(tabControl.SelectedTab);

            TryEnableCalculateButt();
        }

        private void BackgroundInputControlsFiller_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result is not Dictionary<TableLayoutPanel, IEnumerable<Parameter>> controlsToAdd)
                return;

            foreach (KeyValuePair<TableLayoutPanel, IEnumerable<Parameter>> tabControlsAndParameters in controlsToAdd)
            {
                TableLayoutPanel tableLayoutPanel = tabControlsAndParameters.Key;

                var parametersAndInputs = new Dictionary<ParameterInput, Parameter>();

                foreach (Parameter parameter in tabControlsAndParameters.Value)
                    parametersAndInputs.Add(GetInputControl(parameter), parameter);

                FillLayoutWithInputParameters(tableLayoutPanel, parametersAndInputs);
            }

            foreach (TabPage tabPage in PagesStatuses.Keys)
                ChangeTabPageStatus(tabPage);

            if (InputControlsFillerAwaiters.Any())
            {
                (ComboBox comboBox, EventArgs cE) = InputControlsFillerAwaiters.Dequeue();
                ComboBox_NewIndexSelected(comboBox, cE);
            }
        }

        private void ChangeTabPageStatus(TabPage tabPage)
        {
            TabPageStatus tabPageStatus = GetTabPageStatus(tabPage);

            PagesStatuses[tabPage] = tabPageStatus;

            const int okIconIndex = 0;
            const int editingIconIndex = 1;
            const int errorIconIndex = 2;
            const int incompleteIconIndex = 3;

            int iconIndex;

            iconIndex = tabPageStatus switch
            {
                TabPageStatus.Ok => okIconIndex,
                TabPageStatus.Editing => editingIconIndex,
                TabPageStatus.Error => errorIconIndex,
                TabPageStatus.Incomplete => incompleteIconIndex,
                _ => errorIconIndex
            };

            tabPage.ImageIndex = iconIndex;
        }

        private TabPageStatus GetTabPageStatus(TabPage tabPage)
        {
            IEnumerable<ParameterInput> inputControls = FindAllChildControls<ParameterInput>(tabPage.Controls);

            IEnumerable<ComboBox> comboBoxes = FindAllChildControls<ComboBox>(tabPage.Controls);

            IEnumerable<ParameterInput> wrongControls = inputControls.Where(cntrl =>
            cntrl.ParameterInputStatus
            is ParameterInputStatus.Error
            or ParameterInputStatus.IsNullOrEmpty);

            if (wrongControls.Any(wrngCntrl => wrngCntrl.ParameterInputStatus is ParameterInputStatus.Error))
                return TabPageStatus.Error;

            bool tabPageIsSelected = default;
            bool anyComboBoxDontHaveSelectedItem = default;
            bool anyControlFound = inputControls.Any() || comboBoxes.Any();

            tabPageIsSelected = tabControl.SelectedTab == tabPage;
            anyComboBoxDontHaveSelectedItem = comboBoxes.Any(cb => cb.SelectedItem is null);

            if (!anyControlFound || anyComboBoxDontHaveSelectedItem ||
                wrongControls.Any(wrngCntrl => wrngCntrl.ParameterInputStatus is ParameterInputStatus.IsNullOrEmpty))
                if (tabPageIsSelected)
                    return TabPageStatus.Editing;
                else
                    return TabPageStatus.Incomplete;

            return TabPageStatus.Ok;
        }

        private IEnumerable<ControlType> FindAllChildControls<ControlType>(Control.ControlCollection childControls)
            where ControlType : Control
        {
            List<ControlType> foundControls = new();
            foreach (Control child in childControls)
            {
                if (child.Controls is not null)
                    foundControls.AddRange(FindAllChildControls<ControlType>(child.Controls));

                if (child is ControlType foundControl)
                    foundControls.Add(foundControl);
            }

            return foundControls;
        }

        private void FillLayoutWithInputParameters(TableLayoutPanel tableLayoutPanel,
            Dictionary<ParameterInput, Parameter> parameterInputs)
        {
            tableLayoutPanel.Controls.Clear();

            int parametersCount = parameterInputs.Count();

            float rowsSizeInPercent = 100 / parametersCount;

            tableLayoutPanel.RowCount = parametersCount;

            for (int i = 0; i < parametersCount; i++)
            {
                if (i != parametersCount - 1)
                    tableLayoutPanel.RowStyles.Add(new RowStyle());

                tableLayoutPanel.RowStyles[i].SizeType = SizeType.Percent;
                tableLayoutPanel.RowStyles[i].Height = rowsSizeInPercent;

                ParameterInput parameterInput = parameterInputs.Keys.ElementAt(i);

                InputControlsAndParameters[parameterInput] = parameterInputs[parameterInput];

                parameterInput.Dock = DockStyle.Fill;

                tableLayoutPanel.Controls.Add(parameterInput, 0, i);
            }
        }

        private Dictionary<ParameterInput, Parameter> InputControlsAndParameters { get; set; } = new();

        private Dictionary<TabPage, TabPageStatus> PagesStatuses { get; set; } = new();

        enum TabPageStatus
        {
            Ok,
            Editing,
            Error,
            Incomplete
        }

        private void CalculateStripMenuItem_Click(object? sender, EventArgs e)
        {
            this.Enabled = false;
            this.SuspendLayout();
            CoordinatesValues.Clear();
            TemperaturesValues.Clear();
            ViscositiesValues.Clear();

            var calculationProcessor = new CalculationProcessor();
            calculationProcessor.CalculationFunc += CalculationRequired;
            calculationProcessor.CanalProductivityOutput += (canalProductivity) => canalProductivityOutput.Value = string.Format("{0:0.00}", canalProductivity);
            calculationProcessor.QualityIndicatorsOutput += ((double temperature, double viscosity) qualityIndicators) => 
            {
                (double temperature, double viscosity) = qualityIndicators;
                productTemperatureOutput.Value = string.Format("{0:0.00}", temperature);
                productViscosityOutput.Value = string.Format("{0:0.00}", viscosity);
            };

            calculationProcessor.ContiniousValuesOutputPreparations = new List<Action> { PreparePlots, PrepareResultsTable };
            calculationProcessor.ContiniousValuesOutputs = new List<Action<(double coordinate, double temperature, double viscosity)>> { AddResultToResultsLists };
            calculationProcessor.ContiniousValuesOutputFinalizations = new List<Action> { FinalizationForResultsGrid, FinalizationForPlots };

            TaskDialogResult taskDialogResult = calculationProcessor.ProceedCalculation(InputControlsAndParameters.Values);

            if (taskDialogResult is TaskDialogResult.Canceled or TaskDialogResult.Closed)
            {
                temperaturePlot.plt.Clear();
                viscosityPlot.plt.Clear();
                resultsGrid.Rows.Clear();

                IEnumerable<ParameterOutput> discreteResultsOutputs = FindAllChildControls<ParameterOutput>(resultsPage.Controls);
                foreach (ParameterOutput parameterOutput in discreteResultsOutputs)
                    parameterOutput.Value = null;

                this.Enabled = true;
                this.BringToFront();
                return;
            }

            tabControl.SelectedTab = resultsPage;

            this.ResumeLayout();
            this.PerformLayout();

            this.Enabled = true;

            this.BringToFront();
        }

        List<double> CoordinatesValues { get; set; } = new();

        List<double> TemperaturesValues { get; set; } = new();

        List<double> ViscositiesValues { get; set; } = new();

        private void PreparePlots()
        {
            this.Invoke(new MethodInvoker(() =>
            {
            temperaturePlot.SuspendLayout();
            viscosityPlot.SuspendLayout();

            temperaturePlot.plt.Clear();
            viscosityPlot.plt.Clear();
            }));
        }

        private void PrepareResultsTable()
        {
            this.Invoke(new MethodInvoker(() =>
            {
                resultsGrid.SuspendLayout();
                resultsGrid.Rows.Clear();
            }));
        }

        private void AddResultToResultsLists((double coordinate, double temperature, double viscosity) result)
        {
            (double coordinate, double temperature, double viscosity) = result;
            CoordinatesValues.Add(coordinate);
            TemperaturesValues.Add(temperature);
            ViscositiesValues.Add(viscosity);
        }

        private void FinalizationForResultsGrid()
        {
            this.Invoke(new MethodInvoker(() =>
            {
                var gridRows = new List<DataGridViewRow>(CoordinatesValues.Count);

                for (int i = 0; i < CoordinatesValues.Count; i++)
                {
                    var gridRow = new DataGridViewRow();

                    gridRow.CreateCells(resultsGrid, CoordinatesValues[i],
                        $"{TemperaturesValues[i]:0.00}", $"{ViscositiesValues[i]:0.00}");

                    gridRows.Add(gridRow);
                }

                resultsGrid.Rows.AddRange(gridRows.ToArray());
                resultsGrid.ResumeLayout();
            }));
        }

        private void FinalizationForPlots()
        {
            temperaturePlot.plt.PlotScatter(CoordinatesValues.ToArray(), TemperaturesValues.ToArray());
            viscosityPlot.plt.PlotScatter(CoordinatesValues.ToArray(), ViscositiesValues.ToArray());

            temperaturePlot.ResumeLayout();
            viscosityPlot.ResumeLayout();
        }
    }

    public struct CalculationResults
    {
        public IAsyncEnumerable<(double coordinate, double tempreture, double viscosity)> ResultsTable { get; set; }

        public (double tempreture, double viscosity) QualityIndicators { get; set; }

        public double CanalProductivity { get; set; }
    }

    public struct CalculationParameters
    {
        public IEnumerable<Parameter> Parameters { get; set; }

        public Action<double> ProgressIncrementor { get; set; }

        public int ProgressMaxValueForCalculation { get; set; }

        public int? IteratableResultsCalculationsStepsCount { get; set; }

        public CancellationToken CancellationToken { get; set; }
    }
}