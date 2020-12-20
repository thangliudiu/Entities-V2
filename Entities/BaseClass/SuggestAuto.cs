using AutocompleteMenuNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Entities
{
    public class SuggestAuto : AutocompleteItem
    {
        public static string KeySearch { get; set; }

        public static List<SuggestAuto> CreateListSuggest(string keySearch, IEnumerable<string> texts)
        {
            List<SuggestAuto> SuggestAutos = new List<SuggestAuto>();
            keySearch = keySearch.ToLower();
            foreach (var text in texts)
            {
                SuggestAuto suggestAuto = new SuggestAuto(text, keySearch);
                SuggestAutos.Add(suggestAuto);
            }

            return SuggestAutos;
        }

        public static void SetList(AutocompleteMenu autocompleteMenu,List<SuggestAuto> suggests,string keySearch)
        {
            KeySearch = keySearch.ToLower();
            autocompleteMenu.SetAutocompleteItems(suggests);
        }

        public string Key { get; set; }

        private string text { get; set; }

        public SuggestAuto(string text, string key = null) : base(text)
        {
            Key = key.ToLower();
            this.text = text.ToLower();
        }

        public override CompareResult Compare(string fragmentText)
        {
            if (Regex.IsMatch(fragmentText, "\\.$"))
                return CompareResult.Visible;

            fragmentText = fragmentText.Split('.').Last();
            if (KeySearch == null) return CompareResult.Hidden;
            fragmentText = fragmentText.ToLower();

            if((Key != null || KeySearch != null) && Key != KeySearch)
                return CompareResult.Hidden;

            if (fragmentText == text)
                return CompareResult.VisibleAndSelected;

            if (text.Contains(fragmentText))
                return CompareResult.Visible;

            return CompareResult.Hidden;
        }

     
    }
    
}
