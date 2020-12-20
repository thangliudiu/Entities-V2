using Database.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class SQL_Infor
    {
        public static string[] GetDataTypeColumn(SQLProviderName providerName)
        {
            // mssql 
            string[] datatype1 = {
                //String data types:
                "char(n)","varchar(n)","varchar(max)","text","nchar","nvarchar",
                "nvarchar(max)","ntext","binary(n)", "varbinary","varbinary(max)","image",
                //Numeric data types
                "bit","tinyint","smallint","int","bigint","decimal(p,s)","numeric(p,s)",
                "smallmoney","money","float(n)","real",
                //Date and Time data types:
                "datetime","datetime2","smalldatetime","date","time","datetimeoffset","timestamp",
                //Other data types:
                "sql_variant","uniqueidentifier","xml","cursor","table"
            };

            //sqlite
            string[] datatype2 = {
                "NULL","INTEGER","REAL","TEXT","BLOB"
            };

            // sql compact 4.0
            string[] datatype3 =
            {
                "bigint","integer","smallint","tinyint","bit","numeric(p,s)","Synonyms:",
                "decimal(p,s)","dec(p,s)","money","float","real","datetime","national character(n)",
                "Synonym:nchar(n)","national character varying(n)","Synonym:nvarchar(n)","ntext","nchar",
                "binary(n)","varbinary","image","uniqueidentifier","IDENTITY [(s, i)]","ROWGUIDCOL","Timestamp/rowversion"
            };

            string[] datatype = datatype1;

            if (providerName == SQLProviderName.MSSQL) datatype = datatype1;
            if (providerName == SQLProviderName.SQLite) datatype = datatype2;
            if (providerName == SQLProviderName.SQLCE) datatype = datatype3;

            return datatype;
        }
    }
}
