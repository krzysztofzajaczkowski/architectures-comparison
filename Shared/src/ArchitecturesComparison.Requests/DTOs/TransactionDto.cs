namespace ArchitecturesComparison.Requests.DTOs
{
    public class TransactionDto
    {
        public decimal Amount { get; }
        public decimal BalanceChange { get; }

        public TransactionDto(decimal amount, decimal balanceChange)
        {
            Amount = amount;
            BalanceChange = balanceChange;
        }
    }
}