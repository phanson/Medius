using System.IO;
using System.Linq;
using ICSharpCode.SharpZipLib.Zip;
using Medius.Model;

namespace Medius.Controllers
{
    public class ProjectPersistenceController : IProjectPersistenceController
    {
        IBookPersistenceController bookLoader = new XmlPersistenceController();
        FilePersistenceController fileController = new FilePersistenceController();

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
                        else if (Util.Helpers.TextFileExtensions.Contains(Path.GetExtension(entry.Name)))
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
                    fileController.WriteSupportFile(file, zipStream);
                    zipStream.CloseEntry();
                }
            }
        }
    }
}
