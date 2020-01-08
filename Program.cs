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
            StockPriceProcessor.WebClientAPICall(newCo);
            OperatingCashProcessor.WebClientAPICall(newCo);
            BookValueProcessor.WebClientAPICall(newCo);

            var CoResult = SQL.ReadCompanyData();
            var FinancialResult = SQL.ReadCurrentFinancialsData();
            var OperatingCash = SQL.ReadOperatingCashData();
            var BookValue_Dividends = SQL.ReadDividendsData();

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
