using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Helper
{
    public class HTTPService <T>
    {
        public HTTPService(string _baseUri="")
        {
            BaseAddress = _baseUri;
        }

        public string BaseAddress { get; set; }
        public string Path { get; set; }
        public string Authentication { get; set; }
        public string Method { get; set; }
        public string ContentLanguage { get; set; }



        public async Task<T> PostAsync(object Content=null ,bool isStringContent=true, MultipartFormDataContent multipartContent = null)  
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseAddress);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (Authentication is not null)
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Authentication}");

            }
            client.DefaultRequestHeaders.Add("lang", ContentLanguage);
            HttpResponseMessage response;
            if (isStringContent)
            {
                var content = new StringContent(JsonConvert.SerializeObject(Content), Encoding.UTF8, "application/json");
                response = client.PostAsync(Path, content).Result;

            }
            else
            {
                response = client.PostAsync("", multipartContent).Result;
            }
            if (response.StatusCode is System.Net.HttpStatusCode.Unauthorized)
            {
                throw new Exception("401");
            }
            string contents = await response.Content.ReadAsStringAsync();
            T result = JsonConvert.DeserializeObject<T>(contents);

            return result;
        }

        public async Task<T> GetAsync(object StringParamiter=null)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseAddress);
           
            if (Authentication is not null)
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Authentication}");

            }
            client.DefaultRequestHeaders.Add("lang", ContentLanguage);
            var response = await client.GetAsync($"{Path}/{StringParamiter}");
            if (response.StatusCode is System.Net.HttpStatusCode.Unauthorized)
            {
                throw new Exception("401");
            }
     
            string contents = await response.Content.ReadAsStringAsync();
            T result = JsonConvert.DeserializeObject<T>(contents);
            return result;
        }

        public async Task<T> PutAsync(object Content = null, CancellationToken cancellationToken = default)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(BaseAddress);
                if (Authentication is not null)
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Authentication}");
                }
                client.DefaultRequestHeaders.Add("lang", ContentLanguage);

                var d = new StringContent(JsonConvert.SerializeObject(Content), Encoding.UTF8, "application/json");
                var response = await client.PutAsync($"{Path}", d, cancellationToken).ConfigureAwait(false);

                string contents = await response.Content.ReadAsStringAsync();
                T result = JsonConvert.DeserializeObject<T>(contents);
                return result;
            }
            catch(HttpRequestException e) 
            {
                throw new(e.Message);
            }
        }

        public async Task<T> DeleteAsync()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseAddress);

            if (Authentication is not null)
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Authentication}");

            }
            client.DefaultRequestHeaders.Add("lang", ContentLanguage);
            var response = await client.DeleteAsync($"{Path}");


            string contents = await response.Content.ReadAsStringAsync();
            T result = JsonConvert.DeserializeObject<T>(contents);
            return result;
        }



    }
}
