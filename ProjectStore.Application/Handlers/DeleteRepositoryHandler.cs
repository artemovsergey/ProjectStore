using MediatR;
using Microsoft.Extensions.Logging;
using Npgsql;
using ProjectStore.Application.Requests;
using ProjectStore.Infrastructure.Data;

namespace ProjectStore.Application.Handlers;

public class DeleteRepositoryHandler : IRequestHandler<DeleteRepositoryRequest, DeleteRepositoryRequest.Response>
{
    private readonly ProjectStoreContext _db;
    private readonly ILogger<DeleteRepositoryHandler> _log;
    public DeleteRepositoryHandler(ProjectStoreContext db, ILogger<DeleteRepositoryHandler> log)
    {
        _db = db;
        _log = log;
    }
    
    public async Task<DeleteRepositoryRequest.Response> Handle(DeleteRepositoryRequest request, CancellationToken cancellationToken)
    {
        try
        {
            _db.Remove(request.Repository);
            await _db.SaveChangesAsync();
            _log.LogInformation($"Репозиторий с Id = {request.Repository.Id} удален!" );
        }
        catch (NpgsqlException e)
        {
            _log.LogError($"NpgsqlException: {e.InnerException}");
        }
        catch (Exception e)
        {
            _log.LogError($"Exception: {e.InnerException}");
        }

        return new DeleteRepositoryRequest.Response(true);
    }
}