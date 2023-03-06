using MicroservicesProject.Users.Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MicroservicesProject.Users.Service.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class UserController : ControllerBase
	{
		private List<UserDetailsDto> _usersList = new();

		private readonly ILogger<UserController> _logger;

		public UserController(ILogger<UserController> logger)
		{
			_logger = logger;
		}

		[HttpGet(Name = "GetAll")]
		[ProducesResponseType(typeof(IEnumerable<UserDetailsDto>), (int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public ActionResult<IEnumerable<UserDetailsDto>> GetAll()
		{
			return Ok(_usersList);
		}

		[HttpPut(Name = "AddUser")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public ActionResult AddUser(UserDetailsDto user)
		{
			_usersList.Add(user);
			return Ok();
		}

		[HttpPost(Name = "UpdateUser")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public ActionResult UpdateUser(UserDetailsDto user)
		{
			var x = _usersList.FirstOrDefault(x => x.Id == user.Id);
			if (x != null)
			{
				_usersList.Remove(x);
				_usersList.Add(user);
				return Ok();
			}

			return NotFound();
		}

		[HttpDelete(Name = "DeleteUser")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public ActionResult DeleteUser(Guid id)
		{
			var x = _usersList.FirstOrDefault(x => x.Id == id);
			if (x != null)
			{
				_usersList.Remove(x);
				return Ok();
			}

			return NotFound();
		}
	}
}