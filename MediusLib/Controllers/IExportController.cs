using Medius.Model;
using System.IO;

namespace Medius.Controllers
{
    public interface IExportController
    {
        /// <summary>
        /// Export the given project to the specified location.
        /// </summary>
        /// <param name="project">Project to export.</param>
        /// <param name="outputFilename">Path to output file in application-specific format.</param>
        /// <returns><c>true</c> on success.</returns>
        bool Export(Project project, string outputFilename);

        /// <summary>
        /// Export the given project to the given stream.
        /// </summary>
        /// <param name="project">Project to export.</param>
        /// <param name="output">Arbitrary stream.</param>
        /// <returns><c>true</c> on success.</returns>
        bool Export(Project project, Stream output);
    }
}
