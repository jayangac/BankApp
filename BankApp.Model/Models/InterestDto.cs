using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Model.Models
{
    public class InterestDto
    {
        public int Id { get; set; }
        public DateTime TransactDate { get; set; }

        public string TxnId { get; set; }

        public string TransactType { get; set; }

        public decimal Amount { get; set; }

        public decimal Balance { get; set; }

    }
}
