using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace LinqToSqlAttributesCollector.Implementation
{
    public static class RoslynExtensions
    {
        public static AttributeSyntax[] SelectAttributes(this PropertyDeclarationSyntax property, params string[] attributeNames)
        {
            return property.AttributeLists.SelectAttributes(attributeNames);

        }

        public static AttributeSyntax[] SelectAttributes(this ClassDeclarationSyntax @class, params string[] attributeNames)
        {
            return @class.AttributeLists.SelectAttributes(attributeNames);
        }

        public static AttributeSyntax[] SelectAttributes(this SyntaxList<AttributeListSyntax> attributeLists, params string[] attributeNames)
        {
            var query = attributeLists.SelectMany(x => x.Attributes);

            if (attributeNames.Length > 0)
            {
                query = query.Where(x => attributeNames.Contains(x.Name.ToString()));
            }

            return query.ToArray();
        }

        public static bool HasAttribute(this PropertyDeclarationSyntax property, string attributeName)
        {
            var propertyAttributes = property.SelectAttributes(attributeName);
            return propertyAttributes.Length > 0;
        }

        public static bool HasAttribute(this ClassDeclarationSyntax @class, string attributeName)
        {
            var propertyAttributes = @class.SelectAttributes(attributeName);
            return propertyAttributes.Length > 0;
        }

        public static AttributeArgumentSyntax[] SelectParameters(this AttributeSyntax attributes)
        {
            return attributes.ArgumentList.Arguments.ToArray();
        }
    }
}