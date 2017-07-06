namespace Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("metadata.TBL_Messages")]
    public partial class TBL_Messages
    {
        public int ID { get; set; }

        public byte? LanguageID { get; set; }

        [StringLength(100)]
        public string MessageText { get; set; }

        public int ActionUserID { get; set; }

        public int? ActionOrganizationPositionID { get; set; }

        public byte ActionType { get; set; }

        public DateTime ActionTime { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] ActionTimeStamp { get; set; }

        public virtual TBL_Language TBL_Language { get; set; }
    }
}
