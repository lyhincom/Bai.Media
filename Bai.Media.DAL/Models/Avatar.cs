using System;
using Bai.General.DAL.Abstractions.Abstractions;
using Bai.General.DAL.Abstractions.DateTimeSaves.Abstractions;
using Bai.General.DAL.Abstractions.Models;
using Bai.Media.DAL.Abstractions;

namespace Bai.Media.DAL.Models
{
    public class Avatar : GuidEntity, IImage, IUserId, IDateTimeCreated, ISoftDelete
    {
        public Guid FileExtension { get; set; }
        public int FileSize { get; set; }
        public byte[] ImageBytes { get; set; }

        public Guid UserId { get; set; }

        public DateTime CreatedDt { get; set; }

        public bool Deleted { get; set; }
    }
}
