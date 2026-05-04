using Quiz_API.Contracts.Common;
using MediatR;
using Quiz_API.Application.Abstractions.Services;

using System.Threading;
using System.Threading.Tasks;

namespace Quiz_API.Application.Features.Quiz.Questions
{
    public class DeleteQuestionHandler : IRequestHandler<DeleteQuestionCommand, Result<bool>>
    {
        private readonly IQuestionRepository _questionRepository;

        public DeleteQuestionHandler(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task<Result<bool>> Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
        {
            var result = await _questionRepository.DeleteQuestionAsync(request.QuestionId);
            if (!result)
            {
                return Result<bool>.Failure("Question not found");
            }

            return Result<bool>.Success(true);
        }
    }
}