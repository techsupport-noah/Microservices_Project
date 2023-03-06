using MicroservicesProject.Events.Service.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace MicroservicesProject.Events.Service
{
	public static class ServiceExtensions
	{
		public static IServiceCollection AddEventDb(this IServiceCollection services, ServiceLifetime lifeTime = ServiceLifetime.Scoped)
		{

			var connectionString = "Server=127.0.0.1;Port=3306;Database=microservices;User Id=main;Password=main";

			Action<DbContextOptionsBuilder> optionAction = optionsBuilder =>
			{
				optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), contextOptionsBuilder =>
				{
					contextOptionsBuilder.MigrationsAssembly(typeof(EventDbContext).Assembly.FullName);
				});
			};

			services.AddDbContextFactory<EventDbContext>(optionAction);
			services.AddDbContext<EventDbContext>(optionAction);

			return services;
		}
	}
}
