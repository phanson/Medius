using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Medius.Model;
using System.Reflection;

namespace Medius.Controllers
{
    public class WordpressImportController : IImportController
    {
        IBookPersistenceController books;

        public WordpressImportController(IBookPersistenceController books)
        {
            this.books = books;
        }

        public Project Import(string inputFilename)
        {
            Book book;
            using (Stream output = new MemoryStream())
            {
                // read WP XML file
                using (Stream wpFile = new FileStream(inputFilename, FileMode.Open))
                using (Stream xslt = Assembly.GetExecutingAssembly().GetManifestResourceStream("Medius.Controllers.wp2book.xsl"))
                {
                    // transform stream to stream
                    Util.XmlTransformer.Transform(xslt, wpFile, output);
                }

                output.Seek(0, SeekOrigin.Begin);
                book = books.Load(output);
            }

            Project project = new Project();
            project.Book = book;
            return project;
        }
    }
}
