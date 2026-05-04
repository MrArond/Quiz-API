using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz_API.Domain.Entities;
public class Answer
{
    public Guid Id { get; private set; }
    public string? Text { get; private set; }
    public bool IsCorrect { get; private set; }
    public Guid QuestionId { get; private set; }

    public Question? Question { get; private set; }

    public Answer(Guid id, string? text, bool isCorrect, Guid questionId)
    {
        Id = id;
        Text = text;
        IsCorrect = isCorrect;
        QuestionId = questionId;
    }

    public void Update(string? text, bool isCorrect)
    {
        Text = text;
        IsCorrect = isCorrect;
    }

    private Answer()
    {
    }
}

