using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLitePCL;
using SQLitePCL.Extensions;

namespace Sqlite.DbManager
{
    public sealed class DbQuery : DbManagerBase
    {
        SQLiteConnection conn = null;

        public DbQuery()
        {
            conn = new SQLiteConnection("");
            conn.Prepare("");
             
        }

            

    }
}
