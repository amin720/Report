namespace Report.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Employee")]
    public partial class Employee
    {
        public int Id { get; set; }

        [StringLength(20)]
        public string FirstName { get; set; }

        [StringLength(20)]
        public string LastName { get; set; }

        public int? Age { get; set; }

        public int? GnederId { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Salary { get; set; }

        public int DepartmentId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [StringLength(41)]
        public string FullName { get; set; }

        public virtual Department Department { get; set; }

        public virtual Gender Gender { get; set; }

        public virtual Manager Manager { get; set; }
    }
}
