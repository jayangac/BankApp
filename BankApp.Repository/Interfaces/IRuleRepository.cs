﻿using BankApp.Data.EntityModels;

namespace BankApp.Repository.Interfaces
{
    public interface IRuleRepository
    {
        void CreateRule(DateTime ruleDate, string ruleId, string ruleRate);
        bool CheckRuleExist(DateTime ruleDate, string ruleId);
        Rule GetRule(DateTime ruleDate, string ruleId);
        List<Rule> GetAllRules();
        DateTime GetLastMonthLastDate(int monthNum);
        Rule GetRuleAccordingDate(DateTime ruleDate);
    }
}
