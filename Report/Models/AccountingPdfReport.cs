namespace Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AccountingPdfReport")]
    public partial class AccountingPdfReport
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(250)]
        public string Certain { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(250)]
        public string Detailed { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(250)]
        public string Total { get; set; }

        [Key]
        [Column(Order = 3)]
        public string Description { get; set; }

        public long? Debtor { get; set; }

        public long? Creditor { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AccountingDocumentId { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TemporaryDocumentNumber { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PermanantDocumentNumber { get; set; }

        public string Header { get; set; }

        [Key]
        [Column(Order = 7)]
        public bool IsConfirmed { get; set; }

        [Key]
        [Column(Order = 8)]
        public bool IsPermenant { get; set; }

        [StringLength(150)]
        public string OrganizationTitle { get; set; }
    }
}
