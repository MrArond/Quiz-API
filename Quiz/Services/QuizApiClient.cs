using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Quiz.Models;
using Quiz_API.Contracts.Quiz;
using Quiz_API.Contracts.Question;
using Quiz_API.Contracts.Answer;

namespace Quiz.Services
{
    public class QuizApiClient
    {
        private readonly HttpClient _httpClient;

        public QuizApiClient()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7248/"); // You may need to update this URL to match your API port
        }

        public async Task<List<QuizModel>> GetQuizzesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/quizzes/GetQuizzes");
                response.EnsureSuccessStatusCode();
                var getQuizzesResponse = await response.Content.ReadFromJsonAsync<GetQuizzesResponse>();

                var quizzes = new List<QuizModel>();
                if (getQuizzesResponse != null && getQuizzesResponse.Quizzes != null)
                {
                    foreach(var q in getQuizzesResponse.Quizzes)
                    {
                        quizzes.Add(new QuizModel 
                        { 
                            Id = q.Id, 
                            Name = q.Name, 
                            Description = q.Description 
                        });
                    }
                }
                return quizzes;
            }
            catch(Exception)
            {
                return new List<QuizModel>();
            }
        }

        public async Task<QuizModel> CreateQuizAsync(string name, string description)
        {
            var request = new AddQuizRequest(name, description);
            var response = await _httpClient.PostAsJsonAsync("api/quizzes/AddQuiz", request);
            if (response.IsSuccessStatusCode)
            {
                var addResponse = await response.Content.ReadFromJsonAsync<AddQuizResponse>();
                return new QuizModel { Id = Guid.NewGuid(), Name = name, Description = description };
            }
            return null;
        }

        // Add a question
        public async Task<Question> AddQuestionAsync(Guid quizId, string text)
        {
            var request = new AddQuestionRequest(text, quizId);
            var response = await _httpClient.PostAsJsonAsync($"api/question/AddQuestion", request);
            if(response.IsSuccessStatusCode)
            {
                var addResponse = await response.Content.ReadFromJsonAsync<AddQuestionResponse>();
                return new Question { Id = Guid.NewGuid(), Text = text };
            }
            return null;
        }

        // Get questions by quiz id
        public async Task<List<Question>> GetQuestionsByQuizIdAsync(Guid quizId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/Question/GetQuestionsByQuizId/{quizId}");
                response.EnsureSuccessStatusCode();
                var getQuestionsResponse = await response.Content.ReadFromJsonAsync<GetQuestionsResponse>();

                var questions = new List<Question>();
                if (getQuestionsResponse != null && getQuestionsResponse.Questions != null)
                {
                    foreach(var q in getQuestionsResponse.Questions)
                    {
                        questions.Add(new Question 
                        { 
                            Id = q.Id, 
                            Text = q.Text 
                        });
                    }
                }
                return questions;
            }
            catch(Exception)
            {
                return new List<Question>();
            }
        }

        // Get answers by question id
        public async Task<List<Answer>> GetAnswersByQuestionIdAsync(Guid questionId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/Answer/GetAnswersByQuestionId/{questionId}");
                response.EnsureSuccessStatusCode();
                var getAnswersResponse = await response.Content.ReadFromJsonAsync<Quiz_API.Contracts.Answer.GetAnswersResponse>();

                var answers = new List<Answer>();
                if (getAnswersResponse != null && getAnswersResponse.Answers != null)
                {
                    foreach(var a in getAnswersResponse.Answers)
                    {
                        answers.Add(new Answer 
                        { 
                            Id = a.Id, 
                            Text = a.Text,
                            IsCorrect = a.IsCorrect
                        });
                    }
                }
                return answers;
            }
            catch(Exception)
            {
                return new List<Answer>();
            }
        }

        // Add an answer
        public async Task<Answer> AddAnswerAsync(Guid questionId, string text, bool isCorrect)
        {
            var request = new AddAnswerRequest { QuestionId = questionId, Text = text, IsCorrect = isCorrect };
            var response = await _httpClient.PostAsJsonAsync($"api/answer/AddAnswer", request);
            if(response.IsSuccessStatusCode)
            {
                return new Answer { Text = text, IsCorrect = isCorrect };
            }
            return null;
        }

        public async Task<bool> DeleteQuestionAsync(Guid questionId)
        {
            var response = await _httpClient.DeleteAsync($"api/question/DeleteQuestion/{questionId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateQuestionAsync(Guid questionId, string newText)
        {
            var request = new UpdateQuestionRequest { QuestionId = questionId, Text = newText };
            var response = await _httpClient.PutAsJsonAsync($"api/question/UpdateQuestion", request);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateQuizAsync(Guid quizId, string name, string description)
        {
            var request = new UpdateQuizRequest(quizId, name, description);
            var response = await _httpClient.PutAsJsonAsync($"api/quizzes/UpdateQuiz", request);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteQuizAsync(Guid quizId)
        {
            var response = await _httpClient.DeleteAsync($"api/quizzes/DeleteQuiz/{quizId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAnswerAsync(Guid answerId, string text, bool isCorrect)
        {
            var request = new UpdateAnswerRequest { AnswerId = answerId, Text = text, IsCorrect = isCorrect };
            var response = await _httpClient.PutAsJsonAsync($"api/answer/UpdateAnswer", request);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAnswerAsync(Guid answerId)
        {
            var response = await _httpClient.DeleteAsync($"api/answer/DeleteAnswer/{answerId}");
            return response.IsSuccessStatusCode;
        }
    }
}
