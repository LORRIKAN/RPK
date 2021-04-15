#nullable enable
using RPK.InterfaceElements;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPK.View
{
    public class CalculationProcessor
    {
        public event CalculationFunc? CalculationFunc;

        public event Action<CalculationResults?>? VisualizationFunc;

        private Task? VisualizationTask { get; set; }

        private Task<CalculationResults?>? CalculationTask { get; set; }

        private double CalculationProgressIndicator { get; set; }

        private CancellationToken CancellationToken { get; set; }

        private void ProgressIncrementor(double increment)
        {
            CalculationProgressIndicator += increment;
        }

        public async Task<(CalculationResults?, TaskDialogResult)> ProceedCalculationAsync(IEnumerable<Parameter> parameters)
        {
            CalculationProgressIndicator = 0;
            var calculationDialog = new CalculationDialog();
            calculationDialog.GetCalculationProgress += CalculationDialog_GetProgress;
            calculationDialog.GetVisualizationIsFinished += CalculationDialog_GetVisualizationIsFinished;

            var cancelationTokenSource = new CancellationTokenSource();
            CancellationToken = cancelationTokenSource.Token;

            CalculationResults? calculationResults;

            var calculationParameters = new CalculationParameters()
            {
                CancellationToken = CancellationToken,
                Parameters = parameters,
                ProgressIncrementor = ProgressIncrementor,
                ProgressMaxValueForCalculation = 100
            };

            CalculationTask = Task.Run(() =>
            {
                try
                {
                    return CalculationFunc?.Invoke(calculationParameters);
                }
                catch { return null; }
            });

            TaskDialogResult taskDialogResult = calculationDialog.ShowCalculationDialog();

            if (taskDialogResult is TaskDialogResult.Cancel)
            {
                cancelationTokenSource.Cancel();
                return (null, taskDialogResult);
            }

            calculationResults = await CalculationTask;

            VisualizationTask = Task.Run(() => VisualizationFunc?.Invoke(calculationResults));

            taskDialogResult = calculationDialog.ShowVisualizationDialog();

            await VisualizationTask;

            return (calculationResults, taskDialogResult);
        }

        private async IAsyncEnumerable<int> CalculationDialog_GetProgress()
        {
            while (CalculationTask?.IsCompleted is false)
            {
                yield return (int)CalculationProgressIndicator;

                await Task.Delay(100, CancellationToken);
            }
        }
        private async IAsyncEnumerable<bool> CalculationDialog_GetVisualizationIsFinished()
        {
            while (VisualizationTask?.IsCompleted is false)
            {
                yield return VisualizationTask.IsCompleted;

                await Task.Delay(100, CancellationToken);
            }
        }
    }
}