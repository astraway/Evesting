using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using Npgsql;

namespace Evesting
{
    class Value_Investing_Mode_Repository_Postgres : Value_Investing_Mode_Repository
    {
        private static String LoadConnectionString(string id = "postgres")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }


        public override void WriteDate(ValueInvestingCompanyDBModel ViCoModel)
        {

            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into public.value_investing_companies_net ( STOCK_TICKER , CO_NAME , STOCK_PRICE  , DIVIDENDS , OPERATING_CASH, SHAREHOLDER_EQUITY, SALES) values ( @STOCK_TICKER , @CO_NAME , @STOCK_PRICE  , @DIVIDENDS , @OPERATING_CASH, @SHAREHOLDER_EQUITY, @SALES)", ViCoModel);
            }


            Console.WriteLine("Writting to value_investing_companies_net DB");

        }



        public override List<ValueInvestingCompanyDBModel> ReadData()
        {
            Console.WriteLine("Reading from value_investing_companies_net DB");

            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                string sql = $"select * from value_investing_companies_net  ";
                var output = cnn.Query<ValueInvestingCompanyDBModel>(sql, new DynamicParameters());
                foreach (var x in output)
                {
                    Console.WriteLine("ID: " + x.ID + " STOCK_TICKER: " + x.STOCK_TICKER + " CO_NAME: " + x.CO_NAME + " STOCK_PRICE: " + x.STOCK_PRICE + " DIVIDENDS: " + x.DIVIDENDS + " OPERATING_CASH: " + x.OPERATING_CASH + " SHAREHOLDER_EQUITY: " + x.SHAREHOLDER_EQUITY + " SALES: " + x.SALES);
                }

                return output.ToList();

            }

        }


    }
}
