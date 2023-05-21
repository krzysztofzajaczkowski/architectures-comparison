using System.Threading;
using System.Threading.Tasks;
using ArchitecturesComparison.Hexagonal.Application.Ports;
using ArchitecturesComparison.Hexagonal.Application.Ports.Driven;
using ArchitecturesComparison.Requests.DTOs;
using ArchitecturesComparison.Requests.Queries;
using MediatR;

namespace ArchitecturesComparison.Hexagonal.Application.Queries
{
    public class GetCategoriesEntriesExportQueryHandler : IRequestHandler<GetCategoriesEntriesExportQuery, ExportResultDto>
    {
        private readonly IMediator _mediator;
        private readonly IDataExporterPort _dataExporterPort;

        public GetCategoriesEntriesExportQueryHandler(IMediator mediator, IDataExporterPort dataExporterPort)
        {
            _mediator = mediator;
            _dataExporterPort = dataExporterPort;
        }
        
        public async Task<ExportResultDto> Handle(GetCategoriesEntriesExportQuery request, CancellationToken cancellationToken)
        {
            var entries = await _mediator.Send(new GetCategoriesEntriesQuery
            {
                BudgetId = request.BudgetId,
                CategoriesIds = request.CategoriesIds
            }, cancellationToken);

            var result = _dataExporterPort.ExportCategoriesData(entries);

            return result;
        }
    }
}