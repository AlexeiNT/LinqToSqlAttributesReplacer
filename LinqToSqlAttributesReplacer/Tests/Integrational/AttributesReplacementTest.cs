using LightInject;
using LinqToSqlAttributesCommon.Interfaces;
using LinqToSqlAttributesReplacer.Configuration;

namespace LinqToSqlAttributesReplacer.Tests.Integrational
{
    public class AttributesReplacementTest
    {
        private ServiceContainer serviceContainer;

        private const string linqToSqlEntity =
            "using System;\r\n" +
            "using System.Data.Linq.Mapping;\r\n" +
            "using SKBKontur.Billy.Billing.BusinessObjects.DocumentService;\r\n" +
            "using SKBKontur.Billy.Core.BusinessObjects.Mapping;\r\n" +
            "\r\n" +
            "namespace SKBKontur.Billy.Billing.DocumentsService.BusinessObjects.Storage\r\n" +
            "{\r\n" +
            "    [Table(Name = \"TestTable\")]\r\n" +
            "    [History(Flushable = true, KeyProperty = \"BillId\", TrackedProperties = new[] { \"Date\", \"UpToDate\", \"BillId\", \"IsDeleted\" }, RegularlyTrackedProperties = new[] { \"Number\", \"DocumentInfoId\" }, Name = \"Документы\", IsNeedEmptyLog = false, ObjectActualStateKey = \"Document\")]\r\n" +
            "    public class TestEntity\r\n" +
            "    {\r\n" +
            "        [Column(Name = \"EntityName\", DbType = \"bit\", IsPrimaryKey = true, CanBeNull = false, IsDbGenerated = true, UpdateCheck = UpdateCheck.WhenChanged, IsDiscriminator = true)]\r\n" +
            "        [HistoryProperty(\"A\")]\r\n" +
            "        public bool A { get; set; }\r\n" +
            "    }\r\n" +
            "}\r\n";

        private const string entityFrameworkEntity =
            "using System;\r\n" +
            "System.ComponentModel.DataAnnotations;\r\n" +
            "System.ComponentModel.DataAnnotations.Schema;\r\n" +
            "using SKBKontur.Billy.Billing.BusinessObjects.DocumentService;\r\n" +
            "using SKBKontur.Billy.Core.BusinessObjects.Mapping;\r\n" +
            "\r\n" +
            "namespace SKBKontur.Billy.Billing.DocumentsService.BusinessObjects.Storage\r\n" +
            "{\r\n" +
            "    [Table(\"TestTable\")]\r\n" +
            "    [History(Flushable = true, KeyProperty = \"BillId\", TrackedProperties = new[] { \"Date\", \"UpToDate\", \"BillId\", \"IsDeleted\" }, RegularlyTrackedProperties = new[] { \"Number\", \"DocumentInfoId\" }, Name = \"Документы\", IsNeedEmptyLog = false, ObjectActualStateKey = \"Document\")]\r\n" +
            "    public class TestEntity\r\n" +
            "    {\r\n" +
            "        [Column(\"EntityName\", TypeName = \"bit\")]\r\n" +
            "        [Key]\r\n" +
            "        [Required]\r\n" +
            "        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]\r\n" +
            "        [ConcurrencyCheck]\r\n" +
            "        [HistoryProperty(\"A\")]\r\n" +
            "        public bool A { get; set; }\r\n" +
            "    }\r\n" +
            "}\r\n";

        public void TestReplacement()
        {
            var documentProcessor = serviceContainer.GetInstance<IDocumentProcessor>();
            //TODO: There is a test will be
        }

        private void PrepareTestData()
        {
            serviceContainer = new ServiceContainer();
            var configurator = new ContainerConfigurator();
            configurator.Configure(serviceContainer);
        }
    }
}