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
    class HistoryPayRequest
    {
        public String date { set; get; }
        public HistoryPayRequest(String date)
        {
            this.date = date;
        }
    }

    public class HistoryPayDAO
    {
        private static HistoryPayDAO instance;
        String base_url = ConfigurationManager.AppSettings["base_url"];
        private static RestSharpCaller<HistoryPayRequest> history_pay_caller;

        public HistoryPayDAO()
        {
            history_pay_caller = new RestSharpCaller<HistoryPayRequest>(base_url);
        }

        public void LoadHistoryPay(String date)
        {
            HistoryPayRequest historyPayRequest = new HistoryPayRequest(date);
            history_pay_caller.Create("loadpayhistory", historyPayRequest);
        }
    }
}
