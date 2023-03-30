namespace ArchitecturesComparison.Domain.Exceptions
{
    public class NonUtcDateTimeException : DomainException
    {
        public NonUtcDateTimeException() : base($"Only UTC is accepted as date kind.")
        {
        }
    }
}