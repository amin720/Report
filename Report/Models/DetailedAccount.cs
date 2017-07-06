namespace Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Accounting.DetailedAccount")]
    public partial class DetailedAccount
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DetailedAccount()
        {
            AccountingDocumentDetails = new HashSet<AccountingDocumentDetail>();
            DetailedAccount1 = new HashSet<DetailedAccount>();
            CertainAccounts = new HashSet<CertainAccount>();
        }

        public int AccountingYearId { get; set; }

        public int DetailedAccountId { get; set; }

        public int Code { get; set; }

        public string Description { get; set; }

        public int AccountKindId { get; set; }

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

        public int? ParentDetailedAccountID { get; set; }

        public int? ActionOrganizationId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountingDocumentDetail> AccountingDocumentDetails { get; set; }

        public virtual AccountingYear AccountingYear { get; set; }

        public virtual AccountKind AccountKind { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetailedAccount> DetailedAccount1 { get; set; }

        public virtual DetailedAccount DetailedAccount2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CertainAccount> CertainAccounts { get; set; }
    }
}
