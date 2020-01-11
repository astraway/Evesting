using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Evesting
{
    public abstract class Processor
    {

        public virtual ValueInvestingCompanyDBModel WebClientAPICall(ValueInvestingCompanyDBModel company)
        {
            return company;
        }

        public virtual async Task<ValueInvestingCompanyDBModel> WebClientAPICallAsync(ValueInvestingCompanyDBModel company)
        {
            return company;
        }

        
        public virtual void SqlRead()
        {

        }

        public virtual void SqlWrite()
        {

        }

    }
}
