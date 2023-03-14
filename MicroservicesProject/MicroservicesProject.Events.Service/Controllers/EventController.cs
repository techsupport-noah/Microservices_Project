using Microsoft.AspNetCore.Mvc;
using MicroservicesProject.Events.Domain.Dto;
using System.Net;
using AutoMapper;
using FluentValidation;
using MicroservicesProject.Events.Service.DataAccess;
using MicroservicesProject.Events.Service.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace MicroservicesProject.Events.Service.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class EventController : ControllerBase
	{
		private readonly ILogger<EventController> _logger;
		private readonly EventDbContext _dbContext;
		private readonly IMapper _mapper;
		private readonly IValidator<EventDetailsDto> _eValidator;

		public EventController(ILogger<EventController> logger, EventDbContext dbContext, IMapper mapper, IValidator<EventDetailsDto> validator)
		{
			_dbContext = dbContext;
			_logger = logger;
			_mapper = mapper;
			_eValidator = validator;
		}

		[HttpGet(Name = "GetAll")]
		[ProducesResponseType(typeof(IEnumerable<EventDetailsDto>), (int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		[ProducesResponseType((int)HttpStatusCode.Unauthorized)]
		[ProducesResponseType((int)HttpStatusCode.Forbidden)]
		[Authorize(Roles = "User")]
		public ActionResult<IEnumerable<EventDetailsDto>> GetAll()
		{
			var es = _dbContext.Events.ToList();
			var returnList = _mapper.Map<List<EventDetailsDto>>(es);
			return Ok(returnList);
		}

		[HttpPut(Name = "AddEvent")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		[ProducesResponseType((int)HttpStatusCode.Unauthorized)]
		[ProducesResponseType((int)HttpStatusCode.Forbidden)]
		[Authorize(Roles = "Administrator")]
		public ActionResult AddEvent(EventDetailsDto e)
		{
			var result = _eValidator.Validate(e);
			if (!result.IsValid)
			{
				//couldn't validate incomming data
				return ValidationProblem();
			}

			var eModel = _mapper.Map<Event>(e);
			_dbContext.Events.Add(eModel);
			if (_dbContext.SaveChanges() == 0)
			{
				//no data was written to the db --> error
				//possible constraints problem
				return Conflict();
			};
			return Ok();
		}

		[HttpPost(Name = "UpdateEvent")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		[ProducesResponseType((int)HttpStatusCode.Unauthorized)]
		[ProducesResponseType((int)HttpStatusCode.Forbidden)]
		[Authorize(Roles = "Administrator")]
		public ActionResult UpdateEvent(EventDetailsDto e)
		{
			var result = _eValidator.Validate(e);
			if (!result.IsValid)
			{
				//couldn't validate incomming data
				return ValidationProblem();
			}

			var eModel = _mapper.Map<Event>(e);

			var existingEvent = _dbContext.Events.FirstOrDefault(u => u.Id == eModel.Id);
			if (existingEvent == null)
			{
				_logger.LogDebug("Tried updating e which doesn't exist.");
				return NotFound();
			}

			_dbContext.Entry(existingEvent).CurrentValues.SetValues(eModel);

			_dbContext.SaveChanges();
			return Ok();
		}

		[HttpDelete(Name = "DeleteEvent")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		[ProducesResponseType((int)HttpStatusCode.Unauthorized)]
		[ProducesResponseType((int)HttpStatusCode.Forbidden)]
		[Authorize(Roles = "Administrator")]
		public ActionResult DeleteEvent(Guid id)
		{
			var existingEvent = _dbContext.Events.FirstOrDefault(u => u.Id == id);
			if (existingEvent == null)
			{
				_logger.LogDebug("Element not found");
				return NotFound();
			}

			_dbContext.Events.Remove(existingEvent);
			_dbContext.SaveChanges();
			return Ok();
		}
	}
}