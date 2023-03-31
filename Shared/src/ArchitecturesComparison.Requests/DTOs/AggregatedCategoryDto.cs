namespace ArchitecturesComparison.Requests.DTOs
{
    public class AggregatedCategoryDto
    {
        public string Name { get; }
        public decimal Balance { get; }
        
        public AggregatedCategoryDto(string name, decimal balance)
        {
            Name = name;
            Balance = balance;
        }
    }
}