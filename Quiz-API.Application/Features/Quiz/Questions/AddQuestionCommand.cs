using MediatR;
using Quiz_API.Contracts.Common;
using Quiz_API.Contracts.Question;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Quiz_API.Application.Features.Quiz.Questions;

public record AddQuestionCommand(
    string Text,
    Guid QuizId
    ) : IRequest<Result<AddQuestionResponse>>;

