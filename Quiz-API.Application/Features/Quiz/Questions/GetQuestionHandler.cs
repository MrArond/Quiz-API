using MediatR;
using Quiz_API.Application.Abstractions.Services;
using Quiz_API.Contracts.Common;
using Quiz_API.Contracts.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quiz_API.Application.Features.Quiz.Questions
{
    public class GetQuestionHandler : IRequestHandler<GetQuestionQuery, Result<GetQuestionsResponse>>
    {
        public readonly IQuestionRepository _questionRepository;

        public GetQuestionHandler(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task<Result<GetQuestionsResponse>> Handle(GetQuestionQuery getQuestionQuery, CancellationToken cancellationToken)
        {
            try
            {
                var questions = await _questionRepository.GetQuestionsByQuizIdAsync(getQuestionQuery.QuizId);
                if (questions == null)
                {
                    return Result<GetQuestionsResponse>.Failure("Questions not found.");
                }

                var responseList = questions.Select(q => new GetQuestionResponse(
                    Id: q.Id,
                    Text: q.Text ?? string.Empty,
                    QuizId: q.QuizId
                ));

                var response = new GetQuestionsResponse(responseList);

                return Result<GetQuestionsResponse>.Success(response);
            }
            catch (Exception ex)
            {
                return Result<GetQuestionsResponse>.Failure($"An error occurred: {ex.Message} {ex.InnerException?.Message}");
            }
        }

    }
}
