using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPK.InterfaceElements.ResearcherFormElements
{
    public class ExportDialog
    {
        public event Func<Task<ExportStatus>> GetExportStatusAsync;

        public ExportDialog()
        {
            InitialPage.Created += async (sender, e) =>
            {
                if (await GetExportStatusAsync() is ExportStatus.FinishedSuccessfully)
                    InitialPage.Navigate(FinishedSuccessfullyPage);
                else
                    InitialPage.Navigate(FinishedWithErrorPage);
            };
        }

        private TaskDialogPage FinishedSuccessfullyPage { get; set; } =
            new()
            {
                Caption = "Процесс сохранения в файл",
                Heading = "Сохранение в файл прошло успешно",
                Icon = TaskDialogIcon.ShieldSuccessGreenBar
            };

        private TaskDialogPage FinishedWithErrorPage { get; set; } =
            new()
            {
                Caption = "Процесс сохранения в файл",
                Heading = "Сохранение в файл не удалось",
                Text = "Сохранение в файл не удалось из-за неизвестной ошибки",
                Icon = TaskDialogIcon.ShieldErrorRedBar
            };

        private TaskDialogPage InitialPage { get; set; } =
            new()
            {
                Caption = "Процесс сохранения в файл",
                Heading = "Сохранение в файл в процессе...",
                Text = "Процесс сохранения результатов работы программы в файл. Пожалуйста подождите.",
                Icon = TaskDialogIcon.Information,
                ProgressBar = new TaskDialogProgressBar { State = TaskDialogProgressBarState.Marquee },
                Buttons = { new TaskDialogButton { AllowCloseDialog = false, Enabled = false, Text = "Ок" } }
            };

        public void Show()
        {
            TaskDialog.ShowDialog(InitialPage);
        }
    }

    public enum ExportStatus
    {
        FinishedSuccessfully,
        FinishedWithError
    }
}