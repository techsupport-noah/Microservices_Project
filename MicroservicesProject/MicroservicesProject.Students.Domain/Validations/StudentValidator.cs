using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroservicesProject.Students.Domain.Dto;

namespace MicroservicesProject.Students.Domain.Validations
{
	public class StudentValidator : AbstractValidator<StudentDetailsDto>
	{
		public StudentValidator()
		{
			RuleFor(x => x.Id).NotEmpty().WithMessage("Bitte geben Sie eine Id an");
			RuleFor(x => x.Mail).NotEmpty().WithMessage("Bitte geben Sie eine Mail Adresse an");
			RuleFor(x => x.Name).NotEmpty().WithMessage("Bitte geben Sie einen Namen an");
			RuleFor(x => x.Username).NotEmpty().WithMessage("Bitte geben Sie einen Benutzernamen an");
		}
	}
}
