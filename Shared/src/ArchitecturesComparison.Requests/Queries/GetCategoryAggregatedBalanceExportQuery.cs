using System;
using ArchitecturesComparison.Requests.DTOs;
using MediatR;

namespace ArchitecturesComparison.Requests.Queries
{
    public class GetCategoryAggregatedBalanceExportQuery : IRequest<ExportResultDto>
    {
        public Guid BudgetId { get; set; }
        public Guid CategoryId { get; set; }
    }
}