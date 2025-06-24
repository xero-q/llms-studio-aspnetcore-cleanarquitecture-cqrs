using FluentValidation;
using SharedKernel;

namespace Application.Models.Create;

public class CreateModelCommandValidator : AbstractValidator<CreateModelCommand>
{
    public CreateModelCommandValidator()
    {
        RuleFor(m => m.Name).NotEmpty().WithMessage(ErrorMessages.PropertyCanNotBeEmpty("Name"));
        RuleFor(m => m.Identifier).NotEmpty().WithMessage(ErrorMessages.PropertyCanNotBeEmpty("Identifier"));
        RuleFor(m => m.EnvironmentVariable).NotEmpty().WithMessage(ErrorMessages.PropertyCanNotBeEmpty("EnvironmentVariable"));
        
        RuleFor(m => m.Temperature)
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(1)
            .WithMessage(ErrorMessages.TemperatureInvalidValue);
    }
}
