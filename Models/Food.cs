using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Models
{
    class Food
    {
        public int id { get; set; }
        public string title { get; set; }
        public int foodcategory_id { get; set; }
        public int price { get; set; }
        public int unit_id { get; set; }
        public bool isAvailable { get; set; }
        public bool isDeleted { get; set; }
    }
}
