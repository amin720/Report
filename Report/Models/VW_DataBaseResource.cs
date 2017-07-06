namespace Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("metadata.VW_DataBaseResource")]
    public partial class VW_DataBaseResource
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ResourceID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string ResourceName { get; set; }

        [StringLength(100)]
        public string FarsiTitle { get; set; }

        [Key]
        [Column(Order = 2)]
        public bool IsAccessible { get; set; }

        [Key]
        [Column(Order = 3)]
        public bool IsReportable { get; set; }

        [Key]
        [Column(Order = 4)]
        public bool CanUseInQueryBuilder { get; set; }

        [Key]
        [Column(Order = 5)]
        public byte ResourceTypeID { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(50)]
        public string ResourceTypeTitle { get; set; }
    }
}
