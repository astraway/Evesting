

namespace Evesting
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;


    public partial class CashFlow_Top_Level
    {
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("financials")]
        public Balance_Sheet_Financial[] Financials { get; set; }
    }

    public partial class Balance_Sheet_Financial
    {
        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("Depreciation & Amortization")]
        public string DepreciationAmortization { get; set; }

        [JsonProperty("Stock-based compensation")]
        public string StockBasedCompensation { get; set; }

        [JsonProperty("Operating Cash Flow")]
        public double OperatingCashFlow { get; set; }

        [JsonProperty("Capital Expenditure")]
        public string CapitalExpenditure { get; set; }

        [JsonProperty("Acquisitions and disposals")]
        public string AcquisitionsAndDisposals { get; set; }

        [JsonProperty("Investment purchases and sales")]
        public string InvestmentPurchasesAndSales { get; set; }

        [JsonProperty("Investing Cash flow")]
        public string InvestingCashFlow { get; set; }

        [JsonProperty("Issuance (repayment) of debt")]
        public string IssuanceRepaymentOfDebt { get; set; }

        [JsonProperty("Issuance (buybacks) of shares")]
        public string IssuanceBuybacksOfShares { get; set; }

        [JsonProperty("Dividend payments")]
        public double DividendPayments { get; set; }

        [JsonProperty("Financing Cash Flow")]
        public string FinancingCashFlow { get; set; }

        [JsonProperty("Effect of forex changes on cash")]
        public string EffectOfForexChangesOnCash { get; set; }

        [JsonProperty("Net cash flow / Change in cash")]
        public string NetCashFlowChangeInCash { get; set; }

        [JsonProperty("Free Cash Flow")]
        public string FreeCashFlow { get; set; }

        [JsonProperty("Net Cash/Marketcap")]
        public string NetCashMarketcap { get; set; }
    }

    public partial class CashFlow_Top_Level
    {
        public static CashFlow_Top_Level FromJson(string json) => JsonConvert.DeserializeObject<CashFlow_Top_Level>(json, Evesting.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this CashFlow_Top_Level self) => JsonConvert.SerializeObject(self, Evesting.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
