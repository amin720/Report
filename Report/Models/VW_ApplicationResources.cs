namespace Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("metadata.VW_ApplicationResources")]
    public partial class VW_ApplicationResources
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ApplicationID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string ApplicationTitle { get; set; }

        [StringLength(200)]
        public string ApplicationDescription { get; set; }

        [Key]
        [Column(Order = 2)]
        public bool EnabledFlag { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ResourceID { get; set; }

        public int? ParentResourceID { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(100)]
        public string ResourceName { get; set; }

        [StringLength(100)]
        public string FarsiTitle { get; set; }

        [StringLength(200)]
        public string ExtraInformation { get; set; }

        [Key]
        [Column(Order = 5)]
        public byte ResourceTypeID { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(50)]
        public string ResourceTypeTitle { get; set; }

        [Key]
        [Column(Order = 7)]
        public bool IsAccessible { get; set; }
    }
}
