namespace PriceCollector.Collector.Configuration
{
    public static class EnvironmentData
    {
        public static string WorkingDirectory => "C:\\PriceCollectorData";
        public static string InputDirectory => string.Concat(WorkingDirectory, "\\Input");
        public static string OutputDirectory => string.Concat(WorkingDirectory, "\\Output");
        public static string InputFileName => "ProductId";
        public static string OutputFileNamePrefix => "OutputPrice";
    }
}
