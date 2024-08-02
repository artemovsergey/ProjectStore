using FluentValidation;
using MediatR;
using ProjectStore.Application.Generics;
using ProjectStore.Domen.Models;
using ProjectStore.Domen.Validations;

namespace ProjectStore.Application.Requests;

public record RepositoryRequest(string? sortColumn,
                                string? sortOrder,
                                int pageIndex,
                                int pageSize,
                                string? filterColumn,
                                string? filterQuery) : IRequest<RepositoryRequest.Response>
{
    public record Response(ApiResult<Repository> result);
}


// public class AddRepositoryRequestValidator : AbstractValidator<AddRepositoryRequest>
// {
//     public AddRepositoryRequestValidator()
//     {
//         RuleFor(x => x.repository).SetValidator(new RepositoryValidator());
//     }
// }