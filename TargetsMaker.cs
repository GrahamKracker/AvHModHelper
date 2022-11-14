namespace AvHModHelper;

using System;
using System.IO;
using System.Reflection;

internal static class TargetsMaker
{
    
        public static void CreateTargetsFile(string path)
        {
            if (string.IsNullOrEmpty(path)) {MelonLogger.Msg("String is empty");return;}
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception)
                {
                    return;
                }
            }
            var targets = Path.Combine(path, "AvH.targets");
            using var fs = new StreamWriter(targets);
            using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("AvHModHelper.AvH.targets");
            using var reader = new StreamReader(stream!);
            var text = reader.ReadToEnd().Replace(@"YourAvHFolderWITHOUTTRAILINGSLASH", MelonUtils.GameDirectory);
            fs.Write(text);
        }
}
