using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinqToSqlAttributesCollector.Implementation
{
    public class AttributesSummary
    {
        private readonly HashSet<string> classAttributes = new HashSet<string>(); 
        private readonly HashSet<string> classAttributesParamters = new HashSet<string>(); 
        private readonly HashSet<string> propertyAttributes = new HashSet<string>(); 
        private readonly HashSet<string> propertyAttributesParameters = new HashSet<string>(); 

        public void AddClassAttributeNames(string[] names)
        {
            lock (classAttributes)
            {
                names.Aggregate(classAttributes, (current, name) =>
                {
                    current.Add(name);
                    return current;
                });
            }
        }

        public void AddClassAttributeParamtersNames(string[] names)
        {
            lock (classAttributesParamters)
            {
                names.Aggregate(classAttributesParamters, (current, name) =>
                {
                    current.Add(name);
                    return current;
                });
            }
        }

        public void AddPropertyAttributeNames(string[] names)
        {
            lock (propertyAttributes)
            {
                names.Aggregate(propertyAttributes, (current, name) =>
                {
                    current.Add(name);
                    return current;
                });
            }
        }

        public void AddPropertyAttributeParametersNames(string[] names)
        {
            lock (propertyAttributesParameters)
            {
                names.Aggregate(propertyAttributesParameters, (current, name) =>
                {
                    current.Add(name);
                    return current;
                });
            }
        }

        public override string ToString()
        {
            var classAttributesString = string.Join(", ", classAttributes);
            var classAttributesParamtersString = string.Join(", ", classAttributesParamters);
            var propertyAttributesString = string.Join(", ", propertyAttributes);
            var propertyAttributesParametersString = string.Join(", ", propertyAttributesParameters);

            var stringBuilder = new StringBuilder();
            stringBuilder.Append("\nClassAttributes: ");
            stringBuilder.Append(classAttributesString);
            stringBuilder.Append("\nClassAttributeParamters: ");
            stringBuilder.Append(classAttributesParamtersString);

            stringBuilder.Append("\n\n");
            stringBuilder.Append("PropertyAttributes: ");
            stringBuilder.Append(propertyAttributesString);
            stringBuilder.Append("\nPropertyAttributeParamters: ");
            stringBuilder.Append(propertyAttributesParametersString);
            stringBuilder.Append("\n");

            return stringBuilder.ToString();
        }
    }
}