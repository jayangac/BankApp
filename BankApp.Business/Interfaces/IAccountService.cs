namespace BankApp.Business.Interfaces
{
    public interface IAccountService
    {
        bool CreateAccount(string acc_name,string traType);
        int GetAccountInfo(string accName);
    }
}
