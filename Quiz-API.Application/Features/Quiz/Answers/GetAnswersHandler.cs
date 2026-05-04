using MediatR;
using Quiz_API.Application.Abstractions;
using Quiz_API.Application.Abstractions.Services;
using Quiz_API.Contracts.Answer;
using Quiz_API.Contracts.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Quiz_API.Application.Features.Quiz.Answers
{
    public class GetAnswersHandler : IRequestHandler<GetAnswersQuery, Result<GetAnswersResponse>>
    {
        private readonly IAnswerRepository _answerRepository;

        public GetAnswersHandler(IAnswerRepository answerRepository)
        {
            _answerRepository = answerRepository;
        }

        public async Task<Result<GetAnswersResponse>> Handle(GetAnswersQuery query, CancellationToken cancellationToken)
        {
            var answers = await _answerRepository.GetAnswersByQuestionIdAsync(query.QuestionId);

            if (answers == null)
            {
                return Result<GetAnswersResponse>.Failure("Answers not found");
            }

            var responseList = answers.Select(a => new GetAnswerResponse(
                a.Id,
                a.Text ?? string.Empty,
                a.IsCorrect,
                a.QuestionId
            )).ToList();

            var response = new GetAnswersResponse(responseList);
            return Result<GetAnswersResponse>.Success(response);
        }
    }
}