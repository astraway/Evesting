using System;
using System.Collections.Generic;
using System.Text;
// model for net income, first level to create objec to parse the json to.
namespace Evesting
{
    public class NetIncomeFinancialsModel
    {
        public NetIncomeModel Financials { get; set; }
    }
}
