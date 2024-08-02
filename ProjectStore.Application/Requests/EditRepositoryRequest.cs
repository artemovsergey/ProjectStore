using MediatR;
using ProjectStore.Domen.Models;

namespace ProjectStore.Application.Requests;

public class EditRepositoryRequest(Repository repo) : IRequest<EditRepositoryRequest.Response>
{
    public string RouteTemplate = "/api/Repository";

    public Repository Repository = repo;
    public record Response(Repository repo);
}