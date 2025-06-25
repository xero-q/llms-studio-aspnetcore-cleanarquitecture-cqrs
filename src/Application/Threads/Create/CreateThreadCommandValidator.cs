using FluentValidation;
using SharedKernel;

namespace Application.Threads.Create;

public class CreateThreadCommandValidator : AbstractValidator<CreateThreadCommand>
{
    public CreateThreadCommandValidator()
    {
        RuleFor(x=>x.Title).NotEmpty().WithMessage(ErrorMessages.PropertyCanNotBeEmpty("Title"));
    }
}
