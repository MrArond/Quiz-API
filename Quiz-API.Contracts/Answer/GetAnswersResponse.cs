using System;

namespace Quiz_API.Contracts.Answer
{
    public class GetAnswersResponse
    {
        public List<GetAnswerResponse> Answers { get; set; }

        public GetAnswersResponse(List<GetAnswerResponse> answers)
        {
            Answers = answers;
        }
    }

    public class GetAnswerResponse
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
        public Guid QuestionId { get; set; }

        public GetAnswerResponse(Guid id, string text, bool isCorrect, Guid questionId)
        {
            Id = id;
            Text = text;
            IsCorrect = isCorrect;
            QuestionId = questionId;
        }
    }
}