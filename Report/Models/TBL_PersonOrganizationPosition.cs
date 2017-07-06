namespace Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Organization.TBL_PersonOrganizationPosition")]
    public partial class TBL_PersonOrganizationPosition
    {
        [Key]
        public int PersonOrganizationPositionID { get; set; }

        public int PersonID { get; set; }

        public int OrganizationPositionID { get; set; }

        public int ActionUserID { get; set; }

        public int ActionOrganizationPositionID { get; set; }

        public byte ActionType { get; set; }

        public DateTime ActionTime { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] ActionTimeStamp { get; set; }

        public virtual TBL_OrganizationPosition TBL_OrganizationPosition { get; set; }

        public virtual TBL_Person TBL_Person { get; set; }
    }
}
