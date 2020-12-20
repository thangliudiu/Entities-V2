using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Database
{
    public class ColumnInfo
    {
        public string ColumnName { get; set; }
        public string IsNullString { get; set; }

        public string IsNotNull { get; set; }
        public int MaxLenght { get; set; }
        public string Type { get; set; }

        public string TableName { get; set; }

        public bool IsNull
        {
            get
            {
                if (IsNullString == "YES") return true;
                if (IsNotNull == "0") return true;
                return false;
            }
            set { }
        }

        public string GetRealType()
        {
            string result = "";
            if (string.IsNullOrEmpty(Type)) return result;

            string patternRemove = "\\(.*\\)";
            string type = Regex.Replace(Type, patternRemove, "").ToLower();

            if (type == "bigint") result = "long";
            else if (type == "binary") result = "byte[]";
            else if (type == "bit") result = "bool";
            else if (type == "char") result = "string";
            else if (type == "date") result = "DateTime";
            else if (type == "datetime") result = "DateTime";
            else if (type == "datetime2") result = "DateTime";
            else if (type == "datetimeoffset") result = "DateTimeOffset";
            else if (type == "decimal") result = "decimal";
            else if (type == "float") result = "double";
            else if (type == "image") result = "byte[]";
            else if (type == "int") result = "int";
            else if (type == "money") result = "decimal";
            else if (type == "nchar") result = "string";
            else if (type == "ntext") result = "string";
            else if (type == "numeric") result = "decimal";
            else if (type == "numeric") result = "decimal";
            else if (type == "nvarchar") result = "string";
            else if (type == "real") result = "float";
            else if (type == "smalldatetime") result = "DateTime";
            else if (type == "smallint") result = "short";
            else if (type == "smallmoney") result = "decimal";
            else if (type == "text") result = "string";
            else if (type == "time") result = "TimeSpan";
            else if (type == "timestamp") result = "long";
            else if (type == "tinyint") result = "byte";
            else if (type == "uniqueidentifier") result = "Guid";
            else if (type == "varbinary") result = "byte[]";
            else if (type == "varchar") result = "string";
            else result = "UNKNOWN_" + type;

            if (result == "" && DataProvider.Instance.ProviderName == DataProvider.providerName_SQLite)
            {
                if (type == "bigint") result = "long";
                else if (type == "binary") result = "byte[]";
                else if (type == "bit") result = "bool";
                else if (type == "char") result = "string";
                else if (type == "date") result = "DateTime";
                else if (type == "datetime") result = "DateTime";
                else if (type == "datetime2") result = "DateTime";
                else if (type == "datetimeoffset") result = "DateTimeOffset";
                else if (type == "decimal") result = "decimal";
                else if (type == "float") result = "double";
                else if (type == "image") result = "byte[]";
                else if (type == "int" || type == "integer") result = "int";
                else if (type == "money") result = "decimal";
                else if (type == "nchar") result = "string";
                else if (type == "ntext") result = "string";
                else if (type == "numeric") result = "decimal";
                else if (type == "numeric") result = "decimal";
                else if (type == "nvarchar") result = "string";
                else if (type == "real") result = "float";
                else if (type == "smalldatetime") result = "DateTime";
                else if (type == "smallint") result = "short";
                else if (type == "smallmoney") result = "decimal";
                else if (type == "text") result = "string";
                else if (type == "time") result = "TimeSpan";
                else if (type == "timestamp") result = "long";
                else if (type == "tinyint") result = "byte";
                else if (type == "uniqueidentifier") result = "Guid";
                else if (type == "varbinary") result = "byte[]";
                else if (type == "varchar") result = "string";
                else result = "UNKNOWN_" + type;
            }

            return result;
        }
    }
}
