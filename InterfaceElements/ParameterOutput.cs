#nullable enable

namespace RPK.InterfaceElements
{
    public class ParameterOutput : ParameterInput
    {
        public ParameterOutput()
        {
            base.InputTextBox.Enabled = false;
        }

        object? value;
        public object? Value
        {
            get => value;
            set
            {
                this.value = value;
                InputTextBox.Text = this.value?.ToString();
                if (InputTextBox.Text is not null)
                    Validator.ValidateParameter(InputTextBox.Text);
            }
        }
    }
}