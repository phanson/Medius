namespace Medius.Model
{
    public class BinaryFile
    {
        public string Filename { get; set; }
        public SupportFileType FileType { get { return SupportFileType.Binary; } }
        public byte[] Date { get; set; }
    }
}
