using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Newtonsoft.Json.Converters;
using System.Threading.Tasks;
using System.Net.Http;

namespace Evesting
{
    class OperatingCashProcessor : Processor
    {
        //making a webclient call syncly
        public override ValueInvestingCompanyDBModel WebClientAPICall(ValueInvestingCompanyDBModel company)
        {

            string Json = "";
            WebClient client = new WebClient();

            Json = client.DownloadString($"https://financialmodelingprep.com/api/v3/financials/cash-flow-statement/{ company.STOCK_TICKER}");

            //StockPriceModel Sp_json = JsonConvert.DeserializeObject<StockPriceModel>(Json);

            var OCJson = Top_Level.FromJson(Json);

            Console.WriteLine("Writting to Operating_Cash DB");
            foreach (var item in OCJson.Financials)
            {
                Operating_Cash_DB_Model OperatingCash = new Operating_Cash_DB_Model { DATE = (item.Date), OPERATING_CASH_FLOW = (item.OperatingCashFlow), STOCK_TICKER = company.STOCK_TICKER };
                SQL.WriteOperatingCashData(OperatingCash);
            }


            //Operating_Cash_DB_Model OperatingCash = new Operating_Cash_DB_Model { DATE = (OCJson.Symbol[0]), OPERATING_CASH_FLOW = OCJson.  , STOCK_TICKER = company.STOCK_TICKER };

            //SQL.WriteCurrentFinancialsData(StockPrice);




            Random rnd = new Random();
            company.OPERATING_CASH = Convert.ToDouble(rnd.Next());

            return company;


        }

        //making a call async 
        public override async Task<ValueInvestingCompanyDBModel> WebClientAPICallAsync(ValueInvestingCompanyDBModel company)
        {
            string url = $"https://financialmodelingprep.com/api/v3/financials/cash-flow-statement/{ company.STOCK_TICKER}";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    Top_Level result = await response.Content.ReadAsAsync<Top_Level>();

                    // going to need to implement a loop and put all of the yearly values into some colleciton, then find if it is 10% growth and return that percentage.

                    // in the mean time I will return a random number
                    Random rnd = new Random();
                    company.OPERATING_CASH = Convert.ToDouble(rnd.Next());


                    Console.WriteLine("Processing OperatingCash.WebClientAPICallAsync");
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
