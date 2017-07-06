namespace Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("metadata.VW_SystemTables")]
    public partial class VW_SystemTables
    {
        [Key]
        [Column(Order = 0)]
        public string TableName { get; set; }

        [Key]
        [Column(Order = 1)]
        public string SchemaName { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int object_id { get; set; }
    }
}
