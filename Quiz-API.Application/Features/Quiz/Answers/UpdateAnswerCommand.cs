using Quiz_API.Contracts.Common;
using MediatR;
using System;

namespace Quiz_API.Application.Features.Quiz.Answers
{
    public class UpdateAnswerCommand : IRequest<Result<Guid>>
    {
        public Guid AnswerId { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }

        public UpdateAnswerCommand(Guid answerId, string text, bool isCorrect)
        {
            AnswerId = answerId;
            Text = text;
            IsCorrect = isCorrect;
        }
    }
}