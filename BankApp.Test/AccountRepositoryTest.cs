using BankApp.Business.Implementations;
using BankApp.Business.Interfaces;
using BankApp.Data.BaseOperation;
using BankApp.Repository.Implementations;
using BankApp.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Test
{
    [TestFixture]
    public class AccountRepositoryTest
    {
        private BankDbContext _dbContext { get; set; }
        private AccountRepository account;

        [SetUp]
        public void SetUp()
        {
            // initialize here
            _dbContext = new BankDbContext();
            account = new AccountRepository(_dbContext);
        }

        [Test]
        public void CheckAccountExist_WhenWeSelectNotExistingBank()
        {
            // Arrange
            string accountName = "BOC";

            // Act
            var reponse = account.CheckAccountExist(accountName);

            // Assert
            Assert.That(reponse, Is.EqualTo(false));

        }

        [Test]
        public void CheckAccountExist_WhenWeSelectExistingBank()
        {
            // Arrange
            string accountName = "UOB";

            // Act
            var reponse = account.CheckAccountExist(accountName);

            // Assert
            Assert.That(reponse, Is.EqualTo(true));

        }

        [Test]
        public void CreateAccount_Create_NewBank_Account_With_TrasactionType_D()
        {
            // Arrange
            string accountName = "NTB", traType = "D";

            // Act
            var reponse = account.CreateAccount(accountName, traType);

            // Assert
            Assert.That(reponse, Is.EqualTo(true));

        }

        [Test]
        public void CreateAccount_Create_NewBank_Account_With_TrasactionType_W()
        {
            // Arrange
            string accountName = "JPM", traType = "W";

            // Act
            var reponse = account.CreateAccount(accountName, traType);

            // Assert
            Assert.That(reponse, Is.EqualTo(false));

        }

        [Test]
        public void GetAccountInfo_Get_Account_Id_Correct_Value()
        {
            // Arrange
            string accountName = "UOB";

            // Act
            var reponse = account.GetAccountInfo(accountName);

            // Assert
            Assert.That(reponse, Is.EqualTo(6));

        }

        [Test]
        public void GetAccountInfo_Get_Account_Id_InCorrect_Value()
        {
            // Arrange
            string accountName = "UOB";

            // Act
            var reponse = account.GetAccountInfo(accountName);

            // Assert
            Assert.That(7, Is.Not.EqualTo(reponse));

        }
    }
}