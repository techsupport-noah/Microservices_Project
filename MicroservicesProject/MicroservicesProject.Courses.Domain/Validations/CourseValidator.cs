using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroservicesProject.Courses.Domain.Dto;

namespace MicroservicesProject.Courses.Domain.Validations
{
	public class CourseValidator : AbstractValidator<CourseDetailsDto>
	{
		public CourseValidator()
		{
			RuleFor(x => x.Id).NotEmpty().WithMessage("Bitte geben Sie eine Id an");
			RuleFor(x => x.Name).NotEmpty().WithMessage("Bitte geben Sie einen Namen an");
		}
	}
}
