using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Ganss.Excel;
using Ganss.Excel.Exceptions;
using PriceCollector.Core.Enums;
using PriceCollector.Core.Extensions;

namespace PriceCollector.Core.Utils
{
    /// <summary>
    /// For more info go here: https://github.com/mganss/ExcelMapper 
    /// </summary>
    public class ExcelUtils
    {
        public static async Task WriteToExcel<T>(List<T> objects, FileInfo file, string sheetName)
        {
            try
            {
                var excel = new ExcelMapper();
                await excel.SaveAsync(file.FullName, objects, sheetName);
                Task.WaitAll();
            }
            catch (Exception ex)
            {
                throw new ExcelMapperConvertException($"Error when writing objects of {typeof(T).FullName} to file {file.FullName}: {ex}");
            }
        }

        public static async Task WriteToExcel<T>(Dictionary<string, List<T>> itemDataDictionary, FileInfo file)
        {
            try
            {
                var excel = new ExcelMapper();

                foreach (var itemData in itemDataDictionary)
                {
                    await excel.SaveAsync(file.FullName, itemData.Value, itemData.Key);
                }

                Task.WaitAll();
            }
            catch (Exception ex)
            {
                throw new ExcelMapperConvertException($"Error when writing objects of {typeof(T).FullName} to file {file.FullName}: {ex}");
            }
        }

        public static async Task WriteToExcel<T>(List<List<T>> objects, FileInfo file, string sheetName)
        {
            try
            {
                int i = 0;
                var excel = new ExcelMapper();
                foreach (var sheetNamesList in objects)
                {
                    i++;
                    await excel.SaveAsync(file.FullName, sheetNamesList, sheetName + i);
                    Task.WaitAll();
                }
                
            }
            catch (Exception ex)
            {
                throw new ExcelMapperConvertException($"Error when writing objects of {typeof(T).FullName} to file {file.FullName}: {ex}");
            }
        }

        public static async Task<List<T>> ReadExcel<T>(FileInfo file)
        {
            if (!file.Name.EndsWith(FileExtensions.Xlsx.GetDescription()))
                throw new FileLoadException($"Could not recognize file as Excel file '{file.FullName}'");

            try
            {
                var excel = new ExcelMapper();
                var results = (await excel.FetchAsync<T>(file.FullName)).ToList();
                return results;
            }
            catch (Exception ex)
            {
                throw new ExcelMapperConvertException($"Error when reading objects of {typeof(T).FullName} from file {file.FullName}: {ex}");
            }
        }
    }
}