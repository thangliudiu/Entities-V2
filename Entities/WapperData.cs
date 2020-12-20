using Common;
using ControlCustomer;
using Dapper;
using Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Entities
{
    public class WapperData
    {
 
        public string Connection { get; private set; }

        private List<string> tableNames { get; set; }

        public List<string> TableNames
        {
            get
            {
                if (tableNames == null) tableNames = DataProvider.GetTableNames(Connection)?.ToList();

                return tableNames;
            }
        }

        private List<ColumnInfo> columnNames { get; set; }

        public List<ColumnInfo> ColumnNames
        {
            get
            {
                if (columnNames == null) columnNames = DataProvider.GetColumns(Connection).ToList();

                return columnNames;
            }
        }

        public WapperData(string connection)
        {
            Connection = connection;
            tableNames = null;
            columnNames = null;
        }


        public List<ColumnInfo> GetColumnsByTable(string tableName, bool isOrder = true)
        {
            var cols = ColumnNames.Where(x => x.TableName.Equals(tableName, StringComparison.OrdinalIgnoreCase));

            if (isOrder) return cols.OrderBy(x => x.ColumnName).ToList();

            return cols.ToList();
        }

        public List<string> GetColumnNamesByTable(string tableName, bool isOrder = true)
        {
            var cols = ColumnNames.Where(x => x.TableName.Equals(tableName, StringComparison.OrdinalIgnoreCase)).Select(x => x.ColumnName);

            if (isOrder) return cols.OrderBy(x => x).ToList();

            return cols.ToList();
        }

    }
}
