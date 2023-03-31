using ArchitecturesComparison.Domain.Common;
using ArchitecturesComparison.Domain.Entities;
using ArchitecturesComparison.Domain.ValueObjects;
using ArchitecturesComparison.Requests.DTOs;

namespace ArchitecturesComparison.Layered.Tests.Utilities;

public static class New
{
    private static DateTime DefaultDateTime = new DateTime(2023, 03, 28, 21, 37, 00, DateTimeKind.Utc);

    public static Name Name(string value = "name") => 
        Domain.ValueObjects.Name.From(value);
    public static Description Description(string value = "") =>
        Domain.ValueObjects.Description.From(value);

    public static Budget Budget(Guid id, string name = "name", string description = "") =>
        new(id, Name(name), Description(description));

    public static Category Category(Guid id, string name = "name", string description = "") =>
        new(id, Name(name), Description(description));

    public static UtcDateTime UtcDateTime() => Domain.ValueObjects.UtcDateTime.From(DefaultDateTime);

    public static Transaction Transaction(TransactionType type = TransactionType.Income, decimal amount = 0) =>
        Domain.ValueObjects.Transaction.From(type, amount);

    public static TransactionDto TransactionDto(decimal balanceChange = 0) =>
        new TransactionDto(Math.Abs(balanceChange), balanceChange);

    public static Entry Entry(Guid id, string name = "name", string description = "",
        TransactionType type = TransactionType.Income, decimal amount = 0) => new(id, Name(name),
        Description(description), UtcDateTime(),
        Transaction(type, amount));

    public static EntryDto EntryDto(Guid id, string name = "name", 
        string description = "", decimal balanceChange = 0) => 
        new(id, name, description, DefaultDateTime, TransactionDto(balanceChange));
}