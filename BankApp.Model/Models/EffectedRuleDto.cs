using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Model.Models
{
    public class EffectedRuleDto
    {
        public DateTime RuleDate { get; set; }
        public string RuleName { get; set; }
        public decimal Rate { get; set; }
    }
}
