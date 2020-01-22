using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Evesting
{

    public class Balance_Sheet_Rootobject
    {
        public string symbol { get; set; }
        public Balance_Sheet_Financials[] financials { get; set; }
    }

    public class Balance_Sheet_Financials
    {
        public string date { get; set; }
        public string Cashandcashequivalents { get; set; }
        public string Shortterminvestments { get; set; }
        public string Cashandshortterminvestments { get; set; }
        public string Receivables { get; set; }
        public string Inventories { get; set; }
        public string Totalcurrentassets { get; set; }
        public string PropertyPlantEquipmentNet { get; set; }
        public string GoodwillandIntangibleAssets { get; set; }
        public string Longterminvestments { get; set; }
        public string Taxassets { get; set; }
        public string Totalnoncurrentassets { get; set; }
        public string Totalassets { get; set; }
        public string Payables { get; set; }
        public string Shorttermdebt { get; set; }
        public string Totalcurrentliabilities { get; set; }
        public string Longtermdebt { get; set; }
        public string Totaldebt { get; set; }
        public string Deferredrevenue { get; set; }
        public string TaxLiabilities { get; set; }
        public string DepositLiabilities { get; set; }
        public string Totalnoncurrentliabilities { get; set; }
        public string Totalliabilities { get; set; }
        public string Othercomprehensiveincome { get; set; }
        public string Retainedearningsdeficit { get; set; }


        [JsonProperty(PropertyName = "Total shareholders equity")]
        public double Totalshareholdersequity { get; set; }

        public string Investments { get; set; }
        public string NetDebt { get; set; }
        public string OtherAssets { get; set; }
        public string OtherLiabilities { get; set; }
    }

}
