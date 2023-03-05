using Microsoft.AspNetCore.Mvc;
using MicroservicesProject.Events.Domain.Dto;
using System.Net;

namespace MicroservicesProject.Events.Service.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class EventController : ControllerBase
	{
		private List<EventDetailsDto> _eventsList = new();

		private readonly ILogger<EventController> _logger;

		public EventController(ILogger<EventController> logger)
		{
			_logger = logger;
		}

		[HttpGet(Name = "GetAll")]
		[ProducesResponseType(typeof(IEnumerable<EventDetailsDto>), (int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public ActionResult<IEnumerable<EventDetailsDto>> GetAll()
		{
			return Ok(_eventsList);
		}

		[HttpPut(Name = "AddEvent")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public ActionResult AddEvent(EventDetailsDto e)
		{
			_eventsList.Add(e);
			return Ok();
		}

		[HttpPost(Name = "UpdateEvent")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public ActionResult UpdateEvent(EventDetailsDto e)
		{
			var x = _eventsList.FirstOrDefault(x => x.Id == e.Id);
			if (x != null)
			{
				_eventsList.Remove(x);
				_eventsList.Add(e);
				return Ok();
			}

			return NotFound();
		}

		[HttpDelete(Name = "DeleteEvent")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public ActionResult DeleteEvent(Guid id)
		{
			var x = _eventsList.FirstOrDefault(x => x.Id == id);
			if (x != null)
			{
				_eventsList.Remove(x);
				return Ok();
			}

			return NotFound();
		}
	}
}