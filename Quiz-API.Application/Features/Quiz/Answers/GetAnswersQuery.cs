using MediatR;
using Quiz_API.Application.Abstractions;
using Quiz_API.Contracts.Answer;
using Quiz_API.Contracts.Common;
using System;

namespace Quiz_API.Application.Features.Quiz.Answers
{
    public class GetAnswersQuery : IRequest<Result<GetAnswersResponse>>
    {
        public Guid QuestionId { get; set; }

        public GetAnswersQuery(Guid questionId)
        {
            QuestionId = questionId;
        }
    }
}