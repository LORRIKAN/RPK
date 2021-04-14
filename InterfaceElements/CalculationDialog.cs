using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RPK.InterfaceElements
{
    public class CalculationDialog
    {

        public CalculationDialog()
        {
            InitializeInProgressPage();
            InitializeFinishedPage();
        }

        public event Func<IAsyncEnumerable<(int progressPercent, string state)>> GetProgress;

        TaskDialogPage InProgressPage { get; set; }

        TaskDialogPage FinishedPage { get; set; }

        TaskDialogButton ShowResultsButton { get; set; } = new TaskDialogCommandLinkButton("Показать результаты");

        TaskDialogButton CancelButton { get; set; } = new TaskDialogButton("Отмена") { Enabled = false, AllowCloseDialog = true };

        private void InitializeInProgressPage()
        {
            InProgressPage = new TaskDialogPage()
            {
                Caption = "Процессы расчёта и визуализации",
                Heading = "Расчёт и визуализация в процессе...",
                Text = "Пожалуйста подождите, пока идут расчёт и визуализация.",
                Icon = TaskDialogIcon.Information,
                AllowCancel = false,
                AllowMinimize = false,

                Verification = new TaskDialogVerificationCheckBox() { Text = "Я действительно хочу отменить процессы расчёта и визуализации." },

                ProgressBar = new TaskDialogProgressBar()
                {
                    State = TaskDialogProgressBarState.Marquee
                },

                Expander = new TaskDialogExpander()
                {
                    Text = "Инициализация...",
                    Position = TaskDialogExpanderPosition.AfterText,
                    CollapsedButtonText = "Подробнее",
                    ExpandedButtonText = "Скрыть",
                },

                Buttons = { CancelButton },
            };

            TaskDialogVerificationCheckBox checkBox = InProgressPage.Verification;
            checkBox.CheckedChanged += (sender, e) =>
            {
                CancelButton.Enabled = checkBox.Checked;
            };

            InProgressPage.Created += async (s, e) =>
            {
                // Run the background operation and iterate over the streamed values to update
                // the progress. Because we call the async method from the GUI thread,
                // it will use this thread's synchronization context to run the continuations,
                // so we don't need to use Control.[Begin]Invoke() to schedule the callbacks.
                var progressBar = InProgressPage.ProgressBar;

                await foreach ((int progressPercent, string status) progressValue in GetProgress())
                {
                    // When we display the first progress, switch the marquee progress bar
                    // to a regular one.
                    if (progressBar.State == TaskDialogProgressBarState.Marquee)
                        progressBar.State = TaskDialogProgressBarState.Normal;

                    (int progressPercent, string status) = progressValue;

                    progressBar.Value = progressPercent;
                    InProgressPage.Expander.Text = $"{status}: {progressPercent} %";
                }

                // Work is finished, so navigate to the third page.
                InProgressPage.Navigate(FinishedPage);
            };
        }

        private void InitializeFinishedPage()
        {
            FinishedPage = new TaskDialogPage()
            {
                Caption = "Процессы расчёта и визуализации",
                Heading = "Расчёт и визуализация завершёны!",
                Text = "Процессы расчёта и визуализации завершены.",
                Icon = TaskDialogIcon.ShieldSuccessGreenBar,
                Buttons =
                {
                    TaskDialogButton.Close,
                    ShowResultsButton
                }
            };
        }

        public TaskDialogResult Show()
        {
            TaskDialogButton result = TaskDialog.ShowDialog(InProgressPage);

            if (result == ShowResultsButton)
                return TaskDialogResult.ShowResults;
            else if (result == CancelButton)
                return TaskDialogResult.Canceled;
            return TaskDialogResult.Closed;
        }
    }

    public enum TaskDialogResult
    {
        ShowResults,
        Canceled,
        Closed
    }
}