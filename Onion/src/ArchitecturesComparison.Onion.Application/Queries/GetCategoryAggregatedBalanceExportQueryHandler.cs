using System.Threading;
using System.Threading.Tasks;
using ArchitecturesComparison.Onion.Application.Common;
using ArchitecturesComparison.Requests.DTOs;
using ArchitecturesComparison.Requests.Queries;
using MediatR;

namespace ArchitecturesComparison.Onion.Application.Queries
{
    public class GetCategoryAggregatedBalanceExportQueryHandler : IRequestHandler<GetCategoryAggregatedBalanceExportQuery, ExportResultDto>
    {
        private readonly IMediator _mediator;
        private readonly IDataExporter _dataExporter;

        public GetCategoryAggregatedBalanceExportQueryHandler(IMediator mediator, IDataExporter dataExporter)
        {
            _mediator = mediator;
            _dataExporter = dataExporter;
        }
        
        public async Task<ExportResultDto> Handle(GetCategoryAggregatedBalanceExportQuery request, CancellationToken cancellationToken)
        {
            var aggregatedCategoryBalance = await _mediator.Send(new GetCategoryAggregatedBalanceQuery
            {
                BudgetId = request.BudgetId,
                CategoryId = request.CategoryId
            }, cancellationToken);

            var result = _dataExporter.ExportCategoriesAggregatedData(aggregatedCategoryBalance);

            return result;
        }
    }
}