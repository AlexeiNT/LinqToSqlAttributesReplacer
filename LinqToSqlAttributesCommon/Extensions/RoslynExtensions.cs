using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace LinqToSqlAttributesCommon.Extensions
{
    public static class RoslynExtensions
    {
        public static AttributeSyntax[] SelectAttributes(this PropertyDeclarationSyntax property, params string[] attributeNames)
        {
            return property.AttributeLists.InnerSelectAttributes(attributeNames);

        }

        public static AttributeSyntax[] SelectAttributes(this ClassDeclarationSyntax @class, params string[] attributeNames)
        {
            return @class.AttributeLists.InnerSelectAttributes(attributeNames);
        }

        private static AttributeSyntax[] InnerSelectAttributes(this SyntaxList<AttributeListSyntax> attributeLists, params string[] attributeNames)
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
            var propertyAttributes = property.AttributeLists.InnerSelectAttributes(attributeName);
            return propertyAttributes.Length > 0;
        }

        public static bool HasAttribute(this ClassDeclarationSyntax @class, string attributeName)
        {
            var propertyAttributes = @class.AttributeLists.InnerSelectAttributes(attributeName);
            return propertyAttributes.Length > 0;
        }

        public static AttributeArgumentSyntax[] SelectParameters(this AttributeSyntax attributes)
        {
            return attributes.ArgumentList.Arguments.ToArray();
        }

        public static AttributeArgumentSyntax GetArgument(this AttributeSyntax attribute, string attributeName)
        {
            return attribute.ArgumentList.Arguments.Single(x => x.NameEquals.Name.ToString() == attributeName);
        }

        public static string GetStringValue(this AttributeArgumentSyntax argument)
        {
            return argument.Expression.ChildTokens().First().ValueText;
        }

        public static UsingDirectiveSyntax[] SelectUsingDirectives(this SyntaxNode root, params string[] namespaceNames)
        {
            var result = root.DescendantNodes().OfType<UsingDirectiveSyntax>();

            if (namespaceNames.Length > 0)
            {
                result = result.Where(x => namespaceNames.Contains(x.Name.ToString()));
            }

            return result.ToArray();
        }
    }
}