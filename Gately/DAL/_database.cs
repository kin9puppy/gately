using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SQLite;
using System.Data;
using Gately.DAL;
using System.IO;

namespace Gately.DAL
{
    public class _database
    {
        public bool createTables(string db)
        {
            try
            {
                var ret = new transmit().run(db, new _table_create().create(1));
                return ret.completed;
            }
            catch { return false; }
        }
        

        public bool upsertUser(string db)
        {
                var dic = new Dictionary<string, object> { { "@childid", "some value" } };
                var query = "insert into EN_USERS (uid, uname, utype, netid) " +
                            "values (@uid, @uname, @utype, @netid)"; 
                var ret2 = select(db, query, dic);
                return ret2.completed;  
        }
        public string getParentId(string db, string childid)
        {
            var query = "select parentid from EN_HEIR where childid=@childid";
            var sdic = new Dictionary<string, object> { { "@childid", childid } };

            var res = select(db, query, sdic);

            foreach (DataRow row in res.datatable.Rows)
            {
                return row["parentid"].ToString();
            }

            throw new EvaluateException();
        }

        public ReturnObj select(string db, string select, Dictionary<string, object> dic)
        {
            var query = new QueryObj(0, select, dic);
            return new transmit().run(db, query);
        }

    }
}