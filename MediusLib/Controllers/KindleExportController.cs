using System.IO;
using System.Linq;
using ICSharpCode.SharpZipLib.Zip;
using Medius.Model;

namespace Medius.Controllers
{
    public class KindleExportController : AbstractExportController
    {
        FilePersistenceController fileController = new FilePersistenceController();
        private const string htmlFilename = "index.htm";

        public override void Export(Project project, Stream output)
        {
            using (var zipStream = new ZipOutputStream(output))
            {
                HtmlExportController htmlExport = new HtmlExportController(project.Files.Where(f => Path.GetExtension(f.Filename).Equals("css")).Select(f => f.Filename).ToArray());
                zipStream.PutNextEntry(new ZipEntry(htmlFilename));
                htmlExport.Export(project, zipStream);
                zipStream.CloseEntry();

                foreach (var file in project.Files)
                {
                    zipStream.PutNextEntry(new ZipEntry(file.Filename));
                    fileController.WriteSupportFile(file, zipStream);
                    zipStream.CloseEntry();
                }
            }
        }
    }
}
