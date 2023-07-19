namespace BankApp.Repository.Interfaces
{
    public interface IAccountRepository
    {
        bool CreateAccount(string acc_name, string traType);
        int GetAccountInfo(string accName);
        bool CheckAccountExist(string accName);
        DateTime GetCurrentMonthFirstDate(int monthNum);
        DateTime GetCurrentMonthLastDate(int monthNum);
    }
}
