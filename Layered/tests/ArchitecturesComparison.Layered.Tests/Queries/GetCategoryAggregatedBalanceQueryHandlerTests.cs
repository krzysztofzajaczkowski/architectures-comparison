using ArchitecturesComparison.Domain.Common;
using ArchitecturesComparison.Domain.Entities;
using ArchitecturesComparison.Layered.Business.Queries;
using ArchitecturesComparison.Layered.DataAccess.Common;
using ArchitecturesComparison.Layered.Tests.Utilities;
using ArchitecturesComparison.Requests.DTOs;
using ArchitecturesComparison.Requests.Queries;
using Machine.Fakes;
using Machine.Specifications;
using NSubstitute;

// ReSharper disable AsyncVoidLambda
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Local

namespace ArchitecturesComparison.Layered.Tests.Queries;

[Subject(typeof(GetCategoryAggregatedBalanceQueryHandler))]
public class GetCategoryAggregatedBalanceQueryHandlerTests : WithFakes
{
    static readonly Guid BudgetId = Guid.Parse("a7d6ce00-f09e-4078-a8b9-8019dca4c3a4");
    static readonly Guid CategoryId = Guid.Parse("800453f7-6d28-4277-a4a6-76f2917aecf5");
    
    static IBudgetRepository budgetRepository;
    static GetCategoryAggregatedBalanceQueryHandler sut;

    static AggregatedCategoryDto result;
    
    Establish ctx = () =>
    {
        budgetRepository = An<IBudgetRepository>();
        sut = new GetCategoryAggregatedBalanceQueryHandler(budgetRepository);
    };
    
    Because of = async () => result = await sut.Handle(new GetCategoryAggregatedBalanceQuery
    {
        BudgetId = BudgetId,
        CategoryId = CategoryId
    }, CancellationToken.None);

    class When_budget_does_not_exist
    {
        Establish ctx = () => budgetRepository
            .GetById(Arg.Any<Guid>())
            .ReturnsForAnyArgs(Task.FromResult((Budget) null));

        It should_return_null = () => result.ShouldBeNull();
    }

    class When_budget_exists
    {
        static Budget budget;

        private Establish ctx = () =>
        {
            budget = New.Budget(BudgetId);
            budgetRepository.GetById(Arg.Is(BudgetId)).Returns(Task.FromResult(budget));
        };
        
        class When_category_not_found
        {
            It should_return_null = () => result.ShouldBeNull();
        }

        class When_category_found
        {
            private static Category category;
            
            private Establish ctx = () =>
            {
                category = New.Category(CategoryId);
                
                budget.AddCategory(category);
            };
            
            class When_category_empty
            {
                It should_return_empty_balance = () =>
                    result.ShouldBeLike(New.AggregatedCategoryDto(balance: 0));
            }

            class When_category_not_empty
            {
                Establish ctx = () =>
                {
                    category.AddEntry(New.Entry(type: TransactionType.Income, amount: 10));
                    category.AddEntry(New.Entry(type: TransactionType.Expense, amount: 7));
                };
                
                It should_return_correct_result = () => 
                    result.ShouldBeLike(New.AggregatedCategoryDto(balance: 3));
            }
        }
    }
}