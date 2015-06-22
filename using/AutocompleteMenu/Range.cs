using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace AutocompleteMenuNS
{
    public class Range
    {
        public Control control;
        public int Start { get; set; }
        public int End { get; set; }

        public Range(Control control)
        {
            this.control = control;
        }

        public string Text
        {
            get
            {
                return control.Text.Substring(Start, End - Start);
            }

            set
            {
                if (control is TextBoxBase)
                {
                    (control as TextBoxBase).SelectionStart = Start;
                    (control as TextBoxBase).SelectionLength = End - Start;
                    (control as TextBoxBase).SelectedText = value;
                }
                else
                {
                    control.Text = control.Text.Substring(0, Start) + value + control.Text.Substring(End);
                }
            }
        }
    }
}
