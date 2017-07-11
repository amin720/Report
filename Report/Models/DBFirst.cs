namespace Report.Models
{
	using System;
	using System.Data.Entity;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Linq;

	public partial class DBFirst : DbContext
	{
		public DBFirst()
			: base("name=DBFirst")
		{
		}

		public virtual DbSet<Department> Departments { get; set; }
		public virtual DbSet<Employee> Employees { get; set; }
		public virtual DbSet<Gender> Genders { get; set; }
		public virtual DbSet<Manager> Managers { get; set; }
		public virtual DbSet<sysdiagram> sysdiagrams { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Department>()
				.HasMany(e => e.Employees)
				.WithRequired(e => e.Department)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Employee>()
				.Property(e => e.FirstName)
				.IsFixedLength();

			modelBuilder.Entity<Employee>()
				.Property(e => e.LastName)
				.IsFixedLength();

			modelBuilder.Entity<Employee>()
				.Property(e => e.Salary)
				.HasPrecision(6, 0);

			modelBuilder.Entity<Employee>()
				.HasOptional(e => e.Manager)
				.WithRequired(e => e.Employee);

			modelBuilder.Entity<Gender>()
				.Property(e => e.Name)
				.IsFixedLength();

			modelBuilder.Entity<Gender>()
				.HasMany(e => e.Employees)
				.WithOptional(e => e.Gender)
				.HasForeignKey(e => e.GnederId);

			modelBuilder.Entity<Manager>()
				.Property(e => e.Salary)
				.HasPrecision(6, 0);
		}
	}
}
