namespace Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("metadata.TBL_Lookup_ResourceType")]
    public partial class TBL_Lookup_ResourceType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBL_Lookup_ResourceType()
        {
            TBL_Resource = new HashSet<TBL_Resource>();
            TBL_ResourceTypeAuditType = new HashSet<TBL_ResourceTypeAuditType>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte ResourceTypeID { get; set; }

        [Required]
        [StringLength(50)]
        public string ResourceTypeTitle { get; set; }

        public int ActionUserID { get; set; }

        public int? ActionOrganizationPositionID { get; set; }

        public byte ActionType { get; set; }

        public DateTime ActionTime { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] ActionTimeStamp { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_Resource> TBL_Resource { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_ResourceTypeAuditType> TBL_ResourceTypeAuditType { get; set; }
    }
}
