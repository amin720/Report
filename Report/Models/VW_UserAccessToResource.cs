namespace Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("security.VW_UserAccessToResource")]
    public partial class VW_UserAccessToResource
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserID { get; set; }

        public bool? CanRead { get; set; }

        public bool? CanCreate { get; set; }

        public bool? CanUpdate { get; set; }

        public bool? CanDelete { get; set; }

        public bool? CanExecute { get; set; }

        public bool? CanSelect { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ResourceID { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(100)]
        public string ResourceName { get; set; }

        [StringLength(100)]
        public string FarsiTitle { get; set; }

        [StringLength(200)]
        public string ExtraInformation { get; set; }

        [Key]
        [Column(Order = 3)]
        public byte ResourceTypeID { get; set; }
    }
}
