using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ArchitecturesComparison.Layered.DataAccess.Common;
using ArchitecturesComparison.Requests.DTOs;
using ArchitecturesComparison.Requests.Queries;
using MediatR;

namespace ArchitecturesComparison.Layered.Business.Queries
{
    public class GetCategoryAggregatedBalanceQueryHandler : IRequestHandler<GetCategoryAggregatedBalanceQuery, AggregatedCategoryDto>
    {
        private readonly IBudgetRepository _budgetRepository;

        public GetCategoryAggregatedBalanceQueryHandler(IBudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }
        
        public async Task<AggregatedCategoryDto> Handle(GetCategoryAggregatedBalanceQuery request, CancellationToken cancellationToken)
        {
            var budget = await _budgetRepository.GetById(request.BudgetId);

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