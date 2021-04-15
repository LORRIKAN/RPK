using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RPK.InterfaceElements
{
    public class CalculationDialog
    {

        public CalculationDialog()
        {
            InitializeCalculationInProgressPage();
            InitializeVisualizationInProgressPage();
            InitializeFinishedPage();
        }

        public event Func<IAsyncEnumerable<int>> GetCalculationProgress;

        public event Func<IAsyncEnumerable<bool>> GetVisualizationIsFinished;

        TaskDialogPage CalculationInProgressPage { get; set; }

        TaskDialogPage FinishedPage { get; set; }

        TaskDialogPage VisualizationInProgressPage { get; set; }

        TaskDialogButton ShowResultsButton { get; set; } = new TaskDialogCommandLinkButton("Показать результаты");

        TaskDialogButton CancelButton { get; set; } = new TaskDialogButton("Отмена") { Enabled = false, AllowCloseDialog = true };

        private void InitializeCalculationInProgressPage()
        {
            CalculationInProgressPage = new TaskDialogPage()
            {
                Caption = "Процесс расчёта",
                Heading = "Расчёт в процессе...",
                Text = "Пожалуйста подождите, пока идёт расчёт.",
                Icon = TaskDialogIcon.Information,
                AllowCancel = false,
                AllowMinimize = false,

                Verification = new TaskDialogVerificationCheckBox() { Text = "Я действительно хочу отменить процесс расчёта." },

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

            TaskDialogVerificationCheckBox checkBox = CalculationInProgressPage.Verification;
            checkBox.CheckedChanged += (sender, e) =>
            {
                CancelButton.Enabled = checkBox.Checked;
            };

            CalculationInProgressPage.Created += async (s, e) =>
            {
                var progressBar = CalculationInProgressPage.ProgressBar;

                await foreach (int progress in GetCalculationProgress())
                {
                    if (progressBar.State is TaskDialogProgressBarState.Marquee)
                        progressBar.State = TaskDialogProgressBarState.Normal;

                    progressBar.Value = progress;
                    CalculationInProgressPage.Expander.Text = $"Процесс расчёта: {progress} %";
                }

                CalculationInProgressPage.BoundDialog.Close();
            };
        }

        private void InitializeVisualizationInProgressPage()
        {
            VisualizationInProgressPage = new TaskDialogPage()
            {
                Caption = "Процесс визуализации",
                Heading = "Визуализация в процессе...",
                Text = "Пожалуйста подождите, пока идёт визуализация. Этот процесс нельзя отменить.",
                Icon = TaskDialogIcon.Information,
                AllowCancel = false,
                AllowMinimize = false,

                ProgressBar = new TaskDialogProgressBar()
                {
                    State = TaskDialogProgressBarState.Marquee
                },

                Buttons = { new TaskDialogButton() { Text = "Отмена", Enabled = false } }
            };

            VisualizationInProgressPage.Created += async (s, e) =>
            {
                await foreach (bool visualizationIsFinished in GetVisualizationIsFinished())
                {
                }

                VisualizationInProgressPage.Navigate(FinishedPage);
            };
        }

        private void InitializeFinishedPage()
        {
            FinishedPage = new TaskDialogPage()
            {
                Caption = "Процессы расчёта и визуализации",
                Heading = "Расчёт и визуализация завершены!",
                Text = "Процессы расчёта и визуализации завершены.",
                Icon = TaskDialogIcon.ShieldSuccessGreenBar,
                Buttons =
                {
                    TaskDialogButton.Close,
                    ShowResultsButton
                }
            };
        }

        public TaskDialogResult ShowCalculationDialog()
        {
            TaskDialogButton result = TaskDialog.ShowDialog(CalculationInProgressPage);

            if (result == CancelButton)
                return TaskDialogResult.Cancel;

            return TaskDialogResult.Close;
        }

        public TaskDialogResult ShowVisualizationDialog()
        {
            TaskDialogButton result = TaskDialog.ShowDialog(VisualizationInProgressPage);

            if (result == ShowResultsButton)
                return TaskDialogResult.ShowResults;

            return TaskDialogResult.Close;
        }
    }

    public enum TaskDialogResult
    {
        ShowResults,
        Cancel,
        Close
    }
}