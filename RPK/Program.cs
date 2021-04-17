using RPK.Presenter;
using RPK.Repository;
using RPK.View;
using System;
using System.Windows.Forms;

namespace RPK
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new ResearcherFormPresenter(new ResearcherForm(), new DatabaseContext(), new MathModel(),
                new FileExportService()).Run());
        }
    }
}