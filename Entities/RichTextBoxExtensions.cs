using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Entities
{
    public static class RichTextBoxExtensions
    {
        public static bool IsFirstKeyWordFromLine(this RichTextBox box, int row, string text)
        {
            if (row == 0) return false;
            int index = box.GetFirstCharIndexFromLine(row - 1);
            int length = text.Length;

            if (box.Text.Substring(index, length).Equals(text, StringComparison.OrdinalIgnoreCase) == true)
                return true;
            return false;
        }

        public static void AppendText(this RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }

        public static void WriteText(this RichTextBox box, string text, Color color)
        {
            box.Text = "";
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }

        public static void ChangeFontSizeText(this RichTextBox box, int row, string text, float emSize)
        {
            if (row == 0) return;
            box.SuspendLayout();
            Point scroll = box.AutoScrollOffset;
            int slct = box.SelectionIndent;
            int ss = box.SelectionStart;

            text = text.Replace("\\", "\\\\");
            text = text.Replace(".", "\\.");

            int indexMin = box.GetFirstCharIndexFromLine(row - 1); // posision;
            int indexMax = box.Lines[row - 1].Length + indexMin;

            MatchCollection mathes = Regex.Matches(box.Text, text, RegexOptions.Singleline);

            foreach (Match match in mathes)
            {
                var index = match.Index;
                if (index > indexMax || index < indexMin) continue;
                box.SelectionStart = index;
                box.SelectionLength = match.Length;
                box.SelectionFont = new Font(box.SelectionFont.FontFamily, emSize);

            }


            box.SelectionStart = ss;
            box.SelectionIndent = slct;
            box.AutoScrollOffset = scroll;
            box.ResumeLayout(true);
        }

        public static void ChangeFontText(this RichTextBox box, string text, Font font)
        {
            box.SuspendLayout();
            Point scroll = box.AutoScrollOffset;
            int slct = box.SelectionIndent;
            int ss = box.SelectionStart;
            if (text == ".") text = "\\" + text;
            else
            {
                text = text.Replace("\\", "\\\\");
            }
            MatchCollection mathes = Regex.Matches(box.Text, text, RegexOptions.Singleline);

            foreach (Match match in mathes)
            {
                box.SelectionStart = match.Index;
                box.SelectionLength = match.Length;
                box.SelectionFont = font;
            }


            box.SelectionStart = ss;
            box.SelectionIndent = slct;
            box.AutoScrollOffset = scroll;
            box.ResumeLayout(true);
        }

        public static void ChangeCollorText(this RichTextBox box, string text, Color color, bool isBold = false)
        {
            box.SuspendLayout();
            Point scroll = box.AutoScrollOffset;
            int slct = box.SelectionIndent;
            int ss = box.SelectionStart;

            if (text == ".") text = "\\" + text;
            else
            {
                text = text.Replace("\\", "\\\\");
            }
            MatchCollection mathes = Regex.Matches(box.Text, text, RegexOptions.Singleline);

            foreach (Match match in mathes)
            {
                box.SelectionStart = match.Index;
                box.SelectionLength = match.Length;
                box.SelectionColor = color;
                if (isBold == true) box.SelectionFont = new Font(box.SelectionFont.FontFamily, box.SelectionFont.Size, FontStyle.Bold);
            }


            box.SelectionStart = ss;
            box.SelectionIndent = slct;
            box.AutoScrollOffset = scroll;
            box.ResumeLayout(true);
        }

        public static void ChangeCollorText(this RichTextBox box, int row, string text, Color color, bool isBold = false)
        {
            if (row == 0) return;
            box.SuspendLayout();
            Point scroll = box.AutoScrollOffset;
            int slct = box.SelectionIndent;
            int ss = box.SelectionStart;


            int indexMin = box.GetFirstCharIndexFromLine(row - 1); // posision;
            int indexMax = box.Lines[row - 1].Length + indexMin;
            text = text.Replace("\\", "\\\\");
            text = text.Replace(".", "\\.");
            text = text.Replace("*", "\\*");
            MatchCollection mathes = Regex.Matches(box.Text, text, RegexOptions.Singleline);

            foreach (Match match in mathes)
            {
                var index = match.Index;
                if (index > indexMax || index < indexMin) continue;

                box.SelectionStart = index;
                box.SelectionLength = match.Length;
                box.SelectionColor = color;

                if (isBold == true) box.SelectionFont = new Font(box.SelectionFont.FontFamily, box.SelectionFont.Size, FontStyle.Bold);
            }

            box.SelectionStart = ss;
            box.SelectionIndent = slct;
            box.AutoScrollOffset = scroll;
            box.ResumeLayout(true);
        }

        public static void ChangeBackCollorText(this RichTextBox box, int row, string text, Color color, bool isBold = false)
        {
            if (row == 0) return;
            box.SuspendLayout();
            Point scroll = box.AutoScrollOffset;
            int slct = box.SelectionIndent;
            int ss = box.SelectionStart;


            int indexMin = box.GetFirstCharIndexFromLine(row - 1); // posision;
            int indexMax = box.Lines[row - 1].Length + indexMin;

            text = text.Replace("\\", "\\\\");
            text = text.Replace(".", "\\.");
            MatchCollection mathes = Regex.Matches(box.Text, text, RegexOptions.Singleline);

            foreach (Match match in mathes)
            {
                var index = match.Index;
                if (index > indexMax || index < indexMin) continue;

                box.SelectionStart = index;
                box.SelectionLength = match.Length;
                box.SelectionBackColor = color;

                if (isBold == true) box.SelectionFont = new Font(box.SelectionFont.FontFamily, box.SelectionFont.Size, FontStyle.Bold);
            }

            box.SelectionStart = ss;
            box.SelectionIndent = slct;
            box.AutoScrollOffset = scroll;
            box.ResumeLayout(true);
        }

        public static void ChangeBackCollorText(this RichTextBox box, MatchCollection mathes, Color color, bool isBold = false)
        {
            box.SuspendLayout();
            Point scroll = box.AutoScrollOffset;
            int slct = box.SelectionIndent;
            int ss = box.SelectionStart;

            foreach (Match match in mathes)
            {
                var index = match.Index;
                box.SelectionStart = index;
                box.SelectionLength = match.Length;
                box.SelectionBackColor = color;
                if (isBold == true) box.SelectionFont = new Font(box.SelectionFont.FontFamily, box.SelectionFont.Size, FontStyle.Bold);
            }

            box.SelectionStart = ss;
            box.SelectionIndent = slct;
            box.AutoScrollOffset = scroll;
            box.ResumeLayout(true);
        }

        public static void ChangeCollorLine(this RichTextBox box, int row, Color color)
        {
            if (row == 0) return;
            box.SuspendLayout();
            Point scroll = box.AutoScrollOffset;
            int slct = box.SelectionIndent;
            int ss = box.SelectionStart;


            int indexMin = box.GetFirstCharIndexFromLine(row - 1); // posision;
            box.SelectionStart = indexMin;
            box.SelectionLength = box.Lines[row - 1].Length;
            box.SelectionBackColor = color;

            box.SelectionStart = ss;
            box.SelectionIndent = slct;
            box.AutoScrollOffset = scroll;
            box.ResumeLayout(true);
        }

        public static void SetAllColor(this RichTextBox box, Color color)
        {
            box.SelectionStart = 0;
            box.SelectionLength = box.Text.Length;
            box.SelectionColor = color;

        }

        public static void SetAllFont(this RichTextBox box, Font font)
        {
            box.SelectionStart = 0;
            box.SelectionLength = box.Text.Length;
            box.SelectionFont = font;
        }

        public static void SetAllBackColor(this RichTextBox box, Color color)
        {
            box.SelectionStart = 0;
            box.SelectionLength = box.Text.Length;
            box.SelectionBackColor = color;
        }

        public static void ChangeCollorMatch(this RichTextBox box, List<ColorInfo> colorInfos, Color? colorDefault = null)
        {
            if (colorDefault == null)
            {
                colorDefault = Color.FromArgb(200, 200, 200);
            }

            box.SuspendLayout();
            Point scroll = box.AutoScrollOffset;
            int slct = box.SelectionIndent;
            int ss = box.SelectionStart;

            //set white color all text;
            box.SelectionStart = 0;
            box.SelectionLength = box.Text.Length;
            box.SelectionColor = (Color)colorDefault;

            foreach (ColorInfo ColorInfo in colorInfos)
            {
                box.SelectionStart = ColorInfo.Posistion;
                box.SelectionLength = ColorInfo.Length;
                box.SelectionColor = ColorInfo.Color;
            }


            box.SelectionStart = ss;
            box.SelectionIndent = slct;
            box.AutoScrollOffset = scroll;
            box.ResumeLayout(true);
        }

        public static void AppendTable(this RichTextBox box, IEnumerable<string> list)
        {
            int count = 0; int lengthItem = 1;

            int maxLengthTableName = list.Max(x => x.Length);
            if (maxLengthTableName < 10) maxLengthTableName = 12;
            else
            {
                maxLengthTableName += 2;
            }

            if (maxLengthTableName < 13)
            {
                lengthItem = 5;
            }
            else if (maxLengthTableName < 20)
            {
                lengthItem = 4;
            }
            else if (maxLengthTableName < 50)
            {
                lengthItem = 3;
            }

            for (int i = 0; i < lengthItem * 100; i += lengthItem)
            {
                count++;
                var _tables = list.Skip(i).Take(lengthItem);

                if (_tables == null || _tables.Count() == 0)
                    break;
                box.AppendText(JoinWithPad(_tables, maxLengthTableName), Color.Black);
                box.AppendText("\n");
            }

            for (int i = 0; i < box.Lines.Count(); i++)
            {
                if (i % 2 != 0)
                    box.ChangeCollorLine(i + 1, Color.LemonChiffon);
            }
        }

        public static string JoinWithPad(IEnumerable<string> list, int maxlength, string charJoin = "")
        {
            string result = "";
            foreach (var item in list)
            {
                result += (item + charJoin).PadRight(maxlength - 1) + "| ";
            }

            return result;
        }
    }

}
