namespace Medius.Model
{
    public class TextFile : ISupportFile
    {
        public string Filename { get; set; }
        public SupportFileType FileType { get { return SupportFileType.Text; } }
        public string Data { get; set; }
    }
}
