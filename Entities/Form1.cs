using AutocompleteMenuNS;
using Common;
using ControlCustomer;
using Microsoft.CSharp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Database;
using Dapper;
using Entities.TestClass;
using System.IO;

namespace Entities
{
    public partial class Form1 : Form
    {
        //Infor;
        private string Conn
        {
            get
            {
                return rText_Conn.Text;
            }
        }

        private string TableName { get => cbb_Table.Text; }
        private string NameSpace { get => tb_NameSpace.Text; }
        private string TableClassName { get => tb_ClassName.Text; set => tb_ClassName.Text = value; }
        private string TableShowName { get => tb_NameVietNamese.Text; }

        private SuggestController suggestControllerSQLScript = null;
        private SuggestController suggestControllerSQLExecute = null;

        private string[] SuggestTypes = { "Select", "SelectOffset", "Insert", "Update", "Delete", "DropTable" };

        private WapperData WapperData { get; set; }
        private RenderEntity GetRenderEntity()
        {
            return new RenderEntity(WapperData, TableName, TableClassName, TableShowName);
        }


        public Form1()
        {
            InitializeComponent();

            autoCompleteSQLScript.SetAutocompleteMenu(rtb_SQLScript, autoCompleteSQLScript);
            autoCompleteSQLScript.SetAutocompleteMenu(rtb_SQLExecute, autoCompleteSQLScript);
            autoCompleteSQLScript.SearchPattern = "\\w"; //"[\\w\\.]";
            autoCompleteSQLScript.AllowsTabKey = true;


            var fontFarmily = new FontFamily("Consolas");

            rText_Conn.Text = FileHandle.GetTextFile(Constants.PathFile.Conn);
            rText_Conn.Font = new Font(fontFarmily, 10);
            rtb_Result2.Font = new Font(fontFarmily, 8);
            rText_Result.Font = new Font(fontFarmily, 8);
            rtb_SQLScript.Font = new Font(fontFarmily, 11);
            rtb_SQLExecute.Font = new Font(fontFarmily, 11);
            rtb_ResultScript.Font = new Font(fontFarmily, 13);
            rtb_SuggestScript.Font = new Font(fontFarmily, 10.3f);
            rText_Result.BackColor = Color.FromArgb(30, 30, 30);



            foreach (string suggestType in SuggestTypes)
            {
                cbb_SQLScriptType.Items.Add(suggestType);
            }
            cbb_SQLScriptType.SelectedIndex = 0;

            InitForm();
        }

        private bool InitForm()
        {
            try
            {
                if (string.IsNullOrEmpty(Conn) == false)
                {
                    WapperData = new WapperData(Conn);
                    suggestControllerSQLScript = null;
                    suggestControllerSQLExecute = null;
                    RenderControlProvider.CreateItemCbbTable(WapperData.TableNames, cbb_Table);
                    RenderControlProvider.CreateItemCbbTable(WapperData.TableNames, cbb_Table2);
                }

                return true;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return false; }
        }

        private void tabControl1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.F5) return;

            var tabName = tabControl1.SelectedTab.Name;

