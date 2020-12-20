using Dapper;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SQLite;
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
    public class SQLService
    {
        public IDbConnection Connection { get; set; }

        public SQLService(IDbConnection connection)
        {
            Connection = connection;
        }

        public SQLService(string connectionString)
        {
            Connection = DataProvider.Instance.GetConnection(connectionString);
        }

        public void test()
        {
            SqlBuilder sqlBuilder = new SqlBuilder();

            var template = sqlBuilder.AddTemplate("SELECT * /**select**/ from ThingTags /**where**/ ");

            sqlBuilder.Select("*");

            var builder = new SqlBuilder();
            builder.Select("id_something");
            builder.Select("MyCol");
            builder.Select("OtherCol");
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@MyParam", 3, DbType.Int32, ParameterDirection.Input);
            builder.Where("id_something < @MyParam", parameters);
            // builder.Where("id_something < @MyParam", new { MyParam =3}); //this is other option for params.
            builder.InnerJoin("OtherTable on OtherTable.id=MyTable.id");
            //The /**something**/ are placeholders,
            var builderTemplate = builder.AddTemplate("Select /**select**/ from MyTable /**innerjoin**/ /**where**/ ");
            var result = Connection.Query(builderTemplate.RawSql, builderTemplate.Parameters);

        }

    }
}
