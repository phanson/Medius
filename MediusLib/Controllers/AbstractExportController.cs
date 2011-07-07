using System.IO;
using Medius.Model;

namespace Medius.Controllers
{
    /// <summary>
    /// Provides a default implementation for <see cref="Export(Project,string)"/>.
    /// </summary>
    public abstract class AbstractExportController : IExportController
    {
        public virtual void Export(Project project, string outputFilename)
        {
            using (FileStream outfile = new FileStream(outputFilename, FileMode.Create))
            {
                Export(project, outfile);
            }
        }

        public abstract void Export(Project project, Stream output);
    }
}
