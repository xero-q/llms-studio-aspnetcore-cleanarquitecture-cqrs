using FluentValidation;

namespace Application.ModelTypes.Create;

public class CreateModelTypeCommandValidator : AbstractValidator<CreateModelTypeCommand>
{
    public CreateModelTypeCommandValidator()
    {
        RuleFor(m => m.Name).NotEmpty();
    }
}
