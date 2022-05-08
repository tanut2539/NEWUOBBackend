using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UOB.PVD.Helper
{
    public class HTTPService 
    {
        public HTTPService(string _baseUri = "")
        {
            BaseAddress = _baseUri;
        }

        public string BaseAddress { get; set; }
        public string Path { get; set; }
        public string Authentication { get; set; }
        public string Method { get; set; }
        public string ContentLanguage { get; set; }



        public HttpResponseMessage PostAsync(object Content = null)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseAddress);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (Authentication is not null)
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Authentication}");
            }
            client.DefaultRequestHeaders.Add("lang", ContentLanguage);
            var content = new StringContent(JsonConvert.SerializeObject(Content), Encoding.UTF8, "application/json");
            return client.PostAsync(Path, content).Result;
        }

        public HttpResponseMessage GetAsync(object StringParamiter = null)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseAddress);

            if (Authentication is not null)
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Authentication}");

            }
            client.DefaultRequestHeaders.Add("lang", ContentLanguage);
            if (StringParamiter is not null)
            {
                return client.GetAsync($"{Path}/{StringParamiter}").Result;
            }
            return client.GetAsync($"{Path}").Result;


        }

 
    }
}
