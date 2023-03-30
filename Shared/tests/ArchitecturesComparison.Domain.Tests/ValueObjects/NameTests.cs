using ArchitecturesComparison.Domain.Exceptions;
using ArchitecturesComparison.Domain.ValueObjects;
using Machine.Specifications;

// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Local

namespace ArchitecturesComparison.Domain.Tests.ValueObjects;

[Subject(typeof(Name))]
public class NameTests 
{
    static string value;
    static Name sut;
    static Exception exception;

    Because of = () => exception = Catch.Exception(() => sut = Name.From(value));
    
    class When_value_null
    {
        Establish ctx = () => value = null;

        It should_throw_invalid_name_exception = () =>
            exception.ShouldBeOfExactType<InvalidNameException>();
    }

    class When_value_empty
    {
        Establish ctx = () => value = string.Empty;

        It should_throw_invalid_name_exception = () =>
            exception.ShouldBeOfExactType<InvalidNameException>();
    }

    class When_value_filled
    {
        Establish ctx = () => value = "filled value";
                
        It should_not_throw_any_exceptions_when_constructing = () => exception.ShouldBeNull();
        It should_properly_save_value = () => sut.Value.ShouldEqual(value);
    }
}