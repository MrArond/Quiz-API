using MediatR;
using Quiz_API.Application.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Text;

using Quiz_API.Contracts.Common;
using Quiz_API.Contracts.Quiz;

namespace Quiz_API.Application.Features.Quiz.ListQuiz
{
    public class AddQuizHandler : IRequestHandler<AddQuizCommand, Result<AddQuizResponse>>
    {
        private readonly IQuizRepository _quizRepository;

        public AddQuizHandler(IQuizRepository quizRepository)
        {
            _quizRepository = quizRepository;
        }

        public async Task<Result<AddQuizResponse>> Handle(AddQuizCommand request, CancellationToken cancellationToken)
        {
            var quiz = new Domain.Entities.Quiz(
                id: Guid.CreateVersion7(),
                name: request.Name, 
                description: request.Description, 
                DateTime.UtcNow
            );
            await _quizRepository.AddQuizAsync(quiz);
            return Result<AddQuizResponse>.Success(new AddQuizResponse(true));
        }
    }
}
