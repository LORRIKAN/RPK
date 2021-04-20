#nullable enable

namespace RPK.InterfaceElements
{
    public class ParameterOutput : ParameterInput
    {
        public ParameterOutput()
        {
            base.InputTextBox.Enabled = false;
        }
    }
}