using RestaurantManager.Caller;
using RestaurantManager.DTO;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.DAO
{
    public class AccountDAO
    {
        String base_url = ConfigurationManager.AppSettings["base_url"];
        private static AccountDAO instance;
        private static RestSharpCaller<User> user_caller;

        public IRestResponse<Token> Login(String username, String password)
        {
            var client = new RestClient("http://localhost:8000/api");

            //client.AddDefaultHeader("Authorization", string.Format("Bearer {0}", bearerToken));


            var request = new RestRequest("auth/login ", Method.POST);
            Login obj = new Login(username, password, true);
            request.AddJsonBody(obj);
            IRestResponse<Token> respone = client.Execute<Token>(request);
            return respone;
        }


        public IRestResponse<User> GetDetail()
        {
            var client = new RestClient("http://localhost:8000/api");
            var getUserRequest = new RestRequest("auth/user ", Method.GET);
            client.AddDefaultHeader("Authorization", string.Format("Bearer {0}", Program.access_token));
            var respone1 = client.Execute<User>(getUserRequest);
            return respone1;
        }

    }
}
