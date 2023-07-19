using BankApp.Data.EntityModels;
using BankApp.Model.Models;

namespace BankApp.Repository.Interfaces
{
    public interface ITransactionRepository
    {
        decimal CalculateAccountBalance(string accName);
        bool Deposit(string accName,int accId, DateTime traDate,string traType,decimal amount);
        bool Withdraw(string accName, int accId, DateTime traDate, string traType, decimal amount);
        string UniqueNumGeneration(string accName,DateTime traDate);
        List<Transaction> GetTransactionByAccount(int accountId);
        List<InterestDto> PrintStatement(string accName,int month);
        decimal CalculateAccountBalanceForGivenDate(string accName, DateTime lastMonthLastDate);
        InterestDto CalculateInterest(string accName, int month, List<InterestDto> trasactions);
    }
}
