using System.Data;
using System.Net;
using AutoMapper;
using FluentValidation;
using MicroservicesProject.Invitations.Domain.Dto;
using MicroservicesProject.Invitations.Service.DataAccess;
using MicroservicesProject.Invitations.Service.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MicroservicesProject.Invitations.Service.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class InvitationController : ControllerBase
	{
		private readonly ILogger<InvitationController> _logger;
		private readonly InvitationDbContext _dbContext;
		private readonly IMapper _mapper;
		private readonly IValidator<InvitationDetailsDto> _invitationValidator;


		public InvitationController(ILogger<InvitationController> logger, InvitationDbContext dbContext, IMapper mapper, IValidator<InvitationDetailsDto> validator)
		{
			_dbContext = dbContext;
			_logger = logger;
			_mapper = mapper;
			_invitationValidator = validator;
		}

		[HttpGet(Name = "GetAll")]
		[ProducesResponseType(typeof(IEnumerable<InvitationDetailsDto>), (int) HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		[ProducesResponseType((int)HttpStatusCode.Unauthorized)]
		[ProducesResponseType((int)HttpStatusCode.Forbidden)]
		[Authorize(Roles = "User")]
		public ActionResult<IEnumerable<InvitationDetailsDto>> GetAll()
		{
			var invitations = _dbContext.Invitations.ToList();
			var returnList = _mapper.Map<List<InvitationDetailsDto>>(invitations);
			return Ok(returnList);
		}

		[HttpPut(Name = "AddInvitation")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		[ProducesResponseType((int)HttpStatusCode.Unauthorized)]
		[ProducesResponseType((int)HttpStatusCode.Forbidden)]
		[Authorize(Roles = "Administrator")]
		public ActionResult AddInvitation(InvitationDetailsDto invitation)
		{
			var result = _invitationValidator.Validate(invitation);
			if (!result.IsValid)
			{
				//couldn't validate incomming data
				return ValidationProblem();
			}

			var invitationModel = _mapper.Map<Invitation>(invitation);
			_dbContext.Invitations.Add(invitationModel);
			if (_dbContext.SaveChanges() == 0)
			{
				//no data was written to the db --> error
				//possible constraints problem
				return Conflict();
			};
			return Ok();
		}

		[HttpPost(Name = "UpdateInvitation")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		[ProducesResponseType((int)HttpStatusCode.Unauthorized)]
		[ProducesResponseType((int)HttpStatusCode.Forbidden)]
		[Authorize(Roles = "Administrator")]
		public ActionResult UpdateInvitation(InvitationDetailsDto invitation)
		{
			var result = _invitationValidator.Validate(invitation);
			if (!result.IsValid)
			{
				//couldn't validate incomming data
				return ValidationProblem();
			}

			var invitationModel = _mapper.Map<Invitation>(invitation);

			var existingInvitation = _dbContext.Invitations.FirstOrDefault(u => u.Id == invitationModel.Id);
			if (existingInvitation == null)
			{
				_logger.LogDebug("Tried updating invitation which doesn't exist.");
				return NotFound();
			}

			_dbContext.Entry(existingInvitation).CurrentValues.SetValues(invitationModel);

			_dbContext.SaveChanges();
			return Ok();
		}

		[HttpDelete(Name = "DeleteInvitation")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		[ProducesResponseType((int)HttpStatusCode.Unauthorized)]
		[ProducesResponseType((int)HttpStatusCode.Forbidden)]
		[Authorize(Roles = "Administrator")]
		public ActionResult DeleteInvitation(Guid id)
		{
			var existingInvitation = _dbContext.Invitations.FirstOrDefault(u => u.Id == id);
			if (existingInvitation == null)
			{
				_logger.LogDebug("Element not found");
				return NotFound();
			}

			_dbContext.Invitations.Remove(existingInvitation);
			_dbContext.SaveChanges();
			return Ok();
		}
	}
}