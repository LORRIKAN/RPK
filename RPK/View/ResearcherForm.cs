#nullable enable
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPK.View
{
    public partial class ResearcherForm : Form
    {
        public ResearcherForm()
        {
            InitializeComponent();

            exitStripMenuItem.Click += (sender, e) => this.Close();

            InitializeParameterInputs();

            tabControl.TabPages.Remove(resultsPage);
            PagesStatuses.Add(inputParametersPage, TabPageStatus.Incomplete);
            PagesStatuses.Add(variableParametersPage, TabPageStatus.Incomplete);
            PagesStatuses.Add(mathModelParametersPage, TabPageStatus.Incomplete);
        }

        private void InitializeParameterInputs()
        {
            canalDepthInput.ParsedAndValidated += InputControlAcquireResult;
            canalLengthInput.ParsedAndValidated += InputControlAcquireResult;
            canalWidthInput.ParsedAndValidated += InputControlAcquireResult;
            lidSpeedInput.ParsedAndValidated += InputControlAcquireResult;
            lidTemperatureInput.ParsedAndValidated += InputControlAcquireResult;
            materialConsistencyCoefficientInput.ParsedAndValidated += InputControlAcquireResult;
            materialTemperatureViscosityCoefficientInput.ParsedAndValidated += InputControlAcquireResult;
            castingTemperatureInput.ParsedAndValidated += InputControlAcquireResult;
            materialFlowIndexInput.ParsedAndValidated += InputControlAcquireResult;
            heatTransferCoefficientInput.ParsedAndValidated += InputControlAcquireResult;
            stepInput.ParsedAndValidated += InputControlAcquireResult;

            canalDepthInput.Invalidated += InputControlHandleError;
            canalLengthInput.Invalidated += InputControlHandleError;
            canalWidthInput.Invalidated += InputControlHandleError;
            lidSpeedInput.Invalidated += InputControlHandleError;
            lidTemperatureInput.Invalidated += InputControlHandleError;
            materialConsistencyCoefficientInput.Invalidated += InputControlHandleError;
            materialTemperatureViscosityCoefficientInput.Invalidated += InputControlHandleError;
            castingTemperatureInput.Invalidated += InputControlHandleError;
            materialFlowIndexInput.Invalidated += InputControlHandleError;
            heatTransferCoefficientInput.Invalidated += InputControlHandleError;
            stepInput.Invalidated += InputControlHandleError;
        }

        private void InputControlHandleError(ParameterInput inputControl, string errorMessage)
        {
            tabControl.SelectedTab.ImageKey = "editing.png";
            errorProvider.SetError(inputControl.MeasureUnitLabel, errorMessage);
            ChangeTabPageStatus(tabControl, new TabControlEventArgs(tabControl.SelectedTab, 
                tabControl.SelectedIndex, TabControlAction.Deselected));
        }

        private void InputControlAcquireResult(ParameterInput inputControl, object result)
        {
            tabControl.SelectedTab.ImageKey = "editing.png";
            errorProvider.SetError(inputControl.MeasureUnitLabel, null);
            ChangeTabPageStatus(tabControl, new TabControlEventArgs(tabControl.SelectedTab,
                tabControl.SelectedIndex, TabControlAction.Deselected));
        }

        private async void ChangeTabPageStatus(object sender, TabControlEventArgs e)
        {
            TabPageStatus tabPageStatus = await Task.Run(() => GetTabPageStatus(e.TabPage));

            PagesStatuses[e.TabPage] = tabPageStatus;

            calculateStripMenuItem.Enabled = PagesStatuses.Values.All(page => page is TabPageStatus.Ok);

            const int okIconIndex = 0;
            const int errorIconIndex = 2;
            const int incompleteIconIndex = 3;

            e.TabPage.ImageIndex = tabPageStatus switch
            {
                TabPageStatus.Ok => okIconIndex,
                TabPageStatus.Error => errorIconIndex,
                TabPageStatus.Incomplete => incompleteIconIndex,
                _ => errorIconIndex
            };
        }

        private TabPageStatus GetTabPageStatus(TabPage tabPage)
        {
            IEnumerable<ParameterInput> inputControls = FindAllInputControls(tabPage.Controls);

            IEnumerable<ParameterInput> wrongControls = inputControls.Where(cntrl =>
            cntrl.ParameterInputStatus 
            is ParameterInputStatus.Error 
            or ParameterInputStatus.IsNullOrEmpty);

            if (wrongControls.Any(wrngCntrl => wrngCntrl.ParameterInputStatus is ParameterInputStatus.Error))
                return TabPageStatus.Error;

            if (wrongControls.Any(wrngCntrl => wrngCntrl.ParameterInputStatus is ParameterInputStatus.IsNullOrEmpty))
                return TabPageStatus.Incomplete;

            return TabPageStatus.Ok;
        }

        private IEnumerable<ParameterInput> FindAllInputControls(Control.ControlCollection childControls)
        {
            List<ParameterInput> foundInputControls = new();
            foreach (Control child in childControls)
            {
                if (child.Controls is not null)
                    foundInputControls.AddRange(FindAllInputControls(child.Controls));

                if (child is ParameterInput inputControl)
                    foundInputControls.Add(inputControl);
            }

            return foundInputControls;
        }

        private Dictionary<TabPage, TabPageStatus> PagesStatuses { get; set; } = new();

        enum TabPageStatus
        {
            Ok,
            Editing,
            Error,
            Incomplete
        }
    }
}