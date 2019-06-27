using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Models
{
    class User
    {
        public int id { set; get; }
        public string name { set; get; }
        public string email { set; get; }
        public bool isAdmin { set; get; }
   
    }
}
