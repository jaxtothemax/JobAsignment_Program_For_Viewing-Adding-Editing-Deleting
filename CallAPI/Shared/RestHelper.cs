using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace CallAPI.Shared
{
    public static class RestHelper
    {
        private static readonly string baseURL = "https://localhost:44333/api/";
        public static async Task<string> GetALL()
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync(baseURL + "product"))
                {
                    using (HttpContent content = res.Content)
                    {
                        string data = await content.ReadAsStringAsync();
                        if (data != null)
                        {
                            return data;
                        }
                    }
                }
            }
            return string.Empty;
        }
        public static string BeautifyJson(string jsonStr)
        {
            JToken parseJson = JToken.Parse(jsonStr);
            return parseJson.ToString(Formatting.Indented);
        }

        public static async Task<string> Get(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync(baseURL + "product/" + id))
                {
                    using (HttpContent content = res.Content)
                    {
                        string data = await content.ReadAsStringAsync();
                        if (data != null)
                        {
                            return data;
                        }
                    }
                }
            }
            return string.Empty;
        }

        public static async Task<string> Post(string Naziv, string Cena, string Stock, string Code)
        {
            var inputData = new Dictionary<string, string>
            {
                {"Name", Naziv},
                {"Price", Cena },
                {"Stock", Stock },  
                {"Code", Code }
            };
            var input = new FormUrlEncodedContent(inputData);

            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.PostAsync(baseURL + "product", input ))
                {
                    using (HttpContent content = res.Content)
                    {
                        string data = await content.ReadAsStringAsync();
                        if (data != null)
                        {
                            return data;
                        }
                    }
                }
            }
            return string.Empty;
        }

        public static async Task<bool> Put(string id, string Naziv, string Cena, string Stock, string Code)
        {
            var inputData = new Dictionary<string, string>
            {
                {"ProductId", id},
                {"Name", Naziv},
                {"Price", Cena },
                {"Stock", Stock },
                {"Code", Code }
            };
            var input = new FormUrlEncodedContent(inputData);

            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.PutAsync(baseURL + "product/" + id, input))
                {
                    if (res.IsSuccessStatusCode)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static async Task<bool> Delete(string productId)
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.DeleteAsync(baseURL + "product/" + productId))
                {
                    if (res.IsSuccessStatusCode)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}

//ttps://localhost:44333/api/