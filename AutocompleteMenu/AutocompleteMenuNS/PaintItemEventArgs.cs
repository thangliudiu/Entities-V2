using System;
using System.Drawing;
using System.Windows.Forms;

namespace AutocompleteMenuNS
{
	public class PaintItemEventArgs : PaintEventArgs
	{
		public RectangleF TextRect
		{
			get;
			internal set;
		}

		public StringFormat StringFormat
		{
			get;
			internal set;
		}

		public Font Font
		{
			get;
			internal set;
		}

		public bool IsSelected
		{
			get;
			internal set;
		}

		public bool IsHovered
		{
			get;
			internal set;
		}

		public Colors Colors
		{
			get;
			internal set;
		}

		public PaintItemEventArgs(Graphics graphics, Rectangle clipRect) : base(graphics, clipRect)
		{
		}
	}
}
