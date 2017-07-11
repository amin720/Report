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
        public string Description { get; set; }

        [Key]
        [Column(Order = 0)]
        public bool IsPermenant { get; set; }

        [Key]
        [Column(Order = 1)]
        public bool IsConfirmed { get; set; }

        public long? Debtor { get; set; }

        public long? Creditor { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(250)]
        public string Certain { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(250)]
        public string Total { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(250)]
        public string Detailed { get; set; }
    }
}
