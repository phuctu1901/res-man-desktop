using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Models
{
    class Token
    {
        public string access_token { set; get; }
        public string token_type { set; get; }
        public DateTime expires_at { set; get; }
    }
}