            if (tabName == "tabSQLScript")
            {
                Form1Handle.Tab2ExecuteScript(WapperData, rtb_SQLScript, rtb_SuggestScript, rtb_ResultScript);
            }
            else if (tabName == "tabSQLExecute")
            {
                Form1Handle.Tab3Execute(WapperData, rtb_SQLExecute, dgv_SQLExecuteResult);
            }
        }

        private void btn_Test_Click(object sender, EventArgs e)
        {
            //List<GroupEntity> groups = new List<GroupEntity>();
            //for (int i = 0; i < 10; i++)
            //{
            //    GroupEntity group = new GroupEntity();
            //    group.group_id = Guid.NewGuid().ToString();
            //    group.name = i.ToString();
            //    groups.Add(group);

            //}

            //GroupService groupService = new GroupService(Conn);

            //groupService.Insert(groups);

            OpenFileDialog openFileDialog = new OpenFileDialog();

            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;

                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);

                string folderDirect = Path.GetDirectoryName(fileName);

                string jsonString =   FileHandle.GetTextFile(fileName);

                string csvFileName = Path.Combine(folderDirect, fileNameWithoutExtension + ".csv");


                if (File.Exists(csvFileName) == true) return;

                ConvertJsonToCSV.JsonToCSV(jsonString, csvFileName, "");
            }

        }

        #region Tab 1

        // Render;
        private void FinishRender(RenderEntity renderEntity, string text)
        {
            rText_Result.ChangeCollorMatch(renderEntity.FormatColor(rText_Result.Text));

            Clipboard.SetText(text);
            tb_NameSpace.Text = "";
        }

        private void Btn_Connect_Click(object sender, EventArgs e)
        {
            FileHandle.WriteFile(Constants.PathFile.Conn, Conn);

            bool flag1 = InitForm();
            if (flag1) MessageBox.Show(string.Format("Get TableName Complete! It's a {0}", Database.DataProvider.Instance.ProviderName));
        }

        private void Btn_Render_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tb_NameSpace.Text) == true)
            {
                MessageBox.Show("Name Space không được bỏ trống");
                return;
            }
            try
            {
                var renderEnttiy = GetRenderEntity();

                rText_Result.Text = renderEnttiy.RenderEntiites(NameSpace, cb_Require.Checked, cb_MaxLenght.Checked);
                FinishRender(renderEnttiy, rText_Result.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Btn_RenderID_Click(object sender, EventArgs e)
        {
            string textId = "";
            for (int i = 0; i < 20; i++)
            {
                textId += Guid.NewGuid().ToString();
                textId += "\n";
            }
            rText_Result.Text = textId;
            FinishRender(GetRenderEntity(), rText_Result.Text);
        }

        private void Btn_Model_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(NameSpace) == true)
            {
                MessageBox.Show("Name Space không được bỏ trống");
                return;
            }
            try
            {
                var render = GetRenderEntity();
                rText_Result.Text = render.RenderModel(NameSpace, cb_Require.Checked, cb_MaxLenght.Checked);
                FinishRender(render, rText_Result.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Btn_Service_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(NameSpace) == true)
            {
                MessageBox.Show("Name Space không được bỏ trống");
                return;
            }
            try
            {
                var render = GetRenderEntity();
                rText_Result.Text = render.RenderService(NameSpace);
                FinishRender(render, rText_Result.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Btn_Controller_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(NameSpace) == true)
            {
                MessageBox.Show("Name Space không được bỏ trống");
                return;
            }

            List<string> columnUnique = new List<string>();
            foreach (var control in pnl_CheckColumn.Controls)
            {
                CheckBox checkBox = control as CheckBox;
                if (checkBox == null || checkBox.Checked == false) continue;

                columnUnique.Add(checkBox.Text);
            }

            if (columnUnique.Count == 0)
            {
                var answer = MessageBox.Show("You have not choosen  unique columns, Do you want to continue?", "Warning", MessageBoxButtons.YesNo);
                if (answer == DialogResult.No) return;
            }


            try
            {
                var render = GetRenderEntity();
                string res = render.RenderController(NameSpace, columnUnique, cb_AndOr);
                if (string.IsNullOrEmpty(res) == false)
                {
                    rText_Result.Text = res;
                    FinishRender(render, rText_Result.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Cbb_Table_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {

                TableClassName = Form1Handle.GetTableClassName(TableName);

                var render = GetRenderEntity();
                rText_Result.Text = render.RenderTables();

                FinishRender(render, rText_Result.Text);

                // insert checkbox;
                List<ColumnInfo> Columns = WapperData.GetColumnsByTable(TableName, false);
                RenderControlProvider.RenderCheckBox1(pnl_CheckColumn, Columns.Select(x => x.ColumnName).ToList());
                //tb_NameSpace.Focus();
                pnl_CheckColumn.Focus();
                tb_NameVietNamese.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // database
        private void Cbb_Table2_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                var render = GetRenderEntity();

                List<ColumnInfo> Columns = WapperData.GetColumnsByTable(cbb_Table2.Text, false);
                RenderControlProvider.RenderCheckBox2(pnlCol2, Columns.Select(x => x.ColumnName).ToList());
                pnlCol2.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Btn_AddCol2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tb_AddCol2.Text) == true)
            {
                MessageBox.Show("Trường không được để trống");
                return;
            }
            List<string> columnsName = new List<string>();
            List<string> columnsNameChecked = new List<string>();
            foreach (var control in pnlCol2.Controls)
            {
                CheckBox checkBox = control as CheckBox;
                if (checkBox == null) continue;

                columnsName.Add(checkBox.Text);

                if (checkBox.Checked == true)
                    columnsNameChecked.Add(checkBox.Text);
            }

            if (columnsName.Contains(tb_AddCol2.Text, StringComparer.OrdinalIgnoreCase) == true)
            {
                MessageBox.Show("Trường đã tồn tại");
                return;
            }
            columnsName.Add(tb_AddCol2.Text);
            columnsNameChecked.Add(tb_AddCol2.Text);
            RenderControlProvider.RenderCheckBox2(pnlCol2, columnsName, columnsNameChecked);
        }

        private void Btn_Get2_Click(object sender, EventArgs e)
        {
            string tableName = cbb_Table2.Text;
            int page = (int)nb_Page2.Value;

            if (page < 1) page = 1;

            var columns = WapperData.GetColumnsByTable(cbb_Table2.Text);

            if (columns == null || columns.Count == 0) return;

            var columnsName = columns.Select(x => x.ColumnName);

            string orderby = columnsName.First();

            if (columnsName.Contains("CreatedDate", StringComparer.OrdinalIgnoreCase) == true) orderby = "CreatedDate desc";
            else if (columnsName.Contains("id", StringComparer.OrdinalIgnoreCase) == true) orderby = "id";

            int skip = (int)nb_PageSize2.Value;

            if (cb_IsRandom2.Checked == false && skip > 50) skip = 50;



            SQLService sql = new SQLService(Conn);

            string templateQuery = SQLTemplate.SelectOffset(Database.DataProvider.Instance.SQLProviderName);
            string query = string.Format(templateQuery, tableName, orderby, (page - 1) * skip, skip);


            List<dynamic> datas = sql.Connection.Query(query).ToList();

            //   if (datas == null || datas.Count == 0) return;

            List<string> _columnsNameChecked = new List<string>();
            foreach (var control in pnlCol2.Controls)
            {
                CheckBox checkBox = control as CheckBox;
                if (checkBox == null) continue;

                if (checkBox.Checked == true)
                    _columnsNameChecked.Add(checkBox.Text);
            }


            // Create Table
            DataTable table = new DataTable();

            //Create column;
            dta2.Columns.Clear();

            //DataColumn checkColumn = new DataColumn("choose", typeof(bool));
            //table.Columns.Add(checkColumn);

            foreach (var namecolumn in _columnsNameChecked)
            {
                table.Columns.Add(namecolumn);
            }

            // Create Row;
            int countRow = 0;
            foreach (var data in datas)
            {
                countRow++;
                DataRow _row = table.NewRow();

                foreach (var namecolumn in _columnsNameChecked)
                {
                    _row[namecolumn] = namecolumn + countRow;
                }

                foreach (var property in (IDictionary<string, object>)data)
                {
                    if (_columnsNameChecked.Contains(property.Key, StringComparer.OrdinalIgnoreCase) == true)
                        _row[property.Key] = property.Value;
                }
                table.Rows.Add(_row);
            }

            //Random
            int countIndexLoop = 0;
            if (countRow < nb_PageSize2.Value && cb_IsRandom2.Checked == true)
            {

                int length = (int)nb_PageSize2.Value - countRow;
                for (int i = 0; i < length; i++)
                {
                    countRow++;
                    DataRow _row = table.NewRow();
                    foreach (var namecolumn in _columnsNameChecked)
                    {
                        _row[namecolumn] = namecolumn + countRow;
                    }

                    countIndexLoop++; if (countIndexLoop >= datas.Count) countIndexLoop = 0;
                    if (datas.Count > 0)
                    {
                        var properties = (IDictionary<string, object>)datas[countIndexLoop];

                        if (properties != null || properties.Count > 0)
                        {
                            foreach (var property in properties)
                            {
                                if (_columnsNameChecked.Contains(property.Key, StringComparer.OrdinalIgnoreCase) == true)
                                {
                                    _row[property.Key] = property.Value;
                                }

                            }

                            var keyRandom = _columnsNameChecked.First();

                            var keyId = properties.Keys.Where(x => x.Equals("id", StringComparison.OrdinalIgnoreCase) == true).FirstOrDefault();

                            if (keyId != null && _columnsNameChecked.Contains("id", StringComparer.OrdinalIgnoreCase) == true)
                                _row[keyId] = Guid.NewGuid().ToString();
                            else
                                _row[keyRandom] = _row[keyRandom].ToString() + countRow;
                        }
                    }



                    table.Rows.Add(_row);
                }
            }

            dta2.DataSource = table;
        }

        private void Btn_Insert2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chưa làm");
        }

        private void Btn_Convert2_Click(object sender, EventArgs e)
        {
            var data = dta2.DataSource as DataTable;

            if (data == null) return;

            var dataObj = DataProvider.GetDataFromDataTable<dynamic>(data);

            rtb_Result2.Text = JsonConvert.SerializeObject(dataObj, Formatting.Indented);
            Clipboard.SetText(rtb_Result2.Text);
        }

        #endregion

        #region Tab 2 
        private void Btn_SQLScriptGetTable_Click(object sender, EventArgs e)
        {
            rtb_SuggestScript.Text = "";
            rtb_SuggestScript.AppendTable(WapperData.TableNames);
        }

        private void Btn_SQLScriptDataType_Click(object sender, EventArgs e)
        {
            rtb_SuggestScript.Text = "";
            var datatypes = SQL_Infor.GetDataTypeColumn(DataProvider.Instance.SQLProviderName);
            rtb_SuggestScript.AppendTable(datatypes);
        }

        private void Rtb_SQLScript_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            if (!(sender is RichTextBox richTextBox)) return;

            if (!(sender is RichTextBox box)) return;
            int start = box.SelectionStart;
            if (start < 0) start = 0;

            int firstcharindex = richTextBox.GetFirstCharIndexOfCurrentLine();

            int currentline = richTextBox.GetLineFromCharIndex(firstcharindex);

            string currentlinetext = richTextBox.Lines[currentline];

            int length = 0;

            for (int i = start; i < currentlinetext.Length + start && i < richTextBox.Text.Length; i++)
            {
                var subtring = richTextBox.Text.Substring(i, 1);
                if (subtring == " ")
                {
                    break;
                }
                length = i - start + 1;
            }

            richTextBox.SelectionLength = length;

            tb_FindColumnByTableName.Text = richTextBox.SelectedText;

        }

        private void Btn_SQLScriptGetColumn_Click(object sender, EventArgs e)
        {
            string tableName = tb_FindColumnByTableName.Text;
            if (string.IsNullOrEmpty(tableName) == true) return;

            var columns = WapperData.GetColumnsByTable(tableName).Select(x => x.ColumnName);

            if (columns == null || columns.Count() == 0) return;
            //  rtb_SuggestScript.Text = string.Join(", ", columns);

            rtb_SuggestScript.Text = "";
            rtb_SuggestScript.AppendTable(columns);

        }

        private void Btn_ExcuteScript_Click(object sender, EventArgs e)
        {
            Form1Handle.Tab2ExecuteScript(WapperData, rtb_SQLScript, rtb_SuggestScript, rtb_ResultScript);
        }

        private void Rtb_SuggestScript_DoubleClick(object sender, EventArgs e)
        {
            if (!(sender is RichTextBox box)) return;
            int start = box.SelectionStart;
            if (start < 0) start = 0;

            int firstcharindex = box.GetFirstCharIndexOfCurrentLine();

            int currentline = box.GetLineFromCharIndex(firstcharindex);

            string currentlinetext = box.Lines[currentline];

            int length = 0;

            for (int i = start; i < currentlinetext.Length + start && i < box.Text.Length; i++)
            {
                var subtring = box.Text.Substring(i, 1);
                if (subtring == " ")
                {
                    break;
                }
                length = i - start + 1;
            }

            box.SelectionLength = length;
            tb_FindColumnByTableName.Text = box.SelectedText;

            if (!string.IsNullOrEmpty(box.SelectedText))
                Clipboard.SetText(box.SelectedText);
        }

        private void SuggestScriptBase(string suggestQuery)
        {
            // comment all;
            string query = rtb_SQLScript.Text.Trim();
            string downline = "";
            if (string.IsNullOrEmpty(query) == false)
            {
                query = Regex.Replace(query, "^--|^", "--", RegexOptions.Multiline);
                downline = "\n";
            }

            rtb_SQLScript.Text = query + downline + suggestQuery;

            if (!string.IsNullOrEmpty(suggestQuery))
                Clipboard.SetText(suggestQuery);
        }

        private void Btn_SQLScriptSelect_Click(object sender, EventArgs e)
        {
            //  SuggestTypes:  { "Select", "SelectOffset", "Insert", "Update", "Delete", "DropTable" };

            var selectedtype = cbb_SQLScriptType.Text;

            bool isRealTable = false;

            var columnName = "COLUMN_NAME";
            var tableName = "TABLE_NAME";
            var condition = "Id =''";
            var columnNames = new List<string>();
            var tableFindName = tb_FindColumnByTableName.Text;

            if (!string.IsNullOrEmpty(tableFindName)) 
            { 
                columnNames = WapperData.GetColumnNamesByTable(tableFindName);
                if(columnNames.Count > 0)
                {
                    isRealTable = true;
                    tableName = tb_FindColumnByTableName.Text;
                }
            }

            string suggestQuery = "select 1";

            if (selectedtype == SuggestTypes[0])
            {
                string template = SQLTemplate.SelectLimit(DataProvider.Instance.SQLProviderName);

                suggestQuery = string.Format(template, tableName, 10);
            }

            if (selectedtype == SuggestTypes[1])
            {
                string template = SQLTemplate.SelectOffset(DataProvider.Instance.SQLProviderName);

                suggestQuery = string.Format(template, tableName, columnName,2,10);
            }

            if (selectedtype == SuggestTypes[2]) // insert;
            {
                string template = SQLTemplate.Insert(DataProvider.Instance.SQLProviderName);

                if (isRealTable) columnName = string.Join(",", columnNames);
                suggestQuery = string.Format(template, tableName, columnName);
            }

            if (selectedtype == SuggestTypes[3]) // update;
            {
                string template = SQLTemplate.Update(DataProvider.Instance.SQLProviderName);

                suggestQuery = string.Format(template, tableName,columnName +" = ''", condition);
            }

            if (selectedtype == SuggestTypes[4]) // delete;
            {
                string template = SQLTemplate.Delete(DataProvider.Instance.SQLProviderName);

                suggestQuery = string.Format(template, tableName, condition);
            }


            if (selectedtype == SuggestTypes[5]) // drop table;
            {
                string template = SQLTemplate.DropTable(DataProvider.Instance.SQLProviderName);

                suggestQuery = string.Format(template, tableName, condition);
            }

            SuggestScriptBase(suggestQuery);
        }


        private void btn_SQLScriptCreateTable_Click(object sender, EventArgs e)
        {
            string suggestQuery = SQLTemplate.Example.CreateTable(DataProvider.Instance.SQLProviderName);

            SuggestScriptBase(suggestQuery);
        }
        #endregion

        #region Tab3

        private void Btn_SQLExecute_Click(object sender, EventArgs e)
        {
            Form1Handle.Tab3Execute(WapperData, rtb_SQLExecute, dgv_SQLExecuteResult);
        }

        private void Btn_SQLExecutePaste_Click(object sender, EventArgs e)
        {
            string query = rtb_SQLExecute.Text.Trim();
            string downline = "";
            if (string.IsNullOrEmpty(query) == false)
            {
                query = Regex.Replace(query, "^--|^", "--", RegexOptions.Multiline);
                downline = "\n";
            }


            rtb_SQLExecute.Text = query + downline + Clipboard.GetText();
        }



        private void dgv_SQLExecuteResult_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            object value = e.Value;

            if ((value != null) && value.Equals(e.CellStyle.DataSourceNullValue) || value == null)
            {
                e.Value = " - ";
                e.FormattingApplied = true;
            }
        }
        #endregion

        #region Tab 4

        private void btn_ConvertTextCSharp_Click(object sender, EventArgs e)
        {
            string text = rtb_ConvertText.Text;
            text = text.Replace("\\", "\\\\");
            text = text.Replace("\"", "\\\"");
            if (cb_IsKeepDown.Checked == false) { text = text.Replace("\n", " "); text = text.Replace("\r", " "); }

            rtb_ConvertText.Text = text;
            Clipboard.SetText(text);
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            rtb_ConvertText.Text = "";
        }
        #endregion

        #region Function



        #endregion

        private void rtb_SQLScript_KeyDown(object sender, KeyEventArgs e)
        {
            if (suggestControllerSQLScript == null) suggestControllerSQLScript = new SuggestController(WapperData, autoCompleteSQLScript, sender as RichTextBox);

            suggestControllerSQLScript.Suggest(e.KeyValue);

        }


        private void rtb_SQLExecute_KeyDown(object sender, KeyEventArgs e)
        {
            if (suggestControllerSQLExecute == null) suggestControllerSQLExecute = new SuggestController(WapperData, autoCompleteSQLScript, sender as RichTextBox);

            suggestControllerSQLExecute.Suggest(e.KeyValue);
        }


    }









}




