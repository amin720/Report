namespace Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("metadata.TBL_ApplicationIPValid")]
    public partial class TBL_ApplicationIPValid
    {
        [Key]
        public int ApplicationIPID { get; set; }

        public int ApplicationID { get; set; }

        [Required]
        [StringLength(20)]
        public string IP { get; set; }

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
