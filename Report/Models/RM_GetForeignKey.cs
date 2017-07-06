namespace Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("metadata.RM_GetForeignKey")]
    public partial class RM_GetForeignKey
    {
        [Key]
        [Column(Order = 0)]
        public string RelationName { get; set; }

        [Key]
        [Column(Order = 1)]
        public string ParentTableName { get; set; }

        [StringLength(128)]
        public string ParentColumnName { get; set; }

        [Key]
        [Column(Order = 2)]
        public string RefrenceTableName { get; set; }

        [StringLength(128)]
        public string RefrenceColumnName { get; set; }
    }
}
