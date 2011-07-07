using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Medius.Model;
using System.IO;
using System.Reflection;
using Medius.Util;

namespace Medius.Controllers
{
    public class XmlPersistenceController : IBookPersistenceController
    {
        public Book Load(Stream stream)
        {
            // attempt validation if we are sure it won't interfere
            if (stream.CanSeek)
            {
                // validate the
                Stream schema = Assembly.GetExecutingAssembly().GetManifestResourceStream("Medius.Model.book.xsd");
                var v = new XmlValidator();
                if (!v.Validate(schema, stream))
                {
                    throw new InvalidDataException(
                        "Could not load book XML.\r\nCause(s):\r\n" +
                            string.Join("\r\n", v.Errors.Select(
                                e => string.Format(
                                    "Line {0} Col {1}: {2}", e.Line, e.Column, e.Message
                                    )
                                )
                            )
                        );
                }

                // rewind the stream
                stream.Seek(0, SeekOrigin.Begin);
            }

            // I am aware that this is embarrasingly simple.
            // it's called code reuse.
            return (Book)new XmlSerializer(typeof(Book)).Deserialize(stream);
        }

        public void Save(Book book, Stream stream)
        {
            new XmlSerializer(typeof(Book)).Serialize(stream, book);
        }
    }
}
