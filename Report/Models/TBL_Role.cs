namespace Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("security.TBL_Role")]
    public partial class TBL_Role
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBL_Role()
        {
            TBL_RoleAccessToResource = new HashSet<TBL_RoleAccessToResource>();
            TBL_Role_Resource_Auditing = new HashSet<TBL_Role_Resource_Auditing>();
            TBL_UserRole = new HashSet<TBL_UserRole>();
        }

        [Key]
        public int RoleID { get; set; }

        [Required]
        [StringLength(50)]
        public string RoleTitle { get; set; }

        public bool EnabledFlag { get; set; }

        public bool AdminOnlyFlag { get; set; }

        public bool IsGrantableFlag { get; set; }

        public int ActionUserID { get; set; }

        public int ActionOrganizationPositionID { get; set; }

        public byte ActionType { get; set; }

        public DateTime ActionTime { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] ActionTimeStamp { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_RoleAccessToResource> TBL_RoleAccessToResource { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_Role_Resource_Auditing> TBL_Role_Resource_Auditing { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_UserRole> TBL_UserRole { get; set; }
    }
}
