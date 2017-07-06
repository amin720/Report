namespace Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("metadata.RM_GetPrimaryKey")]
    public partial class RM_GetPrimaryKey
    {
        [StringLength(128)]
        public string ColumnName { get; set; }

        [Key]
        public string TableName { get; set; }
    }
}
