using System.Linq;
using LinqToSqlAttributesCommon.Helpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace LinqToSqlAttributesReplacer
{
    public class TableAtributeConverter
    {
        public AttributeSyntax Convert(AttributeSyntax linqAttribute)
        {
            var tableName = linqAttribute.ArgumentList
                .Arguments
                .First(x => x.NameEquals.Name.ToString() == AttributeArgumentName.TableName)
                .Expression.ChildTokens().First().ValueText;

            return Create(tableName);
        }

        private static AttributeSyntax Create(string tableName)
        {
            var attributeArgumentListSyntax = SyntaxFactory.AttributeArgumentList(
                SyntaxFactory.SeparatedList(new[]
                {
                    SyntaxFactory.AttributeArgument(
                        SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression,
                            SyntaxFactory.Literal(tableName)))
                }));

            return  SyntaxFactory.Attribute(SyntaxFactory.IdentifierName(AttributeName.Table), attributeArgumentListSyntax);
        }
    }
}