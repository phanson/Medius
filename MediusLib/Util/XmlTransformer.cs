using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Xsl;

namespace Medius.Util
{
    public static class XmlTransformer
    {
        public static void Transform(Stream xslt, Stream file, Stream output)
        {
            XslCompiledTransform transform = new XslCompiledTransform();
            transform.Load(XmlReader.Create(xslt, null, "bonkers"), new XsltSettings(true, false), new XmlUrlResolver());
            transform.Transform(XmlReader.Create(file), XmlWriter.Create(output, new XmlWriterSettings() { ConformanceLevel = ConformanceLevel.Auto }));
        }
    }
}
