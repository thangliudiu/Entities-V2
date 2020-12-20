using System;

namespace AutocompleteMenuNS
{
	public class SubstringAutocompleteItem : AutocompleteItem
	{
		protected readonly string lowercaseText;

		protected readonly bool ignoreCase;

		public SubstringAutocompleteItem(string text, bool ignoreCase = true) : base(text)
		{
			this.ignoreCase = ignoreCase;
			if (ignoreCase)
			{
				this.lowercaseText = text.ToLower();
			}
		}

		public override CompareResult Compare(string fragmentText)
		{
			bool flag = this.ignoreCase;
			CompareResult result;
			if (flag)
			{
				bool flag2 = this.lowercaseText.Contains(fragmentText.ToLower());
				if (flag2)
				{
					result = CompareResult.Visible;
					return result;
				}
			}
			else
			{
				bool flag3 = base.Text.Contains(fragmentText);
				if (flag3)
				{
					result = CompareResult.Visible;
					return result;
				}
			}
			result = CompareResult.Hidden;
			return result;
		}
	}
}
