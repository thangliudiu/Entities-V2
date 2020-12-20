using Database;
using Microsoft.CSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Entities
{
    public class RenderEntity
    {



        public WapperData WapperData { get; private set; }

        public string TableName { get; private set; }

        public string TableClassName { get; private set; }

        public string TableShowName { get; private set; }

        public RenderEntity(WapperData wapperData, string tableName, string tableClassName, string tableShowName = null)
        {
            WapperData = wapperData;

            TableName = tableName;

            TableClassName = tableClassName;

            if (string.IsNullOrEmpty(tableShowName)) tableShowName = TableClassName;

            TableShowName = tableShowName;
        }
        

        public string RenderEntiites( string NameSpace , bool isRequire = true, bool isMaxlength = true)
        {
            string[] listAddNull = { "bigint", "bit", "date", "datetime", "datetime2", "datetimeoffset", "decimal", "float",
                "int", "money", "numeric", "real", "smalldatetime", "smallint", "smallmoney", "time", "tinyint", "uniqueidentifier" };

            // List Info
            List<string> ExcludeColumn = new List<string>()
            {
                "Id"
            };

            List<string> IgnoreUpdate = new List<string>()
            {
                "CreatedDate","CreatedBy"
            };

            RenderCodeHelper renderCodeHelper = new RenderCodeHelper();

            // using
            renderCodeHelper.WriteCode("using System;");
            renderCodeHelper.WriteCode("using System.ComponentModel.DataAnnotations;");
            renderCodeHelper.WriteCode("using wwwCore.Infrastructure.Attributes;");
            renderCodeHelper.WriteCode("using wwwCore.Utilities.Constants;");
            renderCodeHelper.DownLine();


            //namespace
            if (string.IsNullOrEmpty(NameSpace) == true)
                renderCodeHelper.WriteCode("namespace");
            else
                renderCodeHelper.WriteCode(NameSpace);

            renderCodeHelper.OpenAngleBraket();
            //class 
            renderCodeHelper.WriteCode("[Dapper.Table(\"{0}\")]", TableName);
            renderCodeHelper.WriteCode("[ConnectionString(SystemConstants.ConnectionString)]");

            renderCodeHelper.WriteCode("public class {0}Entity : CommonEntity", TableClassName);
            renderCodeHelper.OpenAngleBraket();

            List<ColumnInfo> Columns = WapperData.GetColumnsByTable(TableName);
            foreach (var col in Columns)
            {
                col.ColumnName = col.ColumnName.Replace(" ", "");

                // don't get column in list exclude;
                if (ExcludeColumn.Contains(col.ColumnName) == true) continue;

                //Require
                if (isRequire == true && col.IsNull == false)
                    renderCodeHelper.WriteCode("[Required]");

                //lenght;
                if (isMaxlength == true)
                {
                    if (col.MaxLenght > 0)
                        renderCodeHelper.WriteCode("[StringLength({0})]", col.MaxLenght);
                }

                //Ignore Update;
                if (IgnoreUpdate.Contains(col.ColumnName) == true) renderCodeHelper.WriteCode("[Dapper.IgnoreUpdate]");

                string realType = col.GetRealType();
                string allowNull = col.IsNull == true && listAddNull.Contains(col.Type) ? "?" : "";
                renderCodeHelper.WriteCode("public {0}{1} {2} {3}", realType, allowNull, col.ColumnName, "{get;set;}");

                renderCodeHelper.DownLine();
            }

            renderCodeHelper.CloseAngleBraket();
            renderCodeHelper.CloseAngleBraket();

            return renderCodeHelper.RenderCode;

        }

        public string RenderModel(string NameSpace, bool isRequire = true, bool isMaxlength = true)
        {
            string[] listAddNull = { "bigint", "bit", "date", "datetime", "datetime2", "datetimeoffset", "decimal", "float",
                "int", "money", "numeric", "real", "smalldatetime", "smallint", "smallmoney", "time", "tinyint", "uniqueidentifier" };

            // List Info
            List<string> ExcludeColumn_Model = new List<string>()
            {
                "Id","CreatedDate","CreatedBy","IsDisabled","EditedDate","EditedBy"
            };

            RenderCodeHelper renderCodeHelper = new RenderCodeHelper();

            // using
            renderCodeHelper.WriteCode("using System;");
            renderCodeHelper.WriteCode("using System.Collections.Generic;");
            renderCodeHelper.WriteCode("using System.ComponentModel.DataAnnotations;");
            renderCodeHelper.DownLine();

            //namespace
            if (string.IsNullOrEmpty(NameSpace) == true)
                renderCodeHelper.WriteCode("namespace");
            else
                renderCodeHelper.WriteCode(NameSpace);

            renderCodeHelper.OpenAngleBraket();

            //class Model
            renderCodeHelper.WriteCode("public class {0}Model", TableClassName);
            renderCodeHelper.OpenAngleBraket();

            List<ColumnInfo> Columns = WapperData.GetColumnsByTable(TableName);
            List<string> ColumnsName = Columns.Select(x => x.ColumnName).ToList();
            foreach (var col in Columns)
            {
                col.ColumnName = col.ColumnName.Replace(" ", "");

                // don't get column in list exclude;
                if (ExcludeColumn_Model.Contains(col.ColumnName) == true) continue;

                //Require
                if (isRequire == true && col.IsNull == false) renderCodeHelper.WriteCode("[Required]");

                //lenght;
                if (isMaxlength == true)
                {
                    if (col.MaxLenght > 0) renderCodeHelper.WriteCode("[StringLength({0})] ", col.MaxLenght);
                }

                string realType = col.GetRealType();
                string allowNull = col.IsNull == true && listAddNull.Contains(col.Type) ? "?" : "";
                renderCodeHelper.WriteCode("public {0}{1} {2} {3}", realType, allowNull, col.ColumnName, "{get;set;}");

                renderCodeHelper.DownLine();
            }
            renderCodeHelper.CloseAngleBraket();
            renderCodeHelper.DownLine();


            //class Create
            renderCodeHelper.WriteCode("public class {0}CreateModel : {0}Model", TableClassName);
            renderCodeHelper.OpenAngleBraket();
            renderCodeHelper.DownLine();
            renderCodeHelper.CloseAngleBraket();
            renderCodeHelper.DownLine();

            //Class update
            renderCodeHelper.WriteCode("public class {0}UpdateModel : {0}Model", TableClassName);
            renderCodeHelper.OpenAngleBraket();

            if (ColumnsName.Contains("Id", StringComparer.OrdinalIgnoreCase) == true)
            {
                renderCodeHelper.WriteCode("[Required]");
                renderCodeHelper.WriteCode("public string Id { get; set; }");
            }
            else
                renderCodeHelper.DownLine();

            renderCodeHelper.CloseAngleBraket();
            renderCodeHelper.DownLine();

            //Class View
            renderCodeHelper.WriteCode("public class {0}ViewModel : {0}Model", TableClassName);
            renderCodeHelper.OpenAngleBraket();

            if (ColumnsName.Contains("Id", StringComparer.OrdinalIgnoreCase) == true)
                renderCodeHelper.WriteCode("public string Id { get; set; }");
            else
                renderCodeHelper.DownLine();

            renderCodeHelper.CloseAngleBraket();
            renderCodeHelper.DownLine();

            // Class ViewList
            renderCodeHelper.WriteCode("public class {0}ViewModel_List", TableClassName);
            renderCodeHelper.OpenAngleBraket();

            renderCodeHelper.WriteCode("public List<{0}ViewModel> Data {{ get; set; }}", TableClassName);
            renderCodeHelper.WriteCode("public int Total { get; set; }");

            renderCodeHelper.CloseAngleBraket();
            renderCodeHelper.DownLine();

            // Class Search
            renderCodeHelper.WriteCode("public class {0}SearchModel", TableClassName);

            renderCodeHelper.OpenAngleBraket();

            renderCodeHelper.WriteCode("public int? Page { get; set; }");
            renderCodeHelper.WriteCode("public int? PageSize { get; set; }");

            renderCodeHelper.CloseAngleBraket();
            renderCodeHelper.DownLine();

            // end namespace;
            renderCodeHelper.CloseAngleBraket();

            return renderCodeHelper.RenderCode;


        }

        public string RenderService(string NameSpace)
        {
            List<ColumnInfo> Columns = WapperData.GetColumnsByTable(TableName);
            IEnumerable<string> ColumnsName = Columns.Select(x => x.ColumnName);

            RenderCodeHelper renderCodeHelper = new RenderCodeHelper();

            // using
            renderCodeHelper.WriteCode("using Dapper;");
            renderCodeHelper.WriteCode("using Microsoft.Extensions.Configuration;");
            renderCodeHelper.WriteCode("using System;");
            renderCodeHelper.WriteCode("using System.Collections.Generic;");
            renderCodeHelper.WriteCode("using System.Data;");
            renderCodeHelper.WriteCode("using System.Linq;");
            renderCodeHelper.WriteCode("using wwwCore.Data.Dapper.Repository;", RenderDownLine.TwoLine);

            //nameSpace
            if (string.IsNullOrEmpty(NameSpace) == true)
                renderCodeHelper.WriteCode("namespace");
            else
                renderCodeHelper.WriteCode(NameSpace);

            renderCodeHelper.OpenAngleBraket();

            //class 
            renderCodeHelper.WriteCode("public class {0}Service : EntityService<{0}Entity>", TableClassName);
            renderCodeHelper.OpenAngleBraket();

            renderCodeHelper.WriteCode("public {0}Service(IConfiguration config)  : base(config) {{ }}", TableClassName);
            renderCodeHelper.WriteCode("public {0}Service(IDbConnection db)  : base(db) {{ }} ", RenderDownLine.TwoLine, TableClassName);

            renderCodeHelper.WriteCode("public {0}ViewModel_List GetList({0}SearchModel searchModel)", TableClassName);
            renderCodeHelper.OpenAngleBraket();

            renderCodeHelper.WriteCode("bool _isGetAll = false;", RenderDownLine.TwoLine);

            renderCodeHelper.WriteCode("{0}ViewModel_List result = new {0}ViewModel_List();", RenderDownLine.TwoLine, TableClassName);

            string query = "string querymain = @\" from {0} as main where 1 = 1";
            if (ColumnsName.Contains("IsDisabled") == true) query += " and IsDisabled = 0";
            query += "\";";
            renderCodeHelper.WriteCode(query, RenderDownLine.TwoLine, TableName);

            renderCodeHelper.WriteCode("string query = \"select main.*\" + querymain;");

            string orderby = ColumnsName.First();
            if (ColumnsName.Contains("CreatedBy") == true)
                orderby = "CreatedBy desc";
            renderCodeHelper.WriteCode("query += \" ORDER BY main.{0}\";", RenderDownLine.TwoLine, orderby);
            renderCodeHelper.WriteCode("if (_isGetAll == false)");

            renderCodeHelper.OpenAngleBraket();

            renderCodeHelper.WriteCode("if (searchModel.PageSize == null || searchModel.PageSize > 50) searchModel.PageSize = 50;");
            renderCodeHelper.WriteCode("if (searchModel.Page == null || searchModel.Page <= 0) searchModel.Page = 1;");
            renderCodeHelper.WriteCode("int skip = (searchModel.Page.Value - 1) * searchModel.PageSize.Value;");
            renderCodeHelper.WriteCode("query += \" OFFSET \" + skip + \" ROWS FETCH NEXT \" ");
            renderCodeHelper.WriteCode("      + searchModel.PageSize.Value + \" ROWS ONLY\";");
            renderCodeHelper.CloseAngleBraket();
            renderCodeHelper.WriteCode("else");
            renderCodeHelper.OpenAngleBraket();

            renderCodeHelper.WriteCode("if (searchModel.Page != null && searchModel.PageSize != null)");
            renderCodeHelper.OpenAngleBraket();
            renderCodeHelper.WriteCode("int skip = (searchModel.Page.Value - 1) * searchModel.PageSize.Value;");
            renderCodeHelper.WriteCode("query += \" OFFSET \" + skip + \" ROWS FETCH NEXT \" + searchModel.PageSize.Value ");
            renderCodeHelper.WriteCode("      + \" ROWS ONLY\";");
            renderCodeHelper.WriteCode("_isGetAll = false;");

            renderCodeHelper.CloseAngleBraket();
            renderCodeHelper.CloseAngleBraket(RenderDownLine.TwoLine);

            renderCodeHelper.WriteCode("result.Data = new List<{0}ViewModel>();", TableClassName);
            renderCodeHelper.WriteCode("result.Data = _connection.Query<{0}ViewModel>(query, searchModel).ToList();", RenderDownLine.TwoLine, TableClassName);

            renderCodeHelper.WriteCode("if (_isGetAll == false)");
            renderCodeHelper.OpenAngleBraket();
            renderCodeHelper.WriteCode("result.Total = _connection.Query<int>(\"select count(*)\" + querymain,");
            renderCodeHelper.WriteCode("               searchModel).FirstOrDefault();");
            renderCodeHelper.CloseAngleBraket();

            renderCodeHelper.WriteCode("else");
            renderCodeHelper.OpenAngleBraket();
            renderCodeHelper.WriteCode("result.Total = result.Data.Count;", RenderDownLine.OneLine);
            renderCodeHelper.CloseAngleBraket(RenderDownLine.TwoLine);

            renderCodeHelper.WriteCode("return result;");

            renderCodeHelper.CloseAngleBraket();
            renderCodeHelper.CloseAngleBraket();

            // end namespace;
            renderCodeHelper.CloseAngleBraket();
            return renderCodeHelper.RenderCode;
        }

        public string RenderController(string NameSpace, List<string> columnUnique, CheckBox cb_AndOr)
        {
            List<ColumnInfo> Columns = WapperData.GetColumnsByTable(TableName);
            IEnumerable<string> ColumnsName = Columns.Select(x => x.ColumnName);

            RenderCodeHelper renderCodeHelper = new RenderCodeHelper();

            // using
            renderCodeHelper.WriteCode("using Microsoft.AspNetCore.Http;");
            renderCodeHelper.WriteCode("using Microsoft.AspNetCore.Mvc; ");
            renderCodeHelper.WriteCode("using Microsoft.Extensions.Caching.Distributed;");
            renderCodeHelper.WriteCode("using Microsoft.Extensions.Configuration;");
            renderCodeHelper.WriteCode("using System;");
            renderCodeHelper.WriteCode("using System.Threading.Tasks;");
            renderCodeHelper.WriteCode("using wwwCore.AdminApi.Authorization;");
            renderCodeHelper.WriteCode("using wwwCore.AdminApi.Controllers.Common;");
            renderCodeHelper.WriteCode("using System.Collections.Generic;");
            renderCodeHelper.WriteCode("using wwwCore.Data.Dapper.Providers;", RenderDownLine.TwoLine);


            //namespace
            if (string.IsNullOrEmpty(NameSpace) == true)
                renderCodeHelper.WriteCode("namespace");
            else
                renderCodeHelper.WriteCode(NameSpace);

            renderCodeHelper.OpenAngleBraket();

            //class 
            renderCodeHelper.WriteCode("public class {0}Controller : CommonController", TableClassName);
            renderCodeHelper.OpenAngleBraket();

            renderCodeHelper.WriteCode("public {0}Controller(IHttpContextAccessor accessor, IDistributedCache cache", TableClassName);
            renderCodeHelper.WriteCode("    , IConfiguration configuration)");
            renderCodeHelper.WriteCode("    : base(FunctionCode.{0}, accessor, cache, configuration) {{ }}", TableClassName);

            renderCodeHelper.WriteCode("[HttpGet]");
            renderCodeHelper.WriteCode("public async Task<ActionResult> GetList({0}SearchModel searchModel)", TableClassName);
            renderCodeHelper.OpenAngleBraket();

            renderCodeHelper.WriteCode("{0}Service mainService = new {0}Service(_configuration);", TableClassName);
            renderCodeHelper.WriteCode("return Ok(mainService.GetList(searchModel));");
            renderCodeHelper.CloseAngleBraket(RenderDownLine.TwoLine);

            renderCodeHelper.WriteCode("[HttpGet(\"getinfo\")]");
            renderCodeHelper.WriteCode("public async Task<ActionResult> GetInfor(string id)");
            renderCodeHelper.OpenAngleBraket();
            renderCodeHelper.WriteCode("{0}Service mainService = new {0}Service(_configuration);", TableClassName);
            renderCodeHelper.WriteCode("{0}Entity entity = mainService.Find(id);", TableClassName);
            renderCodeHelper.WriteCode("if (entity == null) return BadRequest(\"Không tìm thấy {0}\");", RenderDownLine.TwoLine, TableShowName.ToLower());
            renderCodeHelper.WriteCode("{0}ViewModel model = new {0}ViewModel();", TableClassName);
            renderCodeHelper.WriteCode("EntityProvider.Copy(entity, model);");
            renderCodeHelper.WriteCode("return Ok(model);");
            renderCodeHelper.CloseAngleBraket(RenderDownLine.TwoLine);

            renderCodeHelper.WriteCode("[HttpPost]");
            renderCodeHelper.WriteCode("[ClaimRequirement(FunctionCode.{0}, PermissionAction.Create)]", TableClassName);
            renderCodeHelper.WriteCode("public async Task<ActionResult> Create([FromBody]{0}CreateModel model)", TableClassName);
            renderCodeHelper.OpenAngleBraket();
            renderCodeHelper.WriteCode("try");
            renderCodeHelper.OpenAngleBraket();
            renderCodeHelper.WriteCode("EntityProvider.Trim(model);", RenderDownLine.TwoLine);

            renderCodeHelper.WriteCode("if (ModelState.IsValid == false) return BadRequest(ModelState);", RenderDownLine.TwoLine);
            renderCodeHelper.WriteCode("{0}Service mainService = new {0}Service(_configuration);", TableClassName);

            if (columnUnique.Count > 0)
            {
                string condition = "";

                if (cb_AndOr.Checked == true)
                {
                    foreach (var col in columnUnique)
                    {
                        if (condition != "") condition += " and ";
                        condition += string.Format("{0} = @{0}", col);
                    }

                    if (ColumnsName.Contains("IsDisabled") == true)
                    {
                        if (condition != "") condition += " and ";
                        condition += "IsDisabled = 0";
                    }

                    renderCodeHelper.WriteCode("{0}Entity tempExist = mainService.Get(\"*\", \"{1}\", model);", TableClassName, condition);

                    string _message = TableShowName + " đã tồn tại";// _message = _message.FirstCharToUpper();

                    renderCodeHelper.WriteCode(" if (tempExist != null) return BadRequest(\"{0}\");", RenderDownLine.TwoLine, _message);
                }
                else
                {
                    int countTemp = 0;
                    foreach (var col in columnUnique)
                    {
                        countTemp++;
                        condition = string.Format("{0} = @{0}", col);

                        if (ColumnsName.Contains("IsDisabled") == true)
                        {
                            if (condition != "") condition += " and ";
                            condition += "IsDisabled = 0";
                        }

                        renderCodeHelper.WriteCode("{0}Entity tempExist{1} = mainService.Get(\"*\", \"{2}\", model);", TableClassName, countTemp, condition);
                        renderCodeHelper.WriteCode(" if (tempExist{0} != null) return BadRequest(\"{1}  đã tồn tại\");", RenderDownLine.TwoLine, countTemp, col);
                    }
                }
            }

            renderCodeHelper.WriteCode("{0}Entity entity = new {0}Entity();", TableClassName);
            renderCodeHelper.WriteCode("EntityProvider.Copy(model, entity);");

            if (ColumnsName.Contains("CreatedBy") == true)
                renderCodeHelper.WriteCode("entity.CreatedBy = CurrentUserID;");
            if (ColumnsName.Contains("CreatedDate") == true)
                renderCodeHelper.WriteCode("entity.CreatedDate = DateTime.Now;");
            if (ColumnsName.Contains("IsDisabled") == true)
                renderCodeHelper.WriteCode("entity.IsDisabled = false;");

            renderCodeHelper.WriteCode("mainService.Create<string>(entity);");
            renderCodeHelper.WriteCode("return Ok(model);");
            renderCodeHelper.CloseAngleBraket();
            renderCodeHelper.WriteCode("catch (Exception ex)");
            renderCodeHelper.OpenAngleBraket();
            renderCodeHelper.WriteCode("return BadRequest(_error);");
            renderCodeHelper.CloseAngleBraket();
            renderCodeHelper.CloseAngleBraket(RenderDownLine.TwoLine);

            renderCodeHelper.WriteCode("[HttpPut]");
            renderCodeHelper.WriteCode("[ClaimRequirement(FunctionCode.{0}, PermissionAction.Update)]", TableClassName);
            renderCodeHelper.WriteCode("public async Task<ActionResult> Update([FromBody]{0}UpdateModel model)", TableClassName);
            renderCodeHelper.OpenAngleBraket();
            renderCodeHelper.WriteCode("try");
            renderCodeHelper.OpenAngleBraket();
            renderCodeHelper.WriteCode("{0}Service mainService = new {0}Service(_configuration);", TableClassName);
            renderCodeHelper.WriteCode("EntityProvider.Trim(model);", RenderDownLine.TwoLine);
            renderCodeHelper.WriteCode("if (ModelState.IsValid == false) return BadRequest(ModelState);", RenderDownLine.TwoLine);
            renderCodeHelper.WriteCode("{0}Entity entity = mainService.Find(model.Id);", TableClassName);
            renderCodeHelper.WriteCode("if (entity == null) return BadRequest(\"không tìm thấy {1}\");", RenderDownLine.TwoLine, TableClassName, TableShowName.ToLower());

            if (columnUnique.Count > 0)
            {
                string condition = "";

                if (cb_AndOr.Checked == true)
                {
                    foreach (var col in columnUnique)
                    {
                        if (condition != "") condition += " and ";
                        condition += string.Format("{0} = @{0}", col);
                    }

                    if (ColumnsName.Contains("IsDisabled") == true)
                    {
                        if (condition != "") condition += " and ";
                        condition += "IsDisabled = 0";
                    }

                    renderCodeHelper.WriteCode("{0}Entity tempExist = mainService.Get(\"*\", \"{1}  and Id  != @Id\", model);", TableClassName, condition);

                    string _message = TableShowName + " đã tồn tại"; // _message = _message.FirstCharToUpper();

                    renderCodeHelper.WriteCode(" if (tempExist != null) return BadRequest(\"{0}\");", _message);
                }
                else
                {
                    int countTemp = 0;
                    foreach (var col in columnUnique)
                    {
                        countTemp++;
                        condition = string.Format("{0} = @{0}", col);

                        if (ColumnsName.Contains("IsDisabled") == true)
                        {
                            if (condition != "") condition += " and ";
                            condition += "IsDisabled = 0";
                        }

                        renderCodeHelper.WriteCode("{0}Entity tempExist{1} = mainService.Get(\"*\", \"{2}  and Id  != @Id\", model);", TableClassName, countTemp, condition);
                        renderCodeHelper.WriteCode(" if (tempExist{0} != null) return BadRequest(\"{1}  đã tồn tại\");", RenderDownLine.TwoLine, countTemp, col);
                    }

                }
            }

            renderCodeHelper.WriteCode("EntityProvider.Copy(model, entity);", TableClassName);
            if (ColumnsName.Contains("EditedBy") == true)
                renderCodeHelper.WriteCode("entity.EditedBy = CurrentUserID;", TableClassName);
            if (ColumnsName.Contains("EditedDate") == true)
                renderCodeHelper.WriteCode("entity.EditedDate = DateTime.Now;", TableClassName);
            renderCodeHelper.WriteCode("mainService.Update(entity);", TableClassName);
            renderCodeHelper.WriteCode("return Ok(model);", TableClassName);
            renderCodeHelper.CloseAngleBraket();
            renderCodeHelper.WriteCode("catch (Exception ex)", TableClassName);
            renderCodeHelper.OpenAngleBraket();
            renderCodeHelper.WriteCode("return BadRequest(_error);", TableClassName);
            renderCodeHelper.CloseAngleBraket();
            renderCodeHelper.CloseAngleBraket(RenderDownLine.TwoLine);

            renderCodeHelper.WriteCode("[HttpDelete]", TableClassName);
            renderCodeHelper.WriteCode("[ClaimRequirement(FunctionCode.{0}, PermissionAction.Delete)]", TableClassName);
            renderCodeHelper.WriteCode("public async Task<ActionResult> Delete(string id)", TableClassName);
            renderCodeHelper.OpenAngleBraket();

            if (ColumnsName.Contains("IsDisabled") == true)
            {
                renderCodeHelper.WriteCode("try");
                renderCodeHelper.OpenAngleBraket();
                renderCodeHelper.WriteCode("{0}Service mainService = new {0}Service(_configuration);", TableClassName);
                renderCodeHelper.WriteCode("mainService.Remove(id);", RenderDownLine.TwoLine);
                renderCodeHelper.WriteCode("return Ok(\"Xóa thành công\");", TableClassName);
                renderCodeHelper.CloseAngleBraket();
                renderCodeHelper.WriteCode("catch (Exception ex)");
                renderCodeHelper.OpenAngleBraket();

                // update isdisbled;
                renderCodeHelper.WriteCode("try");
                renderCodeHelper.OpenAngleBraket();
                renderCodeHelper.WriteCode("{0}Entity entity = mainService.Find(id);", TableClassName);
                renderCodeHelper.WriteCode("if(entity == null) return Ok(\"Xóa thành công\");", RenderDownLine.TwoLine, TableClassName);
                renderCodeHelper.WriteCode("entity.IsDisabled = true;", TableClassName);
                renderCodeHelper.WriteCode("mainService.Update(entity);", TableClassName);
                renderCodeHelper.CloseAngleBraket();
                renderCodeHelper.WriteCode("catch (Exception ex)", TableClassName);
                renderCodeHelper.OpenAngleBraket();
                renderCodeHelper.WriteCode("return BadRequest(_error);", TableClassName);

                renderCodeHelper.CloseAngleBraket();

                renderCodeHelper.CloseAngleBraket();
            }
            else
            {
                renderCodeHelper.WriteCode("try");
                renderCodeHelper.OpenAngleBraket();
                renderCodeHelper.WriteCode("{0}Service mainService = new {0}Service(_configuration);", TableClassName);
                renderCodeHelper.WriteCode("mainService.Remove(id);", RenderDownLine.TwoLine);
                renderCodeHelper.WriteCode("return Ok(\"Xóa thành công\");", TableClassName);
                renderCodeHelper.CloseAngleBraket();
                renderCodeHelper.WriteCode("catch (Exception ex)");
                renderCodeHelper.OpenAngleBraket();
                renderCodeHelper.WriteCode("return Ok(\"{0} đã sử dụng, không thể xóa\");", TableShowName);
                renderCodeHelper.CloseAngleBraket();
            }



            renderCodeHelper.CloseAngleBraket();
            renderCodeHelper.CloseAngleBraket();
            renderCodeHelper.CloseAngleBraket();
            // renderCodeHelper.CloseAngleBraket();

            return renderCodeHelper.RenderCode;
        }

        public string RenderTables(bool isRequire = true, bool isMaxlength = true)
        {
            string[] listAddNull = { "bigint", "bit", "date", "datetime", "datetime2", "datetimeoffset", "decimal", "float",
                "int", "money", "numeric", "real", "smalldatetime", "smallint", "smallmoney", "time", "tinyint", "uniqueidentifier" };

            RenderCodeHelper renderCodeHelper = new RenderCodeHelper();

            List<ColumnInfo> Columns = WapperData.GetColumnsByTable(TableName);
            foreach (var col in Columns)
            {
                col.ColumnName = col.ColumnName.Replace(" ", "");

                //Require
                if (isRequire == true && col.IsNull == false)
                    renderCodeHelper.WriteCode("[Required]");

                //lenght;
                if (isMaxlength == true)
                {
                    if (col.MaxLenght > 0)
                        renderCodeHelper.WriteCode("[StringLength({0})]", col.MaxLenght);
                }

                //Ignore Update;
                string realType = col.GetRealType();
                string allowNull = col.IsNull == true && listAddNull.Contains(col.Type) ? "?" : "";
                renderCodeHelper.WriteCode("public {0}{1} {2} {3}", RenderDownLine.TwoLine, realType, allowNull, col.ColumnName, "{get;set;}");
            }
            return renderCodeHelper.RenderCode;

        }

        public List<ColorInfo> FormatColor(string text)
        {
            List<ColorInfo> listChangeColor = new List<ColorInfo>();

            CSharpCodeProvider cs = new CSharpCodeProvider();
            string pattern = @"\b" + ".+?" + @"\b";

            List<string> words = new List<string>();

            MatchCollection matchesWord = Regex.Matches(text, pattern);

            bool isNewClass = false; List<string> ListClass = new List<string>();

            Color colorKeyWord = Color.FromArgb(15, 70, 225);
            Color colorString = Color.FromArgb(210, 70, 70);
            Color colorComment = Color.FromArgb(75, 145, 65);
            Color colorClass = Color.FromArgb(120, 185, 130);

            // keyword
            foreach (Match match in matchesWord)
            {

                string _text = match.Value.Trim();
                if (string.IsNullOrEmpty(_text) == true) continue;
                if (Regex.IsMatch(_text, "[;:\\(\\)\\d{}\\.,\\[\\]=<>&\\+\\|-]") == true) continue;
                if (cs.IsValidIdentifier(_text) == true && isNewClass == false) continue;

                if (isNewClass == true)
                {
                    isNewClass = false;
                    if (_text == "List") continue;
                    ListClass.Add(_text);
                    continue;
                }

                ColorInfo colorInfo = new ColorInfo(match, colorKeyWord);

                listChangeColor.Add(colorInfo);

                if (_text == "new")
                {
                    isNewClass = true;
                }
            }

            // class;
            foreach (string _class in ListClass)
            {
                MatchCollection matchesClass = Regex.Matches(text, _class);

                foreach (Match match in matchesClass)
                {
                    ColorInfo colorInfo = new ColorInfo(match, colorClass);
                    listChangeColor.Add(colorInfo);
                }

            }


            // []
            MatchCollection matchesSquare = Regex.Matches(text, "\\[.*?[\\]\\(]", RegexOptions.Singleline);
            foreach (Match match in matchesSquare)
            {
                ColorInfo colorInfo = new ColorInfo(match, colorClass)
                {
                    Posistion = match.Index + 1,
                };
                if (colorInfo.Length > 2) colorInfo.Length = colorInfo.Length - 2;
                listChangeColor.Add(colorInfo);
            }

            // string;
            MatchCollection matchesString = Regex.Matches(text, "\\\".*?\\\"", RegexOptions.Singleline);

            foreach (Match match in matchesString)
            {
                ColorInfo colorInfo = new ColorInfo(match, colorString);

                listChangeColor.Add(colorInfo);
            }

            // Comment // /* */
            MatchCollection matchesCommand = Regex.Matches(text, "//.*?\n", RegexOptions.Singleline);
            foreach (Match match in matchesCommand)
            {
                ColorInfo colorInfo = new ColorInfo(match, colorComment);
                listChangeColor.Add(colorInfo);
            }

            return listChangeColor;


        }

    }
}
