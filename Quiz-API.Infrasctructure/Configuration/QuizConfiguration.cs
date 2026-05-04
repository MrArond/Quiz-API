using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quiz_API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz_API.Infrasctructure.Configuration
{
    internal class QuizConfiguration : IEntityTypeConfiguration<Quiz>
    {
        public void Configure(EntityTypeBuilder<Quiz> builder)
        {
            builder.ToTable("Quizzs");
            builder.HasKey(q => q.Id);

        }
    }
}
