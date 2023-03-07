using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicesProject.Events.Domain.Dto
{
	public class EventDetailsDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public DateOnly Date { get; set; }
		public TimeOnly Time { get; set; }
	}
}
