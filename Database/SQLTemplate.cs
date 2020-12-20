using Database.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class SQLTemplate
    {
        // just sequence;
        private const string TableName = "{0}";
        private const string Column = "{1}";
        private const string TableNameJoin = "{2}";
        private const string Condition = "{3}";

        public static string CreateTable(SQLProviderName providerName = SQLProviderName.None)
        {
            string template = @"CREATE TABLE [{0}] ({1})";

            return template;
        }

        public static string DropTable(SQLProviderName providerName = SQLProviderName.None)
        {
            string template = @"DROP TABLE [{0}]";

            return template;
        }

        public static string Select(SQLProviderName providerName = SQLProviderName.None)
        {
            var template = "SELECT * FROM [{0}];";

            return template;
        }

        public static string Insert(SQLProviderName providerName = SQLProviderName.None)
        {
            var template = @"INSERT INTO [{0}] ({1}) 
VALUES ();";
            return template;
        }

        public static string Update(SQLProviderName providerName = SQLProviderName.None)
        {
            var template = @"UPDATE [{0}] SET {1} WHERE {2};";
            return template;
        }

        public static string Delete(SQLProviderName providerName = SQLProviderName.None)
        {
            var template = @"DELETE FROM [{0}] WHERE {1};";
            return template;
        }

        public static string Count(SQLProviderName providerName = SQLProviderName.None)
        {
            var template = @"SELECT COUNT(*) FROM [{0}] WHERE {1}";
            return template;
        }

        public static string Join(SQLProviderName providerName = SQLProviderName.None)
        {
            var template = @"SELECT {1} FROM [{0}] INNER JOIN {2} ON {3};";
            return template;
        }

        public static string SelectOffset(SQLProviderName providerName = SQLProviderName.None)
        {
            //mssql  
            string template1 = @"SELECT * FROM [{0}] ORDER BY {1} OFFSET {2} ROWS  FETCH NEXT {3} ROWS ONLY;";

            //sqllite
            string template2 = @"SELECT * FROM [{0}] ORDER BY {1} LIMIT {2}, {3};";

            //sql compact 
            string template3 = @"SELECT * FROM [{0}] ORDER BY {1} OFFSET {2} ROWS FETCH NEXT {3} ROWS ONLY;";

            string template = template1;

            if (providerName == SQLProviderName.MSSQL) template = template1;
            if (providerName == SQLProviderName.SQLite) template = template2;
            if (providerName == SQLProviderName.SQLCE) template = template3;

            return template;
        }

        public static string SelectLimit(SQLProviderName providerName = SQLProviderName.None)
        {
            //mssql  
            string template1 = @"SELECT top({1})* FROM [{0}];";

            //sqllite
            string template2 = @"SELECT * FROM [{0}] LIMIT {1};";

            //sql compact 
            string template3 = @"SELECT top({1})* FROM [{0}];";

            string template = template1;

            if (providerName == SQLProviderName.MSSQL) template = template1;
            if (providerName == SQLProviderName.SQLite) template = template2;
            if (providerName == SQLProviderName.SQLCE) template = template3;

            return template;
        }


        public static class Example
        {
            public static string CreateTable(SQLProviderName providerName = SQLProviderName.None)
            {
                // mssql;
                string example1 = @"CREATE TABLE Persons 
(Id int,
LastName varchar(255),
FirstName varchar(255),
Address varchar(255),
City varchar(255) );";

                //sql lite;
                string example2 = @"CREATE TABLE contacts 
(Id INTEGER PRIMARY KEY,
first_name TEXT NOT NULL,
last_name TEXT NOT NULL,
email TEXT NOT NULL UNIQUE,
phone TEXT NOT NULL UNIQUE);";

                //sql compact
                string example3 = @"CREATE TABLE CoolPeople 
(LastName nvarchar (40) not null, 
FirstName nvarchar (40), 
URL nvarchar (256) )";
                string example = example1;

                if (providerName == SQLProviderName.MSSQL) example = example1;
                if (providerName == SQLProviderName.SQLite) example = example2;
                if (providerName == SQLProviderName.SQLCE) example = example3;

                return example;
            }

            public static string DropTable(SQLProviderName providerName = SQLProviderName.None)
            {
                string example = @"DROP TABLE table_name;";
                return example;
            }

            public static string Insert(SQLProviderName providerName = SQLProviderName.None)
            {
                string example = @"INSERT INTO Customers (CustomerName, City, Country)
VALUES ('Cardinal', 'Stavanger', 'Norway');";
                return example;
            }

            public static string Join(SQLProviderName providerName = SQLProviderName.None)
            {
                string example = @"SELECT Orders.OrderID, Customers.CustomerName, Orders.OrderDate
FROM Orders
INNER JOIN Customers ON Orders.CustomerID=Customers.CustomerID;";
                return example;
            }

            public static string SelectOffset(int offset = 2, int amount = 10, SQLProviderName providerName = SQLProviderName.None)
            {
                //  skip [offset] rows => select [amount] rows;
                // --Skip [offset] rows and return only the first [amount] rows from the sorted result set.

                //mssql  
                string example1 = string.Format(@"SELECT * FROM production
                            ORDER BY product_name 
                            OFFSET {0} ROWS 
                            FETCH NEXT {1} ROWS ONLY;", offset, amount);

                //sqllite
                string example2 = string.Format(@"SELECT * FROM production ORDER BY product_name 
                                    LIMIT {0}, {1};", offset, amount);

                //sql compact 
                string example3 = string.Format(@"SELECT * FROM production ORDER BY product_name 
                                    OFFSET {0} ROWS 
                                    FETCH NEXT {1} ROWS ONLY;", offset, amount);
                string example = example1;

                if (providerName == SQLProviderName.MSSQL) example = example1;
                if (providerName == SQLProviderName.SQLite) example = example2;
                if (providerName == SQLProviderName.SQLCE) example = example3;

                return example;
            }

            public static string SelectLimit(int amount = 10, SQLProviderName providerName = SQLProviderName.None)
            {
                //   select [amount] rows;

                //mssql  
                string example1 = string.Format(@"SELECT top({0}) * FROM production;", amount);

                //sqllite
                string example2 = string.Format(@"SELECT * FROM production LIMIT {0}", amount);

                //sql compact 
                string example3 = string.Format(@"SELECT top({0})* FROM production;", amount);

                string example = example1;

                if (providerName == SQLProviderName.MSSQL) example = example1;
                if (providerName == SQLProviderName.SQLite) example = example2;
                if (providerName == SQLProviderName.SQLCE) example = example3;

                return example;
            }
        }

    }
}
