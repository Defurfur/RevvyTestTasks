using FluentValidation;

namespace RevvyTasks.Abstractions;

public class ClerkCollectionValidator : AbstractValidator<IClerkCollection>
{
    public ClerkCollectionValidator()
    {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.Clerks).NotEmpty();
    }
}
