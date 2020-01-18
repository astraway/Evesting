using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks


//net profit or net earnings found on income statement, link below
// using async from tim corey video, not working yet. 
//namespace Evesting
//{
//    public class NetIncomeProcessor
//    {

//        public static async Task<NetIncomeModel> LoadNetIncomeAsync()
//        {
//            string url = "https://financialmodelingprep.com/api/v3/financials/income-statement/AAPL";

//            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
//            {
//                if (response.IsSuccessStatusCode)
//                {
//                    NetIncomeFinancialsModel result = await response.Content.ReadAsAsync<NetIncomeFinancialsModel>();





//                    return result.Financials;
//                }
//                else
//                {
//                    throw new Exception(response.ReasonPhrase);
//                }
//            }
//        }

//        public static async void LoadnetincomedataAsync()
//        {

//            var NetIncomeInfo = await NetIncomeProcessor.LoadNetIncomeAsync();
//            Console.WriteLine("Net Income Async test");
//            Console.WriteLine(NetIncomeInfo.Revenue);

//        }




//    }
//}