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
        static async Task  Main(string[] args)
        {



            
            Console.WriteLine("What is the stock name (ticker) you are looking for?");
            string stockName = Console.ReadLine().ToUpper();
            Console.WriteLine("What is the the name of that company?");
            string companyName = Console.ReadLine().ToUpper();
            Console.WriteLine($"Starting Process for {stockName}" );

            //used to connect to api
            ApiHelper.InitializeClient();
            //creates new object of the above inputs
            ValueInvestingCompanyDBModel newCo = new ValueInvestingCompanyDBModel { CO_NAME = companyName, STOCK_TICKER = stockName };


            ProcessFactory processorFactory = new ProcessFactory();

            Processor StockPrice = processorFactory.CreateInstance("StockPriceProcessor");
            var StockPriceTask = StockPrice.WebClientAPICallAsync(newCo);
            var stockprice = await StockPriceTask;



            Processor OperatingCash = processorFactory.CreateInstance("OperatingCashProcessor"); 
            var OperatingCashTask = OperatingCash.WebClientAPICallAsync(newCo);
            var operatingcash = await OperatingCashTask;


            Processor BookValue = processorFactory.CreateInstance("BookValue__Plus_Dividends_Processor");
            var BookValueTask = BookValue.WebClientAPICallAsync(newCo);
            var bookvalue = await BookValueTask;


            Processor NetIncome = processorFactory.CreateInstance("NetIncomeProcessor");
            var NetIncomeTask = NetIncome.WebClientAPICallAsync(newCo);
            var netincome = await NetIncomeTask;




            //switch this to using dependecny injection. per https://www.youtube.com/watch?v=qJmEI2LtXIY&t=2s
            Value_Investing_Mode_Repository valueInvestingModeRepository = new Value_Investing_Mode_Repository_SQL();
            valueInvestingModeRepository.WriteDate(newCo);
            valueInvestingModeRepository.ReadData();

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


    class ROE {
        public void display()
        {
            //return on equity found already calculate on some sites, found by  net income(income statement)/Equity(balance sheet)
            Console.WriteLine("Calculating ROI");
        }
    }

    class ROIC  {
        public void display()
        {
            //net income / (equity + Debt) 
            Console.WriteLine("Calculating ROIC");
        }

    }


    class Debt {
        public void display()
        {
            // debt 
            Console.WriteLine("Calculating Debt");
        }

    }


}
