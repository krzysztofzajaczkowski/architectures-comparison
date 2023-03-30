using ArchitecturesComparison.Domain.Exceptions;
using ArchitecturesComparison.Domain.ValueObjects;
using Machine.Specifications;

// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Local

namespace ArchitecturesComparison.Domain.Tests.ValueObjects;

[Subject(typeof(Description))]
public class DescriptionTests
{
    static string value;
    static Description sut;
    static Exception exception;
    
    Because of = () => exception = Catch.Exception(() => sut = Description.From(value));

    class When_value_null
    {
        Establish ctx = () => value = null;

        It should_throw_invalid_description_exception = () => 
                exception.ShouldBeOfExactType<InvalidDescriptionException>();
    }

    class When_value_valid
    {
        
        class When_value_empty
        {
            Establish ctx = () => value = string.Empty;
            
            It should_not_throw_any_exceptions_when_constructing = () => exception.ShouldBeNull();
            It should_properly_save_value = () => sut.Value.ShouldEqual(value);
        }
        
        class When_value_filled
        {
            Establish ctx = () => value = "filled value";
            
            It should_not_throw_any_exceptions_when_constructing = () => exception.ShouldBeNull();
            It should_properly_save_value = () => sut.Value.ShouldEqual(value);
        }
    }
}