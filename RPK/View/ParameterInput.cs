#nullable enable
using System;
using System.ComponentModel;
using System.Windows.Forms;
using RPK.View.DataValidation;

namespace RPK.View
{
    public partial class ParameterInput : UserControl
    {
        public ParameterInput()
        {
            InitializeComponent();
        }

        public string ParameterName { get => groupBox.Text; set => groupBox.Text = value; }

        public string MeasureUnit { get => label.Text; set => label.Text = value; }

        [Browsable(false)]
        public Label MeasureUnitLabel => label;

        [Browsable(false)]
        public TextBox InputTextBox => textBox;

        [Browsable(false)]
        public GroupBox GroupBox => groupBox;

        [Browsable(false)]
        [ReadOnly(true)]
        public DataValidatorBase DefaultValidator { get; set; } = new DoubleTypeParameterValidator();

        public event Action<ParameterInput, object>? ParsedAndValidated;

        public new event Action<ParameterInput, string>? Invalidated;

        [Browsable(false)]
        [ReadOnly(true)]
        public ParameterInputStatus ParameterInputStatus { get; private set; } = ParameterInputStatus.IsNullOrEmpty;

        private void TextBox_Validating(object sender, CancelEventArgs e)
        {
            if (this.textBox.Modified is false)
                return;

            if (string.IsNullOrEmpty(this.textBox.Text) || string.IsNullOrWhiteSpace(this.textBox.Text))
            {
                ParameterInputStatus = ParameterInputStatus.IsNullOrEmpty;
                Invalidated?.Invoke(this, "Поле не может быть пустым");
                return;
            }

            var textBox = (TextBox)sender;

            (object? result, string? errorMessage) = DefaultValidator.ValidateParameter(textBox.Text);

            if (result is null)
            {
                ParameterInputStatus = ParameterInputStatus.Error;
                Invalidated?.Invoke(this, errorMessage ?? "Параметр был задан неверно");
                return;
            }

            ParameterInputStatus = ParameterInputStatus.Validated;
            ParsedAndValidated?.Invoke(this, result);
        }
    }

    public enum ParameterInputStatus
    {
        Error,
        IsNullOrEmpty,
        Validated
    }
}
