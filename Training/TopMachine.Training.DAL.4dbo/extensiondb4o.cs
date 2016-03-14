using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Db4objects.Db4o.Config;

namespace Core.DB.ExtensionPoints
{
    /// <summary>
    /// Indicate that this property should be indexed
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public sealed class IndexedAttribute : Attribute
    {
    }

    public static class DBSchemaUtility
    {
        private const string BackingfieldPostFix = "k__BackingField";

        public static void IndexClass(this ICommonConfiguration config, Type type)
        {
            var propertiesToIndex = from p in type.GetProperties()
                                    where 0 < p.GetCustomAttributes(typeof(IndexedAttribute), true).Length
                                    select p;
            foreach (var property in propertiesToIndex)
            {
                var fieldName = "<" + property.Name + ">" + BackingfieldPostFix;
                RaiseExceptionIfFieldIsMissing(fieldName, property.Name, type);
                config.ObjectClass(type).ObjectField(fieldName).Indexed(true);
            }
        }

        private static void RaiseExceptionIfFieldIsMissing(string fieldName, string name, Type type)
        {
            if (null == type.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance))
            {
                throw new ArgumentException(
                    String.Format(CultureInfo.InvariantCulture,
                                  @"The Property '{0}' is marked with the attribute '{1}'. However it seams that this property isn't an auto-property, because the field {2} couldn't be found",
                                  name, typeof(IndexedAttribute).Name, fieldName));
            }
        }
    }
}
