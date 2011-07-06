using System.IO;
using Medius.Model;

namespace Medius.Controllers
{
    /// <summary>
    /// Provides a default implementation for <see cref="Export(Project,string)"/>.
    /// </summary>
    public abstract class AbstractExportController : IExportController
    {
        public virtual bool Export(Project project, string outputFilename)
        {
            bool result;
            using (FileStream outfile = new FileStream(outputFilename, FileMode.Create))
            {
                result = Export(project, outfile);
                outfile.Flush();
            }
            return result;
        }

        public abstract bool Export(Project project, Stream output);
    }
}
