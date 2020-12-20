using System;
using System.Windows.Forms;

namespace AutocompleteMenuNS
{
	public class SelectedEventArgs : EventArgs
	{
		public AutocompleteItem Item
		{
			get;
			internal set;
		}

		public Control Control
		{
			get;
			set;
		}
	}
}
