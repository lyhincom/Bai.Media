using System;
using Bai.General.DAL.Abstractions.Abstractions;
using Bai.General.DAL.Abstractions.DateTimeSaves.Abstractions;
using Bai.General.DAL.Abstractions.Models;
using Bai.Media.DAL.Abstractions.Models;

namespace Bai.Media.DAL.Models
{
    public class LogoEntity : GuidEntity, IImage, IDateTimeCreated, ISoftDelete
    {
        public string FileExtension { get; set; }
        public long FileSizeInBytes { get; set; }
        public string ContentType { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public byte[] ImageBytes { get; set; }

        public Guid PageId { get; set; }

        public DateTime CreatedDt { get; set; }

        public bool Deleted { get; set; }
    }
}
