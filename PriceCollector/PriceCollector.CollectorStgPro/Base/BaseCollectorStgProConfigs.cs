using NUnit.Framework;
using PriceCollector.CollectorStgPro.Configuration;
using PriceCollector.Core.Utils;

namespace PriceCollector.CollectorStgPro.Base
{
    [TestFixture]
    public class BaseCollectorStgProConfigs
    {
        private const string EnvironmentObjName = "Website";
        private const string CredentialsObjName = "Credentials";
        protected readonly JsonReader JsonReader = new();
        protected Websites Website;
        protected Credentials Credentials;


        [OneTimeSetUp]
        public void BeforeAll()
        {
            Website = JsonReader.GetFileContent<Websites>(EnvironmentObjName, "Website");
            Credentials = JsonReader.GetFileContent<Credentials>(CredentialsObjName, "Credentials");
        }
    }
}
