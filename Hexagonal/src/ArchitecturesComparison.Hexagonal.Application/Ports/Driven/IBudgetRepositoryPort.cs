using System;
using System.Threading.Tasks;
using ArchitecturesComparison.Domain.Entities;

namespace ArchitecturesComparison.Hexagonal.Application.Ports.Driven
{
    public interface IBudgetRepositoryPort
    {
        public Task<Budget?> GetById(Guid id);
    }
}