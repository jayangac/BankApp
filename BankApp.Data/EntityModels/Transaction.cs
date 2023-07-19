namespace BankApp.Data.EntityModels;

public partial class Transaction
{
    public int Id { get; set; }

    public DateTime TransactDate { get; set; }

    public string TxnId { get; set; }

    public string TransactType { get; set; }

    public decimal Amount { get; set; }

    public int AccountId { get; set; }

    public virtual Account Account { get; set; }
}
