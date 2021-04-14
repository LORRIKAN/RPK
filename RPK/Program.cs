using RPK.Presenter;
using RPK.Repository;
using RPK.View;
using System.Windows.Forms;

Application.SetHighDpiMode(HighDpiMode.SystemAware);
Application.EnableVisualStyles();
Application.SetCompatibleTextRenderingDefault(false);

new ResearcherFormPresenter(new ResearcherForm(), new DatabaseContext(), new MathModel()).Run();