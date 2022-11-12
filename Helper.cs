namespace AvHModHelper;

using System;
using System.Collections.Generic;
using System.Linq;

internal static class Helper
{
    private static IEnumerable<AvHMod> mods;

    /// <summary>
    ///     Active mods that use ModHelper functionality
    /// </summary>
    public static IEnumerable<AvHMod> Mods =>
        mods ??= Melons.OfType<AvHMod>().OrderByDescending(mod => mod.Priority);

    /// <summary>
    ///     All active mods, whether they're Mod Helper or not
    /// </summary>
    public static IEnumerable<MelonMod> Melons => MelonBase.RegisteredMelons.OfType<MelonMod>();

    private static void PerformHook<T>(Action<T> action) where T : AvHMod
    {
        foreach (var mod in Mods.OfType<T>().OrderByDescending(mod => mod.Priority))
        {
            try
            {
                action.Invoke(mod);
            }
            catch (Exception e)
            {
                mod.LoggerInstance.Error(e);
            }
        }
    }

    internal static void PerformHook(Action<AvHMod> action)
    {
        PerformHook<AvHMod>(action);
    }
}
