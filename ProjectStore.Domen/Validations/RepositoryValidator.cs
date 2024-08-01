namespace ProjectStore.Domen.Validations;

public class RepositoryValidator : AbstractValidator<Repository>
{
    public RepositoryValidator()
    {
        RuleFor(r => r.Name).Length(5).WithMessage("Please enter a name repository");
    }
    
}