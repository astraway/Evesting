using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Evesting
{
    class StockPriceProcessor
    {
         //making a webclient call syncly
        public static string WebClientAPICall()
        {

            string Json = "";
            WebClient client = new WebClient();

            Json = client.DownloadString("https://financialmodelingprep.com/api/v3/stock/real-time-price/AAPL");
            
      

            return Json;
        }

        //making a call async 
        private static async Task<StockPriceModel> WebClientAPICallAsync()
        {
            string url = "https://financialmodelingprep.com/api/v3/stock/real-time-price/GOOGL";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    StockPriceModel result = await response.Content.ReadAsAsync<StockPriceModel>();


                    
                    Console.WriteLine("writting to Current Financials db in WebClientAPICallAsync");
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        //making a call async , call this to call the above. 
        public static async void LoadStockPriceDataAsync()
        {

            var result = await StockPriceProcessor.WebClientAPICallAsync();
            Console.WriteLine("Stock price Async test");
            Console.WriteLine(result.Price);

        }


    }
}
