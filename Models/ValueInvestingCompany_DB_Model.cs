using System;
using System.Collections.Generic;
using System.Text;

namespace Evesting
{   //model for the Companies DB
    public class ValueInvestingCompanyDBModel
    {
        public int ID { get; set; }
        public string STOCK_TICKER { get; set; }

        public string CO_NAME { get; set; }

        public double STOCK_PRICE { get; set; }


        public double DIVIDENDS { get; set; }

        public double OPERATING_CASH { get; set; }

        public double NET_INCOME { get; set; }

        public double SHAREHOLDER_EQUITY { get; set; }

        public double SALES { get; set; }




    }
}
