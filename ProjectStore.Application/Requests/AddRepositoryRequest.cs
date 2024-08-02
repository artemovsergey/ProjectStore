using MediatR;
using ProjectStore.Domen.Models;

namespace ProjectStore.Application.Requests;

public record AddRepositoryRequest(Repository repo) : IRequest<AddRepositoryRequest.Response>
{
    public Repository Repository = repo;
    public const string RouteTemplate = "/api/Repository";
    public record Response(Repository repo);
}