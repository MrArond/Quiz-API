using MediatR;
using Microsoft.AspNetCore.Mvc;
using Quiz_API.Application.Features.Quiz.Questions;
using Quiz_API.Contracts.Question;

namespace Quiz_API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class QuestionController : ControllerBase
{
    private readonly IMediator _mediator;

    public QuestionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("AddQuestion")]

    public async Task<IActionResult> AddQuestion([FromBody] AddQuestionRequest addQuestionRequest)
    {
        var command = new AddQuestionCommand(
            addQuestionRequest.Text, addQuestionRequest.QuizId);

        var result = await _mediator.Send(command);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        return BadRequest(result.Error);
    }

    [HttpGet("GetQuestionsByQuizId/{quizId}")]

    public async Task<IActionResult> GetQuestionsByQuizId(Guid quizId)
    {
        var query = new GetQuestionQuery(quizId);
        var result = await _mediator.Send(query);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        return BadRequest(result.Error);
    }

    [HttpPut("UpdateQuestion")]
    public async Task<IActionResult> UpdateQuestion([FromBody] UpdateQuestionRequest request)
    {
        var command = new UpdateQuestionCommand(request.QuestionId, request.Text);
        var result = await _mediator.Send(command);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        return BadRequest(result.Error);
    }

    [HttpDelete("DeleteQuestion/{questionId}")]
    public async Task<IActionResult> DeleteQuestion(Guid questionId)
    {
        var command = new DeleteQuestionCommand(questionId);
        var result = await _mediator.Send(command);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        return BadRequest(result.Error);
    }
}
