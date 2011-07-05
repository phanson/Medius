namespace Medius.Controllers
{
    public interface IExportController
    {
        /// <summary>
        /// Convert the given input file out of Medius format and save the result in the specified location.
        /// </summary>
        /// <param name="inputFilename">Path to input file in Medius format.</param>
        /// <param name="outputFilename">Path to output file in application-specific format.</param>
        /// <returns><c>true</c> on success.</returns>
        bool Export(string inputFilename, string outputFilename);
    }
}
