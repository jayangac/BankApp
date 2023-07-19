using BankApp.Business.Interfaces;
using BankApp.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Business.Implementations
{
    public class AccountService: IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public bool CreateAccount(string acc_name,string traType)
        {
            return _accountRepository.CreateAccount(acc_name, traType);
        }

        public int GetAccountInfo(string accName)
        {
            return _accountRepository.GetAccountInfo(accName);
        }
    }
}
