using ArchitecturesComparison.Domain.Common;

namespace ArchitecturesComparison.Domain.Exceptions
{
    public class InvalidTransactionException : DomainException
    {
        public InvalidTransactionException(decimal amount, TransactionType type) : base($"Invalid amount: {amount} for transaction of type {type}.")
        {
        }
    }
}