using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestaurantManager.Caller;
using RestaurantManager.DTO;
using Menu = RestaurantManager.DTO.Menu;

namespace RestaurantManager.DAO
{
    public class MenuDAO
    {
        private static MenuDAO instance;
        String base_url = ConfigurationManager.AppSettings["base_url"];
        private static RestSharpCaller<Menu> menucaller;

        public MenuDAO()
        {
           menucaller= new RestSharpCaller<Menu>(base_url);
        }

        public Object LoadMenuByIDTable(int id)
        {
            var menu = menucaller.Get("loadMenuByTableId/" + id);
            return menu;
        }

    }
}
