using MicroservicesProject.Users.Service.Model;
using Microsoft.EntityFrameworkCore;

namespace MicroservicesProject.Users.Service.DataAccess
{
	public class UserDbContext : DbContext
	{
		public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
		{
		}

		public DbSet<User> Users { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<User>().HasData(new User()
				{ 
					Id = Guid.Parse("85B2CDDC-2F5D-444C-B84C-258224F32687"), 
					Mail = "admin@dhbw.de",
					Name = "Admin",
					Username = "admin"
				});
			modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
		}
	}
}
