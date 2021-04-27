#nullable enable
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RPK.Researcher.View
{
    public class VisualizationProcessor
    {
        public event Func<CalculationResults, CancellationToken, Task>? ValuesOutputAsync;

        public event Action<CalculationResults>? VisualizationStarted;

        public event Action<CalculationResults>? VisualizationFinished;

        private CancellationTokenSource? CancellationTokenSource { get; set; }

        private Task? VisualizationTask { get; set; }

        public async Task StartVisualization(CalculationResults calculationResults)
        {
            CancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = CancellationTokenSource.Token;

            VisualizationStarted?.Invoke(calculationResults);

            try
            {
                VisualizationTask = ValuesOutputAsync?.Invoke(calculationResults, cancellationToken);
                if (VisualizationTask is not null)
                    await VisualizationTask;
            }
            catch
            {
                return;
            }

            VisualizationFinished?.Invoke(calculationResults);
        }

        public async void CancelVisualization()
        {
            if (CancellationTokenSource is null || VisualizationTask is null)
                return;

            CancellationTokenSource.Cancel();

            await VisualizationTask;
        }
    }
}