using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AutocompleteMenuNS
{
	public interface IAutocompleteListView
	{
		event EventHandler ItemSelected;

		event EventHandler<HoveredEventArgs> ItemHovered;

		ImageList ImageList
		{
			get;
			set;
		}

		int SelectedItemIndex
		{
			get;
			set;
		}

		int HighlightedItemIndex
		{
			get;
			set;
		}

		IList<AutocompleteItem> VisibleItems
		{
			get;
			set;
		}

		int ToolTipDuration
		{
			get;
			set;
		}

		Colors Colors
		{
			get;
			set;
		}

		void ShowToolTip(AutocompleteItem autocompleteItem, Control control = null);

		Rectangle GetItemRectangle(int itemIndex);
	}
}
