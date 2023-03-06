using MicroservicesProject.Students.Service.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace MicroservicesProject.Students.Service
{
	public static class ServiceExtensions
	{
		public static IServiceCollection AddStudentDb(this IServiceCollection services, ServiceLifetime lifeTime = ServiceLifetime.Scoped)
		{

			var connectionString = "Server=127.0.0.1;Port=3306;Database=microservices;User Id=main;Password=main";

			Action<DbContextOptionsBuilder> optionAction = optionsBuilder =>
			{
				optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), contextOptionsBuilder =>
				{
					contextOptionsBuilder.MigrationsAssembly(typeof(StudentDbContext).Assembly.FullName);
				});
			};

			services.AddDbContextFactory<StudentDbContext>(optionAction);
			services.AddDbContext<StudentDbContext>(optionAction);

			return services;
		}
	}
}
