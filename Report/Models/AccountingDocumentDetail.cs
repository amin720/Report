namespace Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Accounting.AccountingDocumentDetail")]
    public partial class AccountingDocumentDetail
    {
        public int AccountingDocumentDetailId { get; set; }

        [Required]
        public string Description { get; set; }

        public long? Debtor { get; set; }

        public long? Creditor { get; set; }

        public DateTime DateSubmited { get; set; }

        public int DetailedAccountId { get; set; }

        public int? CertainAccountId { get; set; }

        public int AccountingDocumentId { get; set; }

        public int ActionType { get; set; }

        public DateTime ActionTime { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] ActionTimeStamp { get; set; }

        public bool IsDeleted { get; set; }

        public int ActionUserId { get; set; }

        public int? ActionOrganizationId { get; set; }

        public virtual AccountingDocument AccountingDocument { get; set; }

        public virtual CertainAccount CertainAccount { get; set; }

        public virtual DetailedAccount DetailedAccount { get; set; }
    }
}
