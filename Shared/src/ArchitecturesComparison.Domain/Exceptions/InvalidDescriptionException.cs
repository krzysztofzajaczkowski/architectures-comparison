namespace ArchitecturesComparison.Domain.Exceptions
{
    public class InvalidDescriptionException : DomainException
    {
        public InvalidDescriptionException() : base($"Description can not be null.")
        {
        }
    }
}