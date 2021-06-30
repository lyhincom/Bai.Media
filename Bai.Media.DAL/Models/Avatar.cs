using System;
using Bai.General.DAL.Abstractions.Abstractions;
using Bai.General.DAL.Abstractions.DateTimeSaves.Abstractions;
using Bai.General.DAL.Abstractions.Models;

namespace Bai.Media.DAL.Models
{
    public class Avatar : GuidEntity, IUserId, IDateTimeCreated
    {
        public Guid UserId { get; set; }
        public Guid FileExtension { get; set; }
        public int FileSize { get; set; }
        public DateTime CreatedDt { get; set; }
    }
}
