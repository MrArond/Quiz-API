using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Quiz.Models
{
    public class Answer : INotifyPropertyChanged
    {
        private Guid _id;
        private string _text;
        private bool _isCorrect;

        public Guid Id 
        { 
            get => _id; 
            set { _id = value; OnPropertyChanged(); } 
        }

        public string Text 
        { 
            get => _text; 
            set { _text = value; OnPropertyChanged(); } 
        }

        public bool IsCorrect 
        { 
            get => _isCorrect; 
            set { _isCorrect = value; OnPropertyChanged(); } 
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Question : INotifyPropertyChanged
    {
        private Guid _id;
        private string _text;
        private ObservableCollection<Answer> _answers = new ObservableCollection<Answer>();

        public Guid Id 
        { 
            get => _id; 
            set { _id = value; OnPropertyChanged(); } 
        }

        public string Text 
        { 
            get => _text; 
            set { _text = value; OnPropertyChanged(); } 
        }

        public ObservableCollection<Answer> Answers 
        { 
            get => _answers; 
            set { _answers = value; OnPropertyChanged(); } 
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class QuizModel : INotifyPropertyChanged
    {
        private Guid _id;
        private string _name;
        private string _description;
        private ObservableCollection<Question> _questions = new ObservableCollection<Question>();

        public Guid Id 
        { 
            get => _id; 
            set { _id = value; OnPropertyChanged(); } 
        }

        public string Name 
        { 
            get => _name; 
            set { _name = value; OnPropertyChanged(); } 
        }

        public string Description 
        { 
            get => _description; 
            set { _description = value; OnPropertyChanged(); } 
        }

        public ObservableCollection<Question> Questions 
        { 
            get => _questions; 
            set { _questions = value; OnPropertyChanged(); } 
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
