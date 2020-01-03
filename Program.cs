using System;
using System.Data;
using System.Configuration;
using System.Data.SQLite;

using Dapper;
using System.Collections.Generic;
using System.Linq;

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

            FinModel newCo = new FinModel { CO_NAME = companyName, STOCK_TICKER = stockName };

            
            SQL.ConnectToSQL();

            SQL.WriteData(newCo);


            var NetIncome = new NetIncome();
            NetIncome.display(stockName);

            var result = SQL.ReadData();


            
            



            SQL.CloseConnection();

        }
    }

    class SQL {
        public static object FiddleHelper { get; private set; }

        private static String LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
        
        public static void ConnectToSQL() {
            // since im using using statements i dont think i need this function
            Console.WriteLine("Connecting to DataBase");
        }

        public static void WriteData(FinModel finmodel) {
            // auto closes conneciton at last curly
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into Companies ( STOCK_TICKER, CO_NAME) values ( @STOCK_TICKER, @CO_NAME)", finmodel);
            }


            Console.WriteLine("Writting to DB");

        }



        public static List<FinModel> ReadData() {
            Console.WriteLine("Reading DB");
            
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                string sql = "select * from Companies";
                var output = cnn.Query<FinModel>(sql, new DynamicParameters());
                foreach (var x in output)
                {
                    Console.WriteLine("ID: " + x.ID + " STOCK_TICKER: " + x.STOCK_TICKER + " CO_NAME: " + x.CO_NAME );
                }

                return output.ToList();
                
            }
            
        }

        public static void CloseConnection()
        {

            Console.WriteLine("Disconecting from DB");
        }

    }



    class NetIncome {
        public void display(string ticker)
        {
            Console.WriteLine($"Calculating Net Income for {ticker}");
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
