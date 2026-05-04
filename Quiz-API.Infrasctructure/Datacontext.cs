using Microsoft.EntityFrameworkCore;
using Quiz_API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz_API.Infrasctructure;
public class Datacontext : DbContext
{
    public Datacontext(DbContextOptions<Datacontext> options) : base(options)
    {
    }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Quiz> Quizzes { get; set; }

}
