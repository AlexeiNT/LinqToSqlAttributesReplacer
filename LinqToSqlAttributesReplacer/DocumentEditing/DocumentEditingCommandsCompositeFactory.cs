using System.Linq;
using Microsoft.CodeAnalysis;

namespace LinqToSqlAttributesReplacer.DocumentEditing
{
    public class DocumentEditingCommandsCompositeFactory : IDocumentEditingCommandsCompositeFactory
    {
        private readonly IDocumentEditingConcreteFactory[] documentEditingConcreteFactories;

        public DocumentEditingCommandsCompositeFactory(IDocumentEditingConcreteFactory[] documentEditingConcreteFactories)
        {
            this.documentEditingConcreteFactories = documentEditingConcreteFactories;
        }

        public IDocumentEditingCommand[] Create(SyntaxNode documentSyntax)
        {
            var factories = documentEditingConcreteFactories.Where(x => x.CanCreate(documentSyntax));
            return factories.SelectMany(x => x.Create(documentSyntax)).ToArray();
        }
    }
}