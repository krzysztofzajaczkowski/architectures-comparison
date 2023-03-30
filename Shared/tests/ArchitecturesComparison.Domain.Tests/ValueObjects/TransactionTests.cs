using ArchitecturesComparison.Domain.Common;
using ArchitecturesComparison.Domain.Exceptions;
using ArchitecturesComparison.Domain.ValueObjects;
using Machine.Specifications;

// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Local

namespace ArchitecturesComparison.Domain.Tests.ValueObjects;

[Subject(typeof(Transaction))]
public class TransactionTests
{
    static int amount;
    static Exception exception;
    static Transaction sut;
    
    class When_using_amount_only_constructor
    {
        Because of = () => exception = Catch.Exception(() => sut = Transaction.From(amount));

        class When_amount_negative
        {
            Establish ctx = () => amount = -1;

            It should_not_throw_any_exceptions_when_constructing = () => exception.ShouldBeNull();
            It should_properly_save_amount = () => sut.Amount.ShouldEqual(Math.Abs(amount));
            It should_properly_calculate_balance_change = () => sut.BalanceChange.ShouldEqual(amount);
            It should_properly_define_transaction_type = () => sut.Type.ShouldEqual(TransactionType.Expense);
        }

        class When_amount_positive
        {
            Establish ctx = () => amount = 1;
            
            It should_not_throw_any_exceptions_when_constructing = () => exception.ShouldBeNull();
            It should_properly_save_amount = () => sut.Amount.ShouldEqual(amount);
            It should_properly_calculate_balance_change = () => sut.BalanceChange.ShouldEqual(amount);
            It should_properly_define_transaction_type = () => sut.Type.ShouldEqual(TransactionType.Income);
        }

        class When_amount_zero
        {
            Establish ctx = () => amount = 0;
            
            It should_throw_transaction_amount_zero_exception = () => 
                exception.ShouldBeOfExactType<TransactionAmountZeroException>();
        }
    }

    class When_using_amount_and_type_constructor
    {
        static TransactionType type;
        
        Because of = () => exception = Catch.Exception(() => sut = Transaction.From(type, amount));
        
        class When_type_income
        {
            Establish ctx = () => type = TransactionType.Income;

            class When_amount_negative
            {
                Establish ctx = () => amount = -1;

                It should_throw_invalid_transaction_exception = () =>
                    exception.ShouldBeOfExactType<InvalidTransactionException>();
            }

            class When_amount_positive
            {
                Establish ctx = () => amount = 1;
            
                It should_not_throw_any_exceptions_when_constructing = () => exception.ShouldBeNull();
                It should_properly_save_amount = () => sut.Amount.ShouldEqual(amount);
                It should_properly_calculate_balance_change = () => sut.BalanceChange.ShouldEqual(amount);
                It should_properly_define_transaction_type = () => sut.Type.ShouldEqual(type);
            }

            class When_amount_zero
            {
                Establish ctx = () => amount = 0;
            
                It should_throw_transaction_amount_zero_exception = () => 
                    exception.ShouldBeOfExactType<TransactionAmountZeroException>();
            }
        }
        
        class When_type_expense
        {
            Establish ctx = () => type = TransactionType.Expense;

            class When_amount_negative
            {
                Establish ctx = () => amount = -1;

                It should_throw_invalid_transaction_exception = () =>
                    exception.ShouldBeOfExactType<InvalidTransactionException>();
            }

            class When_amount_positive
            {
                Establish ctx = () => amount = 1;
            
                It should_not_throw_any_exceptions_when_constructing = () => exception.ShouldBeNull();
                It should_properly_save_amount = () => sut.Amount.ShouldEqual(amount);
                It should_properly_calculate_balance_change = () => sut.BalanceChange.ShouldEqual(0 - amount);
                It should_properly_define_transaction_type = () => sut.Type.ShouldEqual(type);
            }

            class When_amount_zero
            {
                Establish ctx = () => amount = 0;
            
                It should_throw_transaction_amount_zero_exception = () => 
                    exception.ShouldBeOfExactType<TransactionAmountZeroException>();
            }
        }
    }
    
    
}