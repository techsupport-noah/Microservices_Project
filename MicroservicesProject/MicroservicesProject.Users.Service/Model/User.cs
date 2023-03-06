namespace MicroservicesProject.Users.Service.Model
{
	public class User
	{
		public Guid Id { get; set; }
		public string? Name { get; set; }
		public string? Mail { get; set; }
		public string? Username { get; set; }
		private bool IsAdmin { get; set; }
	}
}
