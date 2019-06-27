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

        public List<T> Get(string path)
        {
            var request = new RestRequest(path, Method.GET);
            var response = client.Execute<List<T>>(request);
            return response.Data;
        }

       

        public T GetSingle(string path)
        {
            var request = new RestRequest(path, Method.GET);
            var respone = client.Execute(request);
            T result = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(respone.Content);
            return result;
        }

        public void Create(string path, T obj)
        {
            var request = new RestRequest(path, Method.POST);
            request.AddJsonBody(obj);
            client.Execute(request);
        }

        public void Update(string path, int id, T obj)
        {
            var request = new RestRequest(path + '/' + id, Method.PUT);
            request.AddJsonBody(obj);
            client.Execute(request);
        }

        public void Delete(string path, int id)
        {
            var request = new RestRequest(path + '/' + id, Method.DELETE);
            client.Execute(request);
        }
    }
}