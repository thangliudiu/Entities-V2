using System;
using System.Drawing;

namespace AutocompleteMenuNS
{
	public class MulticolumnAutocompleteItem : SubstringAutocompleteItem
	{
		public bool CompareBySubstring
		{
			get;
			set;
		}

		public string[] MenuTextByColumns
		{
			get;
			set;
		}

		public int[] ColumnWidth
		{
			get;
			set;
		}

		public MulticolumnAutocompleteItem(string[] menuTextByColumns, string insertingText, bool compareBySubstring = true, bool ignoreCase = true) : base(insertingText, ignoreCase)
		{
			this.CompareBySubstring = compareBySubstring;
			this.MenuTextByColumns = menuTextByColumns;
		}

		public override CompareResult Compare(string fragmentText)
		{
			bool compareBySubstring = this.CompareBySubstring;
			CompareResult result;
			if (compareBySubstring)
			{
				result = base.Compare(fragmentText);
			}
			else
			{
				bool ignoreCase = this.ignoreCase;
				if (ignoreCase)
				{
					bool flag = base.Text.StartsWith(fragmentText, StringComparison.InvariantCultureIgnoreCase);
					if (flag)
					{
						result = CompareResult.VisibleAndSelected;
						return result;
					}
				}
				else
				{
					bool flag2 = base.Text.StartsWith(fragmentText);
					if (flag2)
					{
						result = CompareResult.VisibleAndSelected;
						return result;
					}
				}
				result = CompareResult.Hidden;
			}
			return result;
		}

		public override void OnPaint(PaintItemEventArgs e)
		{
			bool flag = this.ColumnWidth != null && this.ColumnWidth.Length != this.MenuTextByColumns.Length;
			if (flag)
			{
				throw new Exception("ColumnWidth.Length != MenuTextByColumns.Length");
			}
			int[] columnWidth = this.ColumnWidth;
			bool flag2 = columnWidth == null;
			if (flag2)
			{
				columnWidth = new int[this.MenuTextByColumns.Length];
				float step = e.TextRect.Width / (float)this.MenuTextByColumns.Length;
				for (int i = 0; i < this.MenuTextByColumns.Length; i++)
				{
					columnWidth[i] = (int)step;
				}
			}
			Pen pen = Pens.Silver;
			float x = e.TextRect.X;
			e.StringFormat.FormatFlags = (e.StringFormat.FormatFlags | StringFormatFlags.NoWrap);
			using (SolidBrush brush = new SolidBrush(e.IsSelected ? e.Colors.SelectedForeColor : e.Colors.ForeColor))
			{
				for (int j = 0; j < this.MenuTextByColumns.Length; j++)
				{
					int width = columnWidth[j];
					RectangleF rect = new RectangleF(x, e.TextRect.Top, (float)width, e.TextRect.Height);
					e.Graphics.DrawLine(pen, new PointF(x, e.TextRect.Top), new PointF(x, e.TextRect.Bottom));
					e.Graphics.DrawString(this.MenuTextByColumns[j], e.Font, brush, rect, e.StringFormat);
					x += (float)width;
				}
			}
		}
	}
}
