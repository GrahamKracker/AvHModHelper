using System.IO;
using System.Reflection;

namespace AvHModHelper.Extensions;

/// <summary>
///     Extensions for AvHMods
/// </summary>
internal static class AvHModExt
{
    /// <summary>
    ///     Get the name of this mod from the dll name
    /// </summary>
    public static string GetModName(this AvHMod AvHMod)
    {
        return AvHMod.GetAssembly()?.GetName().Name ?? AvHMod.Info.Name;
    }

    /// <summary>
    ///     Get the personal mod directory for this specific mod. Useful for keeping this mod's files seperate from other
    ///     mods."
    /// </summary>
    /// <param name="AvHMod"></param>
    /// <returns></returns>
    public static string GetModDirectory(this AvHMod AvHMod)
    {
        return Path.Combine(MelonHandler.ModsDirectory, AvHMod.GetModName());
    }

    /// <summary>
    ///     Get the personal mod directory for this specific mod. Useful for keeping this mod's files seperate from other
    ///     mods."
    /// </summary>
    /// <param name="AvHMod"></param>
    /// <param name="createIfNotExists">Create the mod's directory if it doesn't exist yet?</param>
    /// <returns></returns>
    public static string GetModDirectory(this AvHMod AvHMod, bool createIfNotExists)
    {
        var path = $"{MelonHandler.ModsDirectory}\\{AvHMod.GetModName()}";
        if (createIfNotExists) Directory.CreateDirectory(path);
        return path;
    }


    internal static Assembly GetAssembly(this MelonMod mod)
    {
        return mod.MelonAssembly.Assembly;
    }
}