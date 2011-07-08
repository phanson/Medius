using System.IO;
using System.Linq;
using System.Reflection;
using Medius.Model;
using Medius.Util;

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
                    XmlTransformer.Transform(xslt, wpFile, output);
                }

                // rewind and validate
                output.Seek(0, SeekOrigin.Begin);
                using (Stream xsd = Assembly.GetExecutingAssembly().GetManifestResourceStream("Medius.Model.book.xsd"))
                {
                    XmlValidator validator = new XmlValidator();
                    if (!validator.Validate(xsd, output))
                    {
                        throw new InvalidDataException(
                        "Could not load book XML.\r\nCause(s):\r\n" +
                            string.Join("\r\n", validator.Errors.Select(
                                e => string.Format(
                                    "Line {0} Col {1}: {2}", e.Line, e.Column, e.Message
                                    )
                                )
                            )
                        );
                    }
                }

                // rewind again and deserialize
                output.Seek(0, SeekOrigin.Begin);
                book = books.Load(output);
            }

            Project project = new Project();
            project.Book = book;
            return project;
        }
    }
}
