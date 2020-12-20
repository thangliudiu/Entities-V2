using System;
using System.Drawing;

namespace AutocompleteMenuNS
{
	[Serializable]
	public class Colors
	{
		public Color ForeColor
		{
			get;
			set;
		}

		public Color BackColor
		{
			get;
			set;
		}

		public Color SelectedForeColor
		{
			get;
			set;
		}

		public Color SelectedBackColor
		{
			get;
			set;
		}

		public Color SelectedBackColor2
		{
			get;
			set;
		}

		public Color HighlightingColor
		{
			get;
			set;
		}

		public Colors()
		{
			this.ForeColor = Color.Black;
			this.BackColor = Color.White;
			this.SelectedForeColor = Color.Black;
			this.SelectedBackColor = Color.Orange;
			this.SelectedBackColor2 = Color.White;
			this.HighlightingColor = Color.Orange;
		}
	}
}
