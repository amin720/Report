namespace Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Organization.TBL_OrganizationPosition")]
    public partial class TBL_OrganizationPosition
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBL_OrganizationPosition()
        {
            TBL_PersonOrganizationPosition = new HashSet<TBL_PersonOrganizationPosition>();
            TBL_Token = new HashSet<TBL_Token>();
        }

        [Key]
        public int OrganizationPositionID { get; set; }

        public int OrganizationUnitID { get; set; }

        [Required]
        [StringLength(50)]
        public string OrganizationPositionTitle { get; set; }

        public int OrganizationPositionTypeID { get; set; }

        public int ActionUserID { get; set; }

        public int ActionOrganizationPositionID { get; set; }

        public byte ActionType { get; set; }

        public DateTime ActionTime { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] ActionTimeStamp { get; set; }

        public virtual TBL_Lookup_OrganizationPositionType TBL_Lookup_OrganizationPositionType { get; set; }

        public virtual TBL_OrganizationUnit TBL_OrganizationUnit { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_PersonOrganizationPosition> TBL_PersonOrganizationPosition { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_Token> TBL_Token { get; set; }
    }
}
