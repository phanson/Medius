using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Medius.Model;
using ICSharpCode.SharpZipLib.Zip;

namespace Medius.Controllers
{
    public class KindleExportController : AbstractExportController
    {
        IExportController htmlExport;
        FilePersistenceController fileController = new FilePersistenceController();

        private const string htmlFilename = "index.htm";

        public KindleExportController(IExportController htmlExport) : base()
        {
            this.htmlExport = htmlExport;
        }

        public override void Export(Project project, Stream output)
        {
            using (var zipStream = new ZipOutputStream(output))
            {
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
