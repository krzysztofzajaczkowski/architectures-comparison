using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ArchitecturesComparison.Hexagonal.Application.Ports;
using ArchitecturesComparison.Hexagonal.Application.Ports.Driven;
using ArchitecturesComparison.Requests.DTOs;
using Newtonsoft.Json;

namespace ArchitecturesComparison.Hexagonal.Adapter.Exporter.Json
{
    public class JsonDataExporterAdapter : IDataExporterPort
    {
        public ExportResultDto ExportCategoriesData(IEnumerable<EntryDto> entries) =>
            ExportData(entries);

        public ExportResultDto ExportCategoriesAggregatedData(AggregatedCategoryDto aggregatedCategoryDto) =>
            ExportData(Enumerable.Repeat(aggregatedCategoryDto, 1));

        private static ExportResultDto ExportData(IEnumerable<dynamic> data)
        {
            var path = $"{Directory.GetCurrentDirectory()}{Path.DirectorySeparatorChar}{Guid.NewGuid()}-export-{DateTime.UtcNow}.json";
            using var file = File.CreateText(path);
            var serializer = new JsonSerializer();
            serializer.Serialize(file, data);
            
            return new ExportResultDto(path, "json");
        }
    }
}