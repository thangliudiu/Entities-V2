using CsvHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class ConvertJsonToCSV
    {
        public static DataTable JsonStringToTable(string jsonContent)
        {
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(jsonContent);
            return dt;
        }

        public static string JsonToCSV(string jsonContent,string fileName, string delimiter)
        {
            StreamWriter csvString = new StreamWriter(fileName);
            using (var csv = new CsvWriter(csvString,CultureInfo.InvariantCulture))
            {
                //csv.Configuration.IgnoreReferences = true;
                //csv.Configuration.Delimiter = delimiter;

                using (var dt = JsonStringToTable(jsonContent))
                {
                    csv.WriteField("sep=,", false);
                    csv.NextRecord();
         
                    foreach (DataColumn column in dt.Columns)
                    {
                        csv.WriteField(column.ColumnName);
                    }
                    csv.NextRecord();

                    foreach (DataRow row in dt.Rows)
                    {
                        for (var i = 0; i < dt.Columns.Count; i++)
                        {
                            csv.WriteField(row[i]);
                        }
                        csv.NextRecord();
                    }
                }
            }
            return csvString.ToString();
        }



    }

 
}
