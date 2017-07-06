namespace Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("metadata.VW_TablesViews")]
    public partial class VW_TablesViews
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TableID { get; set; }

        [Key]
        [Column(Order = 1)]
        public string TableName { get; set; }

        [Key]
        [Column(Order = 2)]
        public string SchemaName { get; set; }
    }
}
