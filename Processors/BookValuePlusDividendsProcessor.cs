using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Evesting
{
    class BookValue__Plus_Dividends_Processor : Processor
    {
        public override ValueInvestingCompanyDBModel WebClientAPICall(ValueInvestingCompanyDBModel company)
        {

            string Json = "";
            WebClient client = new WebClient();

            Json = client.DownloadString($"https://financialmodelingprep.com/api/v3/financials/cash-flow-statement/{ company.STOCK_TICKER}");

            //StockPriceModel Sp_json = JsonConvert.DeserializeObject<StockPriceModel>(Json);

            var OCJson = CashFlow_Top_Level.FromJson(Json);

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



        //making a call async 
        public override async Task<ValueInvestingCompanyDBModel> WebClientAPICallAsync(ValueInvestingCompanyDBModel company)
        {
            string url = $"https://financialmodelingprep.com/api/v3/financials/cash-flow-statement/{ company.STOCK_TICKER}";
            
            
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Processing Dividends for BookValue__Plus_Dividends_Processor.WebClientAPICallAsync");
                    CashFlow_Top_Level result = await response.Content.ReadAsAsync<CashFlow_Top_Level>();

                    //foreach (var item in result.Financials)
                    //{
                    //    Console.WriteLine(item.DividendPayments);
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
                            double change = CalculateChange(result.Financials[i - 1].DividendPayments, result.Financials[i].DividendPayments);
                            growth.Add(change);
                            Console.WriteLine("dividend growth for " + i);
                            Console.WriteLine(change);
                        }

                    }

                    double CalculateChange(double previous, double current)
                    {
                        if (previous == 0)
                            previous = .001;

                        var change = current - previous;
                        return (double)change / previous;
                    }

                    Console.WriteLine("Dividends growth for " + result.Financials.Length.ToString() + " years  is : " + growth.Average());


                    company.DIVIDENDS = growth.Average();


                    
                    
                    // going to need to implement a loop and put all of the yearly values into some colleciton, then find if it is 10% growth and return that percentage.

                    // in the mean time I will return a random number
                    //Random rnd = new Random();
                    //company.DIVIDENDS = Convert.ToDouble(rnd.Next());


                    
                    
                }
                else
                {
                    Console.WriteLine("Somethign went wrong");
                    throw new Exception(response.ReasonPhrase);
                }
            }
            
            string shareholderequityURL = $"https://financialmodelingprep.com/api/v3/financials/balance-sheet-statement/{ company.STOCK_TICKER}";
            
            
                using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(shareholderequityURL))
                {
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Processing Total Share holders Equity for BookValue__Plus_Dividends_Processor.WebClientAPICallAsync");
                    Balance_Sheet_Rootobject BSresult =  await response.Content.ReadAsAsync<Balance_Sheet_Rootobject>();

                    //foreach (var item in BSresult.financials)
                    //{
                    //    Console.WriteLine("Total Share holders Equity: " + item.Totalshareholdersequity);
                    //}
                    List<double> shegrowth = new List<double>();

                    for (int i = 0; i < BSresult.financials.Length; i++)
                    {
                        if (i == 0)
                        {
                            //growth.Add(OCJson.Financials[i].OperatingCashFlow);

                        }
                        else
                        {
                            double change = CalculateChange(BSresult.financials[i - 1].Totalshareholdersequity, BSresult.financials[i].Totalshareholdersequity);
                            shegrowth.Add(change);
                            //Console.WriteLine("Net Income growth for " + i);
                            //Console.WriteLine(change);
                        }

                    }

                    double CalculateChange(double previous, double current)
                    {
                        if (previous == 0)
                            previous = .001;

                        var change = current - previous;
                        return (double)change / previous;
                    }

                    Console.WriteLine("Shareholder Equity growth for " + BSresult.financials.Length.ToString() + " years  is : " + shegrowth.Average());


                    company.SHAREHOLDER_EQUITY = shegrowth.Average();




                    // going to need to implement a loop and put all of the yearly values into some colleciton, then find if it is 10% growth and return that percentage.

                    // in the mean time I will return a random number
                    //random rnd = new random();
                    //company.book_value = convert.todouble(rnd.next());


                    
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


// could take in a CompanyDBModel and return an object of a given type, maybe an object that has the calulations done in each processor , so for these ones its 10% gain over each year, it would return an object that has all of the 
// props for the fields that the invested book says i need, and that object can be passed from processor to processor. then inorder to add a new processor I would just add a new prop in the model and the new processor. 