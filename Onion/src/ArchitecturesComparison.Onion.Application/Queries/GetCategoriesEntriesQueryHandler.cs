using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ArchitecturesComparison.Onion.Application.Common;
using ArchitecturesComparison.Requests.DTOs;
using ArchitecturesComparison.Requests.Queries;
using MediatR;

namespace ArchitecturesComparison.Onion.Application.Queries
{
    public class GetCategoriesEntriesQueryHandler : IRequestHandler<GetCategoriesEntriesQuery, IEnumerable<EntryDto>>
    {
        private readonly IBudgetRepository _budgetRepository;

        public GetCategoriesEntriesQueryHandler(IBudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }
        
        public async Task<IEnumerable<EntryDto>> Handle(GetCategoriesEntriesQuery request, CancellationToken cancellationToken)
        {
            var budget = await _budgetRepository.GetById(request.BudgetId);

            if (budget is null)
            {
                return null!;
            }

            var materializedCategoriesIds = request.CategoriesIds.ToHashSet();
            return budget.Categories
                .Where(x => materializedCategoriesIds.Contains(x.Id))
                .SelectMany(x => x.Entries)
                .Select(x =>
                    new EntryDto(
                        x.Id,
                        x.Name.Value,
                        x.Description.Value,
                        x.Date.Value,
                        new TransactionDto(
                            x.Transaction.Amount,
                            x.Transaction.BalanceChange)
                    )
                );
        }
    }
}