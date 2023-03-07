using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MicroservicesProject.Users.Domain.Dto;

namespace MicroservicesProject.Users.Domain.Validations
{
	public class UserValidator : AbstractValidator<UserDetailsDto>
	{
		public UserValidator()
		{
			RuleFor(x => x.Id).NotEmpty().WithMessage("Bitte geben Sie eine Id an");
			RuleFor(x => x.Mail).NotEmpty().WithMessage("Bitte geben Sie eine Mail Adresse an");
			RuleFor(x => x.Name).NotEmpty().WithMessage("Bitte geben Sie einen Namen an");
			RuleFor(x => x.Username).NotEmpty().WithMessage("Bitte geben Sie einen Benutzernamen an");
		}
	}
}
