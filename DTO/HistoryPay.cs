using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.DTO
{
    public class HistoryPay
    {
        public string title { set; get; }
        public DateTime checkin { set; get; }
        public DateTime checkout { set; get; }
        public float discount { set; get; }
        public float total { set; get; }
        public float afterdiscount { set; get; }
    }
}
