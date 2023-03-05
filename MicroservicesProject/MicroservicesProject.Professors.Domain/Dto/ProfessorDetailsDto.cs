using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicesProject.Professors.Domain.Dto
{
	public class ProfessorDetailsDto
	{
		public Guid Id { get; set; }
		public string? Name { get; set; }
		public string? Mail { get; set; }
		public string? Username { get; set; }

	}
}
