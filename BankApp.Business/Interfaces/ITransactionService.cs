using BankApp.Data.EntityModels;
using BankApp.Model.Models;

namespace BankApp.Business.Interfaces
{
    public interface ITransactionService
    {
        decimal CalculateAccountBalance(string accName);
        void Deposit(string accName, int accId, DateTime traDate, string traType, decimal amount);
        void Withdraw(string accName, int accId, DateTime traDate, string traType, decimal amount);
        void DoTransaction(string accName, int accId, DateTime traDate, string traType, decimal amount);
        List<Transaction> GetTransactionByAccount(int accountId);
        List<InterestDto> PrintStatement(string accName, int month);
    }
}
