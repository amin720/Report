namespace Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("security.TBL_ApplicationActiveTime")]
    public partial class TBL_ApplicationActiveTime
    {
        [Key]
        public int ApplicationActiveTime { get; set; }

        public int ApplicationID { get; set; }

        public DateTimeOffset FromDate { get; set; }

        public DateTimeOffset? ToDate { get; set; }

        public TimeSpan? FromTime { get; set; }

        public TimeSpan? ToTime { get; set; }

        public byte? FromDayOfWeek { get; set; }

        public byte? ToDayOfWeek { get; set; }

        public int ActionUserID { get; set; }

        public int? ActionOrganizationPositionID { get; set; }

        public byte ActionType { get; set; }

        public DateTime ActionTime { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] ActionTimeStamp { get; set; }

        public virtual TBL_Application TBL_Application { get; set; }
    }
}
