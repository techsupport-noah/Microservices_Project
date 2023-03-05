using MicroservicesProject.Professors.Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MicroservicesProject.Professors.Service.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ProfessorController : ControllerBase
	{
		private readonly ILogger<ProfessorController> _logger;

		public ProfessorController(ILogger<ProfessorController> logger)
		{
			_logger = logger;
		}

		[HttpGet(Name = "GetAll")]
		[ProducesResponseType(typeof(IEnumerable<ProfessorDetails>), (int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public ActionResult<IEnumerable<ProfessorDetails>> GetAll()
		{
			return Ok(new List<ProfessorDetails>());
		}
	}
}