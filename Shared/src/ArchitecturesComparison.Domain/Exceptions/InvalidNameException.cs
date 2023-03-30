namespace ArchitecturesComparison.Domain.Exceptions
{
    public class InvalidNameException : DomainException
    {
        public InvalidNameException(string value) : base($"Invalid name: {value}.")
        {
        }
    }
}