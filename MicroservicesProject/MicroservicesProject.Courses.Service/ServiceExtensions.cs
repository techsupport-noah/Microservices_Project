using MicroservicesProject.Courses.Service.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace MicroservicesProject.Courses.Service
{
	public static class ServiceExtensions
	{
		public static IServiceCollection AddCourseDb(this IServiceCollection services, ServiceLifetime lifeTime = ServiceLifetime.Scoped)
		{

			var connectionString = "Server=127.0.0.1;Port=3306;Database=microservices;User Id=main;Password=main";

			Action<DbContextOptionsBuilder> optionAction = optionsBuilder =>
			{
				optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), contextOptionsBuilder =>
				{
					contextOptionsBuilder.MigrationsAssembly(typeof(CourseDbContext).Assembly.FullName);
				});
			};

			services.AddDbContextFactory<CourseDbContext>(optionAction);
			services.AddDbContext<CourseDbContext>(optionAction);

			return services;
		}
	}
}
