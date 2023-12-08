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
using Microsoft.Office.Interop.Excel;
using WMPLib;
using FamilyFeud.Commands;
using FamilyFeud.MVVM.Service;

namespace FamilyFeud.MVVM.ViewModel
{
    public class MainWindowViewModel : ObservableObject
    {
        private readonly WindowsMediaPlayerClass _playerClass;
        private GameService gameService;
        private List<Question> database = new List<Question>(); //temp to check functionality - everything should happen on service site

        public MainWindowViewModel(Game game)
        {
            gameService = new GameService();
            database = gameService.ConvertExcelToJson();
            _playerClass = new WindowsMediaPlayerClass();
            OnWrongAnswerClickCommand = new RelayCommand(OnWrongAnswerClick);
            OnAnswerClickCommand = new RelayCommand(OnAnswerClick);
            ActiveGame = game;
            SetDefaultData();
            SetList();
        }

        private void SetList()
        {
            ActiveGame.QuestionList[ActiveGame.CurrentRound - 1].Answers.ForEach(setAnswerObjectCollection());
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

        private Game activeGame;

        public Game ActiveGame
        {
            get => activeGame;
            set
            {
                activeGame = value;
                NotifyPropertyChanged();
            }
        }

        private Answer currentAnswer;
        public Answer CurrentAnswer
        {
            get { return currentAnswer; }
            set
            {
                currentAnswer = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<Answer> answersObjectCollection;
        public ObservableCollection<Answer> AnswersObjectCollection
        {
            get { return answersObjectCollection; }
            set
            {
                answersObjectCollection = value;
                NotifyPropertyChanged();
            }
        }
        

        private bool _isSymbolVisible1 { get; set; }
        public bool IsSymbolVisible1
        {
            get { return _isSymbolVisible1; }
            set
            {
                _isSymbolVisible1 = value;
                NotifyPropertyChanged();
            }
        }

        private bool _isSymbolVisible2 { get; set; }
        public bool IsSymbolVisible2
        {
            get { return _isSymbolVisible2; }
            set
            {
                _isSymbolVisible2 = value;
                NotifyPropertyChanged();
            }
        }

        private bool _isSymbolVisible11 { get; set; }
        public bool IsSymbolVisible11
        {
            get { return _isSymbolVisible11; }
            set
            {
                _isSymbolVisible11 = value;
                NotifyPropertyChanged();
            }
        }

        private bool _isSymbolVisible12 { get; set; }
        public bool IsSymbolVisible12
        {
            get { return _isSymbolVisible12; }
            set
            {
                _isSymbolVisible12 = value;
                NotifyPropertyChanged();
            }
        }

        private bool _isSymbolVisible13 { get; set; }
        public bool IsSymbolVisible13
        {
            get { return _isSymbolVisible13; }
            set
            {
                _isSymbolVisible13 = value;
                NotifyPropertyChanged();
            }
        }

        private bool _isSymbolVisible21 { get; set; }
        public bool IsSymbolVisible21
        {
            get { return _isSymbolVisible21; }
            set
            {
                _isSymbolVisible21 = value;
                NotifyPropertyChanged();
            }
        }

        private bool _isSymbolVisible22 { get; set; }
        public bool IsSymbolVisible22
        {
            get { return _isSymbolVisible22; }
            set
            {
                _isSymbolVisible22 = value;
                NotifyPropertyChanged();
            }
        }

        private bool _isSymbolVisible23 { get; set; }
        public bool IsSymbolVisible23
        {
            get { return _isSymbolVisible23; }
            set
            {
                _isSymbolVisible23 = value;
                NotifyPropertyChanged();
            }
        }

        private string _firstTeamName { get; set; }
        public string FirstTeamName
        {
            get { return _firstTeamName; }
            set
            {
                _firstTeamName = value;
                ActiveGame.FirstTeam.Name = _firstTeamName;
                NotifyPropertyChanged();
            }
        }

        private string _secondTeamName { get; set; }
        public string SecondTeamName
        {
            get { return _secondTeamName; }
            set
            {
                _secondTeamName = value;
                ActiveGame.SecondTeam.Name = _secondTeamName;
                NotifyPropertyChanged();
            }
        }

        private int _firstTeamScore { get; set; }
        public int FirstTeamScore
        {
            get { return _firstTeamScore; }
            set
            {
                _firstTeamScore = value;
                NotifyPropertyChanged();
            }
        }
        private int _secondTeamScore { get; set; }
        public int SecondTeamScore
        {
            get { return _secondTeamScore; }
            set
            {
                _secondTeamScore = value;
                NotifyPropertyChanged();
            }
        }

        private int _pointsToWin { get; set; }
        public int PointsToWin
        {
            get { return _pointsToWin; }
            set
            {
                _pointsToWin = value;
                NotifyPropertyChanged();
            }
        }

        private int _pointsSum { get; set; }
        public int PointsSum
        {
            get { return _pointsSum; }
            set
            {
                _pointsSum = value;
                NotifyPropertyChanged();
            }
        }

        private List<string> _questionList { get; set; }
        public List<string> QuestionList
        {
            get { return _questionList; }
            set
            {
                _questionList = value;
                NotifyPropertyChanged();
            }
        }


        private bool _isFirstTeamAnswering { get; set; }
        public bool IsFirstTeamAnswering
        {
            get { return _isFirstTeamAnswering; }
            set
            {
                _isFirstTeamAnswering = value;
                NotifyPropertyChanged();
            }
        }

        private bool _isSecondTeamAnswering { get; set; }
        public bool IsSecondTeamAnswering
        {
            get { return _isSecondTeamAnswering; }
            set
            {
                _isSecondTeamAnswering = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand OnWrongAnswerClickCommand { get; protected set; }
        public ICommand OnAnswerClickCommand { get; protected set; }


        public ICommand StartGame { get; }
        public ICommand ResetGame { get; }

        private void OnWrongAnswerClick(object obj)
        {
            if (IsFirstTeamAnswering)
            {
                switch (ActiveGame.WrongAnswers)
                {
                    case 0:
                        ActiveGame.AddWrongAnswer();
                        IsSymbolVisible11 = true;
                        PlayAnswerSound("WrongAnswer");
                        break;
                    case 1:
                        ActiveGame.AddWrongAnswer();
                        IsSymbolVisible12 = true;
                        PlayAnswerSound("WrongAnswer");
                        break;
                    case 2:
                        ActiveGame.AddWrongAnswer();
                        IsSymbolVisible13 = true;
                        PlayAnswerSound("WrongAnswer");
                        break;
                    case 3:
                        ActiveGame.AddWrongAnswer();
                        IsSymbolVisible2 = true;
                        PlayAnswerSound("WrongAnswer");
                        break;
                    default:
                        IsSymbolVisible11 = IsSymbolVisible12 =
                            IsSymbolVisible13 = IsSymbolVisible2 = false;
                        ActiveGame.ResetWrongAnswers();
                        break;
                }
            }
            else if (IsSecondTeamAnswering)
            {
                switch (ActiveGame.WrongAnswers)
                {
                    case 0:
                        ActiveGame.AddWrongAnswer();
                        IsSymbolVisible21 = true;
                        PlayAnswerSound("WrongAnswer");
                        break;
                    case 1:
                        ActiveGame.AddWrongAnswer();
                        IsSymbolVisible22 = true;
                        PlayAnswerSound("WrongAnswer");
                        break;
                    case 2:
                        ActiveGame.AddWrongAnswer();
                        IsSymbolVisible23 = true;
                        PlayAnswerSound("WrongAnswer");
                        break;
                    case 3:
                        ActiveGame.AddWrongAnswer();
                        IsSymbolVisible1 = true;
                        PlayAnswerSound("WrongAnswer");
                        break;
                    default:
                        IsSymbolVisible21 = IsSymbolVisible22 =
                            IsSymbolVisible23 = IsSymbolVisible1 = false;
                        ActiveGame.ResetWrongAnswers();
                        break;
                }
            }
        }

        private void OnAnswerClick(object obj)
        {
            if (obj != null && obj is Answer answer)
            {
                foreach (var answerObject in AnswersObjectCollection)
                {
                    if (answerObject.AnswerString == answer.AnswerString)
                    {
                        var index = AnswersObjectCollection.IndexOf(answerObject);

                        if (!answer.IsVisible)
                        {
                            answerObject.IsVisible = CurrentAnswer.IsVisible = true;
                            AnswersObjectCollection[index] = answerObject;
                            PointsToWin += answerObject.Points;
                            PlayAnswerSound("CorrectAnswer");
                            return;
                            // if team1 currently answering assign points to temporary container (consider creating two separate lists for each team and separate buttons)

                        }
                        else if (answer.IsVisible)
                        {
                            answerObject.IsVisible = false;
                            AnswersObjectCollection[index] = answerObject;
                            PointsToWin -= answerObject.Points;
                            return;
                            // remove points from temporary container
                        }
                    }
                }
            }
        }

        private void PlayAnswerSound(string soundName)
        {
            _playerClass.currentPlaylist.clear();
            _playerClass.URL = $@"MVVM\\Sounds\\{soundName}.mp3"; //adjust path
            _playerClass.controls.play();
        }
    }
}
