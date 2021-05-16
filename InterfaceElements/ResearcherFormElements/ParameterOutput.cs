#nullable enable

namespace RPK.InterfaceElements.ResearcherFormElements
{
    public class ParameterOutput : ParameterInput
    {
        public ParameterOutput()
        {
            InputTextBox.Enabled = false;

            Validator = null;
        }

        public override object? Value
        {
            get => base.Value; 
            set
            {
                base.value = value;
                InputTextBox.Text = value?.ToString();

                ParameterInputStatus = ParameterInputStatus.Validated;
            }
        }
    }
}