using System;

namespace ArchitecturesComparison.Domain.Exceptions
{
    public class CategoryNotFoundException : DomainException
    {
        public CategoryNotFoundException(Guid id) : base($"Category with id: {id} was not found.")
        {
        }
    }
}