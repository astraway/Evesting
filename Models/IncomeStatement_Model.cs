using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Evesting
{

    public class Net_Income_Rootobject
    {
        public string symbol { get; set; }
        public Net_Income_Financial[] financials { get; set; }
    }

    public class Net_Income_Financial
    {
        public string date { get; set; }
        public string Revenue { get; set; }
        public string RevenueGrowth { get; set; }
        public string CostofRevenue { get; set; }
        public string GrossProfit { get; set; }
        public string RDExpenses { get; set; }
        public string SGAExpense { get; set; }
        public string OperatingExpenses { get; set; }
        public string OperatingIncome { get; set; }
        public string InterestExpense { get; set; }
        public string EarningsbeforeTax { get; set; }
        public string IncomeTaxExpense { get; set; }
        public string NetIncomeNonControllingint { get; set; }
        public string NetIncomeDiscontinuedops { get; set; }

        [JsonProperty(PropertyName = "Net Income")]
        public double NetIncome { get; set; }
        public string PreferredDividends { get; set; }
        public string NetIncomeCom { get; set; }
        public string EPS { get; set; }
        public string EPSDiluted { get; set; }
        public string WeightedAverageShsOut { get; set; }
        public string WeightedAverageShsOutDil { get; set; }
        public string DividendperShare { get; set; }
        public string GrossMargin { get; set; }
        public string EBITDAMargin { get; set; }
        public string EBITMargin { get; set; }
        public string ProfitMargin { get; set; }
        public string FreeCashFlowmargin { get; set; }
        public string EBITDA { get; set; }
        public string EBIT { get; set; }
        public string ConsolidatedIncome { get; set; }
        public string EarningsBeforeTaxMargin { get; set; }
        public string NetProfitMargin { get; set; }
    }

}
