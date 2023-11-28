using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FamilyFeud.MVVM.Model;
using FamilyFeud.MVVM.View;
using FamilyFeud.MVVM.ViewModel;
using FamilyFeud.Commands;
using Microsoft.Office.Interop.Excel;

namespace FamilyFeud.MVVM.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly Game _game;


        public MainWindowViewModel(Game game)
        {
            _game = game;
            SetDefaultData();
            _wrongAnswerClickCommand = new WrongAnswerClick(this, _game);
            SetList();
        }

        private void SetList()
        {
            _game.QuestionList[_game.CurrentRound - 1].Answers.ForEach(setAnswerObjectCollection());
            _game.QuestionList[_game.CurrentRound - 1].Answers.ForEach(setHiddenAnswerObjectCollection());
            _game.QuestionList[_game.CurrentRound - 1].Answers.ForEach(setHiddenPointsObjectCollection());
        }

        private void SetDefaultData()
        {
            IsSymbolVisible1 = IsSymbolVisible11 = IsSymbolVisible12 = IsSymbolVisible13 =
                IsSymbolVisible2 = IsSymbolVisible21 = IsSymbolVisible22 = IsSymbolVisible23 = false;
        }
        private Action<Answer> setAnswerObjectCollection()
        {
            this.AnswersObjectCollection = new ObservableCollection<Answer>();
            return f => this.answersObjectCollection.Add(new Answer
            {
                AnswerString = f.AnswerString, Points = f.Points
            });
        }

        private Action<Answer> setHiddenAnswerObjectCollection()
        {
            this.HiddenAnswersObjectCollection = new ObservableCollection<string>();
            return f => this.hiddenAnswersObjectCollection.Add("...................");
        }

        private Action<Answer> setHiddenPointsObjectCollection()
        {
            this.HiddenPointsObjectCollection = new ObservableCollection<string>();
            return f => this.hiddenPointsObjectCollection.Add("...");
        }



        private ObservableCollection<Answer> answersObjectCollection;
        public ObservableCollection<Answer> AnswersObjectCollection
        {
            get { return answersObjectCollection; }
            set
            {
                if (value != this.answersObjectCollection)
                    answersObjectCollection = value;
                OnPropertyChanged(nameof(AnswersObjectCollection));
            }
        }

        private ObservableCollection<string> hiddenAnswersObjectCollection;
        public ObservableCollection<string> HiddenAnswersObjectCollection
        {
            get { return hiddenAnswersObjectCollection; }
            set
            {
                if (value != this.hiddenAnswersObjectCollection)
                    hiddenAnswersObjectCollection = value;
                OnPropertyChanged(nameof(HiddenAnswersObjectCollection));
            }
        }

        private ObservableCollection<string> hiddenPointsObjectCollection;
        public ObservableCollection<string> HiddenPointsObjectCollection
        {
            get { return hiddenPointsObjectCollection; }
            set
            {
                if (value != this.hiddenPointsObjectCollection)
                    hiddenPointsObjectCollection = value;
                OnPropertyChanged(nameof(HiddenPointsObjectCollection));
            }
        }

        private bool _isSymbolVisible1 { get; set; }
        public bool IsSymbolVisible1
        {
            get { return _isSymbolVisible1; }
            set
            {
                _isSymbolVisible1 = value;
                OnPropertyChanged(nameof(IsSymbolVisible1));
            }
        }

        private bool _isSymbolVisible2 { get; set; }
        public bool IsSymbolVisible2
        {
            get { return _isSymbolVisible2; }
            set
            {
                _isSymbolVisible2 = value;
                OnPropertyChanged(nameof(IsSymbolVisible2));
            }
        }

        private bool _isSymbolVisible11 { get; set; }
        public bool IsSymbolVisible11
        {
            get { return _isSymbolVisible11; }
            set
            {
                _isSymbolVisible11 = value;
                OnPropertyChanged(nameof(IsSymbolVisible11));
            }
        }

        private bool _isSymbolVisible12 { get; set; }
        public bool IsSymbolVisible12
        {
            get { return _isSymbolVisible12; }
            set
            {
                _isSymbolVisible12 = value;
                OnPropertyChanged(nameof(IsSymbolVisible12));
            }
        }

        private bool _isSymbolVisible13 { get; set; }
        public bool IsSymbolVisible13
        {
            get { return _isSymbolVisible13; }
            set
            {
                _isSymbolVisible13 = value;
                OnPropertyChanged(nameof(IsSymbolVisible13));
            }
        }

        private bool _isSymbolVisible21 { get; set; }
        public bool IsSymbolVisible21
        {
            get { return _isSymbolVisible21; }
            set
            {
                _isSymbolVisible21 = value;
                OnPropertyChanged(nameof(IsSymbolVisible21));
            }
        }

        private bool _isSymbolVisible22 { get; set; }
        public bool IsSymbolVisible22
        {
            get { return _isSymbolVisible22; }
            set
            {
                _isSymbolVisible22 = value;
                OnPropertyChanged(nameof(IsSymbolVisible22));
            }
        }

        private bool _isSymbolVisible23 { get; set; }
        public bool IsSymbolVisible23
        {
            get { return _isSymbolVisible23; }
            set
            {
                _isSymbolVisible23 = value;
                OnPropertyChanged(nameof(IsSymbolVisible23));
            }
        }

        private string _firstTeamName { get; set; }
        public string FirstTeamName
        {
            get { return _firstTeamName; }
            set
            {
                _firstTeamName = value;
                OnPropertyChanged(nameof(FirstTeamName));
            }
        }

        private string _secondTeamName { get; set; }
        public string SecondTeamName
        {
            get { return _secondTeamName; }
            set
            {
                _secondTeamName = value;
                OnPropertyChanged(nameof(SecondTeamName));
            }
        }

        private int _firstTeamScore { get; set; }
        public int FirstTeamScore
        {
            get { return _firstTeamScore; }
            set
            {
                _firstTeamScore = value;
                OnPropertyChanged(nameof(FirstTeamScore));
            }
        }
        private int _secondTeamScore { get; set; }
        public int SecondTeamScore
        {
            get { return _secondTeamScore; }
            set
            {
                _secondTeamScore = value;
                OnPropertyChanged(nameof(SecondTeamScore));
            }
        }

        private int _pointsToWin { get; set; }
        public int PointsToWin
        {
            get { return _pointsToWin; }
            set
            {
                _pointsToWin = value;
                OnPropertyChanged(nameof(PointsToWin));
            }
        }

        private int _pointsSum { get; set; }
        public int PointsSum
        {
            get { return _pointsSum; }
            set
            {
                _pointsSum = value;
                OnPropertyChanged(nameof(PointsSum));
            }
        }

        private List<string> _questionList { get; set; }
        public List<string> QuestionList
        {
            get { return _questionList; }
            set
            {
                _questionList = value;
                OnPropertyChanged(nameof(QuestionList));
            }
        }


        private bool _isFirstTeamAnswering { get; set; }
        public bool IsFirstTeamAnswering
        {
            get { return _isFirstTeamAnswering; }
            set
            {
                _isFirstTeamAnswering = value;
                OnPropertyChanged(nameof(IsFirstTeamAnswering));
            }
        }

        private bool _isSecondTeamAnswering { get; set; }
        public bool IsSecondTeamAnswering
        {
            get { return _isSecondTeamAnswering; }
            set
            {
                _isSecondTeamAnswering = value;
                OnPropertyChanged(nameof(IsSecondTeamAnswering));
            }
        }

        public ICommand _wrongAnswerClickCommand { get; }

        public ICommand StartGame { get; }
        public ICommand ResetGame { get; }

    }
}
