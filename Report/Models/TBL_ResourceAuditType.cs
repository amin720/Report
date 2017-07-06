namespace Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("security.TBL_ResourceAuditType")]
    public partial class TBL_ResourceAuditType
    {
        [Key]
        public int Resource_Rel_AuditTypeID { get; set; }

        public int ResourceID { get; set; }

        public int AuditTypeID { get; set; }

        public int ActionUserID { get; set; }

        public int ActionOrganizationPositionID { get; set; }

        public byte ActionType { get; set; }

        public DateTime ActionTime { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] ActionTimeStamp { get; set; }

        public virtual TBL_Resource TBL_Resource { get; set; }

        public virtual TBL_Lookup_AuditType TBL_Lookup_AuditType { get; set; }
    }
}
