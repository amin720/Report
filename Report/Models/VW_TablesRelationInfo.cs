namespace Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("metadata.VW_TablesRelationInfo")]
    public partial class VW_TablesRelationInfo
    {
        [Key]
        [Column(Order = 0)]
        public string PrimaryTableName { get; set; }

        [Key]
        [Column(Order = 1)]
        public string ForeignTableName { get; set; }

        [StringLength(128)]
        public string PKColumn { get; set; }

        [StringLength(128)]
        public string FKColumn { get; set; }

        [StringLength(100)]
        public string FarsiTitle { get; set; }
    }
}
