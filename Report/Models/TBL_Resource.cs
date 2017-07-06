namespace Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("metadata.TBL_Resource")]
    public partial class TBL_Resource
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBL_Resource()
        {
            TBL_ResourceAuditType = new HashSet<TBL_ResourceAuditType>();
            TBL_Resource1 = new HashSet<TBL_Resource>();
            TBL_RoleAccessToResource = new HashSet<TBL_RoleAccessToResource>();
            TBL_Role_Resource_Auditing = new HashSet<TBL_Role_Resource_Auditing>();
            TBL_UserAccessToResource = new HashSet<TBL_UserAccessToResource>();
            TBL_UserAccessToResourceData = new HashSet<TBL_UserAccessToResourceData>();
            TBL_User_Resource_Auditing = new HashSet<TBL_User_Resource_Auditing>();
        }

        [Key]
        public int ResourceID { get; set; }

        public int? ParentResourceID { get; set; }

        [Required]
        [StringLength(100)]
        public string ResourceName { get; set; }

        [StringLength(100)]
        public string FarsiTitle { get; set; }

        [StringLength(200)]
        public string ExtraInformation { get; set; }

        public byte ResourceTypeID { get; set; }

        public bool IsAccessible { get; set; }

        public bool IsReportable { get; set; }

        public bool IsAuditable { get; set; }

        public bool CanUseInQueryBuilder { get; set; }

        public int ActionUserID { get; set; }

        public int? ActionOrganizationPositionID { get; set; }

        public byte ActionType { get; set; }

        public DateTime ActionTime { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] ActionTimeStamp { get; set; }

        public virtual TBL_Lookup_ResourceType TBL_Lookup_ResourceType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_ResourceAuditType> TBL_ResourceAuditType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_Resource> TBL_Resource1 { get; set; }

        public virtual TBL_Resource TBL_Resource2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_RoleAccessToResource> TBL_RoleAccessToResource { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_Role_Resource_Auditing> TBL_Role_Resource_Auditing { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_UserAccessToResource> TBL_UserAccessToResource { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_UserAccessToResourceData> TBL_UserAccessToResourceData { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_User_Resource_Auditing> TBL_User_Resource_Auditing { get; set; }
    }
}
