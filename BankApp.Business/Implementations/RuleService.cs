using BankApp.Business.Interfaces;
using BankApp.Data.EntityModels;
using BankApp.Repository.Interfaces;

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
