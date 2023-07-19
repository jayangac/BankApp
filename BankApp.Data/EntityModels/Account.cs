using System;
using System.Collections.Generic;

namespace BankApp.Data.EntityModels;

public partial class Account
{
    public int Id { get; set; }

    public string AccountName { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
