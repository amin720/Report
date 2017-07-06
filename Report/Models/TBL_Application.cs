namespace Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("metadata.TBL_Application")]
    public partial class TBL_Application
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBL_Application()
        {
            TBL_Application_REL_Resource = new HashSet<TBL_Application_REL_Resource>();
            TBL_ApplicationActiveTime = new HashSet<TBL_ApplicationActiveTime>();
            TBL_ApplicationIPValid = new HashSet<TBL_ApplicationIPValid>();
        }

        [Key]
        public int ApplicationID { get; set; }

        [Required]
        [StringLength(100)]
        public string ApplicationTitle { get; set; }

        [StringLength(200)]
        public string ApplicationDescription { get; set; }

        public bool EnabledFlag { get; set; }

        public int ActionUserID { get; set; }

        public int? ActionOrganizationPositionID { get; set; }

        public byte ActionType { get; set; }

        public DateTime ActionTime { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] ActionTimeStamp { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_Application_REL_Resource> TBL_Application_REL_Resource { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_ApplicationActiveTime> TBL_ApplicationActiveTime { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_ApplicationIPValid> TBL_ApplicationIPValid { get; set; }
    }
}
