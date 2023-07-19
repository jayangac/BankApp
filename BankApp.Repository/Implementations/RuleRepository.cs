using BankApp.Data.BaseOperation;
using BankApp.Data.EntityModels;
using BankApp.Model.Models;
using BankApp.Repository.Interfaces;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Repository.Implementations
{
    public class RuleRepository : IRuleRepository
    {
        private readonly BankDbContext _dbContext;

        public RuleRepository(BankDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public bool CheckRuleExist(DateTime ruleDate, string ruleId)
        {
            try
            {
                return _dbContext.Rules.Any(x => x.RuleName == ruleId && x.RuleDate == ruleDate);

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void CreateRule(DateTime ruleDate, string ruleId, string ruleRate)
        {
            try
            {
                var isExit = CheckRuleExist(ruleDate, ruleId);
                if (isExit)
                {
                    //update
                    var rule = GetRule(ruleDate, ruleId);
                    rule.Rate = Convert.ToDecimal(ruleRate);
                    _dbContext.Rules.Update(rule);
                }
                else
                {
                    //insert
                    var rule = new Rule
                    {
                        RuleDate = ruleDate,
                        RuleName = ruleId,
                        Rate = Convert.ToDecimal(ruleRate)
                    };
                    _dbContext.Rules.Add(rule);
                }
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<Rule> GetAllRules()
        {
            try
            {
                return _dbContext.Rules.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DateTime GetLastMonthLastDate(int monthNum)
        {
            var today = DateTime.Today;
            var month = new DateTime(today.Year, monthNum, 1);
            return month.AddDays(-1);
        }

        public Rule GetRule(DateTime ruleDate, string ruleId)
        {
            try
            {
                return _dbContext.Rules.FirstOrDefault(x => x.RuleName == ruleId && x.RuleDate == ruleDate);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Rule GetRuleAccordingDate(DateTime ruleDate)
        {
            try
            {
                return _dbContext.Rules.Where(x => x.RuleDate <= ruleDate).OrderByDescending(x => x.RuleDate).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
