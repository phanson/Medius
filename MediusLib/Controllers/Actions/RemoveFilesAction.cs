using System.Collections.Generic;
using Medius.Model;

namespace Medius.Controllers.Actions
{
    public class RemoveFilesAction : AbstractAction
    {
        protected Project project;
        protected List<ISupportFile> files;

        public RemoveFilesAction(Project project, IList<ISupportFile> files) : base()
        {
            this.project = project;
            this.files = new List<ISupportFile>(files);
        }

        protected override void InternalDo()
        {
            files.ConvertAll(project.Files.Remove);  // has to be ConvertAll because Remove is not an action. :/
        }

        protected override void InternalUndo()
        {
            project.Files.AddRange(files);
        }
    }
}
