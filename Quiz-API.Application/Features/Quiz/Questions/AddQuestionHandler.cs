using MediatR;
using Quiz_API.Application.Abstractions.Services;
using Quiz_API.Contracts.Common;
using Quiz_API.Contracts.Question;
using Quiz_API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz_API.Application.Features.Quiz.Questions
{
    public class AddQuestionHandler : IRequestHandler<AddQuestionCommand, Result<AddQuestionResponse>>
    {
        public readonly IQuestionRepository _questionRepository;

        public AddQuestionHandler(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task<Result<AddQuestionResponse>> Handle(AddQuestionCommand addQuestionCommand, CancellationToken cancellationToken)
        {
            try
            {
                var question = new Question(
                id: Guid.CreateVersion7(),
                text: addQuestionCommand.Text,
                quizId: addQuestionCommand.QuizId
                );

                await _questionRepository.AddQuestionAsync(question);
                return Result<AddQuestionResponse>.Success(new AddQuestionResponse(true));

            }
            catch (Exception ex)
            {
                return Result<AddQuestionResponse>.Failure($"An error occurred: {ex.Message} {ex.InnerException?.Message}");

            }
        }
    }
}
