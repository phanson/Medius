namespace Medius.Controllers
{
    public interface ImportController
    {
        /// <summary>
        /// Convert the given input file into BXF format and save the result in the specified location.
        /// </summary>
        /// <param name="inputFilename">Path to input file in application-specific format.</param>
        /// <param name="outputFilename">Path to output file in BXF format.</param>
        /// <returns><c>true</c> on success.</returns>
        bool Import(string inputFilename, string outputFilename);
    }
}
