using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Evesting
{
    class BookValueProcessor : Processor
    {
        public override ValueInvestingCompanyDBModel WebClientAPICall(ValueInvestingCompanyDBModel company)
        {

            string Json = "";
            WebClient client = new WebClient();

            Json = client.DownloadString($"https://financialmodelingprep.com/api/v3/financials/cash-flow-statement/{ company.STOCK_TICKER}");

            //StockPriceModel Sp_json = JsonConvert.DeserializeObject<StockPriceModel>(Json);

            var OCJson = Top_Level.FromJson(Json);

            Console.WriteLine("Writting to Book Value DB");
            foreach (var item in OCJson.Financials)
            {
                Dividends_DB_Model dividends = new Dividends_DB_Model { DATE = (item.Date), DIVIDENDS = (item.DividendPayments), STOCK_TICKER = company.STOCK_TICKER };
                SQL.WriteDividendsData(dividends);


            }

            //next I need to implement book value and add these two together







            Random rnd = new Random();
            company.DIVIDENDS = Convert.ToDouble(rnd.Next());

            return company;
        }
        

    }
}


// could take in a CompanyDBModel and return an object of a given type, maybe an object that has the calulations done in each processor , so for these ones its 10% gain over each year, it would return an object that has all of the 
// props for the fields that the invested book says i need, and that object can be passed from processor to processor. then inorder to add a new processor I would just add a new prop in the model and the new processor. 