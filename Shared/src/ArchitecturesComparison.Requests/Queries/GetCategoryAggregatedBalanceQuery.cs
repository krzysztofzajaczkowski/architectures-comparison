using System;
using ArchitecturesComparison.Requests.DTOs;
using MediatR;

namespace ArchitecturesComparison.Requests.Queries
{
    public class GetCategoryAggregatedBalanceQuery : IRequest<AggregatedCategoryDto>
    {
        public Guid BudgetId { get; set; }
        public Guid CategoryId { get; set; }
    }
}