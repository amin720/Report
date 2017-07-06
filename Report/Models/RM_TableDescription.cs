namespace Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("metadata.RM_TableDescription")]
    public partial class RM_TableDescription
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TableID { get; set; }

        [Key]
        [Column(Order = 1)]
        public string TableName { get; set; }
    }
}
