using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DatabaseAccess
    {
        public QLBHXDataContext Db {  get;private set; }
        public string serverName { get;private set; }
        public string DbName { get; private set; }

        public DatabaseAccess()
        {
            Db = new QLBHXDataContext(Properties.Settings.Default.SieuThiBHX_V1ConnectionString);
            //them o day
        }
        public DatabaseAccess(QLBHXDataContext db, string serverName, string DbName)
        {
            this.Db = db;
            this.serverName = serverName;
            this.DbName = DbName;   
        }
    }
}
