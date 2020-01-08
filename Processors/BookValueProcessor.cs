using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Evesting
{
    class BookValueProcessor
    {
        public static void WebClientAPICall(CompanyDBModel company)
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
        }


    }
}
