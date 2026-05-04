using Microsoft.EntityFrameworkCore;
using Quiz_API.Application.Abstractions.Services;
using Quiz_API.Infrasctructure;
using Quiz_API.Infrasctructure.Services;

public class Program
{
    public static void Main(string[] args)
    {
           DotNetEnv.Env.Load();

           var builder = WebApplication.CreateBuilder(args);

           builder = AddDbContext(builder);

           builder.Services.AddControllers();

           builder.Services.AddOpenApi();

           builder.Services.AddMediatR(cfg => {
               cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
               cfg.RegisterServicesFromAssembly(typeof(Quiz_API.Application.Features.Quiz.ListQuiz.GetQuizzesHandler).Assembly);
           });

           builder.Services.AddScoped<IQuizRepository, QuizRepository>();
           builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
        builder.Services.AddEndpointsApiExplorer();
           builder.Services.AddSwaggerGen();



        var app = builder.Build();

           if (app.Environment.IsDevelopment())
           {
               app.MapOpenApi();
               app.UseSwagger();
               app.UseSwaggerUI();
           }

           app.UseHttpsRedirection();

           app.UseAuthorization();

           app.MapControllers();

           app.Run();
    }
    private static WebApplicationBuilder AddDbContext(WebApplicationBuilder builder)
    {
        var postgreUrl = Environment.GetEnvironmentVariable("DB_HOST");
        if (string.IsNullOrEmpty(postgreUrl))
        {
            throw new InvalidOperationException("DB_HOST environment variable is not set.");
        }
        var uri = new Uri(postgreUrl);
        var userInfo = uri.UserInfo.Split(':');
        var npgsqlBuilder = new Npgsql.NpgsqlConnectionStringBuilder
        {
            Host = uri.Host,
            Port = uri.Port,
            Username = userInfo[0],
            Password = userInfo[1],
            Database = uri.AbsolutePath.TrimStart('/')
        };

        builder.Services.AddDbContext<Datacontext>(options =>
            options.UseNpgsql(
                npgsqlBuilder.ConnectionString,
                sqlOpts => sqlOpts.EnableRetryOnFailure()
                )
            );
        return builder;
    }
}
