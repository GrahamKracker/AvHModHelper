using System;
using AvHModHelper;

[assembly: MelonInfo(typeof(MainMod), "AvHModHelper", "1.0.0", "GrahamKracker")]
[assembly: MelonGame("Sayan", "Apes vs Helium")]
[assembly: MelonPriority(-1000)]
[assembly: MelonColor(ConsoleColor.DarkBlue)]
[assembly: MelonAuthorColor(ConsoleColor.DarkRed)]


namespace AvHModHelper;

using System.Reflection;
using UnityStandardAssets.Characters.FirstPerson;

internal class MainMod : AvHMod
{
    public override void OnApplicationStart()
    {
        MelonLogger.Msg("AvHModHelper Loaded");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (Input.GetKey(KeyCode.Space) && !(bool) EquipmentScript.instance.gameObject.GetComponent<FirstPersonController>().GetPrivateValue("m_Jumping")) EquipmentScript.instance.gameObject.GetComponent<FirstPersonController>().SetPrivateValue("m_Jump", true);
    }
}

internal static class TypeExtensions
{
    public static void SetPrivateValue<T>(this T obj, string name, object value)
    {
        obj.GetType().GetField(name, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public).SetValue(obj, value);
    }

    public static FieldInfo GetPrivateFieldInfo<T>(this T obj, string field)
    {
        return obj.GetType().GetField(field, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
    }

    public static object GetPrivateValue<T>(this T obj, string name)
    {
        return obj.GetType().GetField(name, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public).GetValue(obj);
    }
}