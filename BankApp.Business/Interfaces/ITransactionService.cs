using BankApp.Data.EntityModels;
using BankApp.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Business.Interfaces
{
    public interface ITransactionService
    {
        decimal CalculateAccountBalance(string accName);
        void Deposit(string accName, int accId, DateTime traDate, string traType, decimal amount);
        void Withdraw(string accName, int accId, DateTime traDate, string traType, decimal amount);
        //string UniqueNumGeneration(string accName, DateTime traDate);
        void DoTransaction(string accName, int accId, DateTime traDate, string traType, decimal amount);
        List<Transaction> GetTransactionByAccount(int accountId);
        List<InterestDto> PrintStatement(string accName, int month);
    }
}
