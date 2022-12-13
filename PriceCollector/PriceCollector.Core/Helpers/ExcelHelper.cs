using System;
using System.Collections.Generic;
using System.IO;
using PriceCollector.Core.Utils;

namespace PriceCollector.Core.Helpers
{
    public static class ExcelHelper
    {
        public static List<T> ReadInputFile<T>(FileInfo fileInfo)
        {
            var directory = fileInfo.Directory?.ToString();

            if (directory != null && !Directory.Exists(directory))
                throw new Exception("INPUT DIRECTORY NOT FOUND! PLEASE CREATE ONE AND PUT THE FILE!");

            if (!FileUtils.IsFileReadyToRead(fileInfo))
                throw new Exception("INPUT FILE IS OPEN OR USED BY ANOTHER PROCESS! PLEASE SAVE AND CLOSE THE INPUT FILE!");

            var result = ExcelUtils.ReadExcel<T>(fileInfo).Result;
            return result;
        }

        public static void WriteToOutputExcelFile<T>(List<T> itemDataList, FileInfo outputFileInfo, string sheetName)
        {
            ExcelUtils.WriteToExcel(itemDataList, outputFileInfo, sheetName).Wait();
        }

        public static void WriteToOutputExcelFile<T>(List<List<T>> itemDataListList, FileInfo outputFileInfo, string sheetName)
        {
            Console.WriteLine($"WRITE DATA INTO OUTPUT FILE! Filename: '{outputFileInfo.Name}'; Directory: '{outputFileInfo.Directory}'");
            ExcelUtils.WriteToExcel(itemDataListList, outputFileInfo, sheetName).Wait();
        }

        public static void WriteToOutputExcelFile<T>(Dictionary<string, List<T>> itemDataDictionary, FileInfo outputFileInfo)
        {
            Console.WriteLine($"WRITE DATA INTO OUTPUT FILE! Filename: '{outputFileInfo.Name}'; Directory: '{outputFileInfo.Directory}'");
            ExcelUtils.WriteToExcel(itemDataDictionary, outputFileInfo).Wait();
        }
    }
}