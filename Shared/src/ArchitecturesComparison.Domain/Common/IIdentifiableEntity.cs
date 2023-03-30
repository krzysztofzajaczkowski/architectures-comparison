using System;

namespace ArchitecturesComparison.Domain.Common
{
    public interface IIdentifiableEntity
    {
        public Guid Id { get; }
    }
}