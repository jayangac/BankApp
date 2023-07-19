using BankApp.Business.Interfaces;
using BankApp.Data.EntityModels;
using BankApp.Model.Models;
using BankApp.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Business.Implementations
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountService _accountService;

        public TransactionService(ITransactionRepository transactionRepository, IAccountService accountService)
        {
            _transactionRepository = transactionRepository;
            _accountService = accountService;
        }

        public decimal CalculateAccountBalance(string accName)
        {
            return _transactionRepository.CalculateAccountBalance(accName);
        }

        public void Deposit(string accName, int accId, DateTime traDate, string traType, decimal amount)
        {
            _transactionRepository.Deposit(accName, accId, traDate, traType, amount);
        }

        public void Withdraw(string accName, int accId, DateTime traDate, string traType, decimal amount)
        {
            _transactionRepository.Withdraw(accName, accId, traDate, traType, amount);
        }

        public void DoTransaction(string accName, int accId, DateTime traDate, string traType, decimal amount)
        {
            // balance check ==0, can't withdrawal, only deposit
            var balance = CalculateAccountBalance(accName);

            if (balance == 0)
            {
                if (traType == "D")
                    Deposit(accName, accId, traDate, traType, amount);
                else if (traType == "W")
                    Console.WriteLine("Can't withdraw insufficient balance..");
            }

            // balance >0 && balance <amount cann't withdraw
            if (balance > 0 && balance < amount)
            {
                if (traType == "D")
                    Deposit(accName, accId, traDate, traType, amount);
                else if (traType == "W")
                    Console.WriteLine("Can't withdraw insufficient balance..");

            }

            //can do both
            else if (balance > 0 && balance > amount && (traType == "D" || traType == "W"))
            {
                if (traType == "D")
                    Deposit(accName, accId, traDate, traType, amount);
                if (traType == "W")
                    Withdraw(accName, accId, traDate, traType, amount);
            }
        }

        public List<Transaction> GetTransactionByAccount(int accountId)
        {
            return _transactionRepository.GetTransactionByAccount(accountId);
        }

        public List<InterestDto> PrintStatement(string accName, int month)
        {
            return _transactionRepository.PrintStatement(accName, month);
        }
    }
}
