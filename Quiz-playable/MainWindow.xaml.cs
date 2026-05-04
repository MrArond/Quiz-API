using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Quiz.Services;

namespace Quiz_playable
{
    public class PlayableQuestion
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public List<PlayableAnswer> Answers { get; set; } = new List<PlayableAnswer>();
    }

    public class PlayableAnswer
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
        // To track what user selected
        public bool IsSelected { get; set; } 
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private QuizApiClient _apiClient;
        private DispatcherTimer _timer;
        private TimeSpan _timeLeft;
        
        private List<PlayableQuestion> _questions = new();
        private int _currentQuestionIndex = 0;
        
        public MainWindow()
        {
            InitializeComponent();
            _apiClient = new QuizApiClient();
            
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;

            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var quizzes = await _apiClient.GetQuizzesAsync();
                CmbQuizzes.ItemsSource = quizzes;
                if (quizzes != null && quizzes.Any())
                {
                    CmbQuizzes.SelectedIndex = 0;
                    BtnRozpocznij.IsEnabled = true;
                }
                else
                {
                    StatusText.Text = "Brak dostępnych quizów.";
                }
            }
            catch (Exception ex)
            {
                StatusText.Text = $"Błąd pobierania quizów: {ex.Message}";
            }
        }

        private async Task LoadQuizDataAsync()
        {
            StatusText.Text = "Ładowanie quizu...";
            _questions.Clear();
            
            try
            {
                if (CmbQuizzes.SelectedItem == null)
                {
                    StatusText.Text = "Proszę wybrać quiz z listy.";
                    return;
                }
                
                var selectedQuizId = (Guid)CmbQuizzes.SelectedValue;
                var selectedQuiz = CmbQuizzes.SelectedItem as dynamic; // Can't easily cast to unknown type without knowing exactly, assuming dynamic or var works
                string quizName = selectedQuiz?.Name?.ToString() ?? "Quiz";

                var apiQuestions = await _apiClient.GetQuestionsByQuizIdAsync(selectedQuizId);
                
                foreach (var q in apiQuestions)
                {
                    var playableQ = new PlayableQuestion { Id = q.Id, Text = q.Text };
                    var apiAnswers = await _apiClient.GetAnswersByQuestionIdAsync(q.Id);
                    foreach (var a in apiAnswers)
                    {
                        playableQ.Answers.Add(new PlayableAnswer { Id = a.Id, Text = a.Text, IsCorrect = a.IsCorrect });
                    }
                    _questions.Add(playableQ);
                }
                
                StatusText.Text = $"Załadowano quiz: {quizName} ({_questions.Count} pytań)";
                
                if (_questions.Count > 0)
                {
                    BtnRozpocznij.IsEnabled = true;
                }
            }
            catch(Exception ex)
            {
                StatusText.Text = $"Błąd pobierania danych: {ex.Message}";
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (_timeLeft.TotalSeconds > 0)
            {
                _timeLeft = _timeLeft.Subtract(TimeSpan.FromSeconds(1));
                TimerText.Text = _timeLeft.ToString(@"mm\:ss");
            }
            else
            {
                // Timeout
                _timer.Stop();
                EndQuiz();
                MessageBox.Show("Czas minął!", "Koniec czasu", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private async void BtnRozpocznij_Click(object sender, RoutedEventArgs e)
        {
            await LoadQuizDataAsync();

            if (_questions.Count == 0) return;

            // Start state
            CmbQuizzes.IsEnabled = false;
            BtnRozpocznij.IsEnabled = false;
            BtnZakoncz.IsEnabled = true;
            BtnShowAnswers.IsEnabled = false;
            QuestionArea.Visibility = Visibility.Visible;
            NavigationPanel.Visibility = Visibility.Visible;
            ScoreText.Text = "";
            StatusText.Text = "Rozwiązywanie quizu...";

            // Reset selections
            foreach(var q in _questions)
            {
                foreach(var a in q.Answers) a.IsSelected = false;
            }

            _currentQuestionIndex = 0;
            ShowQuestion();

            // Set time based on questions count (e.g., 30s per question)
            _timeLeft = TimeSpan.FromSeconds(_questions.Count * 30);
            TimerText.Text = _timeLeft.ToString(@"mm\:ss");
            _timer.Start();
        }

        private void BtnZakoncz_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Czy na pewno chcesz zakończyć quiz?", "Zakończ", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                EndQuiz();
            }
        }

        private void EndQuiz()
        {
            _timer.Stop();
            BtnZakoncz.IsEnabled = false;
            BtnRozpocznij.IsEnabled = true;
            CmbQuizzes.IsEnabled = true;
            BtnShowAnswers.IsEnabled = true;
            
            CalculateScore();
            
            QuestionArea.Visibility = Visibility.Collapsed;
            NavigationPanel.Visibility = Visibility.Collapsed;
            StatusText.Text = "Quiz zakończony.";
        }
        
        private void CalculateScore()
        {
            int score = 0;
            foreach (var q in _questions)
            {
                // Find user selected answer
                var selected = q.Answers.FirstOrDefault(a => a.IsSelected);
                if (selected != null && selected.IsCorrect)
                {
                    score++;
                }
            }
            
            ScoreText.Text = $"Wynik: {score} / {_questions.Count}";
        }

        private void ShowQuestion()
        {
            if (_questions.Count == 0 || _currentQuestionIndex < 0 || _currentQuestionIndex >= _questions.Count) return;
            
            var currentQ = _questions[_currentQuestionIndex];
            QuestionTextBlock.Text = currentQ.Text;
            
            // Re-bind answers
            var currentAnswersList = currentQ.Answers;
            AnswersListBox.ItemsSource = null;
            AnswersListBox.ItemsSource = currentAnswersList;
            
            // Restore selection in UI
            var selected = currentAnswersList.FirstOrDefault(a => a.IsSelected);
            if (selected != null)
            {
                AnswersListBox.SelectedItem = selected;
            }

            ProgressText.Text = $"{_currentQuestionIndex + 1} / {_questions.Count}";
            
            BtnPrevious.IsEnabled = _currentQuestionIndex > 0;
            BtnNext.IsEnabled = _currentQuestionIndex < _questions.Count - 1;
        }
        
        private void SaveCurrentSelection()
        {
            if (_currentQuestionIndex >= 0 && _currentQuestionIndex < _questions.Count)
            {
                var currentQ = _questions[_currentQuestionIndex];
                var selected = AnswersListBox.SelectedItem as PlayableAnswer;
                foreach(var a in currentQ.Answers)
                {
                    a.IsSelected = (a == selected);
                }
            }
        }

        private void BtnPrevious_Click(object sender, RoutedEventArgs e)
        {
            SaveCurrentSelection();
            if (_currentQuestionIndex > 0)
            {
                _currentQuestionIndex--;
                ShowQuestion();
            }
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            SaveCurrentSelection();
            if (_currentQuestionIndex < _questions.Count - 1)
            {
                _currentQuestionIndex++;
                ShowQuestion();
            }
        }
        
        private void BtnShowAnswers_Click(object sender, RoutedEventArgs e)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Poprawne odpowiedzi:");
            sb.AppendLine();
            
            for(int i = 0; i < _questions.Count; i++)
            {
                var q = _questions[i];
                var correctAnsw = q.Answers.Where(a => a.IsCorrect).Select(a => a.Text);
                var userSelected = q.Answers.FirstOrDefault(a => a.IsSelected);
                
                sb.AppendLine($"{i+1}. {q.Text}");
                sb.AppendLine($"Poprawna: {string.Join(", ", correctAnsw)}");
                sb.AppendLine($"Twoja: {(userSelected != null ? userSelected.Text : "Brak")}");
                sb.AppendLine();
            }
            
            MessageBox.Show(sb.ToString(), "Wyniki", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}