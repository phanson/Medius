using System.IO;
using System.Xml;
using Medius.Model;

namespace Medius.Controllers
{
    public class HtmlExportController : AbstractExportController
    {
        string[] cssFiles;

        public HtmlExportController(params string[] cssFiles)
        {
            this.cssFiles = cssFiles ?? new string[] { };
        }

        public override void Export(Project project, Stream output)
        {
            using (XmlWriter writer = XmlWriter.Create(output))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("html");

                writer.WriteStartElement("head");
                writer.WriteElementString("title", project.Book.Title);
                foreach (string file in cssFiles)
                {
                    writer.WriteStartElement("link");
                    writer.WriteAttributeString("rel", "stylesheet");
                    writer.WriteAttributeString("type", "text/css");
                    writer.WriteAttributeString("href", file);
                    writer.WriteEndElement();  // link
                }
                writer.WriteEndElement();  // head

                writer.WriteStartElement("body");
                writer.WriteElementString("h1", project.Book.Title);

                foreach (Chapter c in project.Book.Chapters)
                {
                    writer.WriteElementString("h2", c.Title);

                    writer.WriteStartElement("div");
                    writer.WriteAttributeString("class", "intro");
                    writer.WriteString(c.Introduction);
                    writer.WriteEndElement();  // div

                    foreach (Post p in c.Posts)
                    {
                        writer.WriteElementString("h3", p.Title);

                        if (!string.IsNullOrWhiteSpace(p.Author))
                        {
                            writer.WriteStartElement("p");
                            writer.WriteAttributeString("class", "author");
                            writer.WriteString(string.Format("by {0}", p.Author));
                            writer.WriteEndElement();
                        }
                        writer.WriteStartElement("div");
                        writer.WriteAttributeString("class", "article");
                        writer.WriteRaw(p.Content);
                        writer.WriteEndElement();  // div
                    }
                }

                writer.WriteEndElement();  // body
                writer.WriteEndElement();  // html
            }
        }
    }
}
