using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Core.Common
{
    public class ValidationService
    {
        public bool AmountValidity(decimal amount)
        {
            return  amount > 0;
        }

        //public 
    }
}
