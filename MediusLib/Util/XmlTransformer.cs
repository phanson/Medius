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
        private const string baseUri = "http://philiphanson.org/medius/temp-transform";

        public static void Transform(Stream xslt, Stream file, Stream output)
        {
            // copy the xslt file to a second location in memory for file resolution
            MemoryStream ms = new MemoryStream();
            xslt.CopyTo(ms);
            xslt.Seek(0, SeekOrigin.Begin);
            ms.Seek(0, SeekOrigin.Begin);

            XslCompiledTransform transform = new XslCompiledTransform();
            XmlStreamResolver resolver = new XmlStreamResolver();
            resolver.SetEntity(new Uri(baseUri), ms);

            transform.Load(XmlReader.Create(xslt, null, baseUri), new XsltSettings(true, false), resolver);
            transform.Transform(
                XmlReader.Create(file),
                new XsltArgumentList(),
                XmlWriter.Create(output, new XmlWriterSettings() { ConformanceLevel = ConformanceLevel.Auto }),
                resolver);
        }

        private class XmlStreamResolver : XmlResolver
        {
            Dictionary<Uri, Stream> resolutionTable = new Dictionary<Uri,Stream>();

            public void SetEntity(Uri absoluteUri, Stream stream)
            {
                resolutionTable[absoluteUri] = stream;
            }

            public override System.Net.ICredentials Credentials
            {
                set { throw new InvalidOperationException(); }
            }

            //public override Uri ResolveUri(Uri baseUri, string relativeUri)
            //{
            //    if (baseUri == null)
            //        return new Uri(relativeUri);
            //    return new Uri(baseUri, relativeUri);
            //}

            public override object GetEntity(Uri absoluteUri, string role, Type ofObjectToReturn)
            {
                // contracts from XmlResolver
                if (absoluteUri == null)
                    throw new ArgumentNullException("absoluteUri");
                if (!absoluteUri.IsAbsoluteUri)
                    throw new UriFormatException("URI is not absolute.");
                if ((ofObjectToReturn != null) && !ofObjectToReturn.IsSubclassOf(typeof(Stream)))
                    throw new XmlException();

                // additional private contracts
                if ((ofObjectToReturn != null) && !ofObjectToReturn.Equals(typeof(Stream)))
                    return null;
                if (!resolutionTable.ContainsKey(absoluteUri))
                    return null;

                return resolutionTable[absoluteUri];
            }
        }
    }
}
