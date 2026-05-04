using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz_API.Domain.Entities;
public class Question
{
    public Guid Id { get; private set; }
    public string? Text { get; private set; }
    public Guid QuizId { get; private set; }

    //Navigation
    public Quiz? Quiz { get; private set; }
    public List<Answer>? Answers { get; private set; }

    public Question(Guid id, string? text, Guid quizId)
    {
        Id = id;
        Text = text;
        QuizId = quizId;
    }

    public void Update(string? text)
    {
        Text = text;
    }

    private Question()
    {
    }
}

