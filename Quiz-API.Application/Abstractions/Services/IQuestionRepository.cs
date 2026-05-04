using Quiz_API.Application.Features.Quiz.Questions;
using Quiz_API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz_API.Application.Abstractions.Services
{
    public interface IQuestionRepository
    {
        Task<Question> AddQuestionAsync(Question question);

        Task<List<Question>> GetQuestionsByQuizIdAsync(Guid quizId);
    }
}
