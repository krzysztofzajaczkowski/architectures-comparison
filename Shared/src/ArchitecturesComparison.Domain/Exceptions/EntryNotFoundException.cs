using System;

namespace ArchitecturesComparison.Domain.Exceptions
{
    public class EntryNotFoundException : DomainException
    {
        public EntryNotFoundException(Guid id) : base($"Entry with id: {id} was not found.")
        {
        }
    }
}