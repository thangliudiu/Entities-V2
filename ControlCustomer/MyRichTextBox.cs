using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlCustomer
{
    public class MyRichTextBox:RichTextBox
    {
        public MyRichTextBox()
        {
            Multiline = true;
            WordWrap = false;
            this.ScrollBars = RichTextBoxScrollBars.Both;
        }

        protected override bool IsInputKey(Keys keyData)
        {
            if (keyData == Keys.Tab || keyData == (Keys.Shift | Keys.Tab)) return true;
            return base.IsInputKey(keyData);
        }

        //protected override void OnKeyDown(KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Tab) Console.WriteLine("Tab!");
        //    base.OnKeyDown(e);
        //}
    }
}
