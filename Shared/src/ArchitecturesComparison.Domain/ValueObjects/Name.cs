using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using ArchitecturesComparison.Domain.Exceptions;
using CSharpFunctionalExtensions;

namespace ArchitecturesComparison.Domain.ValueObjects
{
    public class Name : ValueObject
    {
        public string Value { get; }

        private Name(string value)
        {
            Value = value;
        }

        public static Name From(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidNameException(value);
            }

            return new Name(value);
        }
        
        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}