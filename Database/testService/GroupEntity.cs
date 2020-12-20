using Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Entities.TestClass
{
    //    CREATE TABLE groups(
    //   group_id INTEGER PRIMARY KEY,
    //   name TEXT NOT NULL
    //);


    [Database.Table("groups")]
    public class GroupEntity
    {

        [Key]
        public string group_id { get; set; }


        public string name { get; set; }

    }
}
