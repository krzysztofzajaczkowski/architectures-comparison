using System.Threading;
using System.Threading.Tasks;
using ArchitecturesComparison.Hexagonal.Application.Ports;
using ArchitecturesComparison.Hexagonal.Application.Ports.Driven;
using ArchitecturesComparison.Requests.DTOs;
using ArchitecturesComparison.Requests.Queries;
using MediatR;

namespace ArchitecturesComparison.Hexagonal.Application.Queries
{
    public class GetCategoryAggregatedBalanceExportQueryHandler : IRequestHandler<GetCategoryAggregatedBalanceExportQuery, ExportResultDto>
    {
        private readonly IMediator _mediator;
        private readonly IDataExporterPort _dataExporterPort;

        public GetCategoryAggregatedBalanceExportQueryHandler(IMediator mediator, IDataExporterPort dataExporterPort)
        {
            _mediator = mediator;
            _dataExporterPort = dataExporterPort;
        }
        
        public async Task<ExportResultDto> Handle(GetCategoryAggregatedBalanceExportQuery request, CancellationToken cancellationToken)
        {
            var aggregatedCategoryBalance = await _mediator.Send(new GetCategoryAggregatedBalanceQuery
            {
                BudgetId = request.BudgetId,
                CategoryId = request.CategoryId
            }, cancellationToken);

            var result = _dataExporterPort.ExportCategoriesAggregatedData(aggregatedCategoryBalance);

            return result;
        }
    }
}