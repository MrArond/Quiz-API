using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

using Quiz_API.Contracts.Common;
using Quiz_API.Contracts.Quiz;

namespace Quiz_API.Application.Features.Quiz.ListQuiz;

public record AddQuizCommand(
    string Name,
    string Description
    ) : IRequest<Result<AddQuizResponse>>;

