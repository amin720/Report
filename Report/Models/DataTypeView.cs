namespace Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("metadata.DataTypeView")]
    public partial class DataTypeView
    {
        [Key]
        [Column(Order = 0)]
        public string name { get; set; }

        [Key]
        [Column(Order = 1)]
        public byte system_type_id { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int user_type_id { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int schema_id { get; set; }

        public int? principal_id { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short max_length { get; set; }

        [Key]
        [Column(Order = 5)]
        public byte precision { get; set; }

        [Key]
        [Column(Order = 6)]
        public byte scale { get; set; }

        [StringLength(128)]
        public string collation_name { get; set; }

        public bool? is_nullable { get; set; }

        [Key]
        [Column(Order = 7)]
        public bool is_user_defined { get; set; }

        [Key]
        [Column(Order = 8)]
        public bool is_assembly_type { get; set; }

        [Key]
        [Column(Order = 9)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int default_object_id { get; set; }

        [Key]
        [Column(Order = 10)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int rule_object_id { get; set; }

        [Key]
        [Column(Order = 11)]
        public string MapToType { get; set; }
    }
}
