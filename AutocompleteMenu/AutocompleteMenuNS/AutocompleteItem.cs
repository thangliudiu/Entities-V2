using System;
using System.Drawing;

namespace AutocompleteMenuNS
{
	public class AutocompleteItem
	{
		public object Tag;

		private string toolTipTitle;

		private string toolTipText;

		private string menuText;

		public AutocompleteMenu Parent
		{
			get;
			internal set;
		}

		public string Text
		{
			get;
			set;
		}

		public int ImageIndex
		{
			get;
			set;
		}

		public virtual string ToolTipTitle
		{
			get
			{
				return this.toolTipTitle;
			}
			set
			{
				this.toolTipTitle = value;
			}
		}

		public virtual string ToolTipText
		{
			get
			{
				return this.toolTipText;
			}
			set
			{
				this.toolTipText = value;
			}
		}

		public virtual string MenuText
		{
			get
			{
				return this.menuText;
			}
			set
			{
				this.menuText = value;
			}
		}

		public AutocompleteItem()
		{
			this.ImageIndex = -1;
		}

		public AutocompleteItem(string text) : this()
		{
			this.Text = text;
		}

		public AutocompleteItem(string text, int imageIndex) : this(text)
		{
			this.ImageIndex = imageIndex;
		}

		public AutocompleteItem(string text, int imageIndex, string menuText) : this(text, imageIndex)
		{
			this.menuText = menuText;
		}

		public AutocompleteItem(string text, int imageIndex, string menuText, string toolTipTitle, string toolTipText) : this(text, imageIndex, menuText)
		{
			this.toolTipTitle = toolTipTitle;
			this.toolTipText = toolTipText;
		}

		public virtual string GetTextForReplace()
		{
			return this.Text;
		}

		public virtual CompareResult Compare(string fragmentText)
		{
			bool flag = this.Text.StartsWith(fragmentText, StringComparison.InvariantCultureIgnoreCase) && this.Text != fragmentText;
			CompareResult result;
			if (flag)
			{
				result = CompareResult.VisibleAndSelected;
			}
			else
			{
				result = CompareResult.Hidden;
			}
			return result;
		}

		public override string ToString()
		{
			return this.menuText ?? this.Text;
		}

		public virtual void OnSelected(SelectedEventArgs e)
		{
		}

		public virtual void OnPaint(PaintItemEventArgs e)
		{
			using (SolidBrush brush = new SolidBrush(e.IsSelected ? e.Colors.SelectedForeColor : e.Colors.ForeColor))
			{
				e.Graphics.DrawString(this.ToString(), e.Font, brush, e.TextRect, e.StringFormat);
			}
		}
	}
}
