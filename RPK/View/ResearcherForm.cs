#nullable enable
using RPK.InterfaceElements.ResearcherFormElements;
using RPK.Model.MathModel;
using ScottPlot;
using ScottPlot.Plottable;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPK.Researcher.View
{
    public delegate Task<CalculationResults> CalculationFunc(CalculationParameters calculationParameters);

    public partial class ResearcherForm : Form
    {
        private const string temperaturePlotName = "График распределения температуры по длине канала";

        private const string viscosityPlotName = "График распределения вязкости по длине канала";
        public ResearcherForm()
        {
            InitializeComponent();

            exitStripMenuItem.Click += (sender, e) => this.Close();

            tabControl.Selected += (sender, e) =>
            {
                foreach (TabPage tabPage in InputPagesStatuses.Keys)
                    ChangeTabPageStatus(tabPage);
            };
            InputPagesStatuses.Add(inputParametersPage, TabPageStatus.Incomplete);
            InputPagesStatuses.Add(variableParametersPage, TabPageStatus.Incomplete);
            InputPagesStatuses.Add(mathModelParametersPage, TabPageStatus.Incomplete);

            temperaturePlot.Plot.Title(temperaturePlotName);
            temperaturePlot.Plot.XLabel("Координата по длине канала (м)");
            temperaturePlot.Plot.YLabel("Температура материала (°C)");
            temperaturePlot.Render();

            viscosityPlot.Plot.Title(viscosityPlotName);
            viscosityPlot.Plot.XLabel("Координата по длине канала (м)");
            viscosityPlot.Plot.YLabel("Вязкость материала (Па⋅с)");
            viscosityPlot.Render();

            canalChooseComboBox.NewIndexSelected += ComboBox_NewIndexSelected;
            materialChooseComboBox.NewIndexSelected += ComboBox_NewIndexSelected;

            calculateStripMenuItem.Click += CalculateStripMenuItem_Click;

            VisualizationProcessor.VisualizationStarted += OnVisualizationStarted;
            VisualizationProcessor.ValuesOutputAsync += FillResultControlsAsync;
            VisualizationProcessor.VisualizationFinished += OnVisualizationFinished;

            exportResultsStripMenuItem.Click += ExportResultsStripMenuItem_Click;

            var cancellationTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancellationTokenSource.Token;

            this.FormClosing += (sender, e) => cancellationTokenSource.Cancel();

            reloginStripMenuItem.Click += (sender, e) =>
            {
                cancellationTokenSource.Cancel();
                ReloginRequired?.Invoke();
            };

            this.Shown += (sender, e) => InitializeMemoryOutput(cancellationToken);
        }

        public void SetUserDescription(string userName, string userRole)
        {
            this.Text = $"Вы вошли как {userName}: {userRole}";
        }

        private void ExportResultsStripMenuItem_Click(object? sender, EventArgs e)
        {
            exportResultsStripMenuItem.Enabled = false;
            calculateStripMenuItem.Enabled = false;

            if (saveFileDialog.ShowDialog() is not DialogResult.OK || DataToExport is null || LastCalculatedResults is null)
                return;

            string filePath = saveFileDialog.FileName;
            var discreteOutputParameters = new Dictionary<string, IList<Parameter>>();

            discreteOutputParameters.Add("Производительность канала", new[] { new Parameter(null,
                    canalProductivityOutput.ParameterName, string.Empty,
                    canalProductivityOutput.MeasureUnit,
                    canalProductivityOutput.Value)});

            discreteOutputParameters.Add("Показатели качества", new List<ParameterOutput> { productTemperatureOutput,
                    productViscosityOutput}
            .Select(po => new Parameter(null, po.ParameterName, string.Empty, po.MeasureUnit, po.Value)).ToArray());

            DataToExport.DiscreteOutputParameters = discreteOutputParameters;

            DataToExport.ContiniousResults = LastCalculatedResults.ResultsTable
                .Select(t => (new Parameter(null, "Координата по длине канала", string.Empty, "м", t.coordinate),
                    new Parameter(null, "Температура", string.Empty, "°C", t.tempreture),
                    new Parameter(null, "Вязкость", string.Empty, "Па⋅c", t.viscosity))).ToArray();

            var exportDialog = new ExportDialog();
            exportDialog.GetExportStatusAsync += async () =>
            {
                bool success;
                try
                {
                    success = await ExportToFileAsync!.Invoke(DataToExport, filePath);
                }
                catch { success = false; }

                if (success is true)
                    return ExportStatus.FinishedSuccessfully;
                else
                    return ExportStatus.FinishedWithError;
            };

            exportDialog.Show();

            exportResultsStripMenuItem.Enabled = true;
            calculateStripMenuItem.Enabled = true;
        }

        private void InitializeMemoryOutput(CancellationToken cancellationToken)
        {
            OutputAllocatedMemory(cancellationToken);
        }

        private void OutputAllocatedMemory(CancellationToken outputMemoryCancellationToken)
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    try
                    {
                        await Task.Delay(500, outputMemoryCancellationToken);

                        long? allocatedMemory = SetAllocatedMemory?.Invoke() / (1024 * 1024);

                        this.Invoke(new MethodInvoker(() =>
                        {
                            programOccupiedRAMOutput.Value = allocatedMemory;
                        }));
                    }
                    catch (TaskCanceledException) { return; }
                }
            }, outputMemoryCancellationToken);
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

        public event Action? ReloginRequired;

        public event Func<Material, Canal, IEnumerable<Parameter>>? SetSolvingParameters;

        public event CalculationFunc? CalculationRequiredAsync;

        public event Func<long>? SetAllocatedMemory;

        public event Func<DataToExport, string, Task<bool>>? ExportToFileAsync;

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
                    MeasureUnit = parameter.MeasureUnit,
                    Parameter = parameter
                };

                parameterInput.ParsedAndValidated += InputControlAcquireResult;
                parameterInput.Invalidated += InputControlHandleError;

                parameterInput.Value = parameter.Value;

                return parameterInput;
            }

            if (parameter is ParameterWithBounds parameterWithBounds)
            {
                ParameterInputWithBounds parameterInputWithBounds = InitializeInputControl<ParameterInputWithBounds>(parameter);
                parameterInputWithBounds.LowerBound = parameterWithBounds.LowerBound;
                parameterInputWithBounds.UpperBound = parameterWithBounds.UpperBound;
                parameterInputWithBounds.BoundsAreVisible = parameterWithBounds.ShowBounds;
                parameterInputWithBounds.Value = parameterWithBounds.Value;

                return parameterInputWithBounds;
            }

            if (parameter.Value is not null)
            {
                ParameterOutput parameterOutput = InitializeInputControl<ParameterOutput>(parameter);

                return parameterOutput;
            }

            return InitializeInputControl<ParameterInput>(parameter);
        }

        private void InputControlHandleError(ParameterInput inputControl, string errorMessage)
        {
            errorProvider.SetError(inputControl.MeasureUnitLabel, errorMessage);

            ChangeTabPageStatus(tabControl.SelectedTab);

            TryEnableCalculateButt();
        }

        private void TryEnableCalculateButt()
        {
            calculateStripMenuItem.Enabled = false;

            IEnumerable<TabPage> formTabPages = FindAllChildControls<TabPage>(this.Controls).Except(new[] { resultsPage });

            calculateStripMenuItem.Enabled = formTabPages
                .All(ftb => FindAllChildControls<ParameterInput>(ftb.Controls)
                .All(pi => pi.ParameterInputStatus is ParameterInputStatus.Validated));
        }

        private void InputControlAcquireResult(ParameterInput inputControl, object result)
        {
            errorProvider.SetError(inputControl.MeasureUnitLabel, null);

            ChangeTabPageStatus(tabControl.SelectedTab);

            TryEnableCalculateButt();
        }

        private void BackgroundInputControlsFiller_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Invoke(new MethodInvoker(() =>
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

                foreach (TabPage tabPage in InputPagesStatuses.Keys)
                    ChangeTabPageStatus(tabPage);

                if (InputControlsFillerAwaiters.Any())
                {
                    (ComboBox comboBox, EventArgs cE) = InputControlsFillerAwaiters.Dequeue();
                    ComboBox_NewIndexSelected(comboBox, cE);
                }

                TryEnableCalculateButt();
            }));
        }

        private void ChangeTabPageStatus(TabPage tabPage)
        {
            TabPageStatus tabPageStatus = GetTabPageStatus(tabPage);

            InputPagesStatuses[tabPage] = tabPageStatus;

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

            tabPageIsSelected = tabControl.SelectedTab == tabPage;
            anyComboBoxDontHaveSelectedItem = comboBoxes.Any(cb => cb.SelectedItem is null);

            if (anyComboBoxDontHaveSelectedItem ||
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

            int parametersCount = parameterInputs.Count;

            if (parametersCount is 0)
                return;

            float rowsSizeInPercent = 100 / parametersCount;

            tableLayoutPanel.RowCount = parametersCount;

            for (int i = 0; i < parametersCount; i++)
            {
                if (i != parametersCount - 1)
                    tableLayoutPanel.RowStyles.Add(new RowStyle());

                tableLayoutPanel.RowStyles[i].SizeType = SizeType.Percent;
                tableLayoutPanel.RowStyles[i].Height = rowsSizeInPercent;

                ParameterInput parameterInput = parameterInputs.Keys.ElementAt(i);

                parameterInput.Dock = DockStyle.Fill;

                tableLayoutPanel.Controls.Add(parameterInput, 0, i);
            }
        }

        private Dictionary<TabPage, TabPageStatus> InputPagesStatuses { get; set; } = new();

        private DataToExport? DataToExport { get; set; }

        private CalculationResults? LastCalculatedResults { get; set; }

        enum TabPageStatus
        {
            Ok,
            Editing,
            Error,
            Incomplete
        }

        private VisualizationProcessor VisualizationProcessor = new();

        private async void CalculateStripMenuItem_Click(object? sender, EventArgs e)
        {
            DataToExport = null;
            VisualizationProcessor.CancelVisualization();
            PrepareOutputControls();
            SetEnabledCertainControlsDueToVisualization(false);

            var calculationProcessor = new CalculationProcessor();
            calculationProcessor.CalculationFunc += CalculationRequiredAsync;

            static Parameter? SelectParam(ParameterInput paramInput)
            {
                if (paramInput.Parameter is not Parameter param)
                    return null;

                param.Value = paramInput.Value;

                return param;
            }

            IEnumerable<Parameter> canalCharacteristics = FindAllChildControls<ParameterInput>(canalGeometryParametersLayout.Controls)
                .Select(pi => SelectParam(pi)).OfType<Parameter>();

            IEnumerable<Parameter> materialPropertyParameters = FindAllChildControls<ParameterInput>(materialPropertiesLayout.Controls)
                .Select(pi => SelectParam(pi)).OfType<Parameter>();

            IEnumerable<Parameter> variableParameters = FindAllChildControls<ParameterInput>(variableParametersLayout.Controls)
                .Select(pi => SelectParam(pi)).OfType<Parameter>();

            IEnumerable<Parameter> empiricalCoefficientsOfMathModel = FindAllChildControls<ParameterInput>
                (empiricalCoefficientsOfMathModelLayout.Controls)
                .Concat(FindAllChildControls<ParameterInput>(solvingMethodParametersLayout.Controls))
                .Select(pi => SelectParam(pi)).OfType<Parameter>();

            IEnumerable<Parameter> allParameters = canalCharacteristics
                .Concat(materialPropertyParameters)
                .Concat(variableParameters)
                .Concat(empiricalCoefficientsOfMathModel);

            (CalculationResults? calculationResults, TaskDialogResult taskDialogResult) = await
                calculationProcessor.ProceedCalculationAsync(allParameters);

            LastCalculatedResults = calculationResults;

            this.BringToFront();

            SetEnabledCertainControlsDueToVisualization(true);

            if (taskDialogResult is TaskDialogResult.Cancel 
                || taskDialogResult is TaskDialogResult.Close 
                || calculationResults is null)
                return;

            DataToExport = new DataToExport
            {
                CanalCharacteristics = (((Canal)canalChooseComboBox.SelectedItem).ToString(),
                canalCharacteristics.ToArray()),
                MaterialCharacteristics = (((Material)materialChooseComboBox.SelectedItem).ToString(), materialPropertyParameters.ToArray()),
                VariableParameters = variableParameters.ToArray(),
                EmpiricalParametersOfMathModel = empiricalCoefficientsOfMathModel.ToArray(),
                CoordinatePrecision = GetCoordinatePrecision()
            };

            if (taskDialogResult is TaskDialogResult.ShowResults)
                tabControl.SelectedTab = resultsPage;

            await VisualizationProcessor.VisualizeAsync(calculationResults);
        }

        private void PrepareOutputControls()
        {
            IEnumerable<ParameterOutput> parameterOutputs = FindAllChildControls<ParameterOutput>(resultsPage.Controls);

            reloginStripMenuItem.Enabled = false;
            exportResultsStripMenuItem.Enabled = false;

            foreach (ParameterOutput parameterOutput in parameterOutputs)
                parameterOutput.Value = null;

            temperaturePlot.Enabled = false;
            viscosityPlot.Enabled = false;

            temperaturePlot.Plot.Clear();
            viscosityPlot.Plot.Clear();

            temperaturePlotGroupBox.Text = string.Empty;
            viscosityPlotGroupBox.Text = string.Empty;

            temperaturePlot.Plot.Title(temperaturePlotName);
            viscosityPlot.Plot.Title(viscosityPlotName);

            resultsGrid.Rows.Clear();
            resultsTableGroupBox.Text = ($"Таблица результатов");
        }

        private void OnVisualizationStarted(CalculationResults obj)
        {
            temperaturePlot.Plot.Title($"График распределения температуры по длине канала{Environment.NewLine}(в процессе визуализации)");
            viscosityPlot.Plot.Title($"График распределения вязкости по длине канала{Environment.NewLine}(в процессе визуализации)");

            resultsTableGroupBox.Text = ($"Таблица результатов (в процессе визуализации)");
        }

        private void OnVisualizationFinished(CalculationResults calculationResults)
        {
            temperaturePlot.Plot.Title($"График распределения температуры по длине канала{Environment.NewLine}");
            viscosityPlot.Plot.Title($"График распределения вязкости по длине канала{Environment.NewLine}");

            temperaturePlot.Plot.AxisAuto();
            viscosityPlot.Plot.AxisAuto();

            InitializePlotsLines();

            viscosityPlot.Render();
            temperaturePlot.Render();

            viscosityPlot.Enabled = true;
            temperaturePlot.Enabled = true;

            resultsTableGroupBox.Text = ($"Таблица результатов");

            exportResultsStripMenuItem.Enabled = DataToExport is not null;
            reloginStripMenuItem.Enabled = true;
        }

        private void InitializePlotsLines()
        {
            TemperatureHLine = temperaturePlot.Plot.AddHorizontalLine(0);
            TemperatureHLine.IsVisible = false;

            TemperatureVLine = temperaturePlot.Plot.AddVerticalLine(0);
            TemperatureVLine.IsVisible = false;

            ViscosityHLine = viscosityPlot.Plot.AddHorizontalLine(0);
            ViscosityHLine.IsVisible = false;

            ViscosityVLine = viscosityPlot.Plot.AddVerticalLine(0);
            ViscosityVLine.IsVisible = false;
        }

        private void SetEnabledCertainControlsDueToVisualization(bool enabled)
        {
            calculateStripMenuItem.Enabled = enabled;
            fileStripMenuItem.Enabled = enabled;
            foreach (TabPage tabPage in InputPagesStatuses.Keys)
            {
                tabPage.Enabled = enabled;
            }
        }

        private long GetCoordinatePrecision()
        {
            IEnumerable<ParameterInput> inputControls = FindAllChildControls<ParameterInput>(solvingMethodParametersLayout.Controls);
            ParameterInput? stepInput =
                inputControls.FirstOrDefault(paramInput => paramInput.ParameterName.Contains("Шаг решения"));

            if (stepInput is null)
                return 0;

            string step = stepInput.InputTextBox.Text;

            return step.SkipWhile(sym => sym is not ('.' or ',')).Count(sym => sym is not ('.' or ','));
        }

        private async Task FillResultControlsAsync(CalculationResults calculationResults, CancellationToken cancellationToken)
        {
            calculationTimeOutput.Value = calculationResults.CalculationTime;
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            List<(double coordinate, double temperature, double viscosity)> results = calculationResults.ResultsTable;
            (double temperature, double viscosity) = calculationResults.QualityIndicators;
            productTemperatureOutput.Value = string.Format($"{temperature:0.00}");
            productViscosityOutput.Value = string.Format($"{viscosity:0.00}");
            canalProductivityOutput.Value = string.Format($"{calculationResults.CanalProductivity:0.00}");

            double[] plotsCoordinates = new double[results.Count];
            double[] plotTemperatures = new double[results.Count];
            double[] plotViscosities = new double[results.Count];

            temperaturePlot.Plot.AddSignal(plotTemperatures, (results.Count - 1) / results.Last().coordinate);
            viscosityPlot.Plot.AddSignal(plotViscosities, (results.Count - 1) / results.Last().coordinate);

            long coordinatePrecision = GetCoordinatePrecision();

            Task visualizationTask = Task.Run(() =>
            {
                try
                {
                    for (int i = 0; i < results.Count; i++)
                    {
                        cancellationToken.ThrowIfCancellationRequested();

                        (double coordinate, double intermediateTemperature, double intermediateViscosity) =
                            results[i];

                        plotsCoordinates[i] = coordinate;
                        plotTemperatures[i] = intermediateTemperature;
                        plotViscosities[i] = intermediateViscosity;

                        this.Invoke(new MethodInvoker(() =>
                            resultsGrid.Rows.Add(coordinate.ToString($"F{coordinatePrecision}"), string.Format($"{intermediateTemperature:0.00}"),
                            string.Format($"{intermediateViscosity:0}"))));
                    }
                }
                catch { return; }
            }, cancellationToken);

            while (visualizationTask.IsCompleted is false)
            {
                try
                {
                    this.Invoke(new MethodInvoker(() =>
                    {
                        visualizationTimeOutput.Value = stopwatch.ElapsedMilliseconds;

                        temperaturePlot.Plot.AxisAuto();
                        temperaturePlot.Render();

                        viscosityPlot.Plot.AxisAuto();
                        viscosityPlot.Render();
                    }));
                    await Task.Delay(1000, cancellationToken);
                }
                catch { break; }
            }

            await visualizationTask;

            stopwatch.Stop();
        }

#nullable disable
        private HLine TemperatureHLine { get; set; }

        private VLine TemperatureVLine { get; set; }

        private HLine ViscosityHLine { get; set; }

        private VLine ViscosityVLine { get; set; }

        private void Plot_MouseMove(object sender, MouseEventArgs e)
        {
            if (sender is not FormsPlot formsPlot || (formsPlot.Plot.GetPlottables().ElementAtOrDefault(0) as SignalPlot) is null)
                return;

            // determine point nearest the cursor
            (double mouseCoordX, _) = formsPlot.GetMouseCoordinates();
            (double pointX, double pointY, int pointIndex) = ((SignalPlot)formsPlot.Plot.GetPlottables().ElementAt(0))
                .GetPointNearestX(mouseCoordX);

            //formsPlot.Plot.AddHorizontalLine().

            // place the highlight over the point of interest
            long coordinatePrecision = GetCoordinatePrecision();

            if (formsPlot == temperaturePlot)
            {
                TemperatureHLine.IsVisible = true;
                TemperatureHLine.Label = $"Y:{pointY:0.00}";
                TemperatureHLine.Y = pointY;

                TemperatureVLine.IsVisible = true;
                TemperatureVLine.Label = $"X:{pointX.ToString($"F{coordinatePrecision}")}";
                TemperatureVLine.X = pointX;
            }
            else if (formsPlot == viscosityPlot)
            {
                ViscosityHLine.IsVisible = true;
                ViscosityHLine.Label = $"Y:{pointY:0}";
                ViscosityHLine.Y = pointY;

                ViscosityVLine.IsVisible = true;
                ViscosityVLine.Label = $"X:{pointX.ToString($"F{coordinatePrecision}")}";
                ViscosityVLine.X = pointX;
            }

            formsPlot.Plot.Legend();
            formsPlot.Render();
        }
    }
    public class CalculationResults
    {
        public List<(double coordinate, double tempreture, double viscosity)> ResultsTable { get; set; }

        public (double tempreture, double viscosity) QualityIndicators { get; set; }

        public double CanalProductivity { get; set; }

        public long CalculationTime { get; set; }
    }

    public class CalculationParameters
    {
        public IEnumerable<Parameter> Parameters { get; set; }

        public Action<double> ProgressIncrementor { get; set; }

        public int ProgressMaxValueForCalculation { get; set; }

        public CancellationToken CancellationToken { get; set; }
    }

    public class DataToExport
    {
        public (string canalType, IList<Parameter>) CanalCharacteristics { get; set; }

        public (string materialType, IList<Parameter>) MaterialCharacteristics { get; set; }

        public IList<Parameter> VariableParameters { get; set; }

        public IList<Parameter> EmpiricalParametersOfMathModel { get; set; }

        public IList<(Parameter coordinate, Parameter temperature, Parameter viscosity)> ContiniousResults { get; set; }

        public long CoordinatePrecision { get; set; }

        public IDictionary<string, IList<Parameter>> DiscreteOutputParameters { get; set; }

        //public Bitmap TemperaturePlot { get; set; }

        //public Bitmap ViscosityPlot { get; set; }
    }
}