using System;

namespace Quiz_API.Contracts.Answer
{
    public class UpdateAnswerRequest
    {
        public Guid AnswerId { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
    }
}