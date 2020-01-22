using System;
using System.Collections.Generic;
using System.Text;

namespace Evesting
{
    public abstract class Value_Investing_Mode_Repository
    {


        public virtual void WriteDate(ValueInvestingCompanyDBModel company)
        {

        }

        public virtual List<ValueInvestingCompanyDBModel> ReadData()
        {
            List<ValueInvestingCompanyDBModel> company = new List<ValueInvestingCompanyDBModel>();
            return company;
        }

    }
}
