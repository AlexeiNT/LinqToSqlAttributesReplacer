using System.Collections.Generic;
using LinqToSqlAttributesCommon.Extensions;
using LinqToSqlAttributesCommon.Helpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace LinqToSqlAttributesReplacer.DocumentEditing.Factories.PropertyAttributeConverters
{
    public class NameAndTypeAttributeParameterConverter : IAttributeParameterConverter
    {
        public AttributeSyntax Convert(AttributeSyntax attribute)
        {
            var columnName = attribute
                .FindArgument(AttributeArgumentName.ColumnName)
                ?.GetStringValue();

            var columnType = attribute
                .FindArgument(AttributeArgumentName.ColumnType)
                ?.GetStringValue();

            return CreateReplacement(columnName, columnType);
        }

        public int Priority => 0;

        private static AttributeSyntax CreateReplacement(string columnName, string columnType)
        {
            var attributesArguments = new List<AttributeArgumentSyntax>();
            if (columnName != null)
            {
                var columnNameAttribute = SyntaxFactory.AttributeArgument(
                    SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression,
                        SyntaxFactory.Literal(columnName)));

                attributesArguments.Add(columnNameAttribute);
            }

            if (columnType != null)
            {
                var columnNameAttribute = SyntaxFactory.AttributeArgument(
                    SyntaxFactory.AssignmentExpression(
                        SyntaxKind.SimpleAssignmentExpression,
                        SyntaxFactory.IdentifierName(AttributeArgumentName.EntityColumnType),
                        SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal(columnType))
                        ));

                attributesArguments.Add(columnNameAttribute);
            }

            var attributes = columnType != null
                ? SyntaxFactory.AttributeArgumentList(SyntaxFactory.SeparatedList(attributesArguments))
                : null;

            return SyntaxFactory.Attribute(SyntaxFactory.IdentifierName(AttributeName.Column), attributes);
        }
    }
}