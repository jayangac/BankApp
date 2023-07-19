using BankApp.Data.BaseOperation;
using BankApp.Repository.Implementations;
using BankApp.Repository.Interfaces;

namespace BankApp.Test
{
    [TestFixture]
    public class TransactionRepositoryTest
    {
        private BankDbContext _dbContext { get; set; }
        private TransactionRepository _transaction;
        private IRuleRepository ruleRepository { get; set; }
        private IAccountRepository accountRepository { get; set; }

        [SetUp]
        public void SetUp()
        {
            // initialize here
            _dbContext = new BankDbContext();
            _transaction = new TransactionRepository(_dbContext, ruleRepository, accountRepository);
        }

        [Test]
        public void Deposit_Deposit_To_Account_With_Correct_Parameter()
        {
            // Arrange
            string accName = "UOB"; int accId = 6; DateTime traDate = Convert.ToDateTime("2023-05-01"); string traType = "D"; decimal amount = 600;

            // Act
            var reponse = _transaction.Deposit(accName, accId, traDate, traType, amount);

            // Assert
            Assert.That(true, Is.EqualTo(reponse));

        }

        [Test]
        public void Deposit_Deposit_To_Account_With_InCorrect_Parameter()
        {
            // Arrange
            string accName = "UOB"; int accId = 2; DateTime traDate = Convert.ToDateTime("2023-05-01"); string traType = "D"; decimal amount = 600;

            // Act
            var reponse = _transaction.Deposit(accName, accId, traDate, traType, amount);

            // Assert
            Assert.That(false, Is.EqualTo(reponse));

        }

        [Test]
        public void Withdraw_Withdraw_from_Account_When_Balance_Is_Zero()
        {
            // Arrange
            string accName = "UOB"; int accId = 6; DateTime traDate = Convert.ToDateTime("2023-07-19"); string traType = "W"; decimal amount = 3329;

            // Act
            var reponse = _transaction.Withdraw(accName, accId, traDate, traType, amount);

            // Assert
            Assert.That(false, Is.EqualTo(reponse));

        }

        [Test]
        public void Withdrawt_Withdraw_from_Account_When_Withdraw_Amount_Is_Greater_Than_Balance()
        {
            // Arrange
            string accName = "UOB"; int accId = 6; DateTime traDate = Convert.ToDateTime("2023-07-19"); string traType = "W"; decimal amount = 100000;

            // Act
            var reponse = _transaction.Withdraw(accName, accId, traDate, traType, amount);

            // Assert
            Assert.That(false, Is.EqualTo(reponse));

        }


        [Test]
        public void Withdrawt_Withdraw_from_Account_When_Balance_Is_Greater_Than_Withdraw_Amount()
        {
            // Arrange
            string accName = "UOB"; int accId = 6; DateTime traDate = Convert.ToDateTime("2023-07-19"); string traType = "W"; decimal amount = 600;

            // Act
            var reponse = _transaction.Withdraw(accName, accId, traDate, traType, amount);

            // Assert
            Assert.That(true, Is.EqualTo(reponse));

        }
    }
}
