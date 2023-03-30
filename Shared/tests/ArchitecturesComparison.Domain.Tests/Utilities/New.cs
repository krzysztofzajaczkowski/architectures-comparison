using ArchitecturesComparison.Domain.Entities;
using ArchitecturesComparison.Domain.ValueObjects;

namespace ArchitecturesComparison.Domain.Tests.Utilities;

public static class New
{
    public static Entry Entry(Guid? guid = null, string name = "value", string description = "", DateTime dateTime = default,
        decimal amount = 1)
    {
        guid = guid ?? Guid.NewGuid();
        dateTime = dateTime != default ? dateTime : new DateTime(2023, 3, 28, 21, 37, 00, DateTimeKind.Utc); 
        return new Entry(guid.Value, Name.From(name), Description.From(description), UtcDateTime.From(dateTime),
            Transaction.From(amount));
    }

    public static Category Category(Guid? guid = null, string name = "value", string description = "",
        IEnumerable<Entry> entries = null)
    {
        guid = guid ?? Guid.NewGuid();
        var category = new Category(guid.Value, Name.From(name), Description.From(description));
        
        foreach (var entry in entries ?? Enumerable.Empty<Entry>())
        {
            category.AddEntry(entry);
        }

        return category;
    }
    
    public static Budget Budget(Guid? guid = null, string name = "value", string description = "",
        IEnumerable<Category> categories = null)
    {
        guid = guid ?? Guid.NewGuid();
        var budget = new Budget(guid.Value, Name.From(name), Description.From(description));
        
        foreach (var category in categories ?? Enumerable.Empty<Category>())
        {
            budget.AddCategory(category);
        }
        
        return budget;
    }
    
    
}