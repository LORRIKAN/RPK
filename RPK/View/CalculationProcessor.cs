#nullable enable
using RPK.InterfaceElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RPK.View
{
    public class CalculationProcessor
    {
        public event CalculationFunc? CalculationFunc;

        public event Action<double>? CanalProductivityOutput;

        public event Action<(double temperature, double viscosity)>? QualityIndicatorsOutput;

        public List<Action>? ContiniousValuesOutputPreparations { get; set; } = new();

        public List<Action<(double coordinate, double temperature, double viscosity)>>? ContiniousValuesOutputs { get; set; } = new();

        public List<Action>? ContiniousValuesOutputFinalizations { get; set; } = new();

        private Task? CalculationTask { get; set; }

        private Task? VisualizationTask { get; set; }

        private double CalculationProgressIndicator { get; set; }

        private double VisualizationProgressIndicator { get; set; }

        private void ProgressIncrementor(double increment)
        {
            CalculationProgressIndicator += increment;
        }

        public TaskDialogResult ProceedCalculation(IEnumerable<Parameter> parameters)
        {
            CalculationProgressIndicator = 0;
            var calculationDialog = new CalculationDialog();
            calculationDialog.GetProgress += CalculationDialog_GetProgress;

            var cancelationTokenSource = new CancellationTokenSource();
            var cancelationToken = cancelationTokenSource.Token;

            var calculationResults = new CalculationResults();

            var calculationParameters = new CalculationParameters()
            {
                CancellationToken = cancelationToken,
                IteratableResultsCalculationsStepsCount = null,
                Parameters = parameters,
                ProgressIncrementor = ProgressIncrementor,
                ProgressMaxValueForCalculation = 100
            };

            CalculationTask = Task.Run(() =>
            {
                try
                {
                    CalculationFunc?.Invoke(ref calculationParameters, out calculationResults);
                }
                catch { return; }
            });

            while (calculationParameters.IteratableResultsCalculationsStepsCount is null) { }

            const double discreteValuesOutputs = 2;

            double visualizationProgressIncrement = 50.0 /
                (discreteValuesOutputs +
                ContiniousValuesOutputPreparations!.Count +
                ContiniousValuesOutputs!.Count * calculationParameters.IteratableResultsCalculationsStepsCount.Value +
                ContiniousValuesOutputFinalizations!.Count);

            VisualizationTask = Task.Run(async () =>
                {
                    try
                    {
                        foreach (Action continiousValuesOutputPreparation in ContiniousValuesOutputPreparations!)
                        {
                            cancelationToken.ThrowIfCancellationRequested();
                            continiousValuesOutputPreparation();
                            VisualizationProgressIndicator += visualizationProgressIncrement;
                        }

                        while (calculationResults.ResultsTable is null) { }
                        await foreach ((double coordinate, double temperature, double viscosity) result in calculationResults.ResultsTable)
                        {
                            cancelationToken.ThrowIfCancellationRequested();

                            foreach (Action<(double coordinate, double temperature, double viscosity)> outputAction in ContiniousValuesOutputs!)
                            {
                                outputAction(result);

                                VisualizationProgressIndicator += visualizationProgressIncrement;
                            }
                        }

                        foreach (Action finalizeAction in ContiniousValuesOutputFinalizations)
                        {
                            cancelationToken.ThrowIfCancellationRequested();
                            finalizeAction();

                            VisualizationProgressIndicator += visualizationProgressIncrement;
                        }
                    }
                    catch { return; }
                });

            TaskDialogResult taskDialogResult = calculationDialog.Show();

            if (taskDialogResult is TaskDialogResult.Canceled)
                cancelationTokenSource.Cancel();

            Task.WaitAll(CalculationTask, VisualizationTask);

            CanalProductivityOutput?.Invoke(calculationResults.CanalProductivity);
            QualityIndicatorsOutput?.Invoke(calculationResults.QualityIndicators);

            return taskDialogResult;
        }

        private async IAsyncEnumerable<(int progressPercent, string status)> CalculationDialog_GetProgress()
        {
            while (CalculationTask?.IsCompleted is false)
            {
                yield return ((int)CalculationProgressIndicator, "Производится расчёт");

                await Task.Delay(100);
            }

            while (VisualizationTask?.IsCompleted is false)
            {
                yield return ((int)VisualizationProgressIndicator, "Производится визуализация");

                await Task.Delay(100);
            }
        }
    }
}