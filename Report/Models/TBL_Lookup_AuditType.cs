namespace Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("security.TBL_Lookup_AuditType")]
    public partial class TBL_Lookup_AuditType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBL_Lookup_AuditType()
        {
            TBL_ResourceAuditType = new HashSet<TBL_ResourceAuditType>();
            TBL_ResourceTypeAuditType = new HashSet<TBL_ResourceTypeAuditType>();
        }

        [Key]
        public int AuditTypeID { get; set; }

        [Required]
        [StringLength(50)]
        public string AuditTypeTitle { get; set; }

        public int ActionUserID { get; set; }

        public int ActionOrganizationPositionID { get; set; }

        public byte ActionType { get; set; }

        public DateTime ActionTime { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] ActionTimeStamp { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_ResourceAuditType> TBL_ResourceAuditType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_ResourceTypeAuditType> TBL_ResourceTypeAuditType { get; set; }
    }
}
