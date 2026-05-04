using Microsoft.EntityFrameworkCore;
using Quiz_API.Application.Abstractions.Services;
using Quiz_API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz_API.Infrasctructure.Services
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly Datacontext _context;

        public AnswerRepository(Datacontext context)
        {
            _context = context;
        }

        public async Task<Answer> AddAnswerAsync(Answer answer)
        {
            _context.Answers.Add(answer);
            await _context.SaveChangesAsync();
            return answer;
        }

        public async Task<bool> DeleteAnswerAsync(Guid answerId)
        {
            var answer = await _context.Answers.FindAsync(answerId);
            if (answer == null) return false;

            _context.Answers.Remove(answer);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Answer> GetAnswerByIdAsync(Guid answerId)
        {
            return await _context.Answers.FindAsync(answerId);
        }

        public async Task<List<Answer>> GetAnswersByQuestionIdAsync(Guid questionId)
        {
            return await _context.Answers.Where(a => a.QuestionId == questionId).ToListAsync();
        }

        public async Task<Answer> UpdateAnswerAsync(Answer answer)
        {
            _context.Answers.Update(answer);
            await _context.SaveChangesAsync();
            return answer;
        }
    }
}