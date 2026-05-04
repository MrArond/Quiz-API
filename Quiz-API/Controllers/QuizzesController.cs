using MediatR;
using Microsoft.AspNetCore.Mvc;
using Quiz_API.Application.Features.AddQuiz;
using Quiz_API.Application.Features.Quiz.ListQuiz;
using Quiz_API.Contracts.Quiz;

namespace Quiz_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuizzesController : ControllerBase
{
    private readonly IMediator _mediator;
    public QuizzesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetQuizzes")]
    public async Task<IActionResult> GetQuizzes()
    {
        var query = new GetQuizzesQuery();
        var result = await _mediator.Send(query);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        return BadRequest(result.Error);
    }

    [HttpPost("AddQuiz")]
    public async Task<IActionResult> AddQuiz([FromBody] AddQuizRequest request)
    {
        var command = new AddQuizCommand(request.Name, request.Description);
        var result = await _mediator.Send(command);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        return BadRequest(result.Error);
    }
}

