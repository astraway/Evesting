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
    class Value_Investing_Mode_Repository_SQL : Value_Investing_Mode_Repository
    {
        private static String LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }


        public override void WriteDate(ValueInvestingCompanyDBModel ViCoModel)
        {

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into Value_Investing_Companies ( STOCK_TICKER , CO_NAME , STOCK_PRICE  , DIVIDENDS , OPERATING_CASH, SHAREHOLDER_EQUITY, SALES) values ( @STOCK_TICKER , @CO_NAME , @STOCK_PRICE  , @DIVIDENDS , @OPERATING_CASH, @SHAREHOLDER_EQUITY, @SALES)", ViCoModel);
            }


            Console.WriteLine("Writting to Value_Investing_Companies DB");

        }



        public override List<ValueInvestingCompanyDBModel> ReadData()
        {
            Console.WriteLine("Reading from Value_Investing_Companies DB");

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                string sql = $"select * from Value_Investing_Companies  ";
                var output = cnn.Query<ValueInvestingCompanyDBModel>(sql, new DynamicParameters());
                foreach (var x in output)
                {
                    Console.WriteLine("ID: " + x.ID + " STOCK_TICKER: " + x.STOCK_TICKER + " CO_NAME: " + x.CO_NAME + " STOCK_PRICE: " + x.STOCK_PRICE +  " DIVIDENDS: " + x.DIVIDENDS + " OPERATING_CASH: " + x.OPERATING_CASH + " SHAREHOLDER_EQUITY: " + x.SHAREHOLDER_EQUITY + " SALES: " + x.SALES);
                }

                return output.ToList();

            }

        }


    }
}
