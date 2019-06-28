using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.DTO
{
    public class Menu
    {
        public string title { get; set; }
        public float food_price { get; set; }
        public float total_price { get; set; }
        public int count { get; set; }

    }
}
