using System;
using System.Threading.Tasks;
using ArchitecturesComparison.Domain.Entities;

namespace ArchitecturesComparison.Layered.DataAccess.Common
{
    public interface IBudgetRepository
    {
        public Task<Budget?> GetById(Guid id);
    }
}