using Microsoft.AspNetCore.Mvc;
using MicroservicesProject.Events.Domain.Dto;
using System.Net;

namespace MicroservicesProject.Events.Service.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class EventController : ControllerBase
	{
		private readonly ILogger<EventController> _logger;

		public EventController(ILogger<EventController> logger)
		{
			_logger = logger;
		}

		[HttpGet(Name = "GetAll")]
		[ProducesResponseType(typeof(IEnumerable<EventDetails>), (int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public ActionResult<IEnumerable<EventDetails>> GetAll()
		{
			return Ok(new List<EventDetails>());
		}
	}
}