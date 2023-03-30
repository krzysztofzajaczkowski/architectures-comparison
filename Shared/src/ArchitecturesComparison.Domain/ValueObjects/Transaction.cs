using System;
using System.Collections.Generic;
using ArchitecturesComparison.Domain.Common;
using ArchitecturesComparison.Domain.Exceptions;
using CSharpFunctionalExtensions;

namespace ArchitecturesComparison.Domain.ValueObjects
{
    public class Transaction : ValueObject
    {
        public static char Currency => '$';
        public TransactionType Type { get; }
        public decimal Amount { get; }

        public decimal BalanceChange => Type switch
        {
            TransactionType.Expense => -Amount,
            TransactionType.Income => Amount,
            _ => throw new ArgumentOutOfRangeException()
        };

        private Transaction(TransactionType type, decimal amount)
        {
            Type = type;
            Amount = amount;
        }

        public static Transaction From(decimal amount) =>
            amount switch
            {
                < 0 => From(TransactionType.Expense, Math.Abs(amount)),
                _ => From(TransactionType.Income, amount)
            };

        public static Transaction From(TransactionType type, decimal amount)
        {
            return amount switch
            {
                0 => throw new TransactionAmountZeroException(),
                < 0 => throw new InvalidTransactionException(amount, type),
                _ => new Transaction(type, amount)
            };
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Type;
            yield return Amount;
        }
    }
}