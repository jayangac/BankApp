namespace BankApp.Core.Common
{
    public class ValidationService
    {
        public bool AmountValidity(decimal amount)
        {
            return  amount > 0;
        }

    }
}
