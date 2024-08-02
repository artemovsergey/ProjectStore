using MediatR;
using ProjectStore.Domen.Models;

namespace ProjectStore.Application.Requests;

public class DeleteRepositoryRequest(Repository repo) : IRequest<DeleteRepositoryRequest.Response>
{
    public Repository Repository = repo;
    public record Response(bool Result);
}