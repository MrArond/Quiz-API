using MediatR;
using Quiz_API.Contracts.Common;
using Quiz_API.Contracts.Quiz;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Quiz_API.Application.Features.AddQuiz;

public record GetQuizzesQuery(
) : IRequest<Result<GetQuizzesResponse>>;

