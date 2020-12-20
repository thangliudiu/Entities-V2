using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AutocompleteMenuNS
{
	[ToolboxItem(false)]
	internal class AutocompleteMenuHost : ToolStripDropDown
	{
		private IAutocompleteListView listView;

		public readonly AutocompleteMenu Menu;

		public ToolStripControlHost Host
		{
			get;
			set;
		}

		public IAutocompleteListView ListView
		{
			get
			{
				return this.listView;
			}
			set
			{
				bool flag = this.listView != null;
				if (flag)
				{
					(this.listView as Control).LostFocus -= new EventHandler(this.ListView_LostFocus);
				}
				bool flag2 = value == null;
				if (flag2)
				{
					this.listView = new AutocompleteListView();
				}
				else
				{
					bool flag3 = !(value is Control);
					if (flag3)
					{
						throw new Exception("ListView must be derived from Control class");
					}
					this.listView = value;
				}
				this.Host = new ToolStripControlHost(this.ListView as Control);
				this.Host.Margin = new Padding(2, 2, 2, 2);
				this.Host.Padding = Padding.Empty;
				this.Host.AutoSize = false;
				this.Host.AutoToolTip = false;
				(this.ListView as Control).MaximumSize = this.Menu.MaximumSize;
				(this.ListView as Control).Size = this.Menu.MaximumSize;
				(this.ListView as Control).LostFocus += new EventHandler(this.ListView_LostFocus);
				this.CalcSize();
				base.Items.Clear();
				base.Items.Add(this.Host);
				(this.ListView as Control).Parent = this;
			}
		}

		public override RightToLeft RightToLeft
		{
			get
			{
				return base.RightToLeft;
			}
			set
			{
				base.RightToLeft = value;
				(this.ListView as Control).RightToLeft = value;
			}
		}

		public AutocompleteMenuHost(AutocompleteMenu menu)
		{
			base.AutoClose = false;
			this.AutoSize = false;
			base.Margin = Padding.Empty;
			base.Padding = Padding.Empty;
			this.Menu = menu;
			this.ListView = new AutocompleteListView();
		}

		protected override void OnPaintBackground(PaintEventArgs e)
		{
			using (SolidBrush brush = new SolidBrush(this.listView.Colors.BackColor))
			{
				e.Graphics.FillRectangle(brush, e.ClipRectangle);
			}
		}

		internal void CalcSize()
		{
			this.Host.Size = (this.ListView as Control).Size;
			base.Size = new Size((this.ListView as Control).Size.Width + 4, (this.ListView as Control).Size.Height + 4);
		}

		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			bool flag = !(this.ListView as Control).Focused;
			if (flag)
			{
				base.Close();
			}
		}

		private void ListView_LostFocus(object sender, EventArgs e)
		{
			bool flag = !this.Focused;
			if (flag)
			{
				base.Close();
			}
		}
	}
}
