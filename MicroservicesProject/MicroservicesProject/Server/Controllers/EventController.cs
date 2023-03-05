using System.Numerics;
using MicroservicesProject.Shared;
using Microsoft.AspNetCore.Mvc;

namespace MicroservicesProject.Server.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class EventController : ControllerBase
	{
		private static readonly string[] Summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};

		private readonly ILogger<EventController> _logger;

		public EventController(ILogger<EventController> logger)
		{
			_logger = logger;
		}

		[HttpGet]
		public IEnumerable<Event> Get()
		{
			return Enumerable.Range(1, 5).Select(index => new Event
				{
					Id = 1,
					Name = "Test",
					Description = "Test"
				})
				.ToArray();
		}
	}
}