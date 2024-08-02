using MediatR;
using Microsoft.Extensions.Logging;
using Npgsql;
using ProjectStore.Application.Requests;
using ProjectStore.Infrastructure.Data;

namespace ProjectStore.Application.Handlers;

public class EditRepositoryHandler : IRequestHandler<EditRepositoryRequest, EditRepositoryRequest.Response>
{
    private readonly ProjectStoreContext _db;
    private readonly ILogger<EditRepositoryHandler> _log;

    public EditRepositoryHandler(ProjectStoreContext db, ILogger<EditRepositoryHandler> log)
    {
        _db = db;
        _log = log;
    }


    public async Task<EditRepositoryRequest.Response> Handle(EditRepositoryRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            _db.Update(request.Repository);
            await _db.SaveChangesAsync();
            _log.LogInformation($"Репозиторий отредактирован");
        }
        catch (NpgsqlException e)
        {
            _log.LogError($"NpgsqlException: {e.InnerException}");
        }
        catch (Exception e)
        {
            _log.LogError($"Exception: {e.InnerException}");
        }

        return new EditRepositoryRequest.Response(request.Repository);
    }
 

}
