using System;
using System.Collections.Generic;
using System.Text;

namespace Evesting
{
    public abstract class Processor
    {

        public virtual ValueInvestingCompanyDBModel WebClientAPICall(ValueInvestingCompanyDBModel company)
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
