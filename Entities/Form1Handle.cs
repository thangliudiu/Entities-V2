using Common;
using Dapper;
using Database;
using Entities.TestClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Entities
{
    public static class Form1Handle
    {
        public static void Tab3Execute(WapperData wapperData, RichTextBox rtbExecute, DataGridView dataGridView)
        {
            try
            {
                string query = rtbExecute.Text;

                if (rtbExecute.SelectionLength > 0)
                {
                    query = rtbExecute.SelectedText;
                }

                if (string.IsNullOrEmpty(query) == true) return;

                SQLService sql = new SQLService(wapperData.Connection);

                var dataReader = sql.Connection.ExecuteReader(query);
                var dataTable = DataProvider.GetDataTable(dataReader);

                //  DataProvider.ChangeColumnBoolType(dataTable);
                dataGridView.DataSource = dataTable;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                rtbExecute.SelectionLength = 0;
                rtbExecute.SelectionStart = rtbExecute.Text.Length;
            }
        }

        public static void Tab2ExecuteScript(WapperData wapperData, RichTextBox rtbScript, RichTextBox rtbSuggest, RichTextBox rtbResult)
        {
            string script = rtbScript.Text;

            if (string.IsNullOrEmpty(script) == true) return;

            string query = script.Trim();

            Regex regexReplaceDoubleSpace = new Regex("\\ {2,}", RegexOptions.Multiline);
            query = regexReplaceDoubleSpace.Replace(query, " ");

            Regex regexReplaceDoubleDot = new Regex("\\.{2,}", RegexOptions.Multiline);
            query = regexReplaceDoubleDot.Replace(query, ".");

            Regex regexRemove = new Regex("^\\.|\\.$", RegexOptions.Multiline);
            query = regexRemove.Replace(query, "");

            Regex regexReplace = new Regex("\\ +\\.|\\.\\ +");

            query = regexReplace.Replace(query, ".");

            try
            {
                if (query != rtbScript.Text)
                {
                    rtbScript.Text = query;

                    rtbScript.SetAllColor(Color.DarkViolet);

                    rtbResult.WriteText("Notify Change Query", Color.DarkRed);// = "OK";
                    return;
                }

                if (rtbScript.SelectionLength > 0)
                {
                    query = rtbScript.SelectedText;
                }

                Regex regex = new Regex("@\\w*");

                query = regex.Replace(query, "''");

                if (string.IsNullOrEmpty(query) == true) return;

                SQLService sql = new SQLService(wapperData.Connection);

                var fontConsolas = new FontFamily("Consolas");
                rtbScript.SetAllColor(Color.Black);
                rtbScript.SetAllBackColor(Color.White);
                rtbScript.SetAllFont(new Font(fontConsolas, 11, FontStyle.Regular));

                rtbScript.ChangeCollorMatch(FormatSQLColor(rtbScript.Text), Color.Black);

                rtbScript.Font = new Font(fontConsolas, 11, FontStyle.Regular); // Consolas;
                //format text;

                bool isValidDoubleQuotation = true;
                for (int i = 0; i < rtbScript.Lines.Count(); i++)
                {
                    var _text = rtbScript.Lines[i];
                    if (_text.Contains("\"") == true)
                    {
                        rtbScript.ChangeCollorText("\"", Color.Red, true);
                        rtbScript.ChangeCollorLine(i + 1, Color.LightGray);

                        isValidDoubleQuotation = false;
                        break;
                    }
                }

                bool isValidDot = true;
                if (isValidDoubleQuotation == true)
                {
                    string patern = "\\s+\\.|\\.\\s+|\\.$";
                    Regex regexDot = new Regex(patern, RegexOptions.Multiline);

                    MatchCollection matches = regexDot.Matches(rtbScript.Text);

                    if (matches != null && matches.Count > 0)
                    {
                        rtbScript.ChangeBackCollorText(matches, Color.LightBlue, false);
                        isValidDot = false;
                    }
                }
                if (isValidDoubleQuotation && isValidDot)
                {
                    sql.Connection.Execute(query);

                    rtbResult.WriteText("OK", Color.Blue);// = "OK";
                }
                else
                {
                    if (isValidDoubleQuotation == false)
                        rtbScript.WriteText("Don't use character \" in query", Color.Green);
                    else if (isValidDot == false)
                        rtbScript.WriteText("Incorrect systax near character . ", Color.Green);
                }

            }
            catch (SqlException ex)
            {
                var error = ex.Errors[0];

                var message = error.Message;

                List<string> typeValidName = new List<string>()
                {
                     "Invalid column name","Invalid object name","The multi-part identifier",
                };

                List<string> typeValidSyntax = new List<string>()
                {
                    "Incorrect syntax near","Incorrect syntax near the keyword",
                    "An expression of non-boolean type specified in a context where a condition is expected, near",
                };

                List<string> typeValid = new List<string>();
                typeValid.AddRange(typeValidName);
                typeValid.AddRange(typeValidSyntax);
                string patern = "(" + string.Join("|", typeValid) + ") ['\"](.*?)['\"]";
                Regex regexMessage = new Regex(patern, RegexOptions.Multiline);

                Match match = regexMessage.Match(message);

                var lineError = ex.LineNumber;
                rtbScript.ChangeCollorLine(lineError, Color.LightGray);

                if (match != null && string.IsNullOrEmpty(match.Value) == false)
                {
                    var groups = match.Groups;
                    var typeError = groups[1].Value;
                    var valueError = groups[2].Value;

                    if (valueError != "." && valueError != "," && valueError != "=")
                    {
                        if (typeValidName.Contains(typeError) == true)
                        {
                            Regex _regex = new Regex(typeError + " ['\"](.*?)['\"]", RegexOptions.Multiline);
                            MatchCollection _matches = _regex.Matches(ex.Message);

                            foreach (Match _match in _matches)
                            {
                                var _valueError = _match.Groups[1].Value;
                                rtbScript.ChangeCollorText(_valueError, Color.Red, true);
                            }

                            if (typeError == typeValidName[0])
                            {
                                Match _matchFindTable = null;
                                Regex _regexFind = new Regex("(\\w+)\\." + valueError, RegexOptions.Multiline);
                                Match _matchFind = _regexFind.Match(query);
                                Regex _regexFindTable = null;
                                var _paternFindTableMain = string.Format("from (\\w+)");
                                if (_matchFind != null && _matchFind.Groups.Count > 1)
                                {
                                    var _tableName2 = _matchFind.Groups[1].Value;
                                    var _paternFindTableJoin = string.Format("join ({0})|join (\\w+) {0}|join (\\w+) as {0}", _tableName2);

                                    _regexFindTable = new Regex(_paternFindTableJoin, RegexOptions.Multiline);
                                    _matchFindTable = _regexFindTable.Match(query);
                                }

                                if (_matchFindTable == null || _matchFindTable.Length == 0)
                                {
                                    _regexFindTable = new Regex(_paternFindTableMain, RegexOptions.Multiline);
                                    _matchFindTable = _regexFindTable.Match(query);
                                }
                                if (_matchFindTable != null)
                                {
                                    var _tableName = _matchFindTable.Groups[1].Value;
                                    if (string.IsNullOrEmpty(_tableName) == true) _tableName = _matchFindTable.Groups[2].Value;
                                    if (string.IsNullOrEmpty(_tableName) == true) _tableName = _matchFindTable.Groups[3].Value;
                                    var columns = wapperData.GetColumnsByTable(_tableName).Select(x => x.ColumnName)
                                        .OrderByDescending(x =>
                                        {
                                            var number = LevenshteinDistance.Compute2(valueError, x);
                                            if (number < 10) number = 0;
                                            return number;
                                        }


                                    );
                                    rtbSuggest.Text = _tableName.ToUpper() + ": =>  \n";
                                    rtbSuggest.AppendTable(columns);

                                }
                            }
                            else if (typeError == typeValidName[1])
                            {
                                var tables = wapperData.TableNames.OrderByDescending(x =>
                                {
                                    var number = LevenshteinDistance.Compute2(valueError, x);
                                    if (number < 10) number = 0;
                                    return number;
                                });
                                rtbSuggest.Text = "";
                                rtbSuggest.AppendTable(tables);
                            }
                        }
                        else if (typeValidSyntax.Contains(typeError) == true)
                        {
                            if (typeError == "Incorrect syntax near")
                            {
                                rtbScript.ChangeCollorText(lineError, valueError, Color.Green, true);
                            }
                            else if (typeError == "Incorrect syntax near the keyword")
                            {
                                rtbScript.ChangeCollorText(lineError, valueError, Color.Green, false);
                                //  rtb_SQLScript.ChangeBackCollorText(lineError, valueError, Color.LightGray, true);
                            }
                        }
                    }
                    else
                    {

                        //   rtb_SQLScript.ChangeCollorText(lineError,valueError, Color.Red);
                    }
                }
                else
                {
                    // rtb_SQLScript.ChangeCollorLine(lineError, Color.LightGray);
                }
                rtbResult.WriteText(ex.Message, Color.Red);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            rtbScript.SelectionLength = 0;
            rtbScript.SelectionStart = rtbScript.Text.Length;
        }

        public static List<ColorInfo> FormatSQLColor(string text)
        {
            List<ColorInfo> listChangeColor = new List<ColorInfo>();

            string pattern = @"\b" + ".+?" + @"\b";

            List<string> words = new List<string>();

            MatchCollection matchesWord = Regex.Matches(text, pattern);

            List<string> ListClass = new List<string>();

            Color colorKeyWord = Color.FromArgb(15, 70, 225);
            Color colorString = Color.Red;
            Color colorComment = Color.FromArgb(75, 145, 65);

            // keyword
            foreach (Match match in matchesWord)
            {
                string _text = match.Value.Trim();
                if (string.IsNullOrEmpty(_text) == true) continue;
                if (Utils.IsKeyWordSQL(_text) == false) continue;

                ColorInfo colorInfo = new ColorInfo(match, colorKeyWord);

                listChangeColor.Add(colorInfo);

            }


            // string;
            MatchCollection matchesString = Regex.Matches(text, "\\\'.*?\\\'", RegexOptions.Singleline);

            foreach (Match match in matchesString)
            {
                ColorInfo colorInfo = new ColorInfo(match, colorString);

                listChangeColor.Add(colorInfo);
            }

            return listChangeColor;


        }

        public static string GetTableClassName(string tableName)
        {
            if (string.IsNullOrEmpty(tableName) == true) return "";

            return Regex.Replace(tableName, "s$", "", RegexOptions.Singleline);
        }
    }
}
