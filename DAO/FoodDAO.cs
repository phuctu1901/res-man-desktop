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
    public class FoodDAO
    {
        String base_url = ConfigurationManager.AppSettings["base_url"];
        private static FoodDAO instance;
        private static RestSharpCaller<Food> food_caller;

        public FoodDAO()
        {
            food_caller = new RestSharpCaller<Food>(base_url);
        }
        public Object LoadFoodByCategoryID(int id)
        {
            var foods = food_caller.Get("food/" + id);
            return foods;
        }

    }
}
