using BankApp.Data.BaseOperation;
using BankApp.Data.EntityModels;
using BankApp.Repository.Implementations;
using Newtonsoft.Json;

namespace BankApp.Test
{
    [TestFixture]
    public class RuleRepositoryTest
    {
        private BankDbContext _dbContext { get; set; }
        private RuleRepository _rule;

        [SetUp]
        public void SetUp()
        {
            // initialize here
            _dbContext = new BankDbContext();
            _rule = new RuleRepository(_dbContext);
        }

        [Test]
        public void CheckRuleExist_Select_Rule_With_Date_and_Rule_Name_WithExisting()
        {
            // Arrange
            DateTime ruleDate = Convert.ToDateTime("2023-06-04"); string ruleName = "Rule03";

            // Act
            var reponse = _rule.CheckRuleExist(ruleDate, ruleName);

            // Assert
            Assert.That(reponse, Is.EqualTo(true));

        }

        [Test]
        public void CheckRuleExist_Select_Rule_With_Date_and_Rule_Name_WithputExisting()
        {
            // Arrange
            DateTime ruleDate = Convert.ToDateTime("2023-06-04"); string ruleName = "Rule04";

            // Act
            var reponse = _rule.CheckRuleExist(ruleDate, ruleName);

            // Assert
            Assert.That(reponse, Is.EqualTo(false));

        }

        [Test]
        
        public void GetRule_Select_Rule_With_Date_and_Rule_Name_With_Existing()
        {
            // Arrange
            DateTime ruleDate = Convert.ToDateTime("2023-06-04"); string ruleName = "Rule03";       
            var ruleData = new Rule() { Id = 3, Rate = Convert.ToDecimal("8.00"), RuleDate = ruleDate, RuleName = ruleName };

             // Act
            var reponse = _rule.GetRule(ruleDate, ruleName);

            var expected = JsonConvert.SerializeObject(ruleData);
            var result = JsonConvert.SerializeObject(reponse);

            // Assert
            Assert.AreEqual(result, expected);

        }

        [Test]
        public void GetRule_Select_Rule_With_Date_and_Rule_Name_With_Not_Existing()
        {
            // Arrange
            DateTime ruleDate = Convert.ToDateTime("2023-06-04"); string ruleName = "Rule04";
            var ruleData = new Rule() { Id = 3, Rate = Convert.ToDecimal("8.00"), RuleDate = ruleDate, RuleName = ruleName };

            // Act
            var reponse = _rule.GetRule(ruleDate, ruleName);

            var expected = JsonConvert.SerializeObject(ruleData);
            var result = JsonConvert.SerializeObject(reponse);

            // Assert
            Assert.That(expected, Is.Not.EqualTo(result));

        }

        [Test]
        public void GetRule_Select_Rule_With_Date_With_Existing()
        {
            // Arrange
            DateTime ruleDate = Convert.ToDateTime("2023-06-08"); string ruleName = "Rule03";
            DateTime expectedRuleDate = Convert.ToDateTime("2023-06-04");
            var ruleData = new Rule() { Id = 3, Rate = Convert.ToDecimal("8.00"), RuleDate = expectedRuleDate, RuleName = ruleName };

            // Act
            var reponse = _rule.GetRuleAccordingDate(ruleDate);

            var expected = JsonConvert.SerializeObject(ruleData);
            var result = JsonConvert.SerializeObject(reponse);

            // Assert
            Assert.That(expected, Is.EqualTo(result));

        }

        [Test]
        public void GetRule_Select_Rule_With_Date_WithNot_Existing()
        {
            // Arrange
            DateTime ruleDate = Convert.ToDateTime("2023-06-08"); string ruleName = "Rule04";
            var ruleData = new Rule() { Id = 3, Rate = Convert.ToDecimal("8.00"), RuleDate = ruleDate, RuleName = ruleName };

            // Act
            var reponse = _rule.GetRuleAccordingDate(ruleDate);

            var expected = JsonConvert.SerializeObject(ruleData);
            var result = JsonConvert.SerializeObject(reponse);

            // Assert
            Assert.That(expected, Is.Not.EqualTo(result));

        }

    }
}
