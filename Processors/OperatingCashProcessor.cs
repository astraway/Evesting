using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Newtonsoft.Json.Converters;


namespace Evesting
{
    class OperatingCashProcessor
    {
        //making a webclient call syncly
        public static void WebClientAPICall(CompanyDBModel company)
        {

            string Json = "";
            WebClient client = new WebClient();

            Json = client.DownloadString($"https://financialmodelingprep.com/api/v3/financials/cash-flow-statement/{ company.STOCK_TICKER}");

            //StockPriceModel Sp_json = JsonConvert.DeserializeObject<StockPriceModel>(Json);

            var OCJson = Top_Level.FromJson(Json);

            foreach (var item in OCJson.Financials)
            {
                Operating_Cash_DB_Model OperatingCash = new Operating_Cash_DB_Model { DATE = (item.Date), OPERATING_CASH_FLOW = (item.OperatingCashFlow), STOCK_TICKER = company.STOCK_TICKER };
                SQL.WriteOperatingCashData(OperatingCash);
            }


            //Operating_Cash_DB_Model OperatingCash = new Operating_Cash_DB_Model { DATE = (OCJson.Symbol[0]), OPERATING_CASH_FLOW = OCJson.  , STOCK_TICKER = company.STOCK_TICKER };

            //SQL.WriteCurrentFinancialsData(StockPrice);


        }

    }
}
