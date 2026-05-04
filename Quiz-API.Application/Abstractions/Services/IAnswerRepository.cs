using Quiz_API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quiz_API.Application.Abstractions.Services
{
    public interface IAnswerRepository
    {
        Task<Answer> AddAnswerAsync(Answer answer);
        Task<Answer> UpdateAnswerAsync(Answer answer);
        Task<bool> DeleteAnswerAsync(Guid answerId);
        Task<Answer> GetAnswerByIdAsync(Guid answerId);
        Task<List<Answer>> GetAnswersByQuestionIdAsync(Guid questionId);
    }
}