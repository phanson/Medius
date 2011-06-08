namespace Medius.Model
{
    public interface ISupportFile
    {
        string Filename { get; set; }
        SupportFileType FileType { get; }
    }

    public enum SupportFileType
    {
        Text, Binary
    }
}
