using System;
using System.Collections.Generic;
using System.Linq;
using ArchitecturesComparison.Domain.Common;
using ArchitecturesComparison.Domain.Exceptions;
using ArchitecturesComparison.Domain.ValueObjects;

namespace ArchitecturesComparison.Domain.Entities
{
    public class Category : IIdentifiableEntity
    {
        public Guid Id { get; }
        public Name Name { get; }
        public Description Description { get; }

        private readonly HashSet<Entry> _entries;
        public IReadOnlyCollection<Entry> Entries => _entries;

        public Category(Name name, Description description)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            _entries = new HashSet<Entry>();
        }
        
        public Category(Guid id, Name name, Description description)
        {
            Id = id;
            Name = name;
            Description = description;
            _entries = new HashSet<Entry>();
        }

        public void AddEntry(Entry entry)
        {
            if (_entries.Any(x => x.Id == entry.Id))
            {
                throw new EntryAlreadyAddedException(entry);
            }
            
            _entries.Add(entry);
        }

        public void RemoveEntry(Guid id)
        {
            var entry = _entries.FirstOrDefault(x => x.Id == id);

            if (entry == null)
            {
                throw new EntryNotFoundException(id);
            }

            _entries.Remove(entry);
        }
    }
}