using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Entities
{
    public static class RenderControlProvider
    {
        public static bool CreateItemCbbTable(List<string> TableNames, ComboBox comboBox)
        {
            bool isSucceess = false;
            try
            {
                comboBox.Items.Clear();
                for (int i = 0; i < TableNames.Count; i++)
                {
                    comboBox.Items.Add(TableNames[i]);
                }
                isSucceess = true;
                //richTextBox1.Text = string.Join("", TableNames);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return isSucceess;
        }

        public static void RenderCheckBox1(Panel panel, List<string> textCheckBox)
        {
            List<string> ExcludeColumn = new List<string>() { "createddate", "createdby", "editeddate", "editedby", "isdisabled" };
            panel.Controls.Clear();

            int height = 0;
            foreach (var text in textCheckBox)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Text = text;
                checkBox.Checked = false;
                checkBox.Location = new Point(0, height);
                checkBox.Width = 140;
                checkBox.BackColor = Color.LightBlue;
                height += checkBox.Height + 1;
                panel.Controls.Add(checkBox);
            }

        }

        public static void RenderCheckBox2(Panel panel, List<string> textCheckBox, List<string> textCheckBoxTrue = null)
        {
            List<string> ExcludeColumn = new List<string>() { "createddate", "createdby", "editeddate", "editedby", "isdisabled" };
            panel.Controls.Clear();

            int height = 0;
            foreach (var text in textCheckBox)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Text = text;
                checkBox.Checked = false;
                if (textCheckBoxTrue == null && ExcludeColumn.Contains(text, StringComparer.OrdinalIgnoreCase) == false)
                {
                    checkBox.Checked = true;
                }
                if (textCheckBoxTrue != null && textCheckBoxTrue.Contains(text, StringComparer.OrdinalIgnoreCase) == true)
                    checkBox.Checked = true;
                checkBox.Location = new Point(0, height);
                checkBox.Width = 140;
                checkBox.BackColor = Color.LightBlue;
                height += checkBox.Height + 1;
                panel.Controls.Add(checkBox);
            }

        }

    }
}
