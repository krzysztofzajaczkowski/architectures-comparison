using ArchitecturesComparison.Domain.Exceptions;
using ArchitecturesComparison.Domain.ValueObjects;
using Machine.Specifications;

// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Local

namespace ArchitecturesComparison.Domain.Tests.ValueObjects;

[Subject(typeof(UtcDateTime))]
public class UtcDateTimeTests
{
    static DateTime CreateDateTime(DateTimeKind kind) => new(2023, 03, 28, 21, 37, 00, kind); 
    
    static DateTime value;
    static Exception exception;
    static UtcDateTime sut;
    
    Because of = () => exception = Catch.Exception(() => sut = UtcDateTime.From(value));

    class When_kind_unspecified
    {
        Establish ctx = () => value = CreateDateTime(DateTimeKind.Unspecified);

        It should_throw_non_utc_date_time_exception = () => exception.ShouldBeOfExactType<NonUtcDateTimeException>();
    }
    
    class When_kind_local
    {
        Establish ctx = () => value = CreateDateTime(DateTimeKind.Local);
        
        It should_throw_non_utc_date_time_exception = () => exception.ShouldBeOfExactType<NonUtcDateTimeException>();
    }
    
    class When_kind_utc
    {
        Establish ctx = () => value = CreateDateTime(DateTimeKind.Utc);
        
        It should_not_throw_any_exceptions_when_constructing = () => exception.ShouldBeNull();
        It should_properly_save_value = () => sut.Value.ShouldEqual(value);
    }
}