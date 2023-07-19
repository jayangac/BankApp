using BankApp.Business.Interfaces;
using BankApp.Data.EntityModels;
using BankApp.Model.Models;
using BankApp.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Business.Implementations
{
    public class RuleService : IRuleService
    {
        private readonly IRuleRepository _ruleRepository;

        public RuleService(IRuleRepository ruleRepository)
        {
            _ruleRepository = ruleRepository;
        }
       
        public void CreateRule(DateTime ruleDate, string ruleId, string ruleRate)
        {
            _ruleRepository.CreateRule(ruleDate, ruleId, ruleRate);
        }

        public List<Rule> GetAllRules()
        {
            return _ruleRepository.GetAllRules();
        }

    }
}
