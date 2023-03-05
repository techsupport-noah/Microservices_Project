using MicroservicesProject.Professors.Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MicroservicesProject.Professors.Service.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ProfessorController : ControllerBase
	{
		private List<ProfessorDetailsDto> _professorsList = new();

		private readonly ILogger<ProfessorController> _logger;

		public ProfessorController(ILogger<ProfessorController> logger)
		{
			_logger = logger;
		}

		[HttpGet(Name = "GetAll")]
		[ProducesResponseType(typeof(IEnumerable<ProfessorDetailsDto>), (int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public ActionResult<IEnumerable<ProfessorDetailsDto>> GetAll()
		{
			return Ok(_professorsList);
		}

		[HttpPut(Name = "AddProfessor")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public ActionResult AddProfessor(ProfessorDetailsDto professor)
		{
			_professorsList.Add(professor);
			return Ok();
		}

		[HttpPost(Name = "UpdateProfessor")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public ActionResult UpdateProfessor(ProfessorDetailsDto professor)
		{
			var x = _professorsList.FirstOrDefault(x => x.Id == professor.Id);
			if (x != null)
			{
				_professorsList.Remove(x);
				_professorsList.Add(professor);
				return Ok();
			}

			return NotFound();
		}

		[HttpDelete(Name = "DeleteProfessor")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public ActionResult DeleteProfessor(Guid id)
		{
			var x = _professorsList.FirstOrDefault(x => x.Id == id);
			if (x != null)
			{
				_professorsList.Remove(x);
				return Ok();
			}

			return NotFound();
		}
	}
}