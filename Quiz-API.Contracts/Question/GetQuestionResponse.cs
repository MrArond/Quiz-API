using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz_API.Contracts.Question;

public record GetQuestionResponse(
    Guid Id,
    string Text,
    Guid QuizId
    );

public record GetQuestionsResponse(
    IEnumerable<GetQuestionResponse> Questions
    );
