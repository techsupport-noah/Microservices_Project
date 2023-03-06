using MicroservicesProject.Events.Service.Model;
using Microsoft.EntityFrameworkCore;

namespace MicroservicesProject.Events.Service.DataAccess
{
	public class EventDbContext : DbContext
	{
		public EventDbContext(DbContextOptions<EventDbContext> options) : base(options)
		{
		}

		public DbSet<Event>? Events { get; set; }
	}
}


