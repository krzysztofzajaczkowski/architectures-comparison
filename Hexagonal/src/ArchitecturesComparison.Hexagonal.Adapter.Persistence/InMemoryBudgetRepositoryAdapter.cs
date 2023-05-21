using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArchitecturesComparison.Domain.Entities;
using ArchitecturesComparison.Hexagonal.Application.Ports;
using ArchitecturesComparison.Hexagonal.Application.Ports.Driven;

namespace ArchitecturesComparison.Hexagonal.Adapter.Persistence
{
    public class InMemoryBudgetRepositoryAdapter : IBudgetRepositoryPort
    {
        private readonly Dictionary<Guid,Budget> _budgets;

        public InMemoryBudgetRepositoryAdapter()
        {
            _budgets = new Dictionary<Guid, Budget>();
        }

        public Task<Budget?> GetById(Guid id) => Task.FromResult(_budgets!.GetValueOrDefault(id, null));
    }
}