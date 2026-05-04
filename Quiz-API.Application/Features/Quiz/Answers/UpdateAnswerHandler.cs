using Quiz_API.Contracts.Common;
using MediatR;
using Quiz_API.Application.Abstractions.Services;

using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Quiz_API.Application.Features.Quiz.Answers
{
    public class UpdateAnswerHandler : IRequestHandler<UpdateAnswerCommand, Result<Guid>>
    {
        private readonly IAnswerRepository _answerRepository;

        public UpdateAnswerHandler(IAnswerRepository answerRepository)
        {
            _answerRepository = answerRepository;
        }

        public async Task<Result<Guid>> Handle(UpdateAnswerCommand request, CancellationToken cancellationToken)
        {
            var answer = await _answerRepository.GetAnswerByIdAsync(request.AnswerId);
            if (answer == null)
            {
                return Result<Guid>.Failure("Answer not found");
            }

            answer.Update(request.Text, request.IsCorrect);

            await _answerRepository.UpdateAnswerAsync(answer);

            return Result<Guid>.Success(answer.Id);
        }
    }
}