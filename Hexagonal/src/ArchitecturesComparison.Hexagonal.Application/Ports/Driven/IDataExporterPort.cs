using System.Collections.Generic;
using ArchitecturesComparison.Requests.DTOs;

namespace ArchitecturesComparison.Hexagonal.Application.Ports.Driven
{
    public interface IDataExporterPort
    {
        ExportResultDto ExportCategoriesData(IEnumerable<EntryDto> entries);
        ExportResultDto ExportCategoriesAggregatedData(AggregatedCategoryDto aggregatedCategoryDto);
    }
}