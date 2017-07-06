namespace Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("security.TBL_Role_Resource_Auditing")]
    public partial class TBL_Role_Resource_Auditing
    {
        [Key]
        public int RoleAuditingResourceID { get; set; }

        public int RoleID { get; set; }

        public int ResourceID { get; set; }

        public DateTime? AuditingDateFrom { get; set; }

        public DateTime? AuditingDateTo { get; set; }

        public int ActionUserID { get; set; }

        public int ActionOrganizationPositionID { get; set; }

        public byte ActionType { get; set; }

        public DateTime ActionTime { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] ActionTimeStamp { get; set; }

        public virtual TBL_Resource TBL_Resource { get; set; }

        public virtual TBL_Role TBL_Role { get; set; }
    }
}
