namespace Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("security.TBL_User")]
    public partial class TBL_User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBL_User()
        {
            TBL_Person = new HashSet<TBL_Person>();
            TBL_Token = new HashSet<TBL_Token>();
            TBL_UserRole = new HashSet<TBL_UserRole>();
            TBL_UserAccessToResource = new HashSet<TBL_UserAccessToResource>();
            TBL_UserActiveTime = new HashSet<TBL_UserActiveTime>();
        }

        [Key]
        public int UserID { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [StringLength(100)]
        public string Question { get; set; }

        [StringLength(50)]
        public string Answer { get; set; }

        public bool EnabledFlag { get; set; }

        public bool IsAdminFlag { get; set; }

        public bool IsSuperAdmin { get; set; }

        public bool IsChangedAuthorization { get; set; }

        public int ActionUserID { get; set; }

        public int? ActionOrganizationPositionID { get; set; }

        public byte ActionType { get; set; }

        public DateTime ActionTime { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] ActionTimeStamp { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_Person> TBL_Person { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_Token> TBL_Token { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_UserRole> TBL_UserRole { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_UserAccessToResource> TBL_UserAccessToResource { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_UserActiveTime> TBL_UserActiveTime { get; set; }
    }
}
