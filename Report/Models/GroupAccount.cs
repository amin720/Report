namespace Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Accounting.GroupAccount")]
    public partial class GroupAccount
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GroupAccount()
        {
            TotalAccounts = new HashSet<TotalAccount>();
        }

        public int AccountingYearId { get; set; }

        public int GroupAccountId { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        public int Code { get; set; }

        public int AccountTypeId { get; set; }

        public int NatureId { get; set; }

        public int ActionType { get; set; }

        public DateTime ActionTime { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] ActionTimeStamp { get; set; }

        public bool IsDeleted { get; set; }

        public int ActionUserId { get; set; }

        public int? ActionOrganizationId { get; set; }

        public virtual AccountingYear AccountingYear { get; set; }

        public virtual AccountNature AccountNature { get; set; }

        public virtual GroupAccountType GroupAccountType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TotalAccount> TotalAccounts { get; set; }
    }
}
