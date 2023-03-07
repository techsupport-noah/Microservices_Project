using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicesProject.Students.Domain.Dto
{
	public class StudentDetailsDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Mail { get; set; }
		public string Username { get; set; }

		public Guid? CourseId { get; set; }
	}
}
