namespace Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("metadata.VW_SystemViews")]
    public partial class VW_SystemViews
    {
        [Key]
        [Column(Order = 0)]
        public string name { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int object_id { get; set; }

        [Key]
        [Column(Order = 2)]
        public string SchemaName { get; set; }
    }
}
