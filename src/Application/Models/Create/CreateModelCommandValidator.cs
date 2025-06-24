using FluentValidation;

namespace Application.ModelTypes.Create;

public class CreateModelCommandValidator : AbstractValidator<CreateModelTypeCommand>
{
    public CreateModelCommandValidator()
    {
        RuleFor(m => m.Name).NotEmpty();
    }
}
