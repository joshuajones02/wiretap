namespace WireTap
{
    using System;
    using System.Linq;

    public static class TypeExtensions
    {
        public static string GetFullNameFormatted(this Type type, bool isRoot = true)
        {
            if (!type.IsGenericType)
                return isRoot ? type.FullName : type.Name;

            var definition = type.GetGenericTypeDefinition();
            var name = isRoot ? definition.FullName : definition.Name;
            name = name.Substring(0, name.IndexOf('`'));
            var genericTypes = type.GetGenericArguments()
                                   .Select(ta => ta.GetFullNameFormatted(false))
                                   .ToArray();
            var genericTypeNames = string.Join(", ", genericTypes);

            return string.Format("{0}<{1}>", name, genericTypeNames);
        }
    }
}