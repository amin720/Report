namespace Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Organization.TBL_Lookup_OrganizationPositionType")]
    public partial class TBL_Lookup_OrganizationPositionType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBL_Lookup_OrganizationPositionType()
        {
            TBL_OrganizationPosition = new HashSet<TBL_OrganizationPosition>();
        }

        [Key]
        public int OrganizationPositionTypeID { get; set; }

        [Required]
        [StringLength(100)]
        public string OrganizationPositionTypeTitle { get; set; }

        public int ActionUserID { get; set; }

        public int ActionOrganizationPositionID { get; set; }

        public byte ActionType { get; set; }

        public DateTime ActionTime { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] ActionTimeStamp { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_OrganizationPosition> TBL_OrganizationPosition { get; set; }
    }
}
