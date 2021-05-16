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
            InitializeErrorPage();
        }

#nullable enable
        public event Func<IAsyncEnumerable<(int progress, string? errorMessage)>> GetCalculationProgress;
#nullable disable

        TaskDialogPage CalculationInProgressPage { get; set; }

        TaskDialogPage SuccessPage { get; set; }

        TaskDialogPage ErrorPage { get; set; }

        TaskDialogButton ShowResultsButton { get; set; } = new TaskDialogCommandLinkButton("Показать результаты");

        TaskDialogButton CancelButton { get; set; } = new TaskDialogButton("Отмена") { Enabled = false, AllowCloseDialog = true };

#nullable enable
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

                string? finalErrorMessage = null;

                await foreach ((int progress, string? errorMessage) in GetCalculationProgress())
                {
                    finalErrorMessage = errorMessage;

                    if (finalErrorMessage is not null)
                        break;

                    if (progressBar.State is TaskDialogProgressBarState.Marquee)
                        progressBar.State = TaskDialogProgressBarState.Normal;

                    progressBar.Value = progress;
                    CalculationInProgressPage.Expander.Text = $"Процесс расчёта: {progress} %";
                }

                try
                {
                    if (finalErrorMessage is null)
                        CalculationInProgressPage.Navigate(SuccessPage);
                    else
                    {
                        ErrorPage.Text = finalErrorMessage;
                        CalculationInProgressPage.Navigate(ErrorPage);
                    }
                }
                catch { }
            };
        }

        private void InitializeFinishedPage()
        {
            SuccessPage = new TaskDialogPage()
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

        private void InitializeErrorPage()
        {
            ErrorPage = new TaskDialogPage
            {
                Caption = "Процесс расчёта",
                Heading = "Процесс расчёта не удался",
                Icon = TaskDialogIcon.Error,
                Buttons = { TaskDialogButton.Close }
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