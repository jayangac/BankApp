using BankApp.Data.EntityModels;
using BankApp.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Business.Interfaces
{
    public interface IRuleService
    {
        void CreateRule(DateTime ruleDate, string ruleId, string ruleRate);
        List<Rule> GetAllRules();
    }
}
