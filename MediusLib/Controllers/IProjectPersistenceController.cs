using Medius.Model;

namespace Medius.Controllers
{
    public interface IProjectPersistenceController
    {
        /// <summary>
        /// Deserializes a <see cref="Project"/> object from a file.
        /// </summary>
        /// <param name="filename">Path to the file.</param>
        /// <returns>Deserialized object, or <c>null</c> on error.</returns>
        Project Load(string filename);

        /// <summary>
        /// Serializes the given <see cref="Project"/> object to a file.
        /// </summary>
        /// <param name="book">Object to serialize.</param>
        /// <param name="filename">Path to the file.</param>
        void Save(Project project, string filename);
    }
}
