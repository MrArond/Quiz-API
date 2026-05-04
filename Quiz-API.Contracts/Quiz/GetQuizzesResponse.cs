using Quiz_API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz_API.Contracts.Quiz;

public record QuizResponse(
    Guid Id,
    string Name,
    string Description,
    DateTime CreatedAt
    );

public record GetQuizzesResponse(
    IEnumerable<QuizResponse> Quizzes
    );


