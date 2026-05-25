using FluentValidation;
using Momentum.Application.DTOs.Habits;

namespace Momentum.Application.Validators.Habits;

public class CreateHabitRequestValidator
    : AbstractValidator<CreateHabitRequest>
{
    public CreateHabitRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Título é obrigatório.")
            .MaximumLength(100)
            .WithMessage("Título deve ter no máximo 100 caracteres.");

        RuleFor(x => x.Description)
            .MaximumLength(300)
            .WithMessage("Descrição deve ter no máximo 300 caracteres.");

        RuleFor(x => x.Frequency)
            .IsInEnum()
            .WithMessage("Frequência inválida.");
    }
}