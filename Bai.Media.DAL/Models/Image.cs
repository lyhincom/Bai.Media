using System;
using Bai.General.DAL.Abstractions.Abstractions;
using Bai.General.DAL.Abstractions.DateTimeSaves.Abstractions;
using Bai.General.DAL.Abstractions.Models;
using Bai.Media.DAL.Abstractions;

namespace Bai.Media.DAL.Models
{
    public class Image : GuidEntity, IImage, IUserId, IPageKey, IDateTimeCreated, ISoftDelete
    {
        public Guid FileExtension { get; set; }
        public int FileSize { get; set; }
        public byte[] ImageBytes { get; set; }

        public Guid UserId { get; set; }

        public Guid PageId { get; set; }
        public string PageType { get; set; }

        public DateTime CreatedDt { get; set; }

        public bool Deleted { get; set; }

        public Guid? ImageGroupId { get; set; }
        public int? Priority { get; set; }
    }
}
