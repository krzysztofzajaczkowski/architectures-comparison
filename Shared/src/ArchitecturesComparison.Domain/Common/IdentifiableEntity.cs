using System;

namespace ArchitecturesComparison.Domain.Common
{
    public abstract class IdentifiableEntity
    {
        public Guid Id { get; }

        protected IdentifiableEntity(Guid id)
        {
            Id = id;
        }
    }
}