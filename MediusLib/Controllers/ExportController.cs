namespace Medius.Controllers
{
    public interface ExportController
    {
        /// <summary>
        /// Convert the given input file out of BXF format and save the result in the specified location.
        /// </summary>
        /// <param name="inputFilename">Path to input file in BXF format.</param>
        /// <param name="outputFilename">Path to output file in application-specific format.</param>
        /// <returns><c>true</c> on success.</returns>
        bool Export(string inputFilename, string outputFilename);
    }
}
