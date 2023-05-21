using System.Collections.Generic;
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
    public class GetCategoriesEntriesQueryHandler : IRequestHandler<GetCategoriesEntriesQuery, IEnumerable<EntryDto>>
    {
        private readonly IBudgetRepositoryPort _budgetRepositoryPort;

        public GetCategoriesEntriesQueryHandler(IBudgetRepositoryPort budgetRepositoryPort)
        {
            _budgetRepositoryPort = budgetRepositoryPort;
        }
        
        public async Task<IEnumerable<EntryDto>> Handle(GetCategoriesEntriesQuery request, CancellationToken cancellationToken)
        {
            var budget = await _budgetRepositoryPort.GetById(request.BudgetId);

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