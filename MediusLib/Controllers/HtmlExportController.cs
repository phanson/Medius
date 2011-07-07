using System.IO;
using System.Xml;
using Medius.Model;

namespace Medius.Controllers
{
    public class HtmlExportController : AbstractExportController
    {
        public override void Export(Project project, Stream output)
        {
            using (XmlWriter writer = XmlWriter.Create(output))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("html");

                writer.WriteStartElement("head");
                writer.WriteElementString("title", project.Book.Title);
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
