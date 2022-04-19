using System;
using System.Linq;
using AlphaWeb.Core.Extensions;

namespace AlphaWeb.Core.Common
{
    public static class EnumParser
    {
        public static T GetEnumValue<T>(string enumName)
        {
            //TODO GETSON refactoring and testing
            var values = Enum.GetNames(typeof(T));

            var selected = values.FirstOrDefault(value => value.EqualsIgnoreCase(enumName));

            if (selected == null)
                throw new ArgumentException($"Cannot Parse value '{enumName}' in {typeof(T).FullName} ");

            return (T)Enum.Parse(typeof(T), selected);

        }
    }
}
