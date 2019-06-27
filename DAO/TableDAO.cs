using RestaurantManager.Caller;
using RestaurantManager.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.DAO
{
    public class TableDAO
    {
        String base_url = ConfigurationManager.AppSettings["base_url"];
        private static TableDAO instance;
        private static RestSharpCaller<Table> tablecaller;
        public TableDAO()
        {
            tablecaller = new RestSharpCaller<Table>(base_url);

        }
        public Object LoadListTable()
        {
            //List<Table> listTable = new List<Table>();

            var listTable = tablecaller.Get("table");
            return listTable;
        }

        public Object LoadTable(int id)
        {
            var table = tablecaller.GetSingle("table/" + id);
            return table;
        }
    }
}
