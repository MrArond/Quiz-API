using Quiz_API.Contracts.Common;
using MediatR;
using Quiz_API.Application.Abstractions.Services;
using Quiz_API.Domain.Entities;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Quiz_API.Application.Features.Quiz.Answers
{
    public class AddAnswerHandler : IRequestHandler<AddAnswerCommand, Result<Guid>>
    {
        private readonly IAnswerRepository _answerRepository;

        public AddAnswerHandler(IAnswerRepository answerRepository)
        {
            _answerRepository = answerRepository;
        }

        public async Task<Result<Guid>> Handle(AddAnswerCommand request, CancellationToken cancellationToken)
        {
            var answer = new Answer(Guid.NewGuid(), request.Text, request.IsCorrect, request.QuestionId);
            await _answerRepository.AddAnswerAsync(answer);

            return Result<Guid>.Success(answer.Id);
        }
    }
}