namespace Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("metadata.TBL_Configuration")]
    public partial class TBL_Configuration
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short TokenExpireTimeSlice { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ActionUserID { get; set; }

        public int? ActionOrganizationPositionID { get; set; }

        [Key]
        [Column(Order = 2)]
        public byte ActionType { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime ActionTime { get; set; }

        [Key]
        [Column(Order = 4, TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] ActionTimeStamp { get; set; }
    }
}
