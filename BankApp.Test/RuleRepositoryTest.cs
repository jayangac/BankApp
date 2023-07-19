using BankApp.Data.BaseOperation;
using BankApp.Data.EntityModels;
using BankApp.Repository.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Test
{
    [TestFixture]
    public class RuleRepositoryTest
    {
        private BankDbContext _dbContext { get; set; }
        private RuleRepository rule;

        [SetUp]
        public void SetUp()
        {
            // initialize here
            _dbContext = new BankDbContext();
            rule = new RuleRepository(_dbContext);
        }

        [Test]
        public void CheckRuleExist_Select_Rule_With_Date_and_Rule_Name_WithExisting()
        {
            // Arrange
            DateTime ruleDate = Convert.ToDateTime("2023-06-04"); string ruleName = "Rule03";

            // Act
            var reponse = rule.CheckRuleExist(ruleDate, ruleName);

            // Assert
            Assert.That(reponse, Is.EqualTo(true));

        }

        [Test]
        public void CheckRuleExist_Select_Rule_With_Date_and_Rule_Name_WithputExisting()
        {
            // Arrange
            DateTime ruleDate = Convert.ToDateTime("2023-06-04"); string ruleName = "Rule04";

            // Act
            var reponse = rule.CheckRuleExist(ruleDate, ruleName);

            // Assert
            Assert.That(reponse, Is.EqualTo(false));

        }

        [Test]
        public void GetRule_Select_Rule_With_Date_and_Rule_Name_WithputExisting()
        {
            // Arrange
            DateTime ruleDate = Convert.ToDateTime("2023-06-04"); string ruleName = "Rule03";
            var ruleData = new Rule { Id = 3, Rate = Convert.ToDecimal(8.00), RuleDate = ruleDate, RuleName = ruleName };

            // Act
            var reponse = rule.GetRule(ruleDate, ruleName);

            //Assert.AreEqual(reponse, ruleData);
            Assert.That(reponse, Is.TypeOf<Rule>());
            //Assert.That(reponse, Is.EqualTo(ruleData));

        }

    }
}
