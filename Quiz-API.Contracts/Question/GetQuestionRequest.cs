using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz_API.Contracts.Question;

public record GetQuestionRequest(
    Guid QuizId
    );

