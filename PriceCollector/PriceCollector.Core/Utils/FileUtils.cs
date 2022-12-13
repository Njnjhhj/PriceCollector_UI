using System;
using System.IO;
using System.Linq;
using System.Net;
using PriceCollector.Core.Enums;
using PriceCollector.Core.Extensions;
using Path = System.IO.Path;

namespace PriceCollector.Core.Utils
{
    public class FileUtils
    {
        // ReSharper disable once UnusedMember.Global
        // ReSharper disable once InconsistentNaming
        public static int MAX_LENGTH_WINDOWS = byte.MaxValue; //255 - 260

        public static FileInfo CreateFileInfo(string fileName, FileExtensions fileExtension, string directory)
        {
            return CreateFileInfo(fileName + $".{fileExtension.GetDescription()}", directory);
        }

        public static FileInfo CreateFileInfo(string fileName, string directory)
        {
            var fullFileName = CombineFilePath(directory, fileName);
            var file = new FileInfo(fullFileName);
            return file;
        }

        public static bool IsFileReadyToRead(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
                return true;
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return false;
            }
            finally
            {
                stream?.Close();
            }
        }

        public static string Read(string fullFileName)
        {
            if (!File.Exists(fullFileName))
                throw new FileNotFoundException($"File '{fullFileName}' could not be found");
            return File.ReadAllText(fullFileName);
        }

        public static void SaveByteArrayToFile(byte[] byteArray, string filePath)
        {
            var directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory))
            {
                CreateDirectory(directory);
            }
            using var fs = new FileStream(filePath, FileMode.OpenOrCreate);
            fs.Write(byteArray, 0, byteArray.Length);
        }

        public static void Write(FileInfo file, string content)
        {
            File.WriteAllText(file.FullName, content);
        }

        public static void Write(string fileFullPath, string content)
        {
            File.WriteAllText(fileFullPath, content);
        }

        public static void DeleteFileIfExists(FileInfo file)
        {
            if (file.Exists)
                file.Delete();
        }

        public static void DeleteFileIfExists(string filePath)
        {
            if (File.Exists(filePath))
                File.Delete(filePath);
        }

        public static DirectoryInfo CreateDirectoryIfNotExist(string directoryName)
        {
            var directoryExists = Directory.Exists(directoryName);
            return directoryExists
                ? new DirectoryInfo(directoryName)
                : CreateDirectory(directoryName);
        }

        public static DirectoryInfo CreateDirectory(string directoryName)
        {
            return Directory.CreateDirectory(directoryName);
        }

        public static string CombineFilePath(string directoryFullName, string fileName)
        {
            var path = Path.Combine(directoryFullName, fileName);
            return path;
        }

        public static string RemoveForbiddenSymbols(string name)
        {
            return Path.GetInvalidFileNameChars().Aggregate(name, (symbol, s) => symbol.Replace(s.ToString(), string.Empty));
        }

        private static void DownloadFileFromUrlAs(Uri url, FileInfo file)
        {
            using var client = new WebClient();
            client.DownloadFile(url, file.FullName);
        }
    }
}
