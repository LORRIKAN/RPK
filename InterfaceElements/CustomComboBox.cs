using System;
using System.Windows.Forms;

namespace RPK.InterfaceElements
{
    public class CustomComboBox : ComboBox
    {
        public CustomComboBox()
        {
            
            base.SelectedIndexChanged += (sender, e) => 
            {
                if (this.SelectedIndex != PreviousSelectedIndex)
                {
                    this.NewIndexSelected?.Invoke(sender, e);
                    this.PreviousSelectedIndex = this.SelectedIndex;
                }
            };
        }

        public event EventHandler NewIndexSelected;

        private int PreviousSelectedIndex { get; set; } = -1;
    }
}