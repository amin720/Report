namespace Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Organization.VW_UserAccessToMenu")]
    public partial class VW_UserAccessToMenu
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ResourceID { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(100)]
        public string ResourceName { get; set; }

        [StringLength(100)]
        public string FarsiTitle { get; set; }

        [StringLength(200)]
        public string ExtraInformation { get; set; }

        public int? OrganizationPositionID { get; set; }
    }
}
