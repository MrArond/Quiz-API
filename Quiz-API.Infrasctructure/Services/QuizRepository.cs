using Quiz_API.Application.Abstractions.Services;
using Quiz_API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Quiz_API.Infrasctructure.Services
{
    public class QuizRepository : IQuizRepository
    {
        private readonly Datacontext _context;

        public QuizRepository(Datacontext context)
        {
            _context = context;
        }

        public async Task<List<Quiz>> GetQuizzesAsync()
        {
            var quizzes = await _context.Quizzes.ToListAsync();
            return quizzes;
        }

        public async Task<Quiz> AddQuizAsync(Quiz quiz)
        {
            await _context.Quizzes.AddAsync(quiz);
            await _context.SaveChangesAsync();
            return quiz;
        }
    }
}
