using FluentValidation;
using MediatR;
using ProjectStore.Domen.Models;
using ProjectStore.Domen.Validations;

namespace ProjectStore.Application.Requests;

public record RepositoryRequest() : IRequest<RepositoryRequest.Response>
{
    public const string RouteTemplate = "/api/repositories";
    public record Response(IEnumerable<Repository> users);
}


// public class AddRepositoryRequestValidator : AbstractValidator<AddRepositoryRequest>
// {
//     public AddRepositoryRequestValidator()
//     {
//         RuleFor(x => x.repository).SetValidator(new RepositoryValidator());
//     }
// }