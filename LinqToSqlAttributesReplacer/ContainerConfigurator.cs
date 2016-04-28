using LightInject;
using LinqToSqlAttributesCommon.Container;
using LinqToSqlAttributesCommon.Interfaces;

namespace LinqToSqlAttributesReplacer
{
    public class ContainerConfigurator : ContainerConfiguratorBase
    {
        public override void Configure(ServiceContainer container)
        {
            base.Configure(container);

            container.Register<IDocumentProcessor, DocumentProcessor>();
        }
    }
}