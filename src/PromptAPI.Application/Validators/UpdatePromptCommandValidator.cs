using FluentValidation;
using PromptAPI.Application.Commands;

namespace PromptAPI.Application.Validators;

public class UpdatePromptCommandValidator : AbstractValidator<UpdatePromptCommand>
{
    public UpdatePromptCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("O ID é obrigatório");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("O título é obrigatório")
            .MaximumLength(200).WithMessage("O título não pode ter mais de 200 caracteres");

        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("O conteúdo é obrigatório")
            .MinimumLength(10).WithMessage("O conteúdo deve ter pelo menos 10 caracteres");

        RuleFor(x => x.Category)
            .NotEmpty().WithMessage("A categoria é obrigatória")
            .MaximumLength(100).WithMessage("A categoria não pode ter mais de 100 caracteres");
    }
}
