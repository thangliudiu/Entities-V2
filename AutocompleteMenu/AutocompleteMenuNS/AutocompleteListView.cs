using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace AutocompleteMenuNS
{
	[ToolboxItem(false)]
	public class AutocompleteListView : UserControl, IAutocompleteListView
	{
		private readonly ToolTip toolTip = new ToolTip();

		private int oldItemCount;

		private int selectedItemIndex = -1;

		private IList<AutocompleteItem> visibleItems;

		private int itemHeight;

		private Point mouseEnterPoint;

		[method: CompilerGenerated]
		public event EventHandler ItemSelected;

		[method: CompilerGenerated]
		public event EventHandler<HoveredEventArgs> ItemHovered;

		public int HighlightedItemIndex
		{
			get;
			set;
		}

		public int ToolTipDuration
		{
			get;
			set;
		}

		public Colors Colors
		{
			get;
			set;
		}

		public int ItemHeight
		{
			get
			{
				return this.itemHeight;
			}
			set
			{
				this.itemHeight = value;
				base.VerticalScroll.SmallChange = value;
				this.oldItemCount = -1;
				this.AdjustScroll();
			}
		}

		public override Font Font
		{
			get
			{
				return base.Font;
			}
			set
			{
				base.Font = value;
				this.ItemHeight = this.Font.Height + 2;
			}
		}

		public int LeftPadding
		{
			get;
			set;
		}

		public ImageList ImageList
		{
			get;
			set;
		}

		public IList<AutocompleteItem> VisibleItems
		{
			get
			{
				return this.visibleItems;
			}
			set
			{
				this.visibleItems = value;
				this.SelectedItemIndex = -1;
				this.AdjustScroll();
				base.Invalidate();
			}
		}

		public int SelectedItemIndex
		{
			get
			{
				return this.selectedItemIndex;
			}
			set
			{
				AutocompleteItem item = null;
				bool flag = value >= 0 && value < this.VisibleItems.Count;
				if (flag)
				{
					item = this.VisibleItems[value];
				}
				this.selectedItemIndex = value;
				this.OnItemHovered(new HoveredEventArgs
				{
					Item = item
				});
				bool flag2 = item != null;
				if (flag2)
				{
					this.ShowToolTip(item, null);
					this.ScrollToSelected();
				}
				base.Invalidate();
			}
		}

		internal AutocompleteListView()
		{
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			base.Font = new Font(FontFamily.GenericSansSerif, 9f);
			this.ItemHeight = this.Font.Height + 2;
			base.VerticalScroll.SmallChange = this.ItemHeight;
			this.BackColor = Color.White;
			this.LeftPadding = 18;
			this.ToolTipDuration = 3000;
			this.Colors = new Colors();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.toolTip.Dispose();
			}
			base.Dispose(disposing);
		}

		private void OnItemHovered(HoveredEventArgs e)
		{
			bool flag = this.ItemHovered != null;
			if (flag)
			{
				this.ItemHovered(this, e);
			}
		}

		private void AdjustScroll()
		{
			bool flag = this.VisibleItems == null;
			if (!flag)
			{
				bool flag2 = this.oldItemCount == this.VisibleItems.Count;
				if (!flag2)
				{
					int needHeight = this.ItemHeight * this.VisibleItems.Count + 1;
					base.Height = Math.Min(needHeight, this.MaximumSize.Height);
					base.AutoScrollMinSize = new Size(0, needHeight);
					this.oldItemCount = this.VisibleItems.Count;
				}
			}
		}

		private void ScrollToSelected()
		{
			int y = this.SelectedItemIndex * this.ItemHeight - base.VerticalScroll.Value;
			bool flag = y < 0;
			if (flag)
			{
				base.VerticalScroll.Value = this.SelectedItemIndex * this.ItemHeight;
			}
			bool flag2 = y > base.ClientSize.Height - this.ItemHeight;
			if (flag2)
			{
				base.VerticalScroll.Value = Math.Min(base.VerticalScroll.Maximum, this.SelectedItemIndex * this.ItemHeight - base.ClientSize.Height + this.ItemHeight);
			}
			base.AutoScrollMinSize -= new Size(1, 0);
			base.AutoScrollMinSize += new Size(1, 0);
		}

		public Rectangle GetItemRectangle(int itemIndex)
		{
			int y = itemIndex * this.ItemHeight - base.VerticalScroll.Value;
			return new Rectangle(0, y, base.ClientSize.Width - 1, this.ItemHeight - 1);
		}

		protected override void OnPaintBackground(PaintEventArgs e)
		{
			e.Graphics.Clear(this.Colors.BackColor);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			bool rtl = this.RightToLeft == RightToLeft.Yes;
			this.AdjustScroll();
			int startI = base.VerticalScroll.Value / this.ItemHeight - 1;
			int finishI = (base.VerticalScroll.Value + base.ClientSize.Height) / this.ItemHeight + 1;
			startI = Math.Max(startI, 0);
			finishI = Math.Min(finishI, this.VisibleItems.Count);
			for (int i = startI; i < finishI; i++)
			{
				int y = i * this.ItemHeight - base.VerticalScroll.Value;
				bool flag = this.ImageList != null && this.VisibleItems[i].ImageIndex >= 0;
				if (flag)
				{
					bool flag2 = rtl;
					if (flag2)
					{
						e.Graphics.DrawImage(this.ImageList.Images[this.VisibleItems[i].ImageIndex], base.Width - 1 - this.LeftPadding, y);
					}
					else
					{
						e.Graphics.DrawImage(this.ImageList.Images[this.VisibleItems[i].ImageIndex], 1, y);
					}
				}
				Rectangle textRect = new Rectangle(this.LeftPadding, y, base.ClientSize.Width - 1 - this.LeftPadding, this.ItemHeight - 1);
				bool flag3 = rtl;
				if (flag3)
				{
					textRect = new Rectangle(1, y, base.ClientSize.Width - 1 - this.LeftPadding, this.ItemHeight - 1);
				}
				bool flag4 = i == this.SelectedItemIndex;
				if (flag4)
				{
					Brush selectedBrush = new LinearGradientBrush(new Point(0, y - 3), new Point(0, y + this.ItemHeight), this.Colors.SelectedBackColor2, this.Colors.SelectedBackColor);
					e.Graphics.FillRectangle(selectedBrush, textRect);
					using (Pen pen = new Pen(this.Colors.SelectedBackColor2))
					{
						e.Graphics.DrawRectangle(pen, textRect);
					}
				}
				bool flag5 = i == this.HighlightedItemIndex;
				if (flag5)
				{
					using (Pen pen2 = new Pen(this.Colors.HighlightingColor))
					{
						e.Graphics.DrawRectangle(pen2, textRect);
					}
				}
				StringFormat sf = new StringFormat();
				bool flag6 = rtl;
				if (flag6)
				{
					sf.FormatFlags = StringFormatFlags.DirectionRightToLeft;
				}
				PaintItemEventArgs args = new PaintItemEventArgs(e.Graphics, e.ClipRectangle)
				{
					Font = this.Font,
					TextRect = new RectangleF(textRect.Location, textRect.Size),
					StringFormat = sf,
					IsSelected = (i == this.SelectedItemIndex),
					IsHovered = (i == this.HighlightedItemIndex),
					Colors = this.Colors
				};
				this.VisibleItems[i].OnPaint(args);
			}
		}

		protected override void OnScroll(ScrollEventArgs se)
		{
			base.OnScroll(se);
			base.Invalidate(true);
		}

		protected override void OnMouseClick(MouseEventArgs e)
		{
			base.OnMouseClick(e);
			bool flag = e.Button == MouseButtons.Left;
			if (flag)
			{
				this.SelectedItemIndex = this.PointToItemIndex(e.Location);
				this.ScrollToSelected();
				base.Invalidate();
			}
		}

		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			this.mouseEnterPoint = Control.MousePosition;
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			bool flag = this.mouseEnterPoint != Control.MousePosition;
			if (flag)
			{
				this.HighlightedItemIndex = this.PointToItemIndex(e.Location);
				base.Invalidate();
			}
		}

		protected override void OnMouseDoubleClick(MouseEventArgs e)
		{
			base.OnMouseDoubleClick(e);
			this.SelectedItemIndex = this.PointToItemIndex(e.Location);
			base.Invalidate();
			this.OnItemSelected();
		}

		private void OnItemSelected()
		{
			bool flag = this.ItemSelected != null;
			if (flag)
			{
				this.ItemSelected(this, EventArgs.Empty);
			}
		}

		private int PointToItemIndex(Point p)
		{
			return (p.Y + base.VerticalScroll.Value) / this.ItemHeight;
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			AutocompleteMenuHost host = base.Parent as AutocompleteMenuHost;
			bool flag = host != null;
			bool result;
			if (flag)
			{
				bool flag2 = host.Menu.ProcessKey((char)keyData, Keys.None);
				if (flag2)
				{
					result = true;
					return result;
				}
			}
			result = base.ProcessCmdKey(ref msg, keyData);
			return result;
		}

		public void SelectItem(int itemIndex)
		{
			this.SelectedItemIndex = itemIndex;
			this.ScrollToSelected();
			base.Invalidate();
		}

		public void SetItems(List<AutocompleteItem> items)
		{
			this.VisibleItems = items;
			this.SelectedItemIndex = -1;
			this.AdjustScroll();
			base.Invalidate();
		}

		public void ShowToolTip(AutocompleteItem autocompleteItem, Control control = null)
		{
			string title = autocompleteItem.ToolTipTitle;
			string text = autocompleteItem.ToolTipText;
			bool flag = control == null;
			if (flag)
			{
				control = this;
			}
			bool flag2 = string.IsNullOrEmpty(title);
			if (flag2)
			{
				this.toolTip.ToolTipTitle = null;
				this.toolTip.SetToolTip(control, null);
			}
			else
			{
				bool flag3 = string.IsNullOrEmpty(text);
				if (flag3)
				{
					this.toolTip.ToolTipTitle = null;
					this.toolTip.Show(title, control, base.Width + 3, 0, this.ToolTipDuration);
				}
				else
				{
					this.toolTip.ToolTipTitle = title;
					this.toolTip.Show(text, control, base.Width + 3, 0, this.ToolTipDuration);
				}
			}
		}
	}
}
