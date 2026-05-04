using MediatR;
using MediatR;
using Quiz_API.Contracts.Common;
using System;

namespace Quiz_API.Application.Features.Quiz.Questions
{
    public class UpdateQuestionCommand : IRequest<Result<Guid>>
    {
        public Guid QuestionId { get; set; }
        public string Text { get; set; }

        public UpdateQuestionCommand(Guid questionId, string text)
        {
            QuestionId = questionId;
            Text = text;
        }
    }
}