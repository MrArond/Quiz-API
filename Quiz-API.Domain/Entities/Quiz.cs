using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz_API.Domain.Entities;
public class Quiz
{
    public Guid Id { get; private set; }
    public string? Name { get; private set; }
    public string? Description { get; private set; }
    public DateTime CreatedAt { get; private set; }

    //Navigation 
    public List<Question>? Questions { get; private set; }

    public Quiz(Guid id, string? name, string? description, DateTime createdAt)
    {
        Id = id;
        Name = name;
        Description = description;
        CreatedAt = createdAt;
    }
    private Quiz()
    {
    }
}
