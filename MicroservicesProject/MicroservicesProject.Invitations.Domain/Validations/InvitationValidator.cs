using FluentValidation;
using MicroservicesProject.Invitations.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicesProject.Invitations.Domain.Validations
{
	public class InvitationValidator : AbstractValidator<InvitationDetailsDto>
	{
		public InvitationValidator()
		{
			RuleFor(x => x.Id).NotEmpty().WithMessage("Bitte geben Sie eine Id an");
			RuleFor(x => x.CourseId).NotEmpty().WithMessage("Bitte geben Sie eine Kurs Id an");
			RuleFor(x => x.EventId).NotEmpty().WithMessage("Bitte geben Sie eine Event Id an");
		}
	}
}
