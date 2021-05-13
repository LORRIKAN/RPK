using System.Windows.Forms;

namespace RPK.InterfaceElements.SharedElements
{
    public class InnerErrorReportDialog
    {
        private TaskDialogPage DialogPage { get; set; } =
            new()
            {
                Caption = "Внутренняя ошибка",
                Heading = "Ошибка в коде программы",
                Icon = TaskDialogIcon.ShieldErrorRedBar,

                Buttons = { TaskDialogButton.Close }
            };

        public void Show(string errorMessage)
        {
            DialogPage.Text = errorMessage;

            TaskDialog.ShowDialog(DialogPage);
        }
    }
}