#nullable enable
using RPK.InterfaceElements.ResearcherFormElements;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace RPK.Researcher.View
{
    public class CalculationProcessor
    {
        public event CalculationFunc? CalculationFunc;

        private Task<CalculationResults?>? CalculationTask { get; set; }

        private double CalculationProgressIndicator { get; set; }

        private CancellationToken CancellationToken { get; set; }

        private string? ErrorMessage { get; set; }

        private void ProgressIncrementor(double increment)
        {
            CalculationProgressIndicator += increment;
        }

        public async Task<(CalculationResults?, TaskDialogResult)> ProceedCalculationAsync(IEnumerable<Parameter> parameters)
        {
            ErrorMessage = null;
            CalculationProgressIndicator = 0;
            var calculationDialog = new CalculationDialog();
            calculationDialog.GetCalculationProgress += CalculationDialog_GetProgress;

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

            CalculationTask = Task.Run(async () =>
            {
                CalculationResults? calculationResultsInner;
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                try
                {
                    calculationResultsInner = await CalculationFunc!.Invoke(calculationParameters);
                    calculationResultsInner!.CalculationTime = stopwatch.ElapsedMilliseconds;
                }
                catch (OperationCanceledException) { calculationResultsInner = null; }
                catch
                {
                    ErrorMessage = "Процесс расчёта был завершён с ошибкой. Возможно, не хватает одного из параметров, или он имеет " +
                "неподходящее для математической модели значение."; calculationResultsInner = null;
                }

                stopwatch.Stop();

                return calculationResultsInner;
            });

            TaskDialogResult taskDialogResult = calculationDialog.ShowCalculationDialog();

            if (taskDialogResult is TaskDialogResult.Cancel)
            {
                cancelationTokenSource.Cancel();
                return (null, taskDialogResult);
            }

            calculationResults = await CalculationTask;

            return (calculationResults, taskDialogResult);
        }

        private async IAsyncEnumerable<(int progress, string? errorMessage)> CalculationDialog_GetProgress()
        {
            yield return (0, ErrorMessage);
            while (CalculationTask?.IsCompleted is false)
            {
                yield return ((int)CalculationProgressIndicator, ErrorMessage);

                await Task.Delay(100);
            }
        }
    }
}