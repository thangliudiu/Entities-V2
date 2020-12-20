using Dapper;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class EntityService<TEntity> where TEntity : class
    {
        public IDbConnection Connection { get; set; }

        private Type _typeEntity { get; set; }

        private Type TypeEntity
        {
            get
            {
                if (_typeEntity == null) _typeEntity = typeof(TEntity);
                return _typeEntity;
            }
        }

        private string TableName
        {
            get => GetTableName(TypeEntity);
        }

        public EntityService(IDbConnection _connection)
        {
            var keyProperty = KeyPropertiesCache(TypeEntity);

            if (keyProperty == null)
                throw new ArgumentException("Entity must have one [Key]");

            Connection = _connection;
        }

        public EntityService(string connectionString)
        {

            var keyProperty = KeyPropertiesCache(TypeEntity);

            if (keyProperty == null)
                throw new ArgumentException("Entity must have one [Key]");

            Connection = DataProvider.Instance.GetConnection(connectionString);
        }



        private static readonly ConcurrentDictionary<RuntimeTypeHandle, PropertyInfo> KeyProperties = new ConcurrentDictionary<RuntimeTypeHandle, PropertyInfo>();
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>> TypeProperties = new ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>>();
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>> IgnoreInsertProperties = new ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>>();
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>> IgnoreUpdateProperties = new ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>>();
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, string> FindQueries = new ConcurrentDictionary<RuntimeTypeHandle, string>();
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, string> GetAllQueries = new ConcurrentDictionary<RuntimeTypeHandle, string>();
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, string> TypetableName = new ConcurrentDictionary<RuntimeTypeHandle, string>();

        private static readonly Dictionary<string, ISqlAdapter> AdapterDictionary = new Dictionary<string, ISqlAdapter> {
                                                                                            {"sqlconnection", new SqlServerAdapter()},
                                                                                            {"sqlceconnection", new SqlCeServerAdapter()},
                                                                                            {"npgsqlconnection", new PostgresAdapter()},
                                                                                            {"sqliteconnection", new SQLiteAdapter()},
                                                                                            {"mysqlconnection", new MySqlAdapter()},
                                                                                        };

        private static List<PropertyInfo> IgnoreInsertPropertiesCache(Type type)
        {
            IEnumerable<PropertyInfo> pi;
            if (IgnoreInsertProperties.TryGetValue(type.TypeHandle, out pi))
            {
                return pi.ToList();
            }

            var ignoreInsertProperties = TypePropertiesCache(type).Where(p => p.GetCustomAttributes(true).Any(a => a is IgnoreInsertAttribute)).ToList();

            IgnoreInsertProperties[type.TypeHandle] = ignoreInsertProperties;
            return ignoreInsertProperties;
        }

        private static List<PropertyInfo> IgnoreUpdatePropertiesCache(Type type)
        {
            IEnumerable<PropertyInfo> pi;
            if (IgnoreUpdateProperties.TryGetValue(type.TypeHandle, out pi))
            {
                return pi.ToList();
            }

            var ignoreUpdateProperties = TypePropertiesCache(type).Where(p => p.GetCustomAttributes(true).Any(a => a is IgnoreUpdateAttribute)).ToList();

            IgnoreUpdateProperties[type.TypeHandle] = ignoreUpdateProperties;
            return ignoreUpdateProperties;
        }

        private static PropertyInfo KeyPropertiesCache(Type type)
        {
            if (KeyProperties.TryGetValue(type.TypeHandle, out PropertyInfo pi))
            {
                return pi;
            }

            var allProperties = TypePropertiesCache(type);
            var keyProperties = allProperties.Where(p => p.GetCustomAttributes(true).Any(a => a is KeyAttribute)).ToList();

            PropertyInfo keyProperty = null;

            if (keyProperties.Count == 0)
            {
                keyProperty = allProperties.FirstOrDefault(p => p.Name.ToLower() == "id");

            }
            else if (keyProperties.Count == 1)
            {
                keyProperty = keyProperties[0];
            }

            KeyProperties[type.TypeHandle] = keyProperty;
            return keyProperty;
        }

        private static List<PropertyInfo> TypePropertiesCache(Type type)
        {
            IEnumerable<PropertyInfo> pis;
            if (TypeProperties.TryGetValue(type.TypeHandle, out pis))
            {
                return pis.ToList();
            }

            var properties = type.GetProperties().Where(IsWriteableProp).ToArray();
            TypeProperties[type.TypeHandle] = properties;
            return properties.ToList();
        }

        private static string FindQueryCache(Type type)
        {
            string query = null;
            if (FindQueries.TryGetValue(type.TypeHandle, out query))
            {
                return query;
            }

            return query;
        }

        private static bool IsWriteableProp(PropertyInfo pi)
        {
            return pi.CanRead && pi.CanWrite;
        }

        private static ISqlAdapter GetFormatter(IDbConnection connection)
        {
            string name = connection.GetType().Name.ToLower();

            return !AdapterDictionary.ContainsKey(name) ?
                new SqlServerAdapter() : AdapterDictionary[name];
        }

        private static string GetTableName(Type type)
        {
            if (TypetableName.TryGetValue(type.TypeHandle, out string name)) return name;

            //NOTE: This as dynamic trick should be able to handle both our own Table-attribute as well as the one in EntityFramework 
            var tableAttr = type.GetCustomAttributes(false).SingleOrDefault(attr => attr.GetType().Name == nameof(TableAttribute)) as dynamic;
            if (tableAttr != null)
                name = tableAttr.Name;
            else
            {
                name = type.Name + "s";
                if (type.IsInterface && name.StartsWith("I"))
                    name = name.Substring(1);
            }

            TypetableName[type.TypeHandle] = name;
            return name;
        }

        // =================== BASE ===============
        public T Find<T>(dynamic id, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var type = TypeEntity;
            var typeParam = id.GetType();

            if (typeParam.IsArray || typeParam.IsGenericType)
                throw new DataException("Find<T> don't supports id is array,list,generic,...");

            if (!FindQueries.TryGetValue(type.TypeHandle, out string sql))
            {
                var keyProperty = KeyPropertiesCache(type);

                sql = "select * from " + TableName + " where " + keyProperty.Name + " = @id";

                FindQueries[type.TypeHandle] = sql;
            }

            var dynParms = new DynamicParameters();
            dynParms.Add("id", id);

            return Connection.Query<T>(sql, dynParms, transaction, commandTimeout: commandTimeout).FirstOrDefault();
        }

        public IEnumerable<T> Finds<T>(dynamic ids, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var type = TypeEntity;
            var typeParam = ids.GetType();

            if (!typeParam.IsArray && !typeParam.IsGenericType)
                throw new DataException("Finds<T> only supports id is array,list,generic,...");

            var keyProperty = KeyPropertiesCache(type);

            string sql = "select * from " + TableName + " where " + keyProperty.Name + " in @id";

            FindQueries[type.TypeHandle] = sql;

            var dynParms = new DynamicParameters();
            dynParms.Add("id", ids);

            return Connection.Query<T>(sql, dynParms, transaction, commandTimeout: commandTimeout);
        }

        public TEntity Find(dynamic id, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return Find<TEntity>(id, transaction, commandTimeout);
        }

        public IEnumerable<TEntity> Finds(dynamic ids, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return Finds<TEntity>(ids, transaction, commandTimeout);
        }

        public bool Insert<T>(T entityToInsert, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var type = TypeEntity;

            var sbColumnList = new StringBuilder(null);
            var allProperties = TypePropertiesCache(type);
            var ignoreInsertProperties = IgnoreInsertPropertiesCache(type);
            var allPropertiesExceptIgnoreInsert = allProperties.Except(ignoreInsertProperties).ToList();

            var adapter = GetFormatter(Connection);

            for (var i = 0; i < allPropertiesExceptIgnoreInsert.Count(); i++)
            {
                var property = allPropertiesExceptIgnoreInsert.ElementAt(i);
                adapter.AppendColumnName(sbColumnList, property.Name);  //fix for issue #336
                if (i < allPropertiesExceptIgnoreInsert.Count() - 1)
                    sbColumnList.Append(", ");
            }

            var sbParameterList = new StringBuilder(null);
            for (var i = 0; i < allPropertiesExceptIgnoreInsert.Count(); i++)
            {
                var property = allPropertiesExceptIgnoreInsert.ElementAt(i);
                sbParameterList.AppendFormat("@{0}", property.Name);
                if (i < allPropertiesExceptIgnoreInsert.Count() - 1)
                    sbParameterList.Append(", ");
            }

            int returnVal;
            var wasClosed = Connection.State == ConnectionState.Closed;
            if (wasClosed) Connection.Open();

            //insert entities
            var cmd = string.Format("insert into {0} ({1}) values ({2})", TableName, sbColumnList, sbParameterList);
            returnVal = Connection.Execute(cmd, entityToInsert, transaction, commandTimeout);

            if (wasClosed) Connection.Close();

            return returnVal > 0;
        }

        public bool Insert2<T>(T entityToInsert, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            try
            {

                var adapter = GetFormatter(Connection);

                if(!(adapter is SqlServerAdapter)) throw new Exception("insert2 just support mssql");

                var type = typeof(T);
                if (type.IsGenericType || type.IsArray)
                {
                    type = entityToInsert.GetType().GetGenericArguments()[0];
                }

                var sbColumnList = new StringBuilder(null);
                var sbColumnListSub = new StringBuilder(null);
                var allProperties = TypePropertiesCache(type);
                var ignoreInsertProperties = IgnoreInsertPropertiesCache(type);
                var allPropertiesExceptIgnoreInsert = allProperties.Except(ignoreInsertProperties).ToList();

                for (var i = 0; i < allPropertiesExceptIgnoreInsert.Count(); i++)
                {
                    var property = allPropertiesExceptIgnoreInsert.ElementAt(i);
                    adapter.AppendColumnName(sbColumnList, property.Name);  //fix for issue #336
                    sbColumnListSub.Append("data.");
                    adapter.AppendColumnName(sbColumnListSub, property.Name);  //fix for issue #336
                    if (i < allPropertiesExceptIgnoreInsert.Count() - 1)
                    { sbColumnList.Append(", "); sbColumnListSub.Append(", "); }
                }



                int returnVal = 0;

                if (entityToInsert is IEnumerable<object> enttities)
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    bool isFirst = true; bool isFirstOfRow = true; bool isLastOfRow = false;
                    List<dynamic> methods = new List<dynamic>();
                    string randomeKey = Guid.NewGuid().ToString();

                    string keyTerminator = randomeKey.Replace("-", "").Substring(0, 5);
                    string keyTerminatorField = keyTerminator + "|";
                    string keyTerminatorEndLine = keyTerminator + "End";

                    string tempTable = "Random_" + randomeKey.Replace("-", "");

                    string fileText = randomeKey + ".dat";

                    string workingDirectory = Environment.CurrentDirectory;

                    fileText = Path.Combine(workingDirectory, fileText);


                    foreach (var entity in enttities)
                    {
                        isFirstOfRow = true;

                        for (var i = 0; i < allPropertiesExceptIgnoreInsert.Count; i++)
                        {
                            if (isFirst)
                            {
                                var property = allPropertiesExceptIgnoreInsert.ElementAt(i);

                                dynamic method = UsingEmit(entity.GetType(), property);
                                methods.Add(method);
                            }

                            isLastOfRow = false;
                            if (!isFirstOfRow && i == allPropertiesExceptIgnoreInsert.Count - 1) isLastOfRow = true;

                            var value = methods[i](entity);
                            string valueString = null;

                            if (value != null)
                            {
                                if (value.GetType().Name == "DateTime")
                                    valueString = value.ToString("yyyy-MM-dd HH:mm:ss.fffffff");
                                else valueString = value.ToString();
                            }
                            WriteProperty(stringBuilder, valueString, isFirstOfRow, isLastOfRow, keyTerminatorField, keyTerminatorEndLine);
                            isFirstOfRow = false;
                        }
                        isFirst = false;
                    }

                    File.AppendAllText(fileText, stringBuilder.ToString());


                    string queryCreateTable = string.Format("SELECT top(0) {0} INTO  {1} FROM  {2}", sbColumnList, tempTable, TableName);

                    string queryInsertTemp = string.Format("BULK INSERT {0} FROM '{1}'" +
                        "WITH (FIRSTROW = 1,  FIELDTERMINATOR = '{2}',ROWTERMINATOR = '{3}'," +
                        "ROWS_PER_BATCH = 10000)", tempTable, fileText, keyTerminatorField, keyTerminatorEndLine);


                    string queryInsertMain = string.Format(@"insert INTO {0} ({1}) SELECT {2} FROM {3} data",
                        TableName, sbColumnList, sbColumnListSub, tempTable);

                    string queryDropTempTable = string.Format("drop table {0}", tempTable);

                    try
                    {
                        Connection.Execute(queryCreateTable, transaction);
                        Connection.Execute(queryInsertTemp, transaction);
                        returnVal = Connection.Execute(queryInsertMain, transaction);
                    }
                    catch (Exception ex)
                    {
                        //throw ex;
                    }
                    finally
                    {
                        try
                        {
                            Connection.Execute(queryDropTempTable, transaction);

                            File.Delete(fileText);
                        }
                        catch { }
                    }
                    return returnVal > 0;
                }

                var sbParameterList = new StringBuilder(null);
                for (var i = 0; i < allPropertiesExceptIgnoreInsert.Count(); i++)
                {
                    var property = allPropertiesExceptIgnoreInsert.ElementAt(i);
                    sbParameterList.AppendFormat("@{0}", property.Name);
                    if (i < allPropertiesExceptIgnoreInsert.Count() - 1)
                        sbParameterList.Append(", ");
                }

                var cmd = string.Format("insert into {0} ({1}) values ({2})", TableName, sbColumnList, sbParameterList);
                returnVal = Connection.Execute(cmd, entityToInsert, transaction, commandTimeout);

                return returnVal > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Delegate UsingEmit(Type type, PropertyInfo property)
        {
            string nameMethod = Guid.NewGuid().ToString();
            var method = new DynamicMethod(nameMethod, property.PropertyType, new[] { typeof(object) }, type, true);

            var il = method.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Castclass, type);
            il.Emit(OpCodes.Callvirt, property.GetGetMethod(true));
            il.Emit(OpCodes.Ret);

            Delegate func = null;

            string properyType = property.PropertyType.Name.ToLower();
            var delegateType = Expression.GetFuncType(typeof(object), property.PropertyType);
            func = method.CreateDelegate(delegateType);
            return func;
        }

        private void WriteProperty(StringBuilder sb, string value, bool first, bool last, string keyField, string keyEndLine)
        {
            if (value == null)
            {
                value = "";
            }

            if (!first)
            {
                sb.Append(keyField);
            }

            sb.Append(value);

            if (last)
            {
                sb.Append(keyEndLine);
            }

        }

        public bool Update<T>(T entityToUpdate, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var type = TypeEntity;

            var keyProperty = KeyPropertiesCache(type);

            var sb = new StringBuilder();
            sb.AppendFormat("update {0} set ", TableName);

            var allProperties = TypePropertiesCache(type);
            var ignoreUpdateProperties = IgnoreUpdatePropertiesCache(type);
            ignoreUpdateProperties.Add(keyProperty);

            var nonIdProps = allProperties.Except(ignoreUpdateProperties).ToList();

            var adapter = GetFormatter(Connection);

            for (var i = 0; i < nonIdProps.Count(); i++)
            {
                var property = nonIdProps.ElementAt(i);
                adapter.AppendColumnNameEqualsValue(sb, property.Name);  //fix for issue #336
                if (i < nonIdProps.Count() - 1)
                    sb.AppendFormat(", ");
            }
            sb.Append(" where ");

            adapter.AppendColumnNameEqualsValue(sb, keyProperty.Name);

            var updated = Connection.Execute(sb.ToString(), entityToUpdate, commandTimeout: commandTimeout, transaction: transaction);
            return updated > 0;
        }

        public bool Delete(dynamic id, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            bool isList = false;

            var typeParam = id.GetType();
            if (typeParam.IsArray || typeParam.IsGenericType)
                isList = true;

            var keyProperty = KeyPropertiesCache(TypeEntity);

            string sql = "delete from " + TableName + " where " + keyProperty.Name;
            if (isList) sql += " in @id";
            else sql += " = @id";
            var dynParms = new DynamicParameters();
            dynParms.Add("id", id);

            var deleted = Connection.Execute(sql, dynParms, transaction, commandTimeout);
            return deleted > 0;
        }

        // =================== New ===============
        private string QueryGetBase(bool isAll = false, string column = null, string condition = null, string orderby = null)
        {
            if (string.IsNullOrEmpty(column) == true) column = "*";

            if (!isAll) column = "top(1) " + column;

            string query = string.Format("select {0} from {1}", column, TableName);

            if (string.IsNullOrEmpty(condition) == false)
            {
                query += " where " + condition;
            }

            if (string.IsNullOrEmpty(orderby) == false)
            {
                query += " order By " + orderby;
            }

            return query;
        }

        public IEnumerable<T> GetAll<T>(string column = null, string condition = null, object obj = null, string orderBy = null, IDbTransaction transaction = null) where T : class
        {
            var sql = QueryGetBase(true, column, condition, orderBy);

            return Connection.Query<T>(sql, obj, transaction);
        }

        public IEnumerable<TEntity> GetAll(string column = null, string condition = null, object obj = null, string orderBy = null, IDbTransaction transaction = null)
        {
            return GetAll<TEntity>(column, condition, obj, orderBy, transaction);
        }

        public T Get<T>(string column = null, string condition = null, object obj = null, IDbTransaction transaction = null) where T : class
        {
            var sql = QueryGetBase(false, column, condition);

            return Connection.Query<T>(sql, obj, transaction).FirstOrDefault();
        }

        public bool Delete(string condition, object obj, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            if (string.IsNullOrEmpty(condition))
                throw new Exception("Condition is must not null");

            var keyProperty = KeyPropertiesCache(TypeEntity);

            string sql = "delete from " + TableName + " where " + condition;

            var deleted = Connection.Execute(sql, obj, transaction, commandTimeout);
            return deleted > 0;
        }

    }

}
