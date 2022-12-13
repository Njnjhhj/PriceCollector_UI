using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;

namespace PriceCollector.Core.Utils
{
    public class JsonReader
    {
        private readonly string _directory = Environment.CurrentDirectory;
        private JObject _file;

        public JObject ReadFile(string filename)
        {
            using StreamReader reader = new StreamReader(Path.Combine(_directory, $"Configuration\\{filename}.json")); //C:\Work\sandbox\PriceCollector\PriceCollector\PriceCollector.Core\Configuration\Websites.json
            string json = reader.ReadToEnd();
            _file = JObject.Parse(json);
            return _file;
        }

        public T GetFileContent<T>(string rootObjectName, string filename) where T : class
        {
            _file = ReadFile(filename);
            return _file[rootObjectName]?.ToObject<T>();
        }

        public List<T> GetFileContentList<T>(string rootObjectName, string filename) where T : class
        {
            _file = ReadFile(filename);
            return _file[rootObjectName]?.ToObject<List<T>>();
        }
    }
}