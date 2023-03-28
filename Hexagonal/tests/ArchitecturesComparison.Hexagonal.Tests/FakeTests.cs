using Machine.Specifications;

// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Local

namespace ArchitecturesComparison.Hexagonal.Tests;

public class FakeTests
{
    private It should_return_true = () => true.ShouldBeTrue();
}