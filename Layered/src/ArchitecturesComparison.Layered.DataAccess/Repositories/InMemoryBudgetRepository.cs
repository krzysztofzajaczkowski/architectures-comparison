using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArchitecturesComparison.Domain.Entities;
using ArchitecturesComparison.Layered.DataAccess.Common;

namespace ArchitecturesComparison.Layered.DataAccess.Repositories
{
    public class InMemoryBudgetRepository : IBudgetRepository
    {
        private readonly Dictionary<Guid,Budget> _budgets;

        public InMemoryBudgetRepository()
        {
            _budgets = new Dictionary<Guid, Budget>();
        }

        public Task<Budget?> GetById(Guid id) => Task.FromResult(_budgets!.GetValueOrDefault(id, null));
    }
}