namespace Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Accounting.GroupAccountType")]
    public partial class GroupAccountType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GroupAccountType()
        {
            GroupAccounts = new HashSet<GroupAccount>();
        }

        [Key]
        public int AccountTypeId { get; set; }

        [Required]
        [StringLength(300)]
        public string Text { get; set; }

        public int ActionType { get; set; }

        public DateTime ActionTime { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] ActionTimeStamp { get; set; }

        public bool IsDeleted { get; set; }

        public int ActionUserId { get; set; }

        public byte? Type { get; set; }

        public int? ActionOrganizationId { get; set; }

        public bool? BalanceOperationType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GroupAccount> GroupAccounts { get; set; }
    }
}
