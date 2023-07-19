using BankApp.Data.BaseOperation;
using BankApp.Data.EntityModels;
using BankApp.Model.Models;
using BankApp.Repository.Interfaces;


namespace BankApp.Repository.Implementations
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly BankDbContext _context;
        private readonly IRuleRepository _ruleRepository;
        private readonly IAccountRepository _accountRepository;

        public TransactionRepository(BankDbContext context, IRuleRepository ruleRepository, IAccountRepository accountRepository)
        {
            _context = context;
            _ruleRepository = ruleRepository;
            _accountRepository = accountRepository;
        }

        public decimal CalculateAccountBalance(string accName)
        {
            var sumOfDeposit = _context.Transactions.Where(x => x.Account.AccountName == accName && x.TransactType == "D").Sum(x => x.Amount);
            var sumOfWithdrawal = _context.Transactions.Where(x => x.Account.AccountName == accName && x.TransactType == "W").Sum(x => x.Amount);
            var balance = (sumOfDeposit - sumOfWithdrawal);
            return Math.Round(balance, 2);
        }

        public bool Deposit(string accName, int accId, DateTime traDate, string traType, decimal amount)
        {
            try
            {
                var uniqueNum = UniqueNumGeneration(accName, traDate);
                var transaction = new Transaction
                {
                    AccountId = accId,
                    TransactDate = traDate,
                    TxnId = uniqueNum,
                    TransactType = traType,
                    Amount = Math.Round(amount, 2)
                };
                _context.Transactions.Add(transaction);
               
                return _context.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool Withdraw(string accName, int accId, DateTime traDate, string traType, decimal amount)
        {
            try
            {
                var result = false;
                var balance = CalculateAccountBalance(accName);
                if (balance == 0)
                {
                    Console.WriteLine("Insuffient balance\n");
                    result = false;
                }
                else if (balance < amount)
                {
                    Console.WriteLine("Insuffient balance\n");
                    result = false;
                }
                else
                {
                    var uniqueNum = UniqueNumGeneration(accName, traDate);
                    var transaction = new Transaction
                    {
                        AccountId = accId,
                        TransactDate = traDate,
                        TxnId = uniqueNum,
                        TransactType = traType,
                        Amount = Math.Round(amount, 2)
                    };
                    _context.Transactions.Add(transaction);
                    result = _context.SaveChanges() > 0;
                }
                return result;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public string UniqueNumGeneration(string accName, DateTime traDate)
        {
            try
            {
                var lastTransaction = _context.Transactions.Where(x => x.Account.AccountName == accName && x.TransactDate == traDate)
                    .Select(x => new { x.TxnId, x.AccountId, x.Id }).OrderByDescending(x => x.Id).FirstOrDefault();
                var uniqueNum = "";
                if (lastTransaction == null)
                {
                    uniqueNum = traDate.ToString("yyyyMMdd") + "-" + "01";
                }
                else
                {
                    var expectedLastDigit = (Convert.ToInt32(lastTransaction.TxnId.Split("-")[1]) + 1).ToString("D2");
                    uniqueNum = traDate.ToString("yyyyMMdd") + "-" + expectedLastDigit;
                }
                return uniqueNum;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Transaction> GetTransactionByAccount(int accountId)
        {
            try
            {
                return _context.Transactions.Where(x => x.AccountId == accountId).OrderBy(x => x.TransactDate).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public decimal CalculateAccountBalanceForGivenDate(string accName, DateTime date)
        {
            try
            {
                var sumOfDeposit = _context.Transactions.Where(x => x.Account.AccountName == accName && x.TransactDate <= date && x.TransactType == "D").Sum(x => x.Amount);
                var sumOfWithdrawal = _context.Transactions.Where(x => x.Account.AccountName == accName && x.TransactDate <= date && x.TransactType == "W").Sum(x => x.Amount);
                var balance = (sumOfDeposit - sumOfWithdrawal);
                return Math.Round(balance, 2);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<InterestDto> PrintStatement(string accName, int month)
        {
            try
            {
                var isStatementAvailable = _context.Transactions.Any(x => x.Account.AccountName == accName && x.TransactDate.Month == month);
                var statements = new List<InterestDto>();
                //calculate initial balance
                if (isStatementAvailable)
                {
                    var currentMonthTransaction = _context.Transactions.
                    Where(x => x.Account.AccountName == accName && x.TransactDate.Month == month).
                    OrderBy(x => x.TransactDate).ToList();

                    var lastMonthLastDate = _ruleRepository.GetLastMonthLastDate(month);
                    var lastMonthBalance = CalculateAccountBalanceForGivenDate(accName, lastMonthLastDate);

                    foreach (var transaction in currentMonthTransaction)
                    {
                        if (transaction.TransactType == "D")
                            lastMonthBalance = lastMonthBalance + transaction.Amount;
                        else if (transaction.TransactType == "W")
                            lastMonthBalance = lastMonthBalance - transaction.Amount;
                        var statment = new InterestDto
                        {
                            Id = transaction.Id,
                            TransactDate = transaction.TransactDate,
                            TxnId = transaction.TxnId,
                            TransactType = transaction.TransactType,
                            Amount = transaction.Amount,
                            Balance = lastMonthBalance
                        };
                        statements.Add(statment);
                    }
                    //get interest
                    var interest = CalculateInterest(accName, month, statements);
                    statements.Add(interest);
                }

                return statements;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public InterestDto CalculateInterest(string accName, int month, List<InterestDto> trasactions)
        {
            try
            {


                var allTranactions = new List<CalculateInterestDto>();
                var firstDateOfMonth = _accountRepository.GetCurrentMonthFirstDate(month);
                var lastBalanceOfMonth = trasactions.OrderByDescending(x => x.Id).FirstOrDefault().Balance;
                //calculate transaction per day
                foreach (var items in trasactions.GroupBy(x => x.TransactDate))
                {
                    var eodBalance = default(decimal);
                    var noOfDays = Convert.ToInt32((items.FirstOrDefault().TransactDate - firstDateOfMonth.Date).TotalDays + 1);
                    firstDateOfMonth = items.FirstOrDefault().TransactDate;
                    var traDate = items.FirstOrDefault().TransactDate;
                    var effectedRule = _ruleRepository.GetRuleAccordingDate(traDate);
                    foreach (var item in items)
                    {
                        if (item.TransactType == "D")
                            eodBalance = eodBalance + item.Amount;
                        if (item.TransactType == "W")
                            eodBalance = eodBalance - item.Amount;
                    }
                    var eodTranaction = new CalculateInterestDto()
                    {
                        EODBalance = eodBalance,
                        NumberOfDays = noOfDays,
                        TransactDate = items.FirstOrDefault().TransactDate,
                        AnnualizedInterest = eodBalance * effectedRule.Rate * noOfDays
                    };
                    allTranactions.Add(eodTranaction);
                }
                //calculate interest
                var AnnualizedInterest = allTranactions.Sum(x => x.AnnualizedInterest) / 365;
                var interest = new InterestDto
                {
                    TransactDate = _accountRepository.GetCurrentMonthLastDate(month),
                    TxnId = "",
                    TransactType = "I",
                    Amount = Math.Round(AnnualizedInterest, 2),
                    Balance = Math.Round(AnnualizedInterest, 2) + lastBalanceOfMonth,
                };
                return interest;

            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
