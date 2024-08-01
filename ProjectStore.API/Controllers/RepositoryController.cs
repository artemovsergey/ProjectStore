using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectStore.Application.Requests;
using Swashbuckle.AspNetCore.Annotations;

namespace ProjectStore.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RepositoryController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<RepositoryController> _logger;
    public RepositoryController(IMediator mediatr, ILogger<RepositoryController> logger)
    {
        _logger = logger;
        _mediator = mediatr;
    }
    
    //[Authorize(Roles = "User, Admin")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = "Получает все репозитории.")]
    [SwaggerResponse(StatusCodes.Status200OK, "All repo successfully retrieved")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Repo not found", typeof(ValidationProblemDetails))]
    public async Task<IActionResult> GetAllRepositories()
    {
        var response = await _mediator.Send(new RepositoryRequest());
        if(response.users == null)
        {
            return NotFound("Репозитории не найдены");
        }
        return Ok(response.users);
    }
    
}