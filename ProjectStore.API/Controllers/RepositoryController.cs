using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectStore.Application.Requests;
using ProjectStore.Domen.Models;
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
    
    [Authorize]
    //[Authorize(Roles = "Administrator")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = "Получает все репозитории.")]
    [SwaggerResponse(StatusCodes.Status200OK, "All repo successfully retrieved")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Repo not found", typeof(ValidationProblemDetails))]
    public async Task<IActionResult> GetAllRepositories(string? sortColumn = null,
        string? sortOrder = null,
        int pageIndex = 0,
        int pageSize = 10,
        string? filterColumn = null,
        string? filterQuery = null)
    {
        var response = await _mediator.Send(new RepositoryRequest(sortColumn,sortOrder,pageIndex,pageSize,filterColumn,filterQuery));
        
        if(response.result == null)
        {
            return NotFound("Репозитории не найдены");
        }
        return Ok(response.result);
    }

   
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation(Summary = "Создание репозитория.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Repo created success")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Repo not created", typeof(ValidationProblemDetails))]
    public async Task<ActionResult<Repository>> PostRepository([FromBody] Repository repo)
    {
        var response = await _mediator.Send(new AddRepositoryRequest(repo));
        if(response.repo == null)
        {
            return BadRequest("Репозиторий не создан!");
        }
        return Ok("Новый репозиторий создан!");
    }
    
    [Authorize]
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation(Summary = "Редактирование репозитория.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Repo edited success")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Repo not edited", typeof(ValidationProblemDetails))]
    public async Task<ActionResult<Repository>> EditRepository([FromBody] Repository repo)
    {
        var response = await _mediator.Send(new EditRepositoryRequest(repo));
        if(response.repo == null)
        {
            return BadRequest("Репозиторий не отредактирован!");
        }
        return Ok("Репозиторий отредактирован!");
    }
    
    
    
    //TODO Оптимизировать
    
    [Authorize]
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation(Summary = "Удаление репозитория.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Repo deleted success")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Repo not deleted", typeof(ValidationProblemDetails))]
    public async Task<ActionResult<bool>> DeleteRepository([FromBody] Repository repo)
    {
        var response = await _mediator.Send(new DeleteRepositoryRequest(repo));
        if(response.Result == false)
        {
            return BadRequest("Репозиторий не удален!");
        }
        return Ok("Репозиторий удален!");
    }
    
    
    
    
    
}