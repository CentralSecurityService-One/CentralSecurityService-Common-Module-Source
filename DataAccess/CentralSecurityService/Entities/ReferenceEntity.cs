﻿using CentralSecurityService.Common.Definitions;
using System.ComponentModel.DataAnnotations;

namespace CentralSecurityService.Common.DataAccess.CentralSecurityService.Entities
{
    public class ReferenceEntity
    {
        [Key]
        public long ReferenceId { get; set; }

        public long UniqueReferenceId { get; set; }

        public int SubReferenceId { get; set; }

        public bool Redacted { get; set; }

        public ReferenceType ReferenceTypeId { get; set; }

        public string ThumbnailFileName { get; set; }

        public string ReferenceName { get; set; }

        public string Description { get; set; }

        public string Categorisations { get; set; }

        public DateTime CreatedDateTimeUtc { get; set; }

        public DateTime? LastUpdatedDateTimeUtc { get; set; }
    }
}
