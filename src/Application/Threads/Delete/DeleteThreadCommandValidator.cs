using FluentValidation;

namespace Application.Threads.Delete;

internal sealed class DeleteThreadCommandValidator : AbstractValidator<DeleteThreadCommand>
{
    public DeleteThreadCommandValidator()
    {
        RuleFor(t => t.ThreadId).NotEmpty();
    }
}
