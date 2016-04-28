using LightInject;

namespace LinqToSqlAttributesCommon.Container
{
    public abstract class ContainerConfiguratorBase
    {
        public virtual void Configure(ServiceContainer container)
        {
            container.Register<ISolutionProcessor, SolutionProcessor>();
        }
    }
}