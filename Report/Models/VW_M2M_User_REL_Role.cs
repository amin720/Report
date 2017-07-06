namespace Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("security.VW_M2M_User_REL_Role")]
    public partial class VW_M2M_User_REL_Role
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserRoleID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserID { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RoleID { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(50)]
        public string UserName { get; set; }

        [Key]
        [Column(Order = 4)]
        public bool IsGrantableFlag { get; set; }

        [Key]
        [Column(Order = 5)]
        public bool UserEnabledFlag { get; set; }

        [Key]
        [Column(Order = 6)]
        public bool IsAdminFlag { get; set; }

        [Key]
        [Column(Order = 7)]
        public bool IsSuperAdmin { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(50)]
        public string RoleTitle { get; set; }

        [Key]
        [Column(Order = 9)]
        public bool RoleEnabledFlag { get; set; }

        [Key]
        [Column(Order = 10)]
        public bool AdminOnlyFlag { get; set; }

        [Key]
        [Column(Order = 11)]
        public byte ActionType { get; set; }

        [Key]
        [Column(Order = 12, TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] ActionTimeStamp { get; set; }

        [Key]
        [Column(Order = 13)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ActionUserID { get; set; }
    }
}
