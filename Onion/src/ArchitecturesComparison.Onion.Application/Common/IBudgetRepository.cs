using System;
using System.Threading.Tasks;
using ArchitecturesComparison.Domain.Entities;

namespace ArchitecturesComparison.Onion.Application.Common
{
    public interface IBudgetRepository
    {
        public Task<Budget?> GetById(Guid id);
    }
}