using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SQLite;
using Gately.DAL;
using System.Data;
using System.IO; 

namespace Gately.DAL
{
    public class _table_create
    {
        public List<QueryObj> create(int queryType)
        {
            var queries = new List<QueryObj>();

            queries.Add(new QueryObj(queryType, EN_PEOPLE())); /* POTENTIAL BOTTLENECK!*/  
            return queries;
        }
         

        public SQLiteTable EN_PEOPLE(){
            SQLiteTable tb = new SQLiteTable("EN_ITEMS");
            tb.Columns.Add(new SQLiteColumn("itemid", true));
            tb.Columns.Add(new SQLiteColumn("itemname"));
            tb.Columns.Add(new SQLiteColumn("type", ColType.Integer)); //0 == pool, 1 == L1, 2 == L2, 3 == note, 4 == file
            tb.Columns.Add(new SQLiteColumn("permitto"));
            tb.Columns.Add(new SQLiteColumn("owner"));
            tb.Columns.Add(new SQLiteColumn("hashtags"));
            tb.Columns.Add(new SQLiteColumn("isactive", ColType.Integer));
            tb.Columns.Add(new SQLiteColumn("islocked", ColType.Integer));
            return tb;
        }

         
    }
}