using ArchitecturesComparison.Requests.DTOs;
using CsvHelper.Configuration;

namespace ArchitecturesComparison.Layered.Business.Services.Exporter.Csv.ClassMaps
{
    public sealed class EntryDtoClassMap : ClassMap<EntryDto>
    {
        public EntryDtoClassMap()
        {
            Map(x => x.Id).Name("id");
            Map(x => x.Date).Name("date");
            Map(x => x.Name).Name("name");
            Map(x => x.Transaction.BalanceChange).Name("balanceChange");
            Map(x => x.Description).Name("description");
        }
    }
}