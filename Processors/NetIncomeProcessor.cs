using Evesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace Evesting
{
    public class NetIncomeProcessor : Processor
    {

        public override async Task<ValueInvestingCompanyDBModel> WebClientAPICallAsync(ValueInvestingCompanyDBModel company)
        {
            string netincomeUrl = $"https://financialmodelingprep.com/api/v3/financials/income-statement/{ company.STOCK_TICKER}";
            Console.WriteLine("Processing NetIncome.WebClientAPICallAsync");

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(netincomeUrl))
            {
                if (response.IsSuccessStatusCode)
                {
                    Net_Income_Rootobject ISresult = await response.Content.ReadAsAsync<Net_Income_Rootobject>();

                    //foreach (var item in ISresult.financials)
                    //{
                    //    Console.WriteLine("Total Net Income: " + item.NetIncome);
                    //}

                    List<double> growth = new List<double>();

                    for (int i = 0; i < ISresult.financials.Length; i++)
                    {
                        if (i == 0)
                        {
                            //growth.Add(OCJson.Financials[i].OperatingCashFlow);

                        }
                        else
                        {
                            double change = CalculateChange(ISresult.financials[i - 1].NetIncome, ISresult.financials[i].NetIncome);
                            growth.Add(change);
                            //Console.WriteLine("Net Income growth for " + i);
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

                    Console.WriteLine("Net Income growth for " + ISresult.financials.Length.ToString() + " years  is : " + growth.Average());


                    company.NET_INCOME = growth.Average();


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