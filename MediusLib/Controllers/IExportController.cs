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
        void Export(Project project, string outputFilename);

        /// <summary>
        /// Export the given project to the given stream.
        /// </summary>
        /// <param name="project">Project to export.</param>
        /// <param name="output">Arbitrary stream.</param>
        void Export(Project project, Stream output);
    }
}
