using RPK.Researcher.Presenter;
using RPK.Repository.MathModel;
using RPK.Researcher.View;
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

            Application.Run(new ResearcherFormPresenter(new ResearcherForm(), new MathModelContext(), new MathModel(),
                new FileExportService()).Run());
        }
    }
}