using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MarkItCore
{
    public class RestfulQuoteRequest
    {
        private const string BaseUrl = "http://dev.markitondemand.com/Api/v2/Quote/";

        public RestfulQuoteRequest()
        {
        }

        public RestfulQuoteRequest(string symbol)
        {
            Symbol = symbol;
        }

        public string Symbol { get; set; }

        public string RequestUrl { get; private set; }

        public string QuoteResult { get; private set; }

        public async Task MakeRequest()
        {
            QuoteResult = await RunRequest();
        }

        public async Task MakeRequest(string symbol)
        {
            if (string.IsNullOrEmpty(Symbol))
                Symbol = symbol;

            QuoteResult = await RunRequest();
        }

        private async Task<string> RunRequest()
        {

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(BaseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            RequestUrl = string.Format("{0}jsonp?symbol={1}&callback=myFunction", BaseUrl, Symbol);

            HttpResponseMessage response = await client.GetAsync(RequestUrl);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

       
    }
}
