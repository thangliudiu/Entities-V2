using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class DapperHelpers
    {
        public static dynamic ToExpandoObject(object value)
        {
            IDictionary<string, object> dapperRowProperties = value as IDictionary<string, object>;

            dynamic expando = new ExpandoObject();
            var expandoDic = expando as IDictionary<string, object>; 
            foreach (KeyValuePair<string, object> property in dapperRowProperties)
                expandoDic.Add(property.Key, property.Value);

            return expando ;
        }
    }
}
