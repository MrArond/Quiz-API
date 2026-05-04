using Quiz_API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz_API.Application.Abstractions.Services
{
    public interface IQuizRepository
    {
        Task<List<Quiz>> GetQuizzesAsync();

        Task<Quiz> AddQuizAsync(Quiz quiz);
    }
}
