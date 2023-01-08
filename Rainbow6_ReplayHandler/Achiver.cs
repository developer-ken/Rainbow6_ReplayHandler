using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using libR6R;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rainbow6_ReplayHandler
{
    internal class Achiver
    {
        FileStream fsOut;
        ZipOutputStream zipStream;
        public Achiver(string savepath)
        {
            fsOut = File.Create(savepath);
            zipStream = new ZipOutputStream(fsOut);
        }

        public void AddFile(string path, string subdir_in_achive = "")
        {
            FileInfo fi = new FileInfo(path);
            string entryName = Path.GetFileName(path);
            ZipEntry newEntry = new ZipEntry(Path.Combine(subdir_in_achive, entryName));
            newEntry.CompressionMethod = CompressionMethod.Deflated;
            newEntry.DateTime = fi.LastWriteTime;
            newEntry.Size = fi.Length;
            zipStream.PutNextEntry(newEntry);
            byte[] buffer = new byte[4096];
            using (FileStream streamReader = File.OpenRead(path))
            {
                StreamUtils.Copy(streamReader, zipStream, buffer);
            }
            zipStream.CloseEntry();
        }

        public void Finish()
        {
            zipStream.IsStreamOwner = false;
            zipStream.Finish();
            zipStream.Close();
            fsOut.Flush();
            fsOut.Close();
        }
    }
}
