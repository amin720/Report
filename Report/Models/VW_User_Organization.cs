namespace Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("security.VW_User_Organization")]
    public partial class VW_User_Organization
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string UserName { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrganizationUnitID { get; set; }

        [StringLength(150)]
        public string OrganizationTitle { get; set; }

        public int? ParentOrganizationUnitID { get; set; }

        [StringLength(150)]
        public string ParentOrganizationTitle { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PersonID { get; set; }

        [StringLength(251)]
        public string PersonName { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(50)]
        public string OrganizationPositionTitle { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrganizationPositionID { get; set; }
    }
}
