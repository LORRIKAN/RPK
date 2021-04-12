using RPK.DataValidation;
using System.Windows.Forms;

namespace RPK.InterfaceElements
{
    public partial class ParameterInputWithBounds : ParameterInput
    {
        public ParameterInputWithBounds()
        {
            Validator = new DoubleTypeParameterParserAndValidatorWithCustomBounds();
        }

        double lowerBound;
        public double LowerBound
        {
            get => lowerBound;
            set
            {
                lowerBound = value;
                ((DoubleTypeParameterParserAndValidatorWithCustomBounds)Validator).LowerBound = lowerBound;
                ChangeBoundsText();
            }
        }

        double upperBound;
        public double UpperBound
        {
            get => upperBound;
            set
            {
                upperBound = value;
                ((DoubleTypeParameterParserAndValidatorWithCustomBounds)Validator).UpperBound = upperBound;
                ChangeBoundsText();
            }
        }

        double oldUpperBound = default;
        double oldLowerBound = default;
        private void ChangeBoundsText()
        {
            if (GroupBox.Text.Contains($"В пределах от {oldLowerBound} до {oldUpperBound}"))
            {
                GroupBox.Text = GroupBox.Text.Replace($"В пределах от {oldLowerBound} до {oldUpperBound}",
                    $"В пределах от {LowerBound} до {UpperBound}");
            }
            else
            {
                GroupBox.Text += $" (В пределах от {LowerBound} до {UpperBound})";
            }

            oldLowerBound = LowerBound;
            oldUpperBound = UpperBound;
        }
    }
}