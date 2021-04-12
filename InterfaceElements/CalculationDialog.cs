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

        public TaskDialogResult Show()
        {
            InitializeInProgressPage();
            InitializeFinishedPage();

            TaskDialogButton result = TaskDialog.ShowDialog(InProgressPage);

            if (result == ShowResultsButton)
                return TaskDialogResult.ShowResult;
            else if (result == TaskDialogButton.Close)
                return TaskDialogResult.Closed;

            return TaskDialogResult.Canceled;
        }

        public TaskDialogResult Close()
        {
            InProgressPage.BoundDialog.Close();
            return TaskDialogResult.Closed;
        }

        public TaskDialogResult Cancel()
        {
            InProgressPage.BoundDialog.Close();
            return TaskDialogResult.Canceled;
        }
    }

    public enum TaskDialogResult
    {
        ShowResult,
        Closed,
        Canceled
    }
}