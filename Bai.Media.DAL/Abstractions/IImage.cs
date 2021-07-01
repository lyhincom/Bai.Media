using System;

namespace Bai.Media.DAL.Abstractions
{
    public interface IImage
    {
        public Guid FileExtension { get; set; }
        public int FileSize { get; set; }
        public byte[] ImageBytes { get; set; }
    }
}
