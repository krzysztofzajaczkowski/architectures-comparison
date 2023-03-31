using ArchitecturesComparison.Domain.Entities;
using ArchitecturesComparison.Layered.Business.Common;
using ArchitecturesComparison.Layered.Business.Queries;
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

[Subject(typeof(GetCategoriesEntriesQueryHandler))]
public class GetCategoriesEntriesQueryHandlerTests : WithFakes
{
    static readonly Guid BudgetId = Guid.Parse("a7d6ce00-f09e-4078-a8b9-8019dca4c3a4");
    static readonly Guid CategoryId1 = Guid.Parse("800453f7-6d28-4277-a4a6-76f2917aecf5");
    static readonly Guid CategoryId2 = Guid.Parse("0b9e39b9-2bde-42c4-acc3-988f85dba8c7");
    
    static IBudgetRepository budgetRepository;
    static GetCategoriesEntriesQueryHandler sut;
    
    static IEnumerable<EntryDto> result;
    
    Establish ctx = () =>
    {
        budgetRepository = An<IBudgetRepository>();
        sut = new GetCategoriesEntriesQueryHandler(budgetRepository);
    };
    
    Because of = async () => result = await sut.Handle(new GetCategoriesEntriesQuery
    {
        BudgetId = BudgetId,
        CategoriesIds = new[]
        {
            CategoryId1,
            CategoryId2
        }
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
        
        class When_no_categories_found
        {
            private It should_return_empty_collection = () => result.ShouldBeEmpty();
        }

        class When_categories_found
        {
            private static Category category1;
            private static Category category2;

            private Establish ctx = () =>
            {
                category1 = New.Category(CategoryId1);
                category2 = New.Category(CategoryId2);
                
                budget.AddCategory(category1);
                budget.AddCategory(category2);
            };
            
            class When_both_categories_empty
            {
                It should_return_empty_collection = () => result.ShouldBeEmpty();
            }
            
            class When_empty_and_non_empty_category
            {
                static readonly Guid EntryId = Guid.Parse("1b18cd2c-36b8-4dfd-8aaa-ec295ec5fd8b");
                
                Establish ctx = () => category1.AddEntry(New.Entry(EntryId, amount: 5));

                It should_return_non_empty_collection = () => result.ShouldNotBeEmpty();

                It should_return_collection_with_categories_entries = () => result.ShouldBeLike(new[]
                {
                    New.EntryDto(EntryId, balanceChange: 5)
                });
            }

            class When_both_categories_not_empty
            {
                static readonly Guid EntryId1 = Guid.Parse("1b18cd2c-36b8-4dfd-8aaa-ec295ec5fd8b");
                static readonly Guid EntryId2 = Guid.Parse("95b18d76-778a-437a-b4f6-c0e52fae9881");
                
                Establish ctx = () =>
                {
                    category1.AddEntry(New.Entry(EntryId1, amount: 5));
                    category2.AddEntry(New.Entry(EntryId2, amount: 10));
                };
                
                It should_return_non_empty_collection = () => result.ShouldNotBeEmpty();

                It should_return_collection_with_categories_entries = () => result.ShouldBeLike(new[]
                {
                    New.EntryDto(EntryId1, balanceChange: 5),
                    New.EntryDto(EntryId2, balanceChange: 10),
                });
            }
        }
    }
}