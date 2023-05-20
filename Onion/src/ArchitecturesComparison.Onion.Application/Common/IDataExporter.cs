using System.Collections.Generic;
using ArchitecturesComparison.Requests.DTOs;

namespace ArchitecturesComparison.Onion.Application.Common
{
    public interface IDataExporter
    {
        ExportResultDto ExportCategoriesData(IEnumerable<EntryDto> entries);
        ExportResultDto ExportCategoriesAggregatedData(AggregatedCategoryDto aggregatedCategoryDto);
    }
}