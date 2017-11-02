using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.IO;
using System.Text;

namespace SuperCoolApp.Controllers
{
    [Route("api/")]
    public class PassThroughApiController : Controller
    {
        private const string baseAddress = "http://localhost:5000/api/";
        static HttpClient http = new HttpClient(new HttpClientHandler { UseCookies = false, AllowAutoRedirect = false });

        /// <summary>
        /// Gets the specified URL.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{*any}")]
        public Task<IActionResult> Get()
        {
            return SendAsync(HttpMethod.Get);
        }

        /// <summary>
        /// Posts the specified URL.
        /// </summary>
        /// <returns></returns>
        [HttpPost("{*any}")]
        public Task<IActionResult> Post()
        {
            return SendAsync(HttpMethod.Post);
        }

        /// <summary>
        /// Puts the specified URL.
        /// </summary>
        /// <returns></returns>
        [HttpPut("{*any}")]
        public Task<IActionResult> Put()
        {
            return SendAsync(HttpMethod.Put);
        }

        /// <summary>
        /// Deletes the specified URL.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{*any}")]
        public Task<IActionResult> Delete()
        {
            return SendAsync(HttpMethod.Delete);
        }

        private async Task<IActionResult> SendAsync(HttpMethod method)
        {
            try
            {
                string path = Request.Path.Value.Replace("/api/", "");
                string pathAndQuery = path + Request.QueryString;

                var uri = new Uri(baseAddress + pathAndQuery);

                HttpRequestMessage request = new HttpRequestMessage(method, uri);

                if (method == HttpMethod.Put || method == HttpMethod.Post)
                {
                    StreamReader reader = new StreamReader(Request.Body);
                    string text = reader.ReadToEnd();

                    //request.Headers.Add("Content-Lenght", text.Length.ToString());
                    request.Content = new StringContent(text);
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                }

                var httpResponse = await http.SendAsync(request);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var responseData = await httpResponse.Content.ReadAsStringAsync();
                    return Ok(Newtonsoft.Json.JsonConvert.DeserializeObject(responseData));
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, httpResponse.ReasonPhrase);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
