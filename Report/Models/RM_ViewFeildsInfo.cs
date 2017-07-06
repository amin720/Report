namespace Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("metadata.RM_ViewFeildsInfo")]
    public partial class RM_ViewFeildsInfo
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ColumnID { get; set; }

        [Key]
        [Column(Order = 1)]
        public string TableName { get; set; }

        [StringLength(128)]
        public string ColumnName { get; set; }

        [Key]
        [Column(Order = 2)]
        public string DBTypeName { get; set; }

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
