using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medius.Model;
using System.IO;

namespace Medius.Controllers.Actions
{
    public class AddFileAction : AbstractAction
    {
        protected Project project;
        protected Stream stream;
        protected string filename;

        private bool createdStream;

        protected ISupportFile file;

        public AddFileAction(Project project, string filename, Stream stream = null)
        {
            this.project = project;
            this.stream = stream;
            this.filename = filename;
        }

        private void openStream()
        {
            if (stream == null)
            {
                createdStream = true;
                stream = new FileStream(filename, FileMode.Open);
            }
        }

        private void closeStream()
        {
            if (createdStream)
            {
                stream.Close();
            }
        }

        protected override void InternalDo()
        {
            openStream();
            try
            {
                if (ProjectPersistenceController.TextFileExtensions.Contains(Path.GetExtension(filename)))
                {
                    // text file
                    file = new TextFile() { Filename = filename, Data = new StreamReader(stream).ReadToEnd() };
                }
                else
                {
                    // binary file
                    int length = (int)stream.Length;
                    byte[] data = new byte[length];
                    stream.Read(data, 0, length);
                    file = new BinaryFile() { Filename = filename, Data = data };
                }

                // add to project
                project.Files.Add(file);
            }
            finally
            {
                closeStream();
            }
        }

        protected override void InternalUndo()
        {
            project.Files.Remove(file);
        }
    }
}
