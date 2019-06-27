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
    public class CategoryDAO
    {
        String base_url = ConfigurationManager.AppSettings["base_url"];
        private static CategoryDAO instance;
        private static RestSharpCaller<FoodCategory> food_category_caller;
        public CategoryDAO()
        {
            food_category_caller = new RestSharpCaller<FoodCategory>(base_url);
        }

        public Object LoadListFoodCategory()
        {
            //List<Table> listTable = new List<Table>();

            var listFoodCategory = food_category_caller.Get("foodcategory");
            return listFoodCategory;
        }
    }
}
