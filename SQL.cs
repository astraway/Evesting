using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace Evesting
{
    class SQL
    {


        private static String LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }


        public static void WriteCompanyData(CompanyDBModel finmodel)
        {
            
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into Companies ( STOCK_TICKER, CO_NAME) values ( @STOCK_TICKER, @CO_NAME)", finmodel);
            }


            Console.WriteLine("Writting to DB");

        }



        public static List<CompanyDBModel> ReadCompanyData()
        {
            Console.WriteLine("Reading DB");

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                string sql = "select * from Companies";
                var output = cnn.Query<CompanyDBModel>(sql, new DynamicParameters());
                foreach (var x in output)
                {
                    Console.WriteLine("ID: " + x.ID + " STOCK_TICKER: " + x.STOCK_TICKER + " CO_NAME: " + x.CO_NAME);
                }

                return output.ToList();

            }

        }

        public static void WriteCurrentFinancialsData(Current_Financials_DB_Model StockPrice)
        {

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into Current_Financials (ID, STOCK_PRICE, STOCK_TICKER) values (@ID, @STOCK_PRICE, @STOCK_TICKER)", StockPrice);
            }


            Console.WriteLine("Writting to Current_Financials DB");

        }



        public static List<Current_Financials_DB_Model> ReadCurrentFinancialsData()
        {
            Console.WriteLine("Reading DB");

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                string sql = "select * from Current_Financials";
                var output = cnn.Query<Current_Financials_DB_Model>(sql, new DynamicParameters());
                foreach (var x in output)
                {
                    Console.WriteLine("ID: " + x.ID + " STOCK_TICKER " + x.STOCK_TICKER + " STOCK_PRICE: " + x.STOCK_PRICE);
                }

                return output.ToList();

            }

        }
    }
}
