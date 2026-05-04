using MediatR;
using Quiz_API.Application.Abstractions.Services;
using Quiz_API.Application.Features.AddQuiz;
using Quiz_API.Contracts.Common;
using Quiz_API.Contracts.Quiz;
using Quiz_API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz_API.Application.Features.Quiz.ListQuiz
{
    public class GetQuizzesHandler : IRequestHandler<GetQuizzesQuery, Result<GetQuizzesResponse>>
    {
        public readonly IQuizRepository _quizRepository;
        public GetQuizzesHandler(IQuizRepository quizRepository)
        {
            _quizRepository = quizRepository;
        }
        public async Task<Result<GetQuizzesResponse>> Handle(GetQuizzesQuery request, CancellationToken cancellationToken)
        {
            var quizzes = await _quizRepository.GetQuizzesAsync();
            var response = quizzes.Select(q => new QuizResponse
            (
                Id: q.Id,
                Name: q.Name,
                Description: q.Description,
                CreatedAt: q.CreatedAt

            )).ToList();
            return Result<GetQuizzesResponse>.Success(new GetQuizzesResponse(response));
        }
    }
}
