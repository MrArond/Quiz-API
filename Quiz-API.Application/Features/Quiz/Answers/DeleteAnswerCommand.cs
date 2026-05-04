using Quiz_API.Contracts.Common;
using MediatR;
using System;

namespace Quiz_API.Application.Features.Quiz.Answers
{
    public class DeleteAnswerCommand : IRequest<Result<bool>>
    {
        public Guid AnswerId { get; set; }

        public DeleteAnswerCommand(Guid answerId)
        {
            AnswerId = answerId;
        }
    }
}