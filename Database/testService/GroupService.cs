using Database;
using System.Data;

namespace Entities.TestClass
{
    public class GroupService : EntityService<GroupEntity>
    {
        public GroupService(string conn) : base(conn) { }
        public GroupService(IDbConnection db) : base(db) { }

    }
}
