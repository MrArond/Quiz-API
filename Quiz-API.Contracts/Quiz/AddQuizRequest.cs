using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz_API.Contracts.Quiz;

public record AddQuizRequest(
    string Name,
    string Description
    );

