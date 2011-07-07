using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medius.Model;
using System.IO;

namespace Medius.Controllers
{
    public class FilePersistenceController
    {
        public void WriteSupportFile(ISupportFile file, Stream stream)
        {
            switch (file.FileType)
            {
                case SupportFileType.Text:
                    WriteSupportFile(file as TextFile, stream);
                    break;
                case SupportFileType.Binary:
                    WriteSupportFile(file as BinaryFile, stream);
                    break;
            }
        }

        public virtual void WriteSupportFile(TextFile file, Stream stream)
        {
            using (StreamWriter writer = new StreamWriter(stream, new UnicodeEncoding()))
            {
                writer.Write(file.Data);
            }
        }

        public virtual void WriteSupportFile(BinaryFile file, Stream stream)
        {
            stream.Write(file.Data, 0, file.Data.Length);
        }
    }
}
