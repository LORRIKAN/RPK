using RPK.DataValidation;
using System.Windows.Forms;

namespace RPK.InterfaceElements.ResearcherFormElements
{
    public partial class ParameterInputWithBounds : ParameterInput
    {
        public ParameterInputWithBounds()
        {
            InitializeComponent();
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
                lowerBoundLabel.Text = $"Нижняя регламентная граница: {lowerBound}";
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
                upperBoundLabel.Text = $"Верхняя регламентная граница: {upperBound}";
            }
        }

        private bool boundsAreVisible;
        public bool BoundsAreVisible
        {
            get => boundsAreVisible;
            set
            {
                boundsAreVisible = value;
                if (boundsAreVisible is false)
                {
                    lowerBoundLabel.Visible = false;
                    upperBoundLabel.Visible = false;
                }
                else
                {
                    lowerBoundLabel.Visible = true;
                    upperBoundLabel.Visible = true;
                }
            }
        }

        private Label lowerBoundLabel;
        private Label upperBoundLabel;
        private void InitializeComponent()
        {
            this.lowerBoundLabel = new System.Windows.Forms.Label();
            this.upperBoundLabel = new System.Windows.Forms.Label();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.upperBoundLabel);
            this.groupBox.Controls.Add(this.lowerBoundLabel);
            this.groupBox.Size = new System.Drawing.Size(619, 180);
            this.groupBox.Controls.SetChildIndex(this.textBox, 0);
            this.groupBox.Controls.SetChildIndex(this.label, 0);
            this.groupBox.Controls.SetChildIndex(this.lowerBoundLabel, 0);
            this.groupBox.Controls.SetChildIndex(this.upperBoundLabel, 0);
            // 
            // lowerBoundLabel
            // 
            this.lowerBoundLabel.AutoSize = true;
            this.lowerBoundLabel.Location = new System.Drawing.Point(3, 65);
            this.lowerBoundLabel.Name = "lowerBoundLabel";
            this.lowerBoundLabel.Size = new System.Drawing.Size(269, 25);
            this.lowerBoundLabel.TabIndex = 2;
            this.lowerBoundLabel.Text = "Нижняя регламентная граница:";
            // 
            // upperBoundLabel
            // 
            this.upperBoundLabel.AutoSize = true;
            this.upperBoundLabel.Location = new System.Drawing.Point(3, 99);
            this.upperBoundLabel.Name = "upperBoundLabel";
            this.upperBoundLabel.Size = new System.Drawing.Size(271, 25);
            this.upperBoundLabel.TabIndex = 3;
            this.upperBoundLabel.Text = "Верхняя регламентная граница:";
            // 
            // ParameterInputWithBounds
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.Name = "ParameterInputWithBounds";
            this.Size = new System.Drawing.Size(619, 180);
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}