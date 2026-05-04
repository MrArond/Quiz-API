using Microsoft.EntityFrameworkCore;
using Quiz_API.Application.Abstractions.Services;
using Quiz_API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quiz_API.Infrasctructure.Services
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly Datacontext _context;

        public QuestionRepository(Datacontext context)
        {
            _context = context;
        }

        public async Task<Question> AddQuestionAsync(Question question)
        {
            _context.Questions.Add(question);
            await _context.SaveChangesAsync();
            return question;
        }

        public async Task<List<Question>> GetQuestionsByQuizIdAsync(Guid quizId)
        {
            return await _context.Questions
                .Where(q => q.QuizId == quizId)
                .ToListAsync();
        }

        public async Task<Question> GetQuestionByIdAsync(Guid questionId)
        {
            return await _context.Questions.FindAsync(questionId);
        }

        public async Task<Question> UpdateQuestionAsync(Question question)
        {
            _context.Questions.Update(question);
            await _context.SaveChangesAsync();
            return question;
        }

        public async Task<bool> DeleteQuestionAsync(Guid questionId)
        {
            var question = await _context.Questions.FindAsync(questionId);
            if (question == null) return false;

            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
