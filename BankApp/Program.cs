using BankApp.Business.Implementations;
using BankApp.Business.Interfaces;
using BankApp.Core.Common;
using BankApp.Data.BaseOperation;
using BankApp.Repository.Implementations;
using BankApp.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Globalization;



string ch;
using IHost host = Host.CreateDefaultBuilder(args).ConfigureServices(servies =>
{
    servies.AddSingleton<IAccountService, AccountService>();
    servies.AddSingleton<IAccountRepository, AccountRepository>();
    servies.AddSingleton<ITransactionRepository, TransactionRepository>();
    servies.AddSingleton<ITransactionService, TransactionService>();
    servies.AddSingleton<IRuleService, RuleService>();
    servies.AddSingleton<IRuleRepository, RuleRepository>();
    servies.AddSingleton<ValidationService>();
    servies.AddSingleton<BankDbContext>();
}).Build();

var accService = host.Services.GetService<IAccountService>();
var valiService = host.Services.GetService<ValidationService>();
var txnService = host.Services.GetService<ITransactionService>();
var ruleService = host.Services.GetService<IRuleService>();

while (true)
{
    Console.WriteLine("-----------------------------------------------------------------------------");
    Console.WriteLine("Welcome to Awesome GIC Bank! What would you like to do?");
    Console.WriteLine("[I]nput transaction\n[D]efine interest rules\n[P]rint statement\n[Q]uit");
    Console.WriteLine("-----------------------------------------------------------------------------");
    ch = Console.ReadLine();

    switch (ch.ToUpper())
    {
        case "I":
            Console.WriteLine("Please enter transaction details in <Date>|<Account>|<Type>|<Amount> format\n");
            var input = Console.ReadLine();
            var accData = input.Split("|");

            if (accData.Length != 4)
            {
                Console.WriteLine("\nInvalid Input\n");
            }
            else
            {
                string traDate = accData[0];

                #region Initial Validation

                DateTime finalTraDate;
                if (!DateTime.TryParseExact(traDate, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out finalTraDate))
                {
                    Console.WriteLine("\nInvalid date entered.\n");
                    continue;
                };

                var accName = accData[1];
                if (accName == null)
                {
                    Console.WriteLine("\nInvalid account entered.\n");
                    continue;
                }
                var traType = accData[2].ToUpper();

                if (traType != "W" && traType != "D")
                {
                    Console.WriteLine("\nInvalid transaction type entered.\n");
                    continue;
                }

                var accAmount = Convert.ToDecimal(accData[3]);
                if (accAmount <= 0)
                {
                    Console.WriteLine("\nInvalid amount entered.\n");
                    continue;
                }
                #endregion

                var isCreateAccount = accService.CreateAccount(accName, traType);
                if (isCreateAccount)
                {
                    int accountId = accService.GetAccountInfo(accName);
                    txnService.DoTransaction(accName, accountId, finalTraDate, traType, accAmount);
                    var trasInfo = txnService.GetTransactionByAccount(accountId);

                    //print transaction
                    if (trasInfo.Count > 0)
                    {
                        Console.WriteLine($"{accName}\nDate    | Txn Id      | Type  | Amount  ");
                        foreach (var transaction in trasInfo)
                        {
                            Console.WriteLine($"{transaction.TransactDate.ToString("yyyyMMdd")}| {transaction.TxnId.PadLeft(10)} | {transaction.TransactType}     | {transaction.Amount.ToString("F2")}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Account creation not successful..\n");
                }
            }

            break;

        case "D":

            Console.Write("Please enter interest rules details in <Date>|<RuleId>|<Rate in %> format\n");
            var inputRule = Console.ReadLine();
            var ruleData = inputRule.Split("|");

            if (ruleData.Length != 3)
            {
                Console.WriteLine("Invalid Input");
            }
            else
            {
                #region Initial Validation

                string ruleDate = ruleData[0];
                DateTime finalRuleDate;
                if (!DateTime.TryParseExact(ruleDate, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out finalRuleDate))
                {
                    Console.WriteLine("\nInvalid date entered.\n");
                    continue;
                };

                var ruleId = ruleData[1];

                var ruleRate = ruleData[2];
                if (Convert.ToDecimal(ruleRate) < 0 || Convert.ToDecimal(ruleRate) > 100)
                {
                    Console.WriteLine("\nInvalid Interest rate entered.\n");
                    continue;
                }

                #endregion

                ruleService.CreateRule(finalRuleDate, ruleId, ruleRate);

                //print rules
                var rules = ruleService.GetAllRules();
                Console.WriteLine("Interest rules:\nDate     | RuleId | Rate (%) ");
                foreach (var rule in rules)
                {
                    Console.WriteLine($"{rule.RuleDate.ToString("yyyyMMdd")} | {rule.RuleName} | {rule.Rate.ToString("F2")}");
                }
            }
            break;

        case "P":

            Console.Write("Please enter account and month to generate the statement <Account>|<Month>\n");
            var inputPrint = Console.ReadLine();
            var printData = inputPrint.Split("|");

            if (printData.Length != 2)
            {
                Console.WriteLine("Invalid Input..\n");
            }
            else
            {
                var statements = txnService.PrintStatement(printData[0], Convert.ToInt32(printData[1]));
                if (statements.Count > 0)
                {
                    Console.WriteLine($"Account: {printData[0]}\nDate     | Txn Id       |  Type | Amount   | Balance");
                    foreach (var statement in statements)
                    {
                        Console.WriteLine($"{statement.TransactDate.ToString("yyyyMMdd")} | {statement.TxnId.PadLeft(12)} | {statement.TransactType.PadLeft(5)} | {statement.Amount.ToString().PadLeft(8)} | {statement.Balance}");
                    }
                }
            }
            break;

        case "Q":

            Console.WriteLine("Thank you for banking with Awesome GIC Bank\n");
            Console.WriteLine("Have a nice day!");
            Environment.Exit(0);
            break;

        default:
            NextMenu();
            break;
    }
}


string NextMenu()
{
    Console.WriteLine("-----------------------------------------------------------------------------");
    Console.WriteLine("Welcome to AwesomeGIC Bank! What would you like to do?");
    Console.WriteLine("[I]nput transaction\n[D]efine interest rules\n[P]rint statement\n[Q]uit");
    Console.WriteLine("-----------------------------------------------------------------------------");
    return Console.ReadLine();
}
