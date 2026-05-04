using MediatR;
using MediatR;
using Quiz_API.Contracts.Common;
using System;

namespace Quiz_API.Application.Features.Quiz.Questions
{
    public class DeleteQuestionCommand : IRequest<Result<bool>>
    {
        public Guid QuestionId { get; set; }

        public DeleteQuestionCommand(Guid questionId)
        {
            QuestionId = questionId;
        }
    }
}