using System;

namespace AutocompleteMenuNS
{
	public class SnippetAutocompleteItem : AutocompleteItem
	{
		public SnippetAutocompleteItem(string snippet)
		{
			base.Text = snippet.Replace("\r", "");
			this.ToolTipTitle = "Code snippet:";
			this.ToolTipText = base.Text;
		}

		public override string ToString()
		{
			return this.MenuText ?? base.Text.Replace("\n", " ").Replace("^", "");
		}

		public override string GetTextForReplace()
		{
			return base.Text;
		}

		public override void OnSelected(SelectedEventArgs e)
		{
			ITextBoxWrapper tb = base.Parent.TargetControlWrapper;
			bool flag = !base.Text.Contains("^");
			if (!flag)
			{
				string text = tb.Text;
				for (int i = base.Parent.Fragment.Start; i < text.Length; i++)
				{
					bool flag2 = text[i] == '^';
                    if(!flag2) flag2 = text[i] == '.';
                    if (flag2)
					{
						tb.SelectionStart = i;
						tb.SelectionLength = 1;
						tb.SelectedText = "";
						break;
					}
				}
			}
		}

		public override CompareResult Compare(string fragmentText)
		{
			bool flag = base.Text.StartsWith(fragmentText, StringComparison.InvariantCultureIgnoreCase) && base.Text != fragmentText;
			CompareResult result;
			if (flag)
			{
				result = CompareResult.Visible;
			}
			else
			{
				result = CompareResult.Hidden;
			}
			return result;
		}
	}
}
