using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
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
        private List<Question> databaseEN = new List<Question>(); //temp to check functionality - everything should happen on service site
        private List<Question> databasePL = new List<Question>(); //temp to check functionality - everything should happen on service site
        private const int roundCount = 10;

        public MainWindowViewModel(Game game)
        {
            gameService = new GameService();
            _playerClass = new WindowsMediaPlayerClass(); 
            SwitchViewCommand = new RelayCommand(SwitchView);
            OnWrongAnswerClickCommand = new RelayCommand(OnWrongAnswerClick);
            OnAnswerClickCommand = new RelayCommand(OnAnswerClick);
            OnResetQuestionsClickCommand = new RelayCommand(OnResetQuestionsClick);
            ActiveGame = game;
            CurrentView = new RoundControlView();
            CurrentViewType = View.Game;
            GameType = "gamePL";
            SetDefaultData();
            SetList();
        }

        private void SetList()
        {
            //ActiveGame.QuestionList[ActiveGame.CurrentRound - 1].Answers.ForEach(setAnswerObjectCollection());
            AnswersObjectCollection = ActiveGame.QuestionList[ActiveGame.CurrentRound - 1].Answers;
            CurrentQuestion = ActiveGame.QuestionList[ActiveGame.CurrentRound - 1];
        }
        
        private void SetDefaultData()
        {
            IsSymbolVisible1 = IsSymbolVisible11 = IsSymbolVisible12 = IsSymbolVisible13 =
                IsSymbolVisible2 = IsSymbolVisible21 = IsSymbolVisible22 = IsSymbolVisible23 = false;

            databaseEN = gameService.GetQuestionsList(GameService.PathEN);
            databasePL = gameService.GetQuestionsList(GameService.PathPL);

            //Setting first question
            if (databaseEN.Count > 0 && GameType == "gameEN")
            {
                ActiveGame.QuestionList.Add(databaseEN.FirstOrDefault(x => x.IsAvailable && x.Answers.Count == gameService.GetNumberOfAnswers(ActiveGame.CurrentRound)));
            }
            else if (databaseEN.Count > 0 && GameType == "gamePL")
            {
                ActiveGame.QuestionList.Add(databasePL.FirstOrDefault(x => x.IsAvailable && x.Answers.Count == gameService.GetNumberOfAnswers(ActiveGame.CurrentRound)));
            }
        }

        /*private Action<Answer> setAnswerObjectCollection()
        {
            this.AnswersObjectCollection = new ObservableCollection<Answer>();
            return f => this.answersObjectCollection.Add(new Answer
            {
                AnswerString = f.AnswerString, Points = f.Points
            });
        }*/

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

        private Question currentQuestion;
        public Question CurrentQuestion
        {
            get => currentQuestion;
            set
            {
                currentQuestion = value;
                NotifyPropertyChanged();
            }
        }

        private Answer currentAnswer;
        public Answer CurrentAnswer
        {
            get => currentAnswer;
            set
            {
                currentAnswer = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<Answer> answersObjectCollection;
        public ObservableCollection<Answer> AnswersObjectCollection
        {
            get => answersObjectCollection;
            set
            {
                answersObjectCollection = value;
                NotifyPropertyChanged();
            }
        }
        

        private bool _isSymbolVisible1; 
        public bool IsSymbolVisible1
        {
            get => _isSymbolVisible1;
            set
            {
                _isSymbolVisible1 = value;
                NotifyPropertyChanged();
            }
        }

        private bool _isSymbolVisible2;
        public bool IsSymbolVisible2
        {
            get => _isSymbolVisible2;
            set
            {
                _isSymbolVisible2 = value;
                NotifyPropertyChanged();
            }
        }

        private bool _isSymbolVisible11;
        public bool IsSymbolVisible11
        {
            get => _isSymbolVisible11;
            set
            {
                _isSymbolVisible11 = value;
                NotifyPropertyChanged();
            }
        }

        private bool _isSymbolVisible12;
        public bool IsSymbolVisible12
        {
            get => _isSymbolVisible12;
            set
            {
                _isSymbolVisible12 = value;
                NotifyPropertyChanged();
            }
        }

        private bool _isSymbolVisible13;
        public bool IsSymbolVisible13
        {
            get => _isSymbolVisible13;
            set
            {
                _isSymbolVisible13 = value;
                NotifyPropertyChanged();
            }
        }

        private bool _isSymbolVisible21;
        public bool IsSymbolVisible21
        {
            get => _isSymbolVisible21;
            set
            {
                _isSymbolVisible21 = value;
                NotifyPropertyChanged();
            }
        }

        private bool _isSymbolVisible22;
        public bool IsSymbolVisible22
        {
            get => _isSymbolVisible22;
            set
            {
                _isSymbolVisible22 = value;
                NotifyPropertyChanged();
            }
        }

        private bool _isSymbolVisible23;    
        public bool IsSymbolVisible23
        {
            get => _isSymbolVisible23;
            set
            {
                _isSymbolVisible23 = value;
                NotifyPropertyChanged();
            }
        }

        private int _firstTeamScore;
        public int FirstTeamScore
        {
            get => _firstTeamScore;
            set
            {
                _firstTeamScore = value;
                NotifyPropertyChanged();
            }
        }
        private int _secondTeamScore;
        public int SecondTeamScore
        {
            get => _secondTeamScore;
            set
            {
                _secondTeamScore = value;
                NotifyPropertyChanged();
            }
        }

        private int _pointsToWinRound = 500;
        public int PointsToWinRound
        {
            get => _pointsToWinRound;
            set
            {
                _pointsToWinRound = value;
                NotifyPropertyChanged();
            }
        }

        private int _pointsToWinInRound;
        public int PointsToWin
        {
            get => _pointsToWinInRound;
            set
            {
                _pointsToWinInRound = value;
                NotifyPropertyChanged();
            }
        }

        private int _pointsSum;
        public int PointsSum
        {
            get => _pointsSum;
            set
            {
                _pointsSum = value;
                NotifyPropertyChanged();
            }
        }

        private List<string> _questionList;
        public List<string> QuestionList
        {
            get => _questionList;
            set
            {
                _questionList = value;
                NotifyPropertyChanged();
            }
        }


        private bool _isFirstTeamAnswering;
        public bool IsFirstTeamAnswering
        {
            get => _isFirstTeamAnswering;
            set
            {
                _isFirstTeamAnswering = value;
                NotifyPropertyChanged();
            }
        }

        private bool _isSecondTeamAnswering;
        public bool IsSecondTeamAnswering
        {
            get => _isSecondTeamAnswering;
            set
            {
                _isSecondTeamAnswering = value;
                NotifyPropertyChanged();
            }
        }

        private string gameType = "gamePL"; //bind to combobox/radiobutton
        public string GameType
        {
            get => gameType;
            set
            {
                if (gameType != value)
                {
                    gameType = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public enum View
        {
            Settings,
            Game,
            Statistics,
        }

        private View _currentViewType;
        public View CurrentViewType
        {
            get => _currentViewType;
            set
            {
                if (_currentViewType != value)
                {
                    _currentViewType = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private UserControl _currentView;
        public UserControl CurrentView
        {
            get => _currentView;
            set
            {
                if (_currentView != value)
                {
                    _currentView = value; 
                    NotifyPropertyChanged();
                }
            }
        }

        public ICommand OnWrongAnswerClickCommand { get; protected set; }
        public ICommand OnAnswerClickCommand { get; protected set; }
        public ICommand OnResetQuestionsClickCommand { get; protected set; }
        public ICommand SwitchViewCommand { get; private set; }

        public ICommand StartGame { get; }
        public ICommand ResetGame { get; }
        private void SwitchView(object obj)
        {
            // Implement logic to switch between views
            // For example, toggle between ViewA and ViewB
            if (obj is string viewType)
            {
                if (viewType == View.Settings.ToString())
                {
                    CurrentView = new SettingsView();
                }
                else if(viewType == View.Game.ToString())
                {
                    CurrentView = new RoundControlView();
                }
            }
        }
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

        private void OnResetQuestionsClick(object obj)
        {
            if (gameService.SerializeObjectsToJson(GameService.PathEN))
            {
                databaseEN = gameService.GetQuestionsList(GameService.PathEN);
            }
            if (gameService.SerializeObjectsToJson(GameService.PathPL))
            {
                databasePL = gameService.GetQuestionsList(GameService.PathPL);
            }
        }
    }
}
