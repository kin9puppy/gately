using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SQLite;
using System.Data;
using Gately.DAL;

namespace Gately.DAL
{
    public class transmit
    {
        public ReturnObj run(string dbfile, List<QueryObj> queries)
        {
            var ret = new ReturnObj();

            try
            {

                using (SQLiteConnection conn = new SQLiteConnection(dbfile))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand())
                    {

                        conn.Open();
                        cmd.Connection = conn;
                        SQLiteHelper sh = new SQLiteHelper(cmd);
                        sh.BeginTransaction();

                        try
                        {

                            foreach (QueryObj query in queries)
                            {
                                if (query != null)
                                {
                                    switch (query.caseid)
                                    {
                                        case 0: ret.datatable = sh.Select(query.select, query.selectionary); break; //select
                                        case 1: sh.CreateTable(query.newtable); break; // create tables
                                        case 2: ret.datatable = sh.GetTableStatus(); break; // get table status
                                        case 3: sh.Update(query.table, query.dictionary, query.selectionary); tryGetId(ref ret, query.selectionary, 3, null); break; //update item, pass id for record
                                        case 4: sh.Insert(query.table, query.dictionary); tryGetId(ref ret, null, 4, sh.LastInsertRowId()); break; //insert item, get id
                                        case 5: query.dictionary["itemid"] = ret.id; sh.Insert(query.table, query.dictionary); break; //insert record with last item id
                                        case 6: sh.Insert(query.table, query.dictionary); break; //simple insert
                                        case 7: query.dictionary["childid"] = ret.id; sh.Insert(query.table, query.dictionary); break; //insert new parent
                                        case 8: sh.UpdateTableStructure(query.newtable.TableName, query.newtable); break;
                                    }
                                }
                            }
                            sh.Commit();
                            conn.Close();
                        }
                        catch (Exception ex)
                        {
                            sh.Rollback();
                            ret.completed = false;
                            return ret;
                        }
                    }
                }
                ret.completed = true;
                return ret;
            }
            catch (Exception ex)
            {
                ret.completed = false;
                return ret;
            }
        }

        public ReturnObj run(string dbfile, QueryObj query)
        {
            var queries = new List<QueryObj>();
            queries.Add(query);
            return run(dbfile, queries);
        }

        public ReturnObj run(string dbfile, int caseid)
        {
            return run(dbfile, new QueryObj(caseid, null));
        }

        public void tryGetId(ref ReturnObj ret, Dictionary<string, object> selectionary, int caller, long? lastid)
        {
            if (caller == 3)
            {
                if(selectionary.ContainsKey("itemid")){
                int i = 0;
                Int32.TryParse(selectionary["itemid"].ToString(), out i);
                ret.id = i; }
            }
            if (caller == 4)
            {
                ret.id = Convert.ToInt32(lastid);
            }
        }
    }
}