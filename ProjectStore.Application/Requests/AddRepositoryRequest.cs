using FluentValidation;
using MediatR;
using ProjectStore.Domen.Models;
using ProjectStore.Domen.Validations;

namespace ProjectStore.Application.Requests;

public record AddRepositoryRequest(Repository repository) : IRequest<AddRepositoryRequest.Response>
{
    public const string RouteTemplate = "api/repository";
    public record Response(int repositoryIdd);
}

public class AddRepositoryRequestValidator : AbstractValidator<AddRepositoryRequest>
{
    public AddRepositoryRequestValidator()
    {
        RuleFor(x => x.repository).SetValidator(new RepositoryValidator());
    }
}