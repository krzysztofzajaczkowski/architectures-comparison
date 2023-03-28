using ArchitecturesComparison.Domain.Entities;

namespace ArchitecturesComparison.Domain.Exceptions
{
    public class CategoryAlreadyAddedException : DomainException
    {
        public CategoryAlreadyAddedException(Category category) : base($"Category with id: {category.Id} and name {category.Name.Value} is already added.")
        {
        }
    }
}