using MicroservicesProject.Students.Service.Model;
using Microsoft.EntityFrameworkCore;

namespace MicroservicesProject.Students.Service.DataAccess
{
	public class StudentDbContext : DbContext
	{
		public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
		{
		}

		public DbSet<Student> Students { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		
			modelBuilder.Entity<Student>().HasIndex(s => s.Username).IsUnique();
		}
	}
}
