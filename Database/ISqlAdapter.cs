using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public partial interface ISqlAdapter
    {
        int Insert(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, string tableName, string columnList, string parameterList, IEnumerable<PropertyInfo> keyProperties, object entityToInsert);

        //new methods for issue #336
        void AppendColumnName(StringBuilder sb, string columnName);
        void AppendColumnNameEqualsValue(StringBuilder sb, string columnName);
    }

    public partial class SqlServerAdapter : ISqlAdapter
    {
        public int Insert(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, string tableName, string columnList, string parameterList, IEnumerable<PropertyInfo> keyProperties, object entityToInsert)
        {
            //var cmd = String.Format("insert into {0} ({1}) values ({2});select SCOPE_IDENTITY() id", tableName, columnList, parameterList);
            //var multi = connection.QueryMultiple(cmd, entityToInsert, transaction, commandTimeout);

            //var first = multi.Read().FirstOrDefault();
            //if (first == null || first.id == null) return 0;

            //var id = (int)first.id;
            //var propertyInfos = keyProperties as PropertyInfo[] ?? keyProperties.ToArray();
            //if (!propertyInfos.Any()) return id;

            //var idProperty = propertyInfos.First();
            //idProperty.SetValue(entityToInsert, Convert.ChangeType(id, idProperty.PropertyType), null);

            //return id;
            return 0;
        }

        public void AppendColumnName(StringBuilder sb, string columnName)
        {
            sb.AppendFormat("[{0}]", columnName);
        }

        public void AppendColumnNameEqualsValue(StringBuilder sb, string columnName)
        {
            sb.AppendFormat("[{0}] = @{1}", columnName, columnName);
        }
    }

    public partial class SqlCeServerAdapter : ISqlAdapter
    {
        public int Insert(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, String tableName, string columnList, string parameterList, IEnumerable<PropertyInfo> keyProperties, object entityToInsert)
        {
            //var cmd = String.Format("insert into {0} ({1}) values ({2})", tableName, columnList, parameterList);
            //connection.Execute(cmd, entityToInsert, transaction, commandTimeout);
            //var r = connection.Query("select @@IDENTITY id", transaction: transaction, commandTimeout: commandTimeout).ToList();

            //if (r.First().id == null) return 0;
            //var id = (int)r.First().id;

            //var propertyInfos = keyProperties as PropertyInfo[] ?? keyProperties.ToArray();
            //if (!propertyInfos.Any()) return id;

            //var idProperty = propertyInfos.First();
            //idProperty.SetValue(entityToInsert, Convert.ChangeType(id, idProperty.PropertyType), null);

            //return id; 
            return 0;
        }

        public void AppendColumnName(StringBuilder sb, string columnName)
        {
            sb.AppendFormat("[{0}]", columnName);
        }

        public void AppendColumnNameEqualsValue(StringBuilder sb, string columnName)
        {
            sb.AppendFormat("[{0}] = @{1}", columnName, columnName);
        }
    }

    public partial class MySqlAdapter : ISqlAdapter
    {
        public int Insert(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, string tableName, string columnList, string parameterList, IEnumerable<PropertyInfo> keyProperties, object entityToInsert)
        {
            //var cmd = String.Format("insert into {0} ({1}) values ({2})", tableName, columnList, parameterList);
            //connection.Execute(cmd, entityToInsert, transaction, commandTimeout);
            //var r = connection.Query("Select LAST_INSERT_ID()", transaction: transaction, commandTimeout: commandTimeout);

            //var id = r.First().id;
            //if (id == null) return 0;
            //var propertyInfos = keyProperties as PropertyInfo[] ?? keyProperties.ToArray();
            //if (!propertyInfos.Any()) return id;

            //var idp = propertyInfos.First();
            //idp.SetValue(entityToInsert, Convert.ChangeType(id, idp.PropertyType), null);

            //return id;
            return 0;
        }

        public void AppendColumnName(StringBuilder sb, string columnName)
        {
            sb.AppendFormat("`{0}`", columnName);
        }

        public void AppendColumnNameEqualsValue(StringBuilder sb, string columnName)
        {
            sb.AppendFormat("`{0}` = @{1}", columnName, columnName);
        }
    }


    public partial class PostgresAdapter : ISqlAdapter
    {
        public int Insert(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, string tableName, string columnList, string parameterList, IEnumerable<PropertyInfo> keyProperties, object entityToInsert)
        {
            //var sb = new StringBuilder();
            //sb.AppendFormat("insert into {0} ({1}) values ({2})", tableName, columnList, parameterList);

            //// If no primary key then safe to assume a join table with not too much data to return
            //var propertyInfos = keyProperties as PropertyInfo[] ?? keyProperties.ToArray();
            //if (!propertyInfos.Any())
            //    sb.Append(" RETURNING *");
            //else
            //{
            //    sb.Append(" RETURNING ");
            //    var first = true;
            //    foreach (var property in propertyInfos)
            //    {
            //        if (!first)
            //            sb.Append(", ");
            //        first = false;
            //        sb.Append(property.Name);
            //    }
            //}

            //var results = connection.Query(sb.ToString(), entityToInsert, transaction, commandTimeout: commandTimeout).ToList();

            //// Return the key by assinging the corresponding property in the object - by product is that it supports compound primary keys
            //var id = 0;
            //foreach (var p in propertyInfos)
            //{
            //    var value = ((IDictionary<string, object>)results.First())[p.Name.ToLower()];
            //    p.SetValue(entityToInsert, value, null);
            //    if (id == 0)
            //        id = Convert.ToInt32(value);
            //}
            //return id;

            return 0;
        }

        public void AppendColumnName(StringBuilder sb, string columnName)
        {
            sb.AppendFormat("\"{0}\"", columnName);
        }

        public void AppendColumnNameEqualsValue(StringBuilder sb, string columnName)
        {
            sb.AppendFormat("\"{0}\" = @{1}", columnName, columnName);
        }
    }

    public partial class SQLiteAdapter : ISqlAdapter
    {
        public int Insert(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, string tableName, string columnList, string parameterList, IEnumerable<PropertyInfo> keyProperties, object entityToInsert)
        {
            //var cmd = String.Format("insert into {0} ({1}) values ({2}); select last_insert_rowid() id", tableName, columnList, parameterList);
            //var multi = connection.QueryMultiple(cmd, entityToInsert, transaction, commandTimeout);

            //var id = (int)multi.Read().First().id;
            //var propertyInfos = keyProperties as PropertyInfo[] ?? keyProperties.ToArray();
            //if (!propertyInfos.Any()) return id;

            //var idProperty = propertyInfos.First();
            //idProperty.SetValue(entityToInsert, Convert.ChangeType(id, idProperty.PropertyType), null);

            //return id;
            return 0;
        }

        public void AppendColumnName(StringBuilder sb, string columnName)
        {
            sb.AppendFormat("\"{0}\"", columnName);
        }

        public void AppendColumnNameEqualsValue(StringBuilder sb, string columnName)
        {
            sb.AppendFormat("\"{0}\" = @{1}", columnName, columnName);
        }
    }
}
