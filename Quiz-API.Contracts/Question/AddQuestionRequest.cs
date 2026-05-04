using Quiz_API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz_API.Contracts.Question;

public record AddQuestionRequest(
    string Text,
    Guid QuizId
    );
