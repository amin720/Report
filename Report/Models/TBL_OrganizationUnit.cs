namespace Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Organization.TBL_OrganizationUnit")]
    public partial class TBL_OrganizationUnit
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBL_OrganizationUnit()
        {
            AccountingYears = new HashSet<AccountingYear>();
            TBL_OrganizationPosition = new HashSet<TBL_OrganizationPosition>();
            TBL_OrganizationUnit1 = new HashSet<TBL_OrganizationUnit>();
        }

        [Key]
        public int OrganizationUnitID { get; set; }

        public int? ParentOrganizationUnitID { get; set; }

        [StringLength(150)]
        public string OrganizationTitle { get; set; }

        public bool? VirtualUnitFlag { get; set; }

        public int ActionUserID { get; set; }

        public int ActionOrganizationPositionID { get; set; }

        public byte ActionType { get; set; }

        public DateTime ActionTime { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] ActionTimeStamp { get; set; }

        [StringLength(25)]
        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountingYear> AccountingYears { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_OrganizationPosition> TBL_OrganizationPosition { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_OrganizationUnit> TBL_OrganizationUnit1 { get; set; }

        public virtual TBL_OrganizationUnit TBL_OrganizationUnit2 { get; set; }
    }
}
