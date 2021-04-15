using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RPK.View
{
    public class VisualizationProcessor
    {
        public event Func<CalculationResults, CancellationToken, Task> ValuesOutputAsync;

        public event Action<CalculationResults> VisualizationStarted;

        public event Action<CalculationResults> VisualizationFinished;

        private CancellationTokenSource CancellationTokenSource { get; set; }

        private Task VisualizationTask { get; set; }

        public async Task StartVisualization(CalculationResults calculationResults)
        {
            CancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = CancellationTokenSource.Token;

            VisualizationStarted(calculationResults);

            try
            {
                VisualizationTask = ValuesOutputAsync(calculationResults, cancellationToken);
                await VisualizationTask;
            }
            catch
            {
                return;
            }

            VisualizationFinished(calculationResults);
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