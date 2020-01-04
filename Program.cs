using System;
using System.Data;
using System.Configuration;
using System.Data.SQLite;
using System.Text;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Evesting
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("What is the stock name (ticker) you are looking for?");
            string stockName = Console.ReadLine().ToUpper();
            Console.WriteLine("What is the the name of that company?");
            string companyName = Console.ReadLine().ToUpper();
            Console.WriteLine($"Starting Process for {stockName}" );

            //used to connect to api
            ApiHelper.InitializeClient();
            //creates new object of the above inputs
            CompanyDBModel newCo = new CompanyDBModel { CO_NAME = companyName, STOCK_TICKER = stockName };
            
            




            SQL.WriteCompanyData(newCo);
            

            


            //used to run the sync version of stock price api call
            static void StockPriceSyncTest(CompanyDBModel comodel) { 

                string SPResult = StockPriceProcessor.WebClientAPICall(comodel);
                
                StockPriceModel Sp_json = JsonConvert.DeserializeObject<StockPriceModel>(SPResult);

                Current_Financials_DB_Model StockPrice = new Current_Financials_DB_Model { STOCK_PRICE = (Convert.ToDouble(Sp_json.Price)), STOCK_TICKER = comodel.STOCK_TICKER}; 
                
                SQL.WriteCurrentFinancialsData(StockPrice);
                }

            StockPriceSyncTest(newCo);

            var CoResult = SQL.ReadCompanieData();
            var FinancialResult = SQL.ReadCurrentFinancialsData();


        }
    }



    



    class BookValue {
        public void display()
        {
            Console.WriteLine("Calculating Book value");
        }
    }

    class Sales {
        public void display()
        {
            Console.WriteLine("Calculating Sales");
        }
    }

    class OperatingCash {
        public void display()
        {
            Console.WriteLine("Calculating Operating Cash ");
        }
    }


    class ROI {
        public void display()
        {
            Console.WriteLine("Calculating ROI");
        }
    }

    class ROIC  {
        public void display()
        {
            Console.WriteLine("Calculating ROIC");
        }

    }


    class Debt {
        public void display()
        {
            Console.WriteLine("Calculating Debt");
        }

    }


}
