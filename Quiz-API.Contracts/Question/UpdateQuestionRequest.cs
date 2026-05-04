using System;

namespace Quiz_API.Contracts.Question
{
    public class UpdateQuestionRequest
    {
        public Guid QuestionId { get; set; }
        public string Text { get; set; }
    }
}