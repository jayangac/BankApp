namespace BankApp.Model.Models
{
    public class CalculateInterestDto
    {
        public DateTime TransactDate { get; set; }
        public int NumberOfDays { get; set; }
        public decimal EODBalance  { get; set; }
        public decimal AnnualizedInterest { get; set; }

    }
}
