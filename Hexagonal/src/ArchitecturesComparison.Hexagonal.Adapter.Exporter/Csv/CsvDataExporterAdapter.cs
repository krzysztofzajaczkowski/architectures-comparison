using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using ArchitecturesComparison.Hexagonal.Application.Ports;
using ArchitecturesComparison.Hexagonal.Application.Ports.Driven;
using ArchitecturesComparison.Requests.DTOs;
using CsvHelper;

namespace ArchitecturesComparison.Hexagonal.Adapter.Exporter.Csv
{
    public class CsvDataExporterAdapter : IDataExporterPort
    {
        public ExportResultDto ExportCategoriesData(IEnumerable<EntryDto> entries) => ExportData(entries);

        public ExportResultDto ExportCategoriesAggregatedData(AggregatedCategoryDto aggregatedCategoryDto) =>
            ExportData(Enumerable.Repeat(aggregatedCategoryDto, 1));
        private static ExportResultDto ExportData<T>(IEnumerable<T> data)
        {
            var path = $"{Directory.GetCurrentDirectory()}{Path.DirectorySeparatorChar}{Guid.NewGuid()}-export-{DateTime.UtcNow}.csv";
            using var writer = new StreamWriter(path);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

            csv.WriteRecords(data);
            csv.Flush();

            return new ExportResultDto(path, "csv");
        }
    }
}