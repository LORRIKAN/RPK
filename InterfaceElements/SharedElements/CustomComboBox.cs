using System;
using System.Windows.Forms;

namespace RPK.InterfaceElements.SharedElements
{
    public class CustomComboBox : ComboBox
    {
        public CustomComboBox()
        {

            SelectedIndexChanged += (sender, e) =>
            {
                if (SelectedIndex != PreviousSelectedIndex)
                {
                    NewIndexSelected?.Invoke(sender, e);
                    PreviousSelectedIndex = SelectedIndex;
                }
            };
        }

        public event EventHandler NewIndexSelected;

        private int PreviousSelectedIndex { get; set; } = -1;
    }
}