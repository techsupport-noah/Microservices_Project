using MicroservicesProject.Users.Service.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace MicroservicesProject.Users.Service
{
	public static class ServiceExtensions
	{
		public static IServiceCollection AddUserDb(this IServiceCollection services, ServiceLifetime lifeTime = ServiceLifetime.Scoped)
		{

			var connectionString = "Server=127.0.0.1;Port=3306;Database=microservices;User Id=main;Password=main";

			Action<DbContextOptionsBuilder> optionAction = optionsBuilder =>
			{
				optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), contextOptionsBuilder =>
				{
					contextOptionsBuilder.MigrationsAssembly(typeof(UserDbContext).Assembly.FullName);
				});
			};

			services.AddDbContextFactory<UserDbContext>(optionAction);
			services.AddDbContext<UserDbContext>(optionAction);

			return services;
		}
	}
}
