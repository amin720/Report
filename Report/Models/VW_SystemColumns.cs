namespace Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("metadata.VW_SystemColumns")]
    public partial class VW_SystemColumns
    {
        [StringLength(128)]
        public string name { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int object_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int column_id { get; set; }

        [Key]
        [Column(Order = 2)]
        public string typeName { get; set; }

        [Key]
        [Column(Order = 3)]
        public byte system_type_id { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short max_length { get; set; }

        public bool? is_nullable { get; set; }
    }
}
