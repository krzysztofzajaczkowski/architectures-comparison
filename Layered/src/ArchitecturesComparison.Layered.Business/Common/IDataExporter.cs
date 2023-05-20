using System.Collections.Generic;
using ArchitecturesComparison.Requests.DTOs;

namespace ArchitecturesComparison.Layered.Business.Common
{
    public interface IDataExporter
    {
        ExportResultDto ExportCategoriesData(IEnumerable<EntryDto> entries);
        ExportResultDto ExportCategoriesAggregatedData(AggregatedCategoryDto aggregatedCategoryDto);
    }
}