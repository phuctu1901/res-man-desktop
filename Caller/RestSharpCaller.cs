using System.Collections.Generic;
using System.Net;
using RestSharp;

namespace RestaurantManager.Caller
{
    public class RestSharpCaller<T>
    {
        private RestClient client;
        public RestSharpCaller(string baseUrl)
        {
            client = new RestClient(baseUrl);
            client.AddDefaultHeader("Authorization", string.Format("Bearer {0}", Program.access_token));

        }
        //Gọi phương thức này để lấy về một danh sách các đối tượng được deserialize từ json 
        public List<T> Get(string path)
        {
            var request = new RestRequest(path, Method.GET);
            var response = client.Execute<List<T>>(request);
            return response.Data;
        }

      
       
        //Gọi phương thức này để lấy về một đối tượng đã được deserialize từ json
        public T GetSingle(string path)
        {
            var request = new RestRequest(path, Method.GET);
            var respone = client.Execute(request);
            T result = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(respone.Content);
            return result;
        }
    }
}