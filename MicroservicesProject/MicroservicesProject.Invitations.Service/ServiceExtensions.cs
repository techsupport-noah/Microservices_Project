using MicroservicesProject.Invitations.Service.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace MicroservicesProject.Invitations.Service
{
	public static class ServiceExtensions
	{
		public static IServiceCollection AddInvitationDb(this IServiceCollection services, ServiceLifetime lifeTime = ServiceLifetime.Scoped)
		{

			var connectionString = "Server=127.0.0.1;Port=3306;Database=microservices;User Id=main;Password=main";

			Action<DbContextOptionsBuilder> optionAction = optionsBuilder =>
			{
				optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), contextOptionsBuilder =>
				{
					contextOptionsBuilder.MigrationsAssembly(typeof(InvitationDbContext).Assembly.FullName);
				});
			};

			services.AddDbContextFactory<InvitationDbContext>(optionAction);
			services.AddDbContext<InvitationDbContext>(optionAction);

			return services;
		}
	}
}
