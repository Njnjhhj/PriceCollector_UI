using System.ComponentModel;

namespace PriceCollector.Core.Enums
{
    public enum FileExtensions
    {
        [Description("pdf")]
        Pdf,
        [Description("doc")]
        Doc,
        [Description("xlsx")]
        Xlsx,
        [Description("png")]
        Png,
        [Description("csv")]
        Csv
    }
}