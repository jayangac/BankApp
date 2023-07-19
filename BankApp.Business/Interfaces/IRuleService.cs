using BankApp.Data.EntityModels;

namespace BankApp.Business.Interfaces
{
    public interface IRuleService
    {
        void CreateRule(DateTime ruleDate, string ruleId, string ruleRate);
        List<Rule> GetAllRules();
    }
}
