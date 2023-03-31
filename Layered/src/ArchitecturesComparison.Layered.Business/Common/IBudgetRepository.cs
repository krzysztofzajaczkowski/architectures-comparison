using System;
using System.Threading.Tasks;
using ArchitecturesComparison.Domain.Entities;

namespace ArchitecturesComparison.Layered.Business.Common
{
    public interface IBudgetRepository
    {
        public Task<Budget?> GetById(Guid id);
    }
}