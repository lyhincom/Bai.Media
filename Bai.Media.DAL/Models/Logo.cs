using System;
using Bai.General.DAL.Abstractions.Abstractions;
using Bai.General.DAL.Abstractions.DateTimeSaves.Abstractions;
using Bai.General.DAL.Abstractions.Models;
using Bai.Media.DAL.Abstractions;

namespace Bai.Media.DAL.Models
{
    public class Logo : GuidEntity, IImage, IDateTimeCreated, ISoftDelete
    {
        public Guid FileExtension { get; set; }
        public int FileSize { get; set; }
        public byte[] ImageBytes { get; set; }

        public Guid PageId { get; set; }

        public DateTime CreatedDt { get; set; }

        public bool Deleted { get; set; }
    }
}
