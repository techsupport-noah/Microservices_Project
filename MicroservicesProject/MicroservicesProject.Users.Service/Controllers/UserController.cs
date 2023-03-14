using MicroservicesProject.Users.Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.ComponentModel.DataAnnotations;
using System.Net;
using FluentValidation;
using MicroservicesProject.Users.Service.DataAccess;
using MicroservicesProject.Users.Service.Model;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace MicroservicesProject.Users.Service.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class UserController : ControllerBase
	{
		private readonly ILogger<UserController> _logger;
		private readonly UserDbContext _dbContext;
		private readonly IMapper _mapper;
		private readonly IValidator<UserDetailsDto> _userValidator;

		public UserController(ILogger<UserController> logger, UserDbContext dbContext, IMapper mapper, IValidator<UserDetailsDto> validator)
		{
			_dbContext = dbContext;
			_logger = logger;
			_mapper = mapper;
			_userValidator = validator;
		}

		[HttpGet(Name = "GetAll")]
		[ProducesResponseType(typeof(IEnumerable<UserDetailsDto>), (int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		[ProducesResponseType((int)HttpStatusCode.Unauthorized)]
		[ProducesResponseType((int)HttpStatusCode.Forbidden)]
		[Authorize(Roles = "Administrator")]
		public ActionResult<IEnumerable<UserDetailsDto>> GetAll()
		{
			var users = _dbContext.Users.ToList();
			var returnList = _mapper.Map<List<UserDetailsDto>>(users);
			return Ok(returnList);
		}

		[HttpPut(Name = "AddUser")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		[ProducesResponseType((int)HttpStatusCode.Unauthorized)]
		[ProducesResponseType((int)HttpStatusCode.Forbidden)]
		[Authorize(Roles = "Administrator")]
		public ActionResult AddUser(UserDetailsDto user)
		{
			var result = _userValidator.Validate(user);
			if (!result.IsValid)
			{
				//couldn't validate incomming data
				return ValidationProblem();
			}

			var userModel = _mapper.Map<User>(user);
			_dbContext.Users.Add(userModel);
			if (_dbContext.SaveChanges() == 0)
			{
				//no data was written to the db --> error
				//possible constraints problem
				return Conflict();
			};
			return Ok();
		}

		[HttpPost(Name = "UpdateUser")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		[ProducesResponseType((int)HttpStatusCode.Unauthorized)]
		[ProducesResponseType((int)HttpStatusCode.Forbidden)]
		[Authorize(Roles = "Administrator")]
		public ActionResult UpdateUser(UserDetailsDto user)
		{
			var result = _userValidator.Validate(user);
			if (!result.IsValid)
			{
				//couldn't validate incomming data
				return ValidationProblem();
			}

			var userModel = _mapper.Map<User>(user);

			var existingUser = _dbContext.Users.FirstOrDefault( u => u.Id == userModel.Id);
			if (existingUser == null)
			{
				_logger.LogDebug("Tried updating user which doesn't exist.");
				return NotFound();
			}

			_dbContext.Entry(existingUser).CurrentValues.SetValues(userModel);
			
			_dbContext.SaveChanges();
			return Ok();
		}

		[HttpDelete(Name = "DeleteUser")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		[ProducesResponseType((int)HttpStatusCode.Unauthorized)]
		[ProducesResponseType((int)HttpStatusCode.Forbidden)]
		[Authorize(Roles = "Administrator")]
		public ActionResult DeleteUser(Guid id)
		{
			var existingUser = _dbContext.Users.FirstOrDefault(u => u.Id == id);
			if (existingUser == null)
			{
				_logger.LogDebug("Element not found");
				return NotFound();
			}

			_dbContext.Users.Remove(existingUser);
			_dbContext.SaveChanges();
			return Ok();
		}
	}
}