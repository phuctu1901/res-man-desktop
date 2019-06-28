using RestaurantManager.Caller;
using RestaurantManager.DTO;
using RestSharp;
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

        public IRestResponse LoadHistoryPay(String date)
        {
            var client = new RestClient(base_url);
            var request = new RestRequest("loadpayhistory", Method.POST);
            HistoryPayRequest historyPayRequest = new HistoryPayRequest(date);
            request.AddJsonBody(historyPayRequest);
            var response = client.Execute(request);
            return response;
        }
    }
}
