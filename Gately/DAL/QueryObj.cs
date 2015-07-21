using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;

namespace Gately.DAL
{
    public class QueryObj
    { 
        public string table { get; set; }
        public int caseid { get; set; }
        public SQLiteTable newtable { get; set; }
        public Dictionary<string, object> dictionary { get; set; }
        public Dictionary<string, object> selectionary { get; set; }
        public string select { get; set; }
        public int param1 { get; set; }
        

        public QueryObj(string _table)
        {
            table = _table;
        }
        public QueryObj(int _caseid, SQLiteTable _newtable) 
        {
            caseid = _caseid;
            newtable = _newtable;
        }
        public QueryObj(int _caseid, string _select, Dictionary<string, object> _selectionary)
        {
            caseid = _caseid;
            select = _select;
            selectionary = _selectionary;
        }
    }

    public class ReturnObj
    {
     public bool completed { get; set; }
     public DataTable datatable {get; set;}
     public int? id { get; set; }
    }
}