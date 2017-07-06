namespace Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("security.TBL_Token")]
    public partial class TBL_Token
    {
        [Key]
        public int TokenID { get; set; }

        public int UserID { get; set; }

        public int OrganizationPositionID { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime ExpireTime { get; set; }

        public int ActionUserID { get; set; }

        public int? ActionOrganizationPositionID { get; set; }

        public byte ActionType { get; set; }

        public DateTime ActionTime { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] ActionTimeStamp { get; set; }

        [MaxLength(8000)]
        public byte[] EncryptedToken { get; set; }

        public virtual TBL_OrganizationPosition TBL_OrganizationPosition { get; set; }

        public virtual TBL_User TBL_User { get; set; }
    }
}
