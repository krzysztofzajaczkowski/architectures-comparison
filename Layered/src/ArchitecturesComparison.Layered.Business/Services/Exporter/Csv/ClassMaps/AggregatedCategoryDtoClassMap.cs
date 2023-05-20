using ArchitecturesComparison.Requests.DTOs;
using CsvHelper.Configuration;

namespace ArchitecturesComparison.Layered.Business.Services.Exporter.Csv.ClassMaps
{
    public class AggregatedCategoryDtoClassMap : ClassMap<AggregatedCategoryDto>
    {
        public AggregatedCategoryDtoClassMap()
        {
            Map(x => x.Name).Name("name");
            Map(x => x.Balance).Name("balance");
        }
    }
}