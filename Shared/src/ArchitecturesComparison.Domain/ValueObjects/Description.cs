using System;
using System.Collections.Generic;
using ArchitecturesComparison.Domain.Exceptions;
using CSharpFunctionalExtensions;

namespace ArchitecturesComparison.Domain.ValueObjects
{
    public class Description : ValueObject
    {
        public string Value { get; }

        private Description(string value)
        {
            Value = value;
        }

        public static Description From(string value)
        {
            if (value == null)
            {
                throw new InvalidDescriptionException();
            }
            
            return new Description(value);
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}