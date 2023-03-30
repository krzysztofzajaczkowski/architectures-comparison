using ArchitecturesComparison.Domain.Entities;
using ArchitecturesComparison.Domain.Exceptions;
using ArchitecturesComparison.Domain.Tests.Utilities;
using Machine.Specifications;

// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Local

namespace ArchitecturesComparison.Domain.Tests.Entities;

[Subject(typeof(Category))]
public class CategoryTests
{
    static Category sut;
    static Entry entry;
    static Exception exception;

    Establish ctx = () =>
    {
        sut = New.Category();
        entry = New.Entry();
    };

    class When_removing_entry
    {
        Because of = () => exception = Catch.Exception(() => sut.RemoveEntry(entry.Id));
        
        class When_entry_does_not_exist
        {
            private It should_throw_entry_not_found_exception = () =>
                exception.ShouldBeOfExactType<EntryNotFoundException>();
        }
        
        class When_entry_exists
        {
            Establish ctx = () => sut.AddEntry(entry);
            
            It should_not_throw_any_exceptions = () => exception.ShouldBeNull();
            It should_remove_entry = () => sut.Entries.ShouldBeEmpty();
        }
    }
    
    class When_adding_entry
    {
        Because of = () => exception = Catch.Exception(() => sut.AddEntry(entry));
        
        class When_entry_does_not_exist
        {
            It should_not_throw_any_exceptions = () => exception.ShouldBeNull();
            It should_add_entry = () => sut.Entries.ShouldEqual(new[] { entry });
        }
        
        class When_entry_exists
        {
            Establish ctx = () => sut.AddEntry(entry);
            
            private It should_throw_entry_already_added_exception = () =>
                exception.ShouldBeOfExactType<EntryAlreadyAddedException>();
        }
    }
}