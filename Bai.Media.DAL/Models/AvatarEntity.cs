using System;
using Bai.General.DAL.Abstractions.Abstractions;
using Bai.General.DAL.Abstractions.DateTimeSaves.Abstractions;
using Bai.General.DAL.Abstractions.Models;
using Bai.Media.DAL.Abstractions.Models;

namespace Bai.Media.DAL.Models
{
    public class AvatarEntity : GuidEntity, IImage, IUserId, IDateTimeCreated, ISoftDelete, IImageUrls
    {
        public string FileExtension { get; set; }
        public long FileSizeInBytes { get; set; }
        public string ContentType { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public byte[] ImageBytes { get; set; }

        public Guid UserId { get; set; }

        public DateTime CreatedDt { get; set; }

        public bool Deleted { get; set; }

        public string DatabaseUrl { get; set; }
        public string FileSystemUrl { get; set; }
    }
}
