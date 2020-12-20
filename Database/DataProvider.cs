using Dapper;
using Database.Enum;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Data.SqlServerCe;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class DataProvider
    {
        public static List<T> GetDataFromDataTable<T>(DataTable datasql)
        {
            var list = new List<T>();
            try
            {
                var listporerties = typeof(T).GetProperties();
                if (listporerties.All(x => x.CanWrite == false) == false)
                {
                    foreach (DataRow row in datasql.Rows)
                    {
                        var instance = (T)Activator.CreateInstance(typeof(T));
                        foreach (var item in listporerties)
                        {
                            string name = item.Name;
                            Type type = item.PropertyType;
                            PropertyInfo prop = instance.GetType().GetProperty(name, BindingFlags.Public | BindingFlags.Instance);
                            if (null != prop && prop.CanWrite)
                            {
                                Type t = Nullable.GetUnderlyingType(type) ?? type;

                                if (row.Table.Columns.Contains(name))
                                {
                                    object safeValue = (row[name].ToString() == "") ? null : Convert.ChangeType(row[name], t);
                                    prop.SetValue(instance, safeValue);
                                }
                                else
                                {
                                    prop.SetValue(instance, null);
                                }
                            }

                        }
                        list.Add(instance);
                    }
                }
                else
                {
                    // is standtans type;
                    foreach (DataRow row in datasql.Rows)
                    {
                        var typeClass = typeof(T);
                        if (typeClass.FullName != "System.Object")
                        {
                            T t = row.ItemArray[0].ToString() == "" ? default(T) : (T)row.ItemArray[0];
                            list.Add(t);
                        }
                        else
                        {
                            try
                            {
                                dynamic t = new ExpandoObject();
                                IDictionary<string, object> myUnderlyingObject = t;
                                int count = 0;
                                foreach (var item in row.ItemArray)
                                {
                                    var columnName = row.Table.Columns[count].ColumnName;
                                    count++;
                                    myUnderlyingObject.Add(columnName, row[columnName]);

                                }
                                list.Add(t);
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }

                        }

                    }
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }


            return list;
        }

        private DataProvider() { }

        private static DataProvider instance;

        private string providerName;
        public string ProviderName { get { return providerName; } private set { providerName = value; SQLProviderName = GetEnumProviderName(providerName); } }

        public SQLProviderName SQLProviderName { get; private set; }

        public static string providerName_SQLServer = "sqlconnection";
        public static string providerName_SQLite = "sqliteconnection";
        public static string providerName_SQLce = "sqlceconnection";

        // sqlserver ; sql azure; oracle; mysql; postgreSQL; sqlite;
        public DbConnection GetConnection(string connectionString)
        {
            DbConnection connection = null;

            if (Instance.ProviderName == "")
            {
                if (connection == null) connection = getSQLceConnection(connectionString);

                if (connection == null) connection = getSQLConnection(connectionString);

                if (connection == null) connection = getSQLiteConnection(connectionString);

                if (connection == null) throw new Exception("can not connect to database");

                Instance.ProviderName = connection.GetType().Name.ToLower();

                return connection;
            }

            if (Instance.ProviderName == providerName_SQLServer) connection = getSQLConnection(connectionString);
            else if (Instance.ProviderName == providerName_SQLite) connection = getSQLiteConnection(connectionString);
            else if (Instance.ProviderName == providerName_SQLce) connection = getSQLceConnection(connectionString);

            if(connection != null) return connection;

            // try get again without old providerName;
            Instance.ProviderName = "";
            return GetConnection(connectionString);
        }

        public static DbConnection getSQLceConnection(string _connectionString)
        {
            try
            {
                var _connection = new SqlCeConnection(_connectionString);
                _connection.ExecuteScalar("select 1");
                return _connection;
            }
            catch { return null; }
          
        }

        public static DbConnection getSQLConnection(string _connectionString)
        {
            try { var _connection = new SqlConnection(_connectionString);
                _connection.ExecuteScalar("select 1");
                return _connection; 
            } 
            catch { return null; }
        }

        public static DbConnection getSQLiteConnection(string _connectionString)
        {
            try
            {
                var _connection = new SQLiteConnection(_connectionString);
                _connection.Query("SELECT name FROM sqlite_master limit 1");
                return _connection;
            }
            catch { return null; }
           
        }

        public static SQLProviderName GetEnumProviderName(string providerName)
        {
            if (providerName == providerName_SQLServer) return SQLProviderName.MSSQL;
            if (providerName == providerName_SQLite) return SQLProviderName.SQLite;
            if (providerName == providerName_SQLce) return SQLProviderName.SQLCE;

            return SQLProviderName.None;
        }

        public static DataProvider Instance {
            get { if (instance == null) instance = new DataProvider();return instance; } 
            private set => instance = value; 
        }

        public static IEnumerable<ColumnInfo> GetColumns(string Connection,string tableName = null)
        {

            SQLService sql = new SQLService(Connection);

            string query = ""; string condition = ""; if (tableName != null) condition = "and TableName = @TableName";

            //if (Instance.ProviderName == providerName_SQLServer)
                 query = string.Format(@"SELECT COLUMN_NAME as ColumnName,TABLE_NAME as TableName, IS_NULLABLE as IsNullString, CHARACTER_MAXIMUM_LENGTH as MaxLenght, 
                             DATA_TYPE as Type FROM INFORMATION_SCHEMA.COLUMNS where 1 = 1 {0} order by TABLE_NAME", condition);

            if (Instance.ProviderName == providerName_SQLite)
                query = string.Format(@"SELECT 
                              tb.name as TableName,  col.[notnull] as IsNotNull,col.[name] as ColumnName, col.[type] as Type
                            FROM  sqlite_master AS tb
                            JOIN 
                              pragma_table_info(tb.name) AS col
                            where 1 = 1 {0}
                            ORDER BY 
                              tb.name, col.name", condition);

            return sql.Connection.Query<ColumnInfo>(query,new { TableName = tableName });

        }

        public static IEnumerable<string> GetTableNames(string Connection)
        {

            SQLService sql = new SQLService(Connection);
            string query = "";

           // if (Instance.ProviderName == providerName_SQLServer)
                query = @"SELECT TABLE_NAME
                            FROM INFORMATION_SCHEMA.TABLES
                            WHERE TABLE_TYPE = 'BASE TABLE' and TABLE_SCHEMA ='dbo' order by  TABLE_NAME";

            if (Instance.ProviderName == providerName_SQLce)
                query = @"select table_name from information_schema.tables";

            if (Instance.ProviderName == providerName_SQLite)
                query = @"SELECT name FROM sqlite_master
                            WHERE type='table'
                            ORDER BY name;";

            return sql.Connection.Query<string>(query);
        }

        public static DataTable GetDataTable(IDataReader dataReader,bool isStringType = true, bool? isAllowNull = true)
        {
            DataTable dtSchema = dataReader.GetSchemaTable();

            var dataTable = new DataTable();

            List<string> colNames = new List<string>();
            foreach (DataRow dtrow in dtSchema.Rows)
            {
                string columnName = Convert.ToString(dtrow["ColumnName"]);
                DataColumn column = new DataColumn(columnName, isStringType? typeof(string): (Type)(dtrow["DataType"]));
                column.AllowDBNull = isAllowNull!=null? (bool)isAllowNull:(bool)(dtrow["AllowDBNull"]); //dont error when wrong recore;
                colNames.Add(columnName);
                dataTable.Columns.Add(column);
            }
            var colLength = colNames.Count;
            while (dataReader.Read())
            {
                var newRow = dataTable.Rows.Add();
                IDataRecord record = (IDataRecord)dataReader;

                for (int i = 0; i < colLength; i++)
                {
                    var colName = colNames[i];
                    newRow[colName] = dataReader[colName];
                }
            }

            //ChangeColumnBoolType(dataTable);

            return dataTable;
        }

        public static void ChangeColumnBoolType (DataTable dataTable)
        {
            var columnBooleans = new List<string>();
            foreach (DataColumn col in dataTable.Columns)
            {
                if(col.DataType.FullName == "System.Boolean")
                {
                    columnBooleans.Add(col.ColumnName);
                }
                
            }

            foreach (var colBoolean in columnBooleans)
            {
                ConvertColumnType(dataTable, colBoolean, typeof(object));
            }
        }

        public static void ConvertColumnType(DataTable dataTable, string columnName, Type newType)
        {
            using (DataColumn dc = new DataColumn(columnName + "_new", newType))
            {
                // Add the new column which has the new type, and move it to the ordinal of the old column
                int ordinal = dataTable.Columns[columnName].Ordinal;
                dataTable.Columns.Add(dc);
                dc.SetOrdinal(ordinal);

                // Get and convert the values of the old column, and insert them into the new
                foreach (DataRow dr in dataTable.Rows)
                    dr[dc.ColumnName] = Convert.ChangeType(dr[columnName], newType);

                // Remove the old column
                dataTable.Columns.Remove(columnName);

                // Give the new column the old column's name
                dc.ColumnName = columnName;
            }
        }
    }

}
