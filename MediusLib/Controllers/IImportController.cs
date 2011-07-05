using Medius.Model;

namespace Medius.Controllers
{
    public interface IImportController
    {
        /// <summary>
        /// Convert the given input file into a Medius project.
        /// </summary>
        /// <param name="inputFilename">Path to input file in application-specific format.</param>
        /// <returns><c>null</c> on failure.</returns>
        Project Import(string inputFilename);
    }
}
