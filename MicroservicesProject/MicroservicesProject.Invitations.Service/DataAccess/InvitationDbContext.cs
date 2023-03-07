using MicroservicesProject.Invitations.Service.Model;
using Microsoft.EntityFrameworkCore;

namespace MicroservicesProject.Invitations.Service.DataAccess
{
	public class InvitationDbContext : DbContext
	{
		public InvitationDbContext(DbContextOptions<InvitationDbContext> options) : base(options)
		{
		}

		public DbSet<Invitation> Invitations { get; set; }
	}
}
