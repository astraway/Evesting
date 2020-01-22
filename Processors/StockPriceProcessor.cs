using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Evesting
{
    class StockPriceProcessor : Processor
    {
         //making a webclient call syncly
        public override ValueInvestingCompanyDBModel WebClientAPICall(ValueInvestingCompanyDBModel company)
        {

            string Json = "";
            WebClient client = new WebClient();

            Json = client.DownloadString($"https://financialmodelingprep.com/api/v3/stock/real-time-price/{ company.STOCK_TICKER}");

            StockPriceModel Sp_json = JsonConvert.DeserializeObject<StockPriceModel>(Json);
            
            
            //writting to stock price db
            Current_StockPrice_DB_Model StockPrice = new Current_StockPrice_DB_Model { STOCK_PRICE = (Convert.ToDouble(Sp_json.Price)), STOCK_TICKER = company.STOCK_TICKER };
            SQL.WriteCurrentStockPriceData(StockPrice);

            //assigning stock price to company object
            company.STOCK_PRICE = StockPrice.STOCK_PRICE;

            return company;
        }




        //making a call async 
        public override async Task<ValueInvestingCompanyDBModel> WebClientAPICallAsync(ValueInvestingCompanyDBModel company)
        {
            string url = $"https://financialmodelingprep.com/api/v3/stock/real-time-price/{ company.STOCK_TICKER}";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    StockPriceModel result = await response.Content.ReadAsAsync<StockPriceModel>();

                    company.STOCK_PRICE = result.Price;

                    Console.WriteLine("Processing StockPriceProcessor.WebClientAPICallAsync");
                    return company;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        



    }
}
