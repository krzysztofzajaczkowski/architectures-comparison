namespace ArchitecturesComparison.Requests.DTOs
{
    public class ExportResultDto
    {
        public string FilePath { get; }
        public string Extension { get; }

        public ExportResultDto(string filePath, string extension)
        {
            FilePath = filePath;
            Extension = extension;
        }
    }
}