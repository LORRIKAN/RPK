#nullable enable
using RPK.DataValidation;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace RPK.InterfaceElements
{
    public partial class ParameterInput : UserControl
    {
        public ParameterInput()
        {
            InitializeComponent();

            Validator = new DoubleTypeParameterParserAndValidator();
        }

        private void Validator_Invalidated(string? errorMessage)
        {
            ParameterInputStatus = ParameterInputStatus.Error;
            Invalidated?.Invoke(this, errorMessage ?? "Параметр был задан неверно");
        }

        private void Validator_Validated(object result)
        {
            value = result;
            ParameterInputStatus = ParameterInputStatus.Validated;
            ParsedAndValidated?.Invoke(this, result);
        }

        public string ParameterName { get => groupBox.Text; set => groupBox.Text = value; }

        public string? MeasureUnit { get => label.Text; set => label.Text = value; }

        [Browsable(false)]
        public Label MeasureUnitLabel => label;

        [Browsable(false)]
        public TextBox InputTextBox => textBox;

        [Browsable(false)]
        public GroupBox GroupBox => groupBox;

        DataParserAndValidatorBase validator = new DoubleTypeParameterParserAndValidator();
        [Browsable(false)]
        [ReadOnly(true)]
        public DataParserAndValidatorBase Validator
        {
            get => validator;
            set
            {
                validator = value;

                validator.Validated += Validator_Validated;
                validator.Invalidated += Validator_Invalidated;
            }
        }

        private object? value;
        public object? Value
        {
            get => value; set
            {
                this.value = value;
                InputTextBox.Text = this.value?.ToString();
                if (string.IsNullOrEmpty(InputTextBox.Text) is false && InputTextBox.Text is not null)
                    Validator.ValidateParameter(InputTextBox.Text);
            }
        }

        public event Action<ParameterInput, object>? ParsedAndValidated;

        public new event Action<ParameterInput, string>? Invalidated;

        [Browsable(false)]
        [ReadOnly(true)]
        public ParameterInputStatus ParameterInputStatus { get; private set; } = ParameterInputStatus.IsNullOrEmpty;

        protected void TextBox_Validating(object sender, CancelEventArgs e)
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

            Validator?.ValidateParameter(textBox.Text);
        }
    }

    public enum ParameterInputStatus
    {
        Error,
        IsNullOrEmpty,
        Validated
    }
}
