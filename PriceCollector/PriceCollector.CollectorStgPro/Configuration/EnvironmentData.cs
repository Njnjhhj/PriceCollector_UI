namespace PriceCollector.CollectorStgPro.Configuration
{
    public static class EnvironmentData
    {
        public static string WorkingDirectory => "C:\\PriceCollectorDataSTG";
        public static string InputDirectory => string.Concat(WorkingDirectory, "\\InputSTG");
        public static string OutputDirectory => string.Concat(WorkingDirectory, "\\OutputSTG");
        public static string InputFileName => "ProductIdSTG";
        public static string OutputFileNamePrefix => "OutputSTG";
    }
}
