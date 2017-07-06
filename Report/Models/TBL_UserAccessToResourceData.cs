namespace Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("security.TBL_UserAccessToResourceData")]
    public partial class TBL_UserAccessToResourceData
    {
        [Key]
        public int UserAccessToResourceDataID { get; set; }

        public int UserID { get; set; }

        public int ResourceID { get; set; }

        public int PrimaryKeyValue { get; set; }

        public int ActionUserID { get; set; }

        public int ActionOrganizationPositionID { get; set; }

        public byte ActionType { get; set; }

        public DateTime ActionTime { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] ActionTimeStamp { get; set; }

        public virtual TBL_Resource TBL_Resource { get; set; }
    }
}
