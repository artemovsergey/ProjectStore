using System.Net.Http.Json;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectStore.Application.Requests;
using ProjectStore.Infrastructure.Data;

namespace ProjectStore.Application.Handlers;

public class UsersHandler : IRequestHandler<RepositoryRequest, RepositoryRequest.Response>
{
    private readonly ProjectStoreContext _db;
    private readonly ILogger<UsersHandler> _log;
    public UsersHandler(ProjectStoreContext db, ILogger<UsersHandler> log)
    {
        _db = db;
        _log = log;
    }

    public async Task<RepositoryRequest.Response> Handle(RepositoryRequest request, CancellationToken cancellationToken)
    {
        _log.LogWarning("Обработка запроса из базы данных!");
       
        //var users = new List<User>() { new User() { Id = 1, Name = "user", Login = "login", Password = "password" } };
        var users = await _db.Repositories.ToListAsync();
        
        
        
        return new RepositoryRequest.Response(users);
    }
}