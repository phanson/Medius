using System.IO;
using Medius.Model;

namespace Medius.Controllers
{
    public interface IBookPersistenceController
    {
        /// <summary>
        /// Deserializes a Book object from a stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>Deserialized object, or <c>null</c> on error.</returns>
        Book Load(Stream stream);

        /// <summary>
        /// Serializes the given book object to a stream.
        /// </summary>
        /// <param name="book">Object to serialize.</param>
        /// <param name="stream">The stream.</param>
        void Save(Book book, Stream stream);
    }
}
