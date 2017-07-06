namespace Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("security.VW_M2M_ResourceAuditType")]
    public partial class VW_M2M_ResourceAuditType
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Resource_Rel_AuditTypeID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ResourceID { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AuditTypeID { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(50)]
        public string AuditTypeTitle { get; set; }

        [Key]
        [Column(Order = 4)]
        public byte ActionType { get; set; }

        [Key]
        [Column(Order = 5, TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] ActionTimeStamp { get; set; }
    }
}
