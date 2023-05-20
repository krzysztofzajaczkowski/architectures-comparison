using System.Threading;
using System.Threading.Tasks;
using ArchitecturesComparison.Layered.Business.Common;
using ArchitecturesComparison.Requests.DTOs;
using ArchitecturesComparison.Requests.Queries;
using MediatR;

namespace ArchitecturesComparison.Layered.Business.Queries
{
    public class GetCategoriesEntriesExportQueryHandler : IRequestHandler<GetCategoriesEntriesExportQuery, ExportResultDto>
    {
        private readonly IMediator _mediator;
        private readonly IDataExporter _dataExporter;

        public GetCategoriesEntriesExportQueryHandler(IMediator mediator, IDataExporter dataExporter)
        {
            _mediator = mediator;
            _dataExporter = dataExporter;
        }
        
        public async Task<ExportResultDto> Handle(GetCategoriesEntriesExportQuery request, CancellationToken cancellationToken)
        {
            var entries = await _mediator.Send(new GetCategoriesEntriesQuery
            {
                BudgetId = request.BudgetId,
                CategoriesIds = request.CategoriesIds
            }, cancellationToken);

            var result = _dataExporter.ExportCategoriesData(entries);

            return result;
        }
    }
}