using Quiz_API.Contracts.Common;
using MediatR;
using Quiz_API.Application.Abstractions.Services;

using System.Threading;
using System.Threading.Tasks;

namespace Quiz_API.Application.Features.Quiz.Answers
{
    public class DeleteAnswerHandler : IRequestHandler<DeleteAnswerCommand, Result<bool>>
    {
        private readonly IAnswerRepository _answerRepository;

        public DeleteAnswerHandler(IAnswerRepository answerRepository)
        {
            _answerRepository = answerRepository;
        }

        public async Task<Result<bool>> Handle(DeleteAnswerCommand request, CancellationToken cancellationToken)
        {
            var result = await _answerRepository.DeleteAnswerAsync(request.AnswerId);
            if (!result)
            {
                return Result<bool>.Failure("Answer not found");
            }

            return Result<bool>.Success(true);
        }
    }
}