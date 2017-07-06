namespace Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("security.TBL_UserAccessToResource")]
    public partial class TBL_UserAccessToResource
    {
        [Key]
        public int UserAccessToResourceID { get; set; }

        public int UserID { get; set; }

        public int ResourceID { get; set; }

        public bool? CanRead { get; set; }

        public bool? CanCreate { get; set; }

        public bool? CanUpdate { get; set; }

        public bool? CanDelete { get; set; }

        public bool? CanExecute { get; set; }

        public bool? CanSelect { get; set; }

        public byte DataAccessLevelTypeCode { get; set; }

        public int ActionUserID { get; set; }

        public int ActionOrganizationPositionID { get; set; }

        public byte ActionType { get; set; }

        public DateTime ActionTime { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] ActionTimeStamp { get; set; }

        public virtual TBL_Resource TBL_Resource { get; set; }

        public virtual TBL_User TBL_User { get; set; }
    }
}
