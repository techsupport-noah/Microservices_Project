using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroservicesProject.Events.Domain.Dto;

namespace MicroservicesProject.Events.Domain.Validations
{
	public class EventValidator : AbstractValidator<EventDetailsDto>
	{
		public EventValidator()
		{
			RuleFor(x => x.Id).NotEmpty().WithMessage("Bitte geben Sie eine Id an");
			RuleFor(x => x.Date).NotEmpty().WithMessage("Bitte geben Sie ein Datum an");
			RuleFor(x => x.Description).NotEmpty().WithMessage("Bitte geben Sie eine Beschreibung an");
			RuleFor(x => x.Name).NotEmpty().WithMessage("Bitte geben Sie einen Namen an");
			RuleFor(x => x.Time).NotEmpty().WithMessage("Bitte geben Sie eine Zeit an");
		}
	}
}
