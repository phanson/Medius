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
                            project.Book = bookLoader.Load(zipStream);
                        }
                        //switch (Path.GetExtension(entry.Name))
                        //{
                        //    //
                        //}
                    }
                }
            }

            return project;
        }

        public void Save(Project project, string filename)
        {
            throw new NotImplementedException();
        }
    }
}
