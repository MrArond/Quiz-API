using System;
using System.Collections.Generic;

namespace Quiz_API.Contracts.Answer
{
    public class GetAnswersResponseFromClient
    {
        public List<GetAnswerResponseClient> Answers { get; set; }
    }

    public class GetAnswerResponseClient
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
        public Guid QuestionId { get; set; }
    }
}