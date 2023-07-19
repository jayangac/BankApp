using System;
using System.Collections.Generic;

namespace BankApp.Data.EntityModels;

public partial class Rule
{
    public int Id { get; set; }

    public DateTime RuleDate { get; set; }

    public string RuleName { get; set; }

    public decimal Rate { get; set; }
}
