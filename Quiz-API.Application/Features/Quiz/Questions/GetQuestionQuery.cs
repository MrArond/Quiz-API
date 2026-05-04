using MediatR;
using Quiz_API.Contracts.Common;
using Quiz_API.Contracts.Question;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz_API.Application.Features.Quiz.Questions;

public record GetQuestionQuery(
    Guid QuizId
    ) : IRequest<Result<GetQuestionsResponse>>;
