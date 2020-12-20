using System;

namespace AutocompleteMenuNS
{
	public class Range
	{
		public ITextBoxWrapper TargetWrapper
		{
			get;
			private set;
		}

		public int Start
		{
			get;
			set;
		}

		public int End
		{
			get;
			set;
		}

		public string Text
		{
			get
			{
				string text = this.TargetWrapper.Text;
				bool flag = string.IsNullOrEmpty(text);
				string result;
				if (flag)
				{
					result = "";
				}
				else
				{
					bool flag2 = this.Start >= text.Length;
					if (flag2)
					{
						result = "";
					}
					else
					{
						bool flag3 = this.End > text.Length;
						if (flag3)
						{
							result = "";
						}
						else
						{
							result = this.TargetWrapper.Text.Substring(this.Start, this.End - this.Start);
						}
					}
				}
				return result;
			}
			set
			{
				this.TargetWrapper.SelectionStart = this.Start;
				this.TargetWrapper.SelectionLength = this.End - this.Start;
				this.TargetWrapper.SelectedText = value;
			}
		}

		public Range(ITextBoxWrapper targetWrapper)
		{
			this.TargetWrapper = targetWrapper;
		}
	}
}
