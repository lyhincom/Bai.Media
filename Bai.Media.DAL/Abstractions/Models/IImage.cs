namespace Bai.Media.DAL.Abstractions.Models
{
    public interface IImage
    {
        public string FileExtension { get; set; }
        public long FileSizeInBytes { get; set; }
        public string ContentType { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public byte[] ImageBytes { get; set; }
    }
}
