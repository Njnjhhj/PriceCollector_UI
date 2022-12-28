using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using PriceCollector.Collector.Base;
using PriceCollector.Collector.Configuration;
using PriceCollector.Collector.DataModels;
using PriceCollector.Collector.Dictionaries;
using PriceCollector.Collector.Enums;
using PriceCollector.Collector.Facades;
using PriceCollector.Core.Enums;
using PriceCollector.Core.Extensions;
using PriceCollector.Core.Helpers;
using PriceCollector.Core.Utils;

namespace PriceCollector.Collector
{
    [TestFixture]
    public class Executor_PriceCollector : BaseCollectorUi
    {
        private PriceCollectorFacade _testFacade;

        private List<string> _inputProductIdList = new();
        private string _outputFileNamePrefix = EnvironmentData.OutputFileNamePrefix;
        private string _outputFileName = string.Empty;
        private DateTime _dateTimeNow = DateTime.Now;
        private FileInfo _outputFileInfo;
        private Dictionary<string, List<ItemData>> _itemDataDict = new();


        [OneTimeSetUp]
        public void Before()
        {
            // Facade
            _testFacade = new PriceCollectorFacade();

            // General variables and actions
            FileUtils.CreateDirectoryIfNotExist(EnvironmentData.OutputDirectory);

            var inputFileInfo = FileUtils.CreateFileInfo(EnvironmentData.InputFileName, FileExtensions.Xlsx, EnvironmentData.InputDirectory);

            _inputProductIdList = ExcelHelper.ReadInputFile<ItemData>(inputFileInfo).Select(x => x.Id).ToList();
        }

        [OneTimeTearDown]
        public void After()
        {
            _outputFileInfo = FileUtils.CreateFileInfo(_outputFileName, FileExtensions.Xlsx, EnvironmentData.OutputDirectory);
            ExcelHelper.WriteToOutputExcelFile(_itemDataDict, _outputFileInfo);
        }

        [Test]
        public void Dmlights_collector()
        {
            var siteName = "Dmlights";

            TestMethod(siteName);
        }

        [Test]
        public void Tecshop_collector()
        {
            var siteName = "Tecshop";

            TestMethod(siteName);
        }

        [Test]
        public void Webshop_collector()
        {
            var siteName = "Webshop";

            TestMethod(siteName);
        }

        [Test]
        public void Solyd_collector()
        {
            var siteName = "Solyd";

            TestMethod(siteName);
        }

        [Test]
        public void Gigatek_collector()
        {
            var siteName = "Gigatek";

            TestMethod(siteName);
        }

        [Test]
        public void Semmatec_collector()
        {
            var siteName = "Semmatec";

            TestMethod(siteName);
        }

        [Test]
        public void Groothandel_collector()
        {
            var siteName = "Groothandel";

            TestMethod(siteName);
        }

        [Test]
        public void Omnielectric_collector()
        {
            var siteName = "Omnielectric";

            TestMethod(siteName);
        }

        [Test]
        public void Zelektro_collector()
        {
            var siteName = "Zelektro";

            TestMethod(siteName, TimeSpan.FromSeconds(2));
        }

        [Test]
        public void MyElectro_collector() //INFO: The site has scanning protection. The pause between requests is 3 seconds.
        {
            var siteName = "MyElectro";
            var delayInSeconds = 3;

            TestMethod(siteName, delayBetweenRequestsInSeconds: TimeSpan.FromSeconds(delayInSeconds));
        }

        [Test]
        public void All_Pages_collector() //INFO: Website 'My-Electro' excluded from general list. Run 'MyElectro_collector' separately!
        {
            var WEBSITES_TO_COLLECT = new List<WebsitesEnum>()
            {
                WebsitesEnum.Dmlights,
                WebsitesEnum.Tecshop,
                WebsitesEnum.Webshop,
                WebsitesEnum.Solyd,
                WebsitesEnum.Gigatek,
                WebsitesEnum.Semmatec,
                WebsitesEnum.Groothandel,
                WebsitesEnum.Omnielectric,
                WebsitesEnum.Zelektro,
                //WebsitesEnum.MyElectro
            };

            var siteName = default(string);

            try
            {
                foreach (var website in Websites)
                {
                    siteName = website.Name;

                    var websitesCollectionList = WEBSITES_TO_COLLECT.Select(x => x.GetDescription()).ToList();

                    if (!websitesCollectionList.Contains(siteName))
                        continue;

                    TestMethod(siteName);
                }
            }
            catch (Exception e)
            {
                _outputFileName = _testFacade.FileNameCreator(_outputFileNamePrefix, _dateTimeNow, "Partial");
                Console.WriteLine($"!!! ERROR OCCURRED DURING WORK WITH WEBSITE '{siteName}'. Error: {e} ");
                throw;
            }

            _outputFileName = _testFacade.FileNameCreator(_outputFileNamePrefix, _dateTimeNow, "AllSites");
        }


        private void TestMethod(string siteName, TimeSpan delayAfterPageOpened = new(), TimeSpan delayBetweenRequestsInSeconds = new())
        {
            try
            {
                var pageFlow = PageFlowDictionary.PageFlowCreator[siteName](driver);

                OpenWebPage(siteName);

                MethodUtils.Wait(delayAfterPageOpened);

                _testFacade.SinglePageTestMethod1(siteName, pageFlow, _inputProductIdList, _itemDataDict, delayBetweenRequestsInSeconds);

                _outputFileName = _testFacade.FileNameCreator(_outputFileNamePrefix, _dateTimeNow, siteName);
            }
            catch (Exception e)
            {
                _outputFileName = _testFacade.FileNameCreator(_outputFileNamePrefix, _dateTimeNow, siteName + "_Partial");
                Console.WriteLine($"!!! ERROR OCCURRED DURING WORK WITH WEBSITE '{siteName}'. Error: {e} ");
                throw;
            }
        }
    }
}
