using System;
using System.Collections.Generic;
using System.Linq;
using ArchitecturesComparison.Domain.Common;
using ArchitecturesComparison.Domain.Exceptions;
using ArchitecturesComparison.Domain.ValueObjects;

namespace ArchitecturesComparison.Domain.Entities
{
    public class Budget : IdentifiableEntity
    {
        public Name Name { get; }
        public Description Description { get; }

        private readonly HashSet<Category> _categories;
        public IReadOnlyCollection<Category> Categories => _categories;

        public Budget(Name name, Description description) : base(Guid.NewGuid())
        {
            Name = name;
            Description = description;
            _categories = new HashSet<Category>();
        }
        
        public Budget(Guid id, Name name, Description description) : base(id)
        {
            Name = name;
            Description = description;
            _categories = new HashSet<Category>();
        }
        
        public void AddCategory(Category category)
        {
            if (_categories.Any(x => x.Id == category.Id))
            {
                throw new CategoryAlreadyAddedException(category);
            }
            
            _categories.Add(category);
        }

        public void RemoveCategory(Guid id)
        {
            var category = _categories.FirstOrDefault(x => x.Id == id);

            if (category == null)
            {
                throw new CategoryNotFoundException(id);
            }

            _categories.Remove(category);
        }
    }
}