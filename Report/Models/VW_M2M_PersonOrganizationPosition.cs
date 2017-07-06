namespace Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Organization.VW_M2M_PersonOrganizationPosition")]
    public partial class VW_M2M_PersonOrganizationPosition
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PersonOrganizationPositionID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PersonID { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrganizationPositionID { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        public string OrganizationPositionTypeTitle { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(50)]
        public string OrganizationPositionTitle { get; set; }

        [StringLength(251)]
        public string Name { get; set; }

        [Key]
        [Column(Order = 5)]
        public byte ActionType { get; set; }

        [Key]
        [Column(Order = 6, TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] ActionTimeStamp { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ActionUserID { get; set; }
    }
}
