using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Newtonsoft.Json.Converters;
using System.Threading.Tasks;
using System.Net.Http;
using System.Linq;

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

            var OCJson = CashFlow_Top_Level.FromJson(Json);

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
                    Console.WriteLine("Processing OperatingCash.WebClientAPICallAsync");
                    CashFlow_Top_Level result = await response.Content.ReadAsAsync<CashFlow_Top_Level>();

                    //foreach (var item in result.Financials)
                    //{
                    //    Console.WriteLine("Operating cash : " + item.OperatingCashFlow);
                    //}

                    List<double> growth = new List<double>();

                    for (int i = 0; i < result.Financials.Length; i++)
                    {
                        if (i == 0)
                        {
                            //growth.Add(OCJson.Financials[i].OperatingCashFlow);

                        }
                        else
                        {
                            double change = CalculateChange(result.Financials[i - 1].OperatingCashFlow, result.Financials[i].OperatingCashFlow);
                            growth.Add(change);
                            //Console.WriteLine("growth for " + i);
                            //Console.WriteLine(change);
                        }

                    }

                    double CalculateChange(double previous, double current)
                    {
                        if (previous == 0)
                            throw new InvalidOperationException();

                        var change = current - previous;
                        return (double)change / previous;
                    }

                    Console.WriteLine("Operating Cash growth for " + result.Financials.Length.ToString() + " years  is : " + growth.Average());
                    

                    company.OPERATING_CASH = growth.Average();

                    
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
