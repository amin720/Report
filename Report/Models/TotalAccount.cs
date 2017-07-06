namespace Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Accounting.TotalAccount")]
    public partial class TotalAccount
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TotalAccount()
        {
            CertainAccounts = new HashSet<CertainAccount>();
        }

        public int AccountingYearId { get; set; }

        public int TotalAccountId { get; set; }

        public int Code { get; set; }

        public string Description { get; set; }

        public int TotalAccountTypeId { get; set; }

        public int GroupAccountId { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CertainAccount> CertainAccounts { get; set; }

        public virtual GroupAccount GroupAccount { get; set; }

        public virtual TotalAccountType TotalAccountType { get; set; }
    }
}
