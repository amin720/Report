namespace Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Accounting.CertainAccount")]
    public partial class CertainAccount
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CertainAccount()
        {
            AccountingDocumentDetails = new HashSet<AccountingDocumentDetail>();
            CertainAccount1 = new HashSet<CertainAccount>();
            DetailedAccounts = new HashSet<DetailedAccount>();
        }

        public int AccountingYearId { get; set; }

        public int CertainAccountId { get; set; }

        public int Code { get; set; }

        public string Description { get; set; }

        public int TotalAccountId { get; set; }

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

        public int? Depth { get; set; }

        public int? ParentCertainAccountID { get; set; }

        public int? ActionOrganizationId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountingDocumentDetail> AccountingDocumentDetails { get; set; }

        public virtual AccountingYear AccountingYear { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CertainAccount> CertainAccount1 { get; set; }

        public virtual CertainAccount CertainAccount2 { get; set; }

        public virtual TotalAccount TotalAccount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetailedAccount> DetailedAccounts { get; set; }
    }
}
