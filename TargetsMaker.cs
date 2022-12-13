using System;
using System.IO;
using AvHModHelper.Extensions;

namespace AvHModHelper;

internal static class TargetsMaker
{
    
        public static void CreateTargetsFile(string path)
        {
            if (string.IsNullOrEmpty(path) || path == "YourAvHFolderWITHOUTTRAILINGSLASH") {MelonLogger.Msg("String is empty");return;}
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
            using var stream = MelonAssembly.FindMelonInstance<Main>().GetAssembly().GetManifestResourceStream("AvHModHelper.AvH.targets");
            using var reader = new StreamReader(stream!);
            var text = reader.ReadToEnd().Replace(@"YourAvHFolderWITHOUTTRAILINGSLASH", MelonUtils.GameDirectory);
            fs.Write(text);
        }
}
