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
    public class BillDAO
    {
        String base_url = ConfigurationManager.AppSettings["base_url"];
        private static BillDAO instance;
        private static RestSharpCaller<Bill> bill_caller;

        public IRestResponse GetUnPaidBill(int tableId)
        {
            var client = new RestClient(base_url);
            var request = new RestRequest("getBillUnPaid/"+tableId, Method.GET);
            var response = client.Execute(request);
            var result = response.Content;
            return response;
        }

        public void AddDiscount(String billId, float discount)
        {
            var client = new RestClient(base_url);
            var addDiscountRequest = new RestRequest("discount", Method.POST);
            addDiscountRequest.AddParameter("bill_id", billId);
            addDiscountRequest.AddParameter("discount", discount);
            addDiscountRequest.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            client.Execute(addDiscountRequest);
        }

        public void CheckOut(String billId, float discount)
        {
            var client = new RestClient(base_url);
            var checkOutRequest = new RestRequest("checkout", Method.POST);
            checkOutRequest.AddParameter("bill_id", billId);
            AddDiscount(billId, discount);
            client.Execute(checkOutRequest);
        }
    }
}
