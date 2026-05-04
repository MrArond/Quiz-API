using MediatR;
using Microsoft.AspNetCore.Mvc;
using Quiz_API.Application.Features.Quiz.Answers;
using Quiz_API.Contracts.Answer;
using System;
using System.Threading.Tasks;

namespace Quiz_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnswerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AnswerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("AddAnswer")]
        public async Task<IActionResult> AddAnswer([FromBody] AddAnswerRequest request)
        {
            var command = new AddAnswerCommand(request.QuestionId, request.Text, request.IsCorrect);
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }

        [HttpPut("UpdateAnswer")]
        public async Task<IActionResult> UpdateAnswer([FromBody] UpdateAnswerRequest request)
        {
            var command = new UpdateAnswerCommand(request.AnswerId, request.Text, request.IsCorrect);
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }

        [HttpDelete("DeleteAnswer/{answerId}")]
        public async Task<IActionResult> DeleteAnswer(Guid answerId)
        {
            var command = new DeleteAnswerCommand(answerId);
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }

        [HttpGet("GetAnswersByQuestionId/{questionId}")]
        public async Task<IActionResult> GetAnswersByQuestionId(Guid questionId)
        {
            var query = new GetAnswersQuery(questionId);
            var result = await _mediator.Send(query);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
    }
}