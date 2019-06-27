using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.DTO
{
    class Bill
    {
        public int id { get; set; }
        public DateTime checkin { get; set; }
        public DateTime checkout { get; set; }
        public int table_id { get; set; }
        public bool isPaid { get; set; }
        public float discount { get; set; }
        public float total { get; set; }
        public float afterdiscount { get; set; }
        public string note { get; set; }

    }
}
