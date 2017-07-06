namespace Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("metadata.VW_RM_GetForeignKey")]
    public partial class VW_RM_GetForeignKey
    {
        [Key]
        [Column(Order = 0)]
        public string RelationName { get; set; }

        [Key]
        [Column(Order = 1)]
        public string ForeignTableName { get; set; }

        [StringLength(128)]
        public string ForeignColumnName { get; set; }

        [Key]
        [Column(Order = 2)]
        public string PrimaryTableName { get; set; }

        [StringLength(128)]
        public string PrimaryColumnName { get; set; }

        [Key]
        [Column(Order = 3)]
        public string SchemaName { get; set; }
    }
}
