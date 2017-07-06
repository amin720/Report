namespace Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("security.TBL_Lookup_DataAccessLevelType")]
    public partial class TBL_Lookup_DataAccessLevelType
    {
        [Key]
        public int DataAccessLevelTypeID { get; set; }

        [Required]
        [StringLength(50)]
        public string DataAccessLevelTitle { get; set; }

        public int ActionUserID { get; set; }

        public int? ActionOrganizationPositionID { get; set; }

        public byte ActionType { get; set; }

        public DateTime ActionTime { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] ActionTimeStamp { get; set; }
    }
}
