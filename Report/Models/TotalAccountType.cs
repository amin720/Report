namespace Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Accounting.TotalAccountType")]
    public partial class TotalAccountType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TotalAccountType()
        {
            TotalAccounts = new HashSet<TotalAccount>();
        }

        public int TotalAccountTypeId { get; set; }

        [Required]
        public string Text { get; set; }

        public int ActionType { get; set; }

        public DateTime ActionTime { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] ActionTimeStamp { get; set; }

        public bool IsDeleted { get; set; }

        public int ActionUserId { get; set; }

        public int? ActionOrganizationId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TotalAccount> TotalAccounts { get; set; }
    }
}
