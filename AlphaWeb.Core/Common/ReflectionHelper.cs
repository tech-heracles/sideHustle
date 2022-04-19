using System;
using System.Linq;
using System.Reflection;

namespace AlphaWeb.Core.Common
{
    public static class ReflectionHelper
    {
        public static T[] GetInterfaces<T>(Assembly assembly, Func<Type, bool> predicate)
        {
            //TODO testing
            if (assembly == null) return new T[0];

            return assembly.GetTypes()
                .Where(type => type.GetInterfaces().Any(predicate))
                .Select(type => (T)Activator.CreateInstance(type))
                .ToArray();
        }
        public static T[] GetTypes<T>(Assembly assembly, Func<Type, bool> predicate)
        {
            return assembly.GetTypes()
                           .Where(predicate)
                           .Select(type => (T)Activator.CreateInstance(type))
                           .ToArray();
        }
    }
}
