
namespace MicroservicesProject.Invitations.Service.Model
{
	public class Invitation
	{
		public Guid Id { get; set; }
		public Guid EventId { get; set; }
		public Guid CourseId { get; set; }
	}
}
