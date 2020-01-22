using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Evesting
{
    public class SalesProcessor : Processor
    {
        public override async Task<ValueInvestingCompanyDBModel> WebClientAPICallAsync(ValueInvestingCompanyDBModel company)
        {
            string salesUrl = $"https://financialmodelingprep.com/api/v3/financials/income-statement/{ company.STOCK_TICKER}";
            Console.WriteLine("Processing SalesProcessor.WebClientAPICallAsync");

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(salesUrl))
            {
                if (response.IsSuccessStatusCode)
                {
                    Income_Statement_Rootobject ISresult = await response.Content.ReadAsAsync<Income_Statement_Rootobject>();

                    //foreach (var item in ISresult.financials)
                    //{
                    //    Console.WriteLine("Total Net Income: " + item.NetIncome);
                    //}

                    List<double> growth = new List<double>();

                    for (int i = 0; i < ISresult.financials.Length; i++)
                    {
                        if (i == 0)
                        {
                           

                        }
                        else
                        {
                            double change = CalculateChange(ISresult.financials[i - 1].Revenue, ISresult.financials[i].Revenue);
                            growth.Add(change);
                            
                        }

                    }

                    double CalculateChange(double previous, double current)
                    {
                        if (previous == 0)
                            throw new InvalidOperationException();

                        var change = current - previous;
                        return (double)change / previous;
                    }

                    Console.WriteLine("Sales growth for " + ISresult.financials.Length.ToString() + " years  is : " + growth.Average());


                    company.SALES = growth.Average();


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
