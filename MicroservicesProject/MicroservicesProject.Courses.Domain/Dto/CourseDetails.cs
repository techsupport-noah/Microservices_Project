using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicesProject.Courses.Domain.Dto
{
	internal class CourseDetails
	{
		public Guid Id { get; set; }
		public string? Name { get; set; }
		//TODO add list of students in the course
	}
}
