using System;
using System.Collections.Generic;
using ArchitecturesComparison.Domain.Exceptions;
using CSharpFunctionalExtensions;

namespace ArchitecturesComparison.Domain.ValueObjects
{
    public class UtcDateTime : ValueObject
    {
        public DateTime Value { get; }

        private UtcDateTime(DateTime value)
        {
            Value = value;
        }

        public static UtcDateTime From(DateTime value)
        {
            if (value.Kind != DateTimeKind.Utc)
            {
                throw new NonUtcDateTimeException();
            }

            return new UtcDateTime(value);
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}