using System;
using ArchitecturesComparison.Domain.Common;
using ArchitecturesComparison.Domain.ValueObjects;

namespace ArchitecturesComparison.Domain.Entities
{
    public class Entry : IdentifiableEntity
    {
        public Name Name { get; }
        public Description Description { get; }
        public UtcDateTime Date { get; }
        public Transaction Transaction { get; }

        public Entry(Name name, Description description, UtcDateTime date, Transaction transaction) : base(Guid.NewGuid())
        {
            Name = name;
            Description = description;
            Date = date;
            Transaction = transaction;
        }
        
        public Entry(Guid id, Name name, Description description, UtcDateTime date, Transaction transaction) : base(id)
        {
            Name = name;
            Description = description;
            Date = date;
            Transaction = transaction;
        }
    }
}