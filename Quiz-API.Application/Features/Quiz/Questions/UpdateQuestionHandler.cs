using Quiz_API.Contracts.Common;
using MediatR;
using Quiz_API.Application.Abstractions.Services;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Quiz_API.Application.Features.Quiz.Questions
{
    public class UpdateQuestionHandler : IRequestHandler<UpdateQuestionCommand, Result<Guid>>
    {
        private readonly IQuestionRepository _questionRepository;

        public UpdateQuestionHandler(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task<Result<Guid>> Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = await _questionRepository.GetQuestionByIdAsync(request.QuestionId);
            if (question == null)
            {
                return Result<Guid>.Failure("Question not found");
            }

            question.Update(request.Text);

            await _questionRepository.UpdateQuestionAsync(question);

            return Result<Guid>.Success(question.Id);
        }
    }
}