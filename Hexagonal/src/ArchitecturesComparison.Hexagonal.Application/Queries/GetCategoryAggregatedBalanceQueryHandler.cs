using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ArchitecturesComparison.Hexagonal.Application.Ports;
using ArchitecturesComparison.Hexagonal.Application.Ports.Driven;
using ArchitecturesComparison.Requests.DTOs;
using ArchitecturesComparison.Requests.Queries;
using MediatR;

namespace ArchitecturesComparison.Hexagonal.Application.Queries
{
    public class GetCategoryAggregatedBalanceQueryHandler : IRequestHandler<GetCategoryAggregatedBalanceQuery, AggregatedCategoryDto>
    {
        private readonly IBudgetRepositoryPort _budgetRepositoryPort;

        public GetCategoryAggregatedBalanceQueryHandler(IBudgetRepositoryPort budgetRepositoryPort)
        {
            _budgetRepositoryPort = budgetRepositoryPort;
        }
        
        public async Task<AggregatedCategoryDto> Handle(GetCategoryAggregatedBalanceQuery request, CancellationToken cancellationToken)
        {
            var budget = await _budgetRepositoryPort.GetById(request.BudgetId);

            var category = budget?.Categories.FirstOrDefault(x => x.Id == request.CategoryId);
            
            if (budget is null || category is null)
            {
                return null!;
            }
            
            var aggregatedBalance = category.Entries
                .Aggregate(0M, (balance, entry) => balance + entry.Transaction.BalanceChange);

            return new AggregatedCategoryDto(
                category.Name.Value,
                aggregatedBalance
            );
        }
    }
}