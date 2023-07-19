using BankApp.Data.BaseOperation;
using BankApp.Data.EntityModels;
using BankApp.Repository.Interfaces;

namespace BankApp.Repository.Implementations
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BankDbContext _context;

        public AccountRepository(BankDbContext context)
        {
            _context = context;
        }

        public bool CheckAccountExist(string accName)
        {
            try
            {

                return _context.Accounts.Any(x => x.AccountName == accName);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool CreateAccount(string acc_name, string traType)
        {
            try
            {
                var checkAccount = CheckAccountExist(acc_name);
                if (!checkAccount && traType == "D")
                {
                    var account = new Account
                    {
                        AccountName = acc_name,
                        CreatedDate = DateTime.Today
                    };
                    _context.Accounts.Add(account);
                    checkAccount = _context.SaveChanges() > 0;
                }
                return checkAccount;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int GetAccountInfo(string accName)
        {
            try
            {
                return _context.Accounts.Select(x => new { x.Id, x.AccountName }).FirstOrDefault(x => x.AccountName == accName).Id;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public DateTime GetCurrentMonthFirstDate(int monthNum)
        {
            var today = DateTime.Today;
            return new DateTime(today.Year, monthNum, 1);
        }

        public DateTime GetCurrentMonthLastDate(int monthNum)
        {
            var today = DateTime.Today;
            var startDate = new DateTime(today.Year, monthNum, 1);
            return startDate.AddMonths(1).AddDays(-1);
        }

    }
}
