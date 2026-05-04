using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Quiz.Models;
using Quiz.Services;

namespace Quiz
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private QuizApiClient _apiClient;
        public ObservableCollection<QuizModel> Quizzes { get; set; } = new ObservableCollection<QuizModel>();

        public MainWindow()
        {
            InitializeComponent();
            _apiClient = new QuizApiClient();
            QuizzesListBox.ItemsSource = Quizzes;
            LoadQuizzes();
        }

        private async void LoadQuizzes()
        {
            var quizzes = await _apiClient.GetQuizzesAsync();
            Quizzes.Clear();
            foreach (var q in quizzes)
            {
                Quizzes.Add(q);
            }
        }

        private async void AddQuizButton_Click(object sender, RoutedEventArgs e)
        {
            var name = NewQuizNameTextBox.Text;
            var desc = NewQuizDescTextBox.Text;
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Name is required");
                return;
            }

            var newQuiz = await _apiClient.CreateQuizAsync(name, desc);
            if (newQuiz != null)
            {
                Quizzes.Add(newQuiz);
                NewQuizNameTextBox.Clear();
                NewQuizDescTextBox.Clear();
            }
        }

        private async void QuizzesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedQuiz = QuizzesListBox.SelectedItem as QuizModel;
            if (selectedQuiz != null)
            {
                if (selectedQuiz.Questions.Count == 0)
                {
                    var questions = await _apiClient.GetQuestionsByQuizIdAsync(selectedQuiz.Id);
                    foreach(var q in questions)
                    {
                        selectedQuiz.Questions.Add(q);
                    }
                }
                QuestionsListBox.ItemsSource = selectedQuiz.Questions;
            }
            else
            {
                QuestionsListBox.ItemsSource = null;
            }
        }

        private async void AddQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedQuiz = QuizzesListBox.SelectedItem as QuizModel;
            if (selectedQuiz == null)
            {
                MessageBox.Show("Select a quiz first");
                return;
            }
            if (string.IsNullOrWhiteSpace(QuestionTextBox.Text))
            {
                MessageBox.Show("Question text cannot be empty");
                return;
            }

            var question = await _apiClient.AddQuestionAsync(selectedQuiz.Id, QuestionTextBox.Text);
            if (question != null)
            {
                selectedQuiz.Questions.Add(question);
                QuestionTextBox.Clear();
            }
        }

        private async void QuestionsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedQuestion = QuestionsListBox.SelectedItem as Models.Question;
            if (selectedQuestion != null)
            {
                if (selectedQuestion.Answers.Count == 0)
                {
                    var answers = await _apiClient.GetAnswersByQuestionIdAsync(selectedQuestion.Id);
                    foreach(var a in answers)
                    {
                        selectedQuestion.Answers.Add(a);
                    }
                }
                AnswersListBox.ItemsSource = selectedQuestion.Answers;
                QuestionTextBox.Text = selectedQuestion.Text;
            }
            else
            {
                AnswersListBox.ItemsSource = null;
            }
        }

        private async void UpdateQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedQuestion = QuestionsListBox.SelectedItem as Models.Question;
            if (selectedQuestion == null) return;

            bool success = await _apiClient.UpdateQuestionAsync(selectedQuestion.Id, QuestionTextBox.Text);
            if (success)
            {
                selectedQuestion.Text = QuestionTextBox.Text;
            }
        }

        private async void RemoveQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedQuiz = QuizzesListBox.SelectedItem as QuizModel;
            var selectedQuestion = QuestionsListBox.SelectedItem as Models.Question;

            if (selectedQuiz == null || selectedQuestion == null) return;

            bool success = await _apiClient.DeleteQuestionAsync(selectedQuestion.Id);
            if (success)
            {
                selectedQuiz.Questions.Remove(selectedQuestion);
            }
        }

        private async void AddAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedQuestion = QuestionsListBox.SelectedItem as Models.Question;
            if (selectedQuestion == null)
            {
                MessageBox.Show("Select a question first");
                return;
            }

            if (selectedQuestion.Answers.Count >= 4)
            {
                MessageBox.Show("A question can have a maximum of four answers");
                return;
            }

            var answerText = AnswerTextBox.Text;
            var isCorrect = IsCorrectCheckBox.IsChecked ?? false;

            var newAnswer = await _apiClient.AddAnswerAsync(selectedQuestion.Id, answerText, isCorrect);
            if (newAnswer != null)
            {
                selectedQuestion.Answers.Add(newAnswer);
                AnswerTextBox.Clear();
                IsCorrectCheckBox.IsChecked = false;
            }
        }

        private async void UpdateAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedAnswer = AnswersListBox.SelectedItem as Models.Answer;
            if(selectedAnswer == null) return;

            var isCorrect = IsCorrectCheckBox.IsChecked ?? false;

            bool success = await _apiClient.UpdateAnswerAsync(selectedAnswer.Id, AnswerTextBox.Text, isCorrect);
            if(success)
            {
                selectedAnswer.Text = AnswerTextBox.Text;
                selectedAnswer.IsCorrect = isCorrect;

                // Force an update to the UI
                var selectedQuestion = QuestionsListBox.SelectedItem as Models.Question;
                if(selectedQuestion != null)
                {
                    AnswersListBox.ItemsSource = null;
                    AnswersListBox.ItemsSource = selectedQuestion.Answers;
                }
            }
        }

        private async void RemoveAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedQuestion = QuestionsListBox.SelectedItem as Models.Question;
            var selectedAnswer = AnswersListBox.SelectedItem as Models.Answer;

            if (selectedQuestion == null || selectedAnswer == null) return;

            bool success = await _apiClient.DeleteAnswerAsync(selectedAnswer.Id);
            if (success)
            {
                selectedQuestion.Answers.Remove(selectedAnswer);
                AnswerTextBox.Clear();
                IsCorrectCheckBox.IsChecked = false;
            }
        }

        private void AnswersListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedAnswer = AnswersListBox.SelectedItem as Models.Answer;
            if(selectedAnswer != null)
            {
                AnswerTextBox.Text = selectedAnswer.Text;
                IsCorrectCheckBox.IsChecked = selectedAnswer.IsCorrect;
            }
        }
    }
}