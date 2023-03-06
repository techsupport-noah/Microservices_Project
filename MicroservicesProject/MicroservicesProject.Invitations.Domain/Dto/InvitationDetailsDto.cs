using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicesProject.Invitations.Domain.Dto
{
	public class InvitationDetailsDto
	{
		public Guid Id { get; set; }
		public Guid EventId { get; set; }
		public Guid CourseId { get; set; }
	}
}
