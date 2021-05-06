using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RPK.InterfaceElements.ResearcherFormElements
{
    public class CalculationDialog
    {

        public CalculationDialog()
        {
            InitializeCalculationInProgressPage();
            InitializeFinishedPage();
        }

        public event Func<IAsyncEnumerable<int>> GetCalculationProgress;

        TaskDialogPage CalculationInProgressPage { get; set; }

        TaskDialogPage FinishedPage { get; set; }

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

                try { CalculationInProgressPage.Navigate(FinishedPage); } catch { }
            };
        }

        private void InitializeFinishedPage()
        {
            FinishedPage = new TaskDialogPage()
            {
                Caption = "Процесс расчёта",
                Heading = "Расчёт завершен!",
                Text = "Процесс расчёта завершён.",
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
            else if (result == ShowResultsButton)
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