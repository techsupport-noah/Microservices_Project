using System.Net;
using MicroservicesProject.Invitations.Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace MicroservicesProject.Invitations.Service.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class InvitationController : ControllerBase
	{
		private List<InvitationDetailsDto> _invitationsList = new();
		private readonly ILogger<InvitationController> _logger;

		public InvitationController(ILogger<InvitationController> logger)
		{
			_logger = logger;
		}

		[HttpGet(Name = "GetAll")]
		[ProducesResponseType(typeof(IEnumerable<InvitationDetailsDto>), (int) HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public ActionResult<IEnumerable<InvitationDetailsDto>> GetAll()
		{
			return Ok(_invitationsList);
		}

		[HttpPut(Name = "AddInvitation")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public ActionResult AddInvitation(InvitationDetailsDto invitation)
		{
			_invitationsList.Add(invitation);
			return Ok();
		}

		[HttpPost(Name = "UpdateInvitation")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public ActionResult UpdateInvitation(InvitationDetailsDto invitation)
		{
			var x = _invitationsList.FirstOrDefault(x => x.Id == invitation.Id);
			if (x != null)
			{
				_invitationsList.Remove(x);
				_invitationsList.Add(invitation);
				return Ok();
			}

			return NotFound();
		}

		[HttpDelete(Name = "DeleteInvitation")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public ActionResult DeleteInvitation(Guid id)
		{
			var x = _invitationsList.FirstOrDefault(x => x.Id == id);
			if (x != null)
			{
				_invitationsList.Remove(x);
				return Ok();
			}

			return NotFound();
		}
	}
}