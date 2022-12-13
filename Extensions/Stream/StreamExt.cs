// Decompiled with JetBrains decompiler
// Type: BTD_Mod_Helper.Extensions.StreamExt
// Assembly: BloonsTD6 Mod Helper, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 91164C6A-4FFC-42B0-8879-5334A2C9692D
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\BloonsTD6\Mods\Btd6ModHelper.dll
// XML documentation location: C:\Program Files (x86)\Steam\steamapps\common\BloonsTD6\Mods\Btd6ModHelper.xml

using System;
using System.IO;
using System.Linq;

namespace AvHModHelper.Extensions.Stream
{
    /// <summary>Extensions for streams</summary>
    public static class StreamExt
    {
        /// <summary>
        /// Synchronously gets the full array of bytes from any stream, disposing with the Stream afterwards
        /// </summary>
        public static byte[] GetByteArray(this System.IO.Stream? stream)
        {
            if (stream == null)
                return Enumerable.Empty<byte>().ToArray();
            try
            {
                using (stream)
                {
                    if (stream is MemoryStream memoryStream)
                        return memoryStream.ToArray();
                    MemoryStream destination;
                    using (destination = new MemoryStream())
                    {
                        stream.CopyTo(destination);
                        return destination.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<byte>().ToArray();
            }
        }
    }
}
