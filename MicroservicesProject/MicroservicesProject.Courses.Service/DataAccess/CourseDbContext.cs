using MicroservicesProject.Courses.Service.Model;
using Microsoft.EntityFrameworkCore;

namespace MicroservicesProject.Courses.Service.DataAccess
{
	public class CourseDbContext : DbContext
	{
		public CourseDbContext(DbContextOptions<CourseDbContext> options) : base(options)
		{
		}

		public DbSet<Course>? Courses { get; set; }
	}
}
