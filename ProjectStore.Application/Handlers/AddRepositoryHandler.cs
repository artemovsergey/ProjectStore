using MediatR;
using Microsoft.Extensions.Logging;
using Npgsql;
using ProjectStore.Application.Requests;
using ProjectStore.Domen.Models;
using ProjectStore.Infrastructure.Data;

namespace ProjectStore.Application.Handlers;

public class AddRepositoryHandler : IRequestHandler<AddRepositoryRequest, AddRepositoryRequest.Response>
{
    private readonly ProjectStoreContext _db;
    private readonly ILogger<AddRepositoryHandler> _log;
    public AddRepositoryHandler(ProjectStoreContext db, ILogger<AddRepositoryHandler> log)
    {
        _db = db;
        _log = log;
    }
    
    public async Task<AddRepositoryRequest.Response> Handle(AddRepositoryRequest request, CancellationToken cancellationToken)
    {
        _log.LogWarning("Создание объекта репозитория");

        try
        {
            _db.Repositories.Add(request.Repository);
            await _db.SaveChangesAsync();
            _log.LogInformation($"Репозиторий создан!"); 
        }
        catch (NpgsqlException e)
        {
            _log.LogError($"NpgsqlException: {e.InnerException}"); 
        }
        catch (Exception e)
        {
            _log.LogError($"Exception: {e.InnerException}");
        }
        
        return new AddRepositoryRequest.Response(new Repository());
        
    }
}