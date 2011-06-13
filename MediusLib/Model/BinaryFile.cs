namespace Medius.Model
{
    public class BinaryFile : ISupportFile
    {
        public string Filename { get; set; }
        public SupportFileType FileType { get { return SupportFileType.Binary; } }
        public byte[] Data { get; set; }
    }
}
