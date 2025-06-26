using FluentValidation;
using SharedKernel;

namespace Application.Prompts.Create;

public class CreatePromptCommandValidator : AbstractValidator<CreatePromptCommand>
{
    public CreatePromptCommandValidator()
    {
        RuleFor(x=>x.PromptText).NotEmpty().WithMessage(ErrorMessages.PropertyCanNotBeEmpty("PromptText"));
    }
}
