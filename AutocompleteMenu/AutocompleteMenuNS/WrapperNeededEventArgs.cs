using System;
using System.Windows.Forms;

namespace AutocompleteMenuNS
{
	public class WrapperNeededEventArgs : EventArgs
	{
		public Control TargetControl
		{
			get;
			private set;
		}

		public ITextBoxWrapper Wrapper
		{
			get;
			set;
		}

		public WrapperNeededEventArgs(Control targetControl)
		{
			this.TargetControl = targetControl;
		}
	}
}
