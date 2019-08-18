using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Autin.Services
{
    public class HttpRequest
    {
        public async Task<string> GetRequest(string requestUri)
        {
            // Create a New HttpClient object and dispose it when done, so the app doesn't leak resources
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var responseBody = await client.GetAsync(requestUri);
                    var content = await responseBody.Content.ReadAsStringAsync();
                    return content.UnicodeDencode();
                }
                catch (HttpRequestException e)
                {
                }
                return "Err";
            }
        }

        public async Task<string> PostRequest(string requestUri, Dictionary<string, string> param)
        {
            // Create a New HttpClient object and dispose it when done, so the app doesn't leak resources
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var encodedContent = new FormUrlEncodedContent(param);
                    var responseBody = await client.PostAsync(requestUri, encodedContent);
                    var content = await responseBody.Content.ReadAsStringAsync();
                    return content.UnicodeDencode();
                }
                catch (HttpRequestException e)
                {
                }
                return "Err";
            }
        }

        public async Task<string> PostJsonRequest(string requestUri, string jsonData)
        {
            // Create a New HttpClient object and dispose it when done, so the app doesn't leak resources
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var encodedContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    var responseBody = await client.PostAsync(requestUri, encodedContent);
                    var content = await responseBody.Content.ReadAsStringAsync();
                    return content.UnicodeDencode();
                }
                catch (HttpRequestException e)
                {
                }
                return "Err";
            }
        }
    }
}
