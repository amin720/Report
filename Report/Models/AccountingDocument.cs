namespace Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Accounting.AccountingDocument")]
    public partial class AccountingDocument
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AccountingDocument()
        {
            AccountingDocumentDetails = new HashSet<AccountingDocumentDetail>();
        }

        public int AccountingDocumentId { get; set; }

        public string Description { get; set; }

        public DateTime DateSubmited { get; set; }

        public DateTime DocumentDate { get; set; }

        public int TemporaryDocumentNumber { get; set; }

        public int PermanantDocumentNumber { get; set; }

        public int TurningNumber { get; set; }

        public int SubHeadingNumber { get; set; }

        public bool IsPermenant { get; set; }

        public bool IsConfirmed { get; set; }

        public int ActionType { get; set; }

        public DateTime ActionTime { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] ActionTimeStamp { get; set; }

        public bool IsDeleted { get; set; }

        public int ActionUserId { get; set; }

        public long? TotalDebtor { get; set; }

        public long? TotalCreditor { get; set; }

        public int? AccountingYearId { get; set; }

        public int? ActionOrganizationId { get; set; }

        public int? AccountingDocumentTypeId { get; set; }

        public virtual AccountingDocumentType AccountingDocumentType { get; set; }

        public virtual AccountingYear AccountingYear { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountingDocumentDetail> AccountingDocumentDetails { get; set; }
    }
}
