using System;
using ArchitecturesComparison.Domain.Entities;

namespace ArchitecturesComparison.Domain.Exceptions
{
    public class EntryAlreadyAddedException : DomainException
    {
        public EntryAlreadyAddedException(Entry entry) : base($"Entry with id: {entry.Id} and name {entry.Name.Value} is already added.")
        {
        }
    }
}