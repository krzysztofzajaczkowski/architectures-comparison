using System;
using System.Collections.Generic;
using ArchitecturesComparison.Requests.DTOs;
using MediatR;

namespace ArchitecturesComparison.Requests.Queries
{
    public class GetCategoriesEntriesExportQuery : IRequest<ExportResultDto>
    {
        public Guid BudgetId { get; set; }
        public IEnumerable<Guid> CategoriesIds { get; set; }
    }
}