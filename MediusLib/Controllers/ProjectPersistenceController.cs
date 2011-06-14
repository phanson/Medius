using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medius.Model;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;
using ICSharpCode.SharpZipLib.Core;

namespace Medius.Controllers
{
    public class ProjectPersistenceController : IProjectPersistenceController
    {
        IBookPersistenceController bookLoader = new XmlPersistenceController();

        protected readonly string[] textFile = { "txt", "xml", "css", "htm", "html", "tex", "bib" };

        public Project Load(string filename)
        {
            Project project = new Project();

            using (var zipStream = new ZipInputStream(new FileStream(filename, FileMode.Open)))
            {
                ZipEntry entry;
                while ((entry = zipStream.GetNextEntry()) != null)
                {
                    if (entry.IsFile)
                    {
                        if (Path.GetFileName(entry.Name).Equals("book.xml"))
                        {
                            // TODO: validate file
                            project.Book = bookLoader.Load(zipStream);
                        }
                        else if (textFile.Contains(Path.GetExtension(entry.Name)))
                        {
                            // text file
                            project.Files.Add(readTextSupportFile(zipStream, entry));
                        }
                        else
                        {
                            // binary file
                            project.Files.Add(readBinarySupportFile(zipStream, entry));
                        }
                    }
                }
            }

            return project;
        }

        private ISupportFile readBinarySupportFile(ZipInputStream zipStream, ZipEntry entry)
        {
            BinaryFile file = new BinaryFile();
            file.Filename = entry.Name;
            file.Data = new byte[entry.Size];
            zipStream.Read(file.Data, 0, file.Data.Length);

            return file;
        }

        private ISupportFile readTextSupportFile(ZipInputStream zipStream, ZipEntry entry)
        {
            TextFile file = new TextFile();
            file.Filename = entry.Name;
            using (StreamReader reader = new StreamReader(zipStream))
            {
                file.Data = reader.ReadToEnd();
            }
            return file;
        }

        public void Save(Project project, string filename)
        {
            using (var zipStream = new ZipOutputStream(new FileStream(filename, FileMode.Create)))
            {
                zipStream.PutNextEntry(new ZipEntry("book.xml"));
                bookLoader.Save(project.Book, zipStream);
                zipStream.CloseEntry();
                foreach (var file in project.Files)
                {
                    zipStream.PutNextEntry(new ZipEntry(file.Filename));
                    writeSupportFile(file, zipStream);
                    zipStream.CloseEntry();
                }
            }
        }

        protected void writeSupportFile(ISupportFile file, Stream stream)
        {
            switch (file.FileType)
            {
                case SupportFileType.Text:
                    writeSupportFile(file as TextFile, stream);
                    break;
                case SupportFileType.Binary:
                    writeSupportFile(file as BinaryFile, stream);
                    break;
            }
        }

        protected virtual void writeSupportFile(TextFile file, Stream stream)
        {
            using (StreamWriter writer = new StreamWriter(stream, new UnicodeEncoding()))
            {
                writer.Write(file.Data);
            }
        }

        protected virtual void writeSupportFile(BinaryFile file, Stream stream)
        {
            stream.Write(file.Data, 0, file.Data.Length);
        }
    }
}
