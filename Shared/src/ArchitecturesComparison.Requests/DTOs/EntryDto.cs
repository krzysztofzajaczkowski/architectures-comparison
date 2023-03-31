using System;

namespace ArchitecturesComparison.Requests.DTOs
{
    public class EntryDto
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Description { get; }
        public DateTime Date { get; }
        public TransactionDto Transaction { get; }

        public EntryDto(Guid id, string name, string description, DateTime date, TransactionDto transaction)
        {
            Id = id;
            Name = name;
            Description = description;
            Date = date;
            Transaction = transaction;
        }
    }
}