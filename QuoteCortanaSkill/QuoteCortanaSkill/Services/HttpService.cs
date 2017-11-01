using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace QuoteCortanaSkill.Services
{
    public static class HttpService
    {
        public static async Task<TClass> GetRequestAsync<TClass>(string url) where TClass : class
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(url).ConfigureAwait(false);
                    var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                        return JsonConvert.DeserializeObject<TClass>(responseString);

                    Debug.WriteLine(responseString);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("exception thrown: " + ex.Message);
                }
            }

            return default(TClass);
        }
    }
}