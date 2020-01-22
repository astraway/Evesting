using System;
using System.Collections.Generic;
using System.Text;

namespace Evesting
{
    class NullProcessor : Processor
    {

        public NullProcessor()
        {
            Console.WriteLine("null processor");
        }

        public override ValueInvestingCompanyDBModel WebClientAPICall(ValueInvestingCompanyDBModel company)
        {



            Random rnd = new Random();
            company.SALES = Convert.ToDouble(rnd.Next());

            return company;
        }

    }
}
