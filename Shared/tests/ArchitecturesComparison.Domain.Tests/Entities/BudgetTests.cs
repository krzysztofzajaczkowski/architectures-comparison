using ArchitecturesComparison.Domain.Entities;
using ArchitecturesComparison.Domain.Exceptions;
using ArchitecturesComparison.Domain.Tests.Utilities;
using Machine.Specifications;

// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Local

namespace ArchitecturesComparison.Domain.Tests.Entities;

[Subject(typeof(Budget))]
public class BudgetTests
{
    static Budget sut;
    static Category category;
    static Exception exception;

    Establish ctx = () =>
    {
        sut = New.Budget();
        category = New.Category();
    };

    class When_removing_category
    {
        Because of = () => exception = Catch.Exception(() => sut.RemoveCategory(category.Id));
        
        class When_category_does_not_exist
        {
            private It should_throw_category_not_found_exception = () =>
                exception.ShouldBeOfExactType<CategoryNotFoundException>();
        }
        
        class When_category_exists
        {
            Establish ctx = () => sut.AddCategory(category);
            
            It should_not_throw_any_exceptions = () => exception.ShouldBeNull();
            It should_remove_category = () => sut.Categories.ShouldBeEmpty();
        }
    }
    
    class When_adding_category
    {
        Because of = () => exception = Catch.Exception(() => sut.AddCategory(category));
        
        class When_category_does_not_exist
        {
            It should_not_throw_any_exceptions = () => exception.ShouldBeNull();
            It should_add_category = () => sut.Categories.ShouldEqual(new[] { category });
        }
        
        class When_category_exists
        {
            Establish ctx = () => sut.AddCategory(category);
            
            private It should_throw_category_already_added_exception = () =>
                exception.ShouldBeOfExactType<CategoryAlreadyAddedException>();
        }
    }
}