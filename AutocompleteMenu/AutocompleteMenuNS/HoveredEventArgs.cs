using System;

namespace AutocompleteMenuNS
{
	public class HoveredEventArgs : EventArgs
	{
		public AutocompleteItem Item
		{
			get;
			internal set;
		}
	}
}
