namespace Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Organization.TBL_Person")]
    public partial class TBL_Person
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBL_Person()
        {
            TBL_PersonOrganizationPosition = new HashSet<TBL_PersonOrganizationPosition>();
        }

        [Key]
        public int PersonID { get; set; }

        public int? OldSystemID { get; set; }

        public int UserID { get; set; }

        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(150)]
        public string LastName { get; set; }

        [StringLength(100)]
        public string FatherName { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? BirthDate { get; set; }

        [StringLength(10)]
        public string NationalCode { get; set; }

        [StringLength(10)]
        public string NationalID { get; set; }

        [StringLength(150)]
        public string E_MailAddress { get; set; }

        [StringLength(25)]
        public string TelephoneNo { get; set; }

        [StringLength(25)]
        public string CellPhoneNo { get; set; }

        public int ActionUserID { get; set; }

        public int ActionOrganizationPositionID { get; set; }

        public byte ActionType { get; set; }

        public DateTime ActionTime { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] ActionTimeStamp { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_PersonOrganizationPosition> TBL_PersonOrganizationPosition { get; set; }

        public virtual TBL_User TBL_User { get; set; }
    }
}
