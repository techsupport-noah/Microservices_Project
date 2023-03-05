namespace MicroservicesProject.Events.Service.Model
{
	public class Event
	{
		public Guid Id { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }
		public DateOnly Date { get; set; }
		public TimeOnly Time { get; set; }
		public Guid[]? InvitedCourses { get; set; }
	}
}
