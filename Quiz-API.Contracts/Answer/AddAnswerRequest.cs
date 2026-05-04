using System;

namespace Quiz_API.Contracts.Answer
{
    public class AddAnswerRequest
    {
        public Guid QuestionId { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
    }
}