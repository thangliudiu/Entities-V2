using System;

namespace AutocompleteMenuNS
{
	public class MethodAutocompleteItem : AutocompleteItem
	{
		private string firstPart;

		private string lowercaseText;

		public MethodAutocompleteItem(string text) : base(text)
		{
			this.lowercaseText = base.Text.ToLower();
		}

		public override CompareResult Compare(string fragmentText)
		{
			int i = fragmentText.LastIndexOf('.');
			bool flag = i < 0;
			CompareResult result;
			if (flag)
			{
				result = CompareResult.Hidden;
			}
			else
			{
				string lastPart = fragmentText.Substring(i + 1);
				this.firstPart = fragmentText.Substring(0, i);
				bool flag2 = lastPart == "";
				if (flag2)
				{
					result = CompareResult.Visible;
				}
				else
				{
					bool flag3 = base.Text.StartsWith(lastPart, StringComparison.InvariantCultureIgnoreCase);
					if (flag3)
					{
						result = CompareResult.VisibleAndSelected;
					}
					else
					{
						bool flag4 = this.lowercaseText.Contains(lastPart.ToLower());
						if (flag4)
						{
							result = CompareResult.Visible;
						}
						else
						{
							result = CompareResult.Hidden;
						}
					}
				}
			}
			return result;
		}

		public override string GetTextForReplace()
		{
			return this.firstPart + "." + base.Text;
		}
	}
}
