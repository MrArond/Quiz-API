using MediatR;
using Quiz_API.Contracts.Common;
using System;

namespace Quiz_API.Application.Features.Quiz.Answers
{
    public class AddAnswerCommand : IRequest<Result<Guid>>
    {
        public Guid QuestionId { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }

        public AddAnswerCommand(Guid questionId, string text, bool isCorrect)
        {
            QuestionId = questionId;
            Text = text;
            IsCorrect = isCorrect;
        }
    }
}