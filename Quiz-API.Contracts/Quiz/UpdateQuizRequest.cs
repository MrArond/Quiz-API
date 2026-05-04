using System;

namespace Quiz_API.Contracts.Quiz;

public record UpdateQuizRequest(Guid Id, string Name, string Description);