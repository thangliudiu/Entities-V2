using AutocompleteMenuNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Entities
{
    public class SuggestController
    {
        private List<SuggestAuto> SuggestAutoKeyWords { get; set; }
        private string keySearchKeyWord = "keyword"; //SearchKey "KeyWord"
        private List<SuggestAuto> SuggestAutoTables { get; set; }
        private string keySearchTable = "table"; //SearchKey "KeyWord"
        private List<SuggestAuto> SuggestAutoColumns { get; set; }

        private WapperData WapperData { get; set; }
        private AutocompleteMenu AutocompleteMenu { get; set; }
        private RichTextBox rtbControl { get; set; }

        public SuggestController(WapperData wapperData, AutocompleteMenu autocompleteMenu, RichTextBox control)
        {
            SuggestAuto.KeySearch = keySearchKeyWord;
            rtbControl = control;
            WapperData = wapperData;

            SuggestAutoKeyWords = SuggestAuto.CreateListSuggest(keySearchKeyWord, Utils.KeyWordSQL);

            SuggestAutoTables = SuggestAuto.CreateListSuggest(keySearchTable, wapperData.TableNames);

            SuggestAutoColumns = new List<SuggestAuto>();
            foreach (var col in wapperData.ColumnNames)
            {
                SuggestAutoColumns.Add(new SuggestAuto(col.ColumnName, col.TableName.ToLower()));
            }

            AutocompleteMenu = autocompleteMenu;

            SuggestAuto.SetList(AutocompleteMenu, SuggestAutoKeyWords, keySearchKeyWord);
        }

        public void Suggest(int keydown)
        {
            var KeyDownsAllow = new List<int>() { 32, 190, 188 }; // space dot comma

            if (!KeyDownsAllow.Contains(keydown)) return;

            var currentPosition = rtbControl.SelectionStart;

            var text = GetSQLRange(rtbControl.Text, currentPosition,ref currentPosition);

            var length = text.Length;

            string pattenWord = "\\w+";

            IEnumerable<string> words = Regex.Matches(text, pattenWord, RegexOptions.IgnoreCase)
                .Cast<Match>().Select(x => x.Value.ToLower());
            var tableNamesWrite = new HashSet<string>();
            var tableNamesAdlis = new HashSet<TableInfor>();

            foreach (var word in words)
            {
                if (WapperData.TableNames.Contains(word, StringComparer.InvariantCultureIgnoreCase))
                {
                    if (tableNamesWrite.Contains(word)) continue;

                    tableNamesWrite.Add(word);

                    string patternAdlisTableName = string.Format(@"{0}\s+(\S+)", word);

                    List<string> adlisTables = Regex.Matches(text, patternAdlisTableName, RegexOptions.IgnoreCase)
                       .Cast<Match>().Select(x => x.Groups[1].Value.ToLower()).ToList();

                    if (adlisTables == null || adlisTables.Count==0) continue;

                    foreach (var adlisTable in adlisTables)
                    {
                        if (Utils.IsKeyWordSQL(adlisTable)) continue;

                        tableNamesAdlis.Add(new TableInfor() { AdlisName = adlisTable, TableName = word });
                    }
                }
            }




            var textPre = text.Substring(0, currentPosition);

            string pattenLastWord = @"\b(\w+)[\s\.]*$";

            var lastWordPre = Regex.Match(textPre, pattenLastWord).Groups[1].Value.ToLower();

            if (string.IsNullOrEmpty(lastWordPre))
            {
                string pattenLastWordEqual = @"(=|\,)[\s\.]*$";
                lastWordPre = Regex.Match(textPre, pattenLastWordEqual).Groups[1].Value.ToLower();
            }

            var listKeyWordDetectTableName = new List<string>() { "update", "from", "delete", "join" ,"table","into"};

            var listKeyWordDetectColumnName = new List<string>() { "set", "select", "where", "on","and","or", "=" };

            if (listKeyWordDetectTableName.Contains(lastWordPre, StringComparer.OrdinalIgnoreCase))
            {
                SuggestAuto.SetList(AutocompleteMenu, SuggestAutoTables, keySearchTable);
            }
            else if (listKeyWordDetectColumnName.Contains(lastWordPre, StringComparer.OrdinalIgnoreCase) || lastWordPre == ",")
            {
                // Current Table; Column unit;
                List<string> listTableAndColumn = new List<string>();

                string key = "showtemp";

                var tableNamesShow = tableNamesWrite.Where(x => tableNamesAdlis.Count(y => y.TableName == x)==0);

                listTableAndColumn.AddRange(tableNamesShow);

                listTableAndColumn.AddRange(tableNamesAdlis.Select(x => x.AdlisName));

                foreach (var tables in tableNamesWrite)
                {
                    var col = WapperData.GetColumnNamesByTable(tables, false);
                    listTableAndColumn.AddRange(col);
                }

                var listShow = listTableAndColumn.Where(x => listTableAndColumn.Count(y => y == x) == 1);
                var listShowSuggest = SuggestAuto.CreateListSuggest(key, listShow);
                SuggestAuto.SetList(AutocompleteMenu, listShowSuggest, key);

            }
            else if (tableNamesWrite.Contains(lastWordPre, StringComparer.OrdinalIgnoreCase))
            {
                SuggestAuto.SetList(AutocompleteMenu, SuggestAutoColumns, lastWordPre);
            }

            else if (tableNamesAdlis.Count(x => x.AdlisName.Equals(lastWordPre, StringComparison.OrdinalIgnoreCase)) > 0)
            {
                var tableInfor = tableNamesAdlis
                    .Where(x => x.AdlisName.Equals(lastWordPre, StringComparison.OrdinalIgnoreCase))
                    .FirstOrDefault();

                if (tableInfor != null)
                    SuggestAuto.SetList(AutocompleteMenu, SuggestAutoColumns, tableInfor.TableName);
            }
            else // if (isHasListSuggest)
            {
                SuggestAuto.SetList(AutocompleteMenu, SuggestAutoKeyWords, keySearchKeyWord);
            }
        }




        public string GetSQLRange(string text, int curentIndex, ref int newCurrentIndex)
        {
            List<string> listKeyBlock = new List<string>()
            { "select", "insert", "update", "delete", "print", "create", "drop", "alter", "backup", ";" };

            string sql = string.Empty;

            int length = text.Length;

            // get currentindex finish word; chuwa lam;

            string patternWord = @"\S+";

            List<Match> matchWords = Regex.Matches(text, patternWord).Cast<Match>().ToList();
            List<Match> matchLeftWords = matchWords.Where(x => x.Index < curentIndex).ToList();
            List<Match> matchRightWords = matchWords.Where(x => x.Index >= curentIndex).ToList();

            int indexLeft = -1; int indexRight = -1;
            for (int i = matchLeftWords.Count - 1; i >= 0; i--)
            {
                var match = matchLeftWords[i];
                string word = match.Value.ToLower();
                if (listKeyBlock.Contains(word))
                {
                    indexLeft = match.Index;
                    break;
                };
            }

            for (int i = 0; i < matchRightWords.Count; i++)
            {
                var match = matchRightWords[i];
                string word = match.Value.ToLower();

                if (listKeyBlock.Contains(word))
                {
                    indexRight = match.Index;
                    break;
                };

            }

            if (indexLeft == -1) indexLeft = 0;
            if (indexRight == -1) indexRight = length;

            sql = text.Substring(indexLeft, indexRight - indexLeft);

            newCurrentIndex = curentIndex - indexLeft;

            return sql;
        }

    }
}
