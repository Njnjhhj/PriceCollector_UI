using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using PriceCollector.CollectorStgPro.Base;
using PriceCollector.CollectorStgPro.Configuration;
using PriceCollector.CollectorStgPro.DataModels;
using PriceCollector.CollectorStgPro.Flows;
using PriceCollector.Core.Enums;
using PriceCollector.Core.Helpers;
using PriceCollector.Core.Utils;

namespace PriceCollector.CollectorStgPro
{
    public class Executor_Stg : BaseCollectorStgProUi
    {
        private LoginFlow _loginFlow;
        private ResultTableFlow _resultTableFlow;

        private List<string> _inputProductIdList = new();
        private FileInfo _outputFileInfo;
        private string _outputFileName = string.Empty;
        private string _outputFileNamePrefix = EnvironmentData.OutputFileNamePrefix;
        private Dictionary<string, List<StgItemData>> _itemDataDict = new();
        private string _dateTimeFormat = "_yyyyMMdd_HHmmss";

        [OneTimeSetUp]
        public void Before()
        {
            // Flows
            _loginFlow = new LoginFlow(driver);
            _resultTableFlow = new ResultTableFlow(driver);

            // General variables and actions
            FileUtils.CreateDirectoryIfNotExist(EnvironmentData.OutputDirectory);
            var inputFileInfo = FileUtils.CreateFileInfo(EnvironmentData.InputFileName, FileExtensions.Xlsx, EnvironmentData.InputDirectory);
            _inputProductIdList = ExcelHelper.ReadInputFile<StgItemData>(inputFileInfo).Select(x => x.ProductId).ToList();
        }

        [OneTimeTearDown]
        public void After()
        {
            _outputFileInfo = FileUtils.CreateFileInfo(_outputFileName, FileExtensions.Xlsx, EnvironmentData.OutputDirectory);
            ExcelHelper.WriteToOutputExcelFile(_itemDataDict, _outputFileInfo);
        }

        [SetUp]
        public void Setup()
        {
            OpenWebPage();
        }


        [Test]
        public void StgPro_Collector()
        {
            var resultItemsDataList = new List<StgItemData>();

            try
            {
                _loginFlow.Login(Credentials);

                resultItemsDataList = _resultTableFlow.GetItemsDataList(_inputProductIdList);

                _itemDataDict.Add("Stg", resultItemsDataList);
            }
            catch (Exception e)
            {
                _outputFileName = string.Concat(_outputFileNamePrefix, "Partial", DateTime.Now.ToString(_dateTimeFormat));
                Console.WriteLine($"!!! ERROR OCCURRED DURING WORK WITH WEBSITE. Error: {e} ");
                throw;
            }
            finally
            {
                PrintResultsToConsole(resultItemsDataList, Separator.Semicolon);
            }

            _outputFileName = string.Concat(_outputFileNamePrefix, DateTime.Now.ToString(_dateTimeFormat));
        }
    }
}