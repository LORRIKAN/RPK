using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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

        public void ReportProgress(int progressValue)
        {
            if (InProgressPage.ProgressBar is null)
                return;

            InProgressPage.ProgressBar.Value = progressValue;

            if (InProgressPage.ProgressBar.Maximum >= progressValue)
                InProgressPage.Navigate(FinishedPage);
        }

        TaskDialogPage InProgressPage { get; set; }

        TaskDialogPage FinishedPage { get; set; }

        TaskDialogCommandLinkButton ShowResultsButton { get; set; } = new("Показать результаты");

        public TaskDialogResult TaskDialogResult { get; private set; } = TaskDialogResult.NotSet;

        public int ProgressBarMaxValue { get => InProgressPage.ProgressBar.Maximum; set => InProgressPage.ProgressBar.Maximum = value; }

        public int ProgressBarMinValue { get => InProgressPage.ProgressBar.Minimum; set => InProgressPage.ProgressBar.Minimum = value; }

        private void InitializeInProgressPage()
        {
            var cancelButton = new TaskDialogButton { Text = "Отмена", Enabled = false, AllowCloseDialog = true };

            InProgressPage = new TaskDialogPage()
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

                Buttons = { cancelButton },
            };

            TaskDialogVerificationCheckBox checkBox = InProgressPage.Verification;
            checkBox.CheckedChanged += (sender, e) =>
            {
                cancelButton.Enabled = checkBox.Checked;
            };
        }

        private void InitializeFinishedPage()
        {
            TaskDialogButton showResultsButton = new TaskDialogCommandLinkButton("Показать результаты");

            FinishedPage = new TaskDialogPage()
            {
                Caption = "Процесс расчёта",
                Heading = "Расчёт завершён!",
                Text = "Процесс расчёта завершён.",
                Icon = TaskDialogIcon.ShieldSuccessGreenBar,
                Buttons =
                {
                    TaskDialogButton.Close,
                    showResultsButton
                }
            };
        }

        public void Show()
        {
            InitializeInProgressPage();
            InitializeFinishedPage();

            TaskDialogResult = TaskDialogResult.NotSet;

            TaskDialogButton result = TaskDialog.ShowDialog(InProgressPage);

            if (result == ShowResultsButton)
            {
                TaskDialogResult = TaskDialogResult.ShowResult;
                return;
            }
            else if (result == TaskDialogButton.Close)
            {
                TaskDialogResult = TaskDialogResult.Closed;
                return;
            }

            TaskDialogResult = TaskDialogResult.Canceled;
        }

        public void Close()
        {
            InProgressPage.BoundDialog.Close();
            TaskDialogResult = TaskDialogResult.Closed;
        }

        public void Cancel()
        {
            InProgressPage.BoundDialog.Close();
            TaskDialogResult = TaskDialogResult.Canceled;
        }
    }

    public enum TaskDialogResult
    {
        NotSet,
        ShowResult,
        Closed,
        Canceled
    }
}