using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Medius.Model;
using System.IO;

namespace Medius.Controllers
{
    public class XmlPersistenceController : IBookPersistenceController
    {
        public Book Load(Stream stream)
        {
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
