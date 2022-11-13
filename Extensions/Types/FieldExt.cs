namespace AvHModHelper.Extensions.Types;

using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public static class FieldExtensions
{
    public static void SetPrivateValue<T>(this T obj, string name, object value)
    {
        obj!.GetType().GetField(name, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public)!.SetValue(obj, value);
    }

    public static FieldInfo GetFieldInfo<T>(this T obj, string field)
    {
        return obj!.GetType().GetField(field, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public)!;
    }

    public static object GetPrivateValue<T>(this T obj, string name)
    {
        return obj!.GetType().GetField(name, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public)!.GetValue(obj);
    }

    public static FieldInfo[] GetAllFields<T>(this T obj)
    {
        return obj!.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
    }

    public static PropertyInfo[] GetAllProperties<T>(this T obj)
    {
        return obj!.GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
    }

    public static List<FieldInfo> GetAllRuntimeFields<T>(this T obj)
    {
        return obj!.GetType().GetRuntimeFields().ToList();
    }
}
