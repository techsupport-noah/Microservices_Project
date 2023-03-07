namespace MicroservicesProject.Students.Service.Model
{
	//equals the StudentDetailsDto definition because all our data is meant to be shared across components (no internal data), so both models don't differ
	public class Student
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Mail { get; set; }
		public string Username { get; set; }

		public Guid? CourseId { get; set; }
	}
}
