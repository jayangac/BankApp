using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Business.Interfaces
{
    public interface IAccountService
    {
        bool CreateAccount(string acc_name,string traType);
        int GetAccountInfo(string accName);
    }
}
