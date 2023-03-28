namespace ArchitecturesComparison.Domain.Exceptions
{
    public class TransactionAmountZeroException : DomainException
    {
        public TransactionAmountZeroException() : base("Transaction amount can not equal 0.")
        {
        }
    }
}