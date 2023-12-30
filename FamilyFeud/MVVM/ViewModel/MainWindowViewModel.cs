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
using FamilyFeud.MVVM.Enums;
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

        public MainWindowViewModel(Game game)
        {
            gameService = new GameService();
            _playerClass = new WindowsMediaPlayerClass(); 
            SwitchViewCommand = new RelayCommand(SwitchView);
            OnQuestionsNumberChangedCommand = new RelayCommand(OnQuestionsNumberChanged);
            OnWrongAnswerClickCommand = new RelayCommand(OnWrongAnswerClick);
            OnAnswerClickCommand = new RelayCommand(OnAnswerClick);
            OnResetQuestionsClickCommand = new RelayCommand(OnResetQuestionsClick);
            OnNextRoundClickCommand = new RelayCommand(NextRound);
            OnPrevRoundClickCommand = new RelayCommand(PrevRound);
            OnWrongResetClickCommand = new RelayCommand(OnWrongResetClick);
            OnAssignPointsClickCommand = new RelayCommand(OnAssignPointsClick);
            OnFinishGameCommand = new RelayCommand(OnFinishGame);
            OnSkipQuestionClickCommand = new RelayCommand(OnSkipQuestionClick);
            ActiveGame = game;
            CurrentView = new RoundControlView();
            CurrentViewType = ViewType.Game;
            GameType = "gameEN";
            SetDefaultData();
            SetQuestions();
            SetCurrentQuestion();
        }

        private void SetCurrentQuestion()
        {
            //ActiveGame.QuestionList[ActiveGame.CurrentRound - 1].Answers.ForEach(setAnswerObjectCollection());
            AnswersObjectCollection = ActiveGame.GameRounds[ActiveGame.CurrentRound - 1].RoundQuestion.Answers;
            CurrentQuestion = ActiveGame.GameRounds[ActiveGame.CurrentRound - 1].RoundQuestion;
        }
        
        private void SetDefaultData()
        {
            IsSymbolVisible1 = IsSymbolVisible11 = IsSymbolVisible12 = IsSymbolVisible13 =
                IsSymbolVisible2 = IsSymbolVisible21 = IsSymbolVisible22 = IsSymbolVisible23 = false;

            databaseEN = gameService.GetQuestionsList(GameService.PathEN);
            databasePL = gameService.GetQuestionsList(GameService.PathPL);
        }

        //move new Round() adding to the list to another place so properties of Round are not overwritten
        private void SetQuestions()
        {
            ActiveGame.GameRounds.Clear();
            for (int i = 1; i <= ActiveGame.MaxRounds; i++)
            {
                ActiveGame.GameRounds.Add(new Round());
                if (databaseEN.Count > 0 && GameType == "gameEN")
                {
                    ActiveGame.GameRounds[i-1].RoundQuestion = (databaseEN.FirstOrDefault(x =>
                        x.IsAvailable && x.Answers.Count == gameService.GetNumberOfAnswers(ActiveGame.CurrentRound) &&
                        !ActiveGame.GameRounds.Select(y=>y.RoundQuestion).Contains(x)));
                }
                else if (databaseEN.Count > 0 && GameType == "gamePL")
                {
                    ActiveGame.GameRounds[i-1].RoundQuestion = (databasePL.FirstOrDefault(x =>
                        x.IsAvailable && x.Answers.Count == gameService.GetNumberOfAnswers(ActiveGame.CurrentRound) &&
                        !ActiveGame.GameRounds.Select(y => y.RoundQuestion).Contains(x)));
                }
            }
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

        private bool _addPointsToSum = true;
        public bool AddPointsToSum
        {
            get => _addPointsToSum;
            set
            {
                _addPointsToSum = value;
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

        private int _pointsToWinGame = 500;
        public int PointsToWinGame
        {
            get => _pointsToWinGame;
            set
            {
                _pointsToWinGame = value;
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

        private int _firstPersonAnswerTime = 15;
        public int FirstPersonAnswerTime
        {
            get => _firstPersonAnswerTime;
            set
            {
                _firstPersonAnswerTime = value;
                NotifyPropertyChanged();
            }
        }

        private int _secondPersonAnswerTime = 20;
        public int SecondPersonAnswerTime
        {
            get => _secondPersonAnswerTime;
            set
            {
                _secondPersonAnswerTime = value;
                NotifyPropertyChanged();
            }
        }

        private int _finalQuestionsNumber = 5;
        public int FinalQuestionsNumber
        {
            get => _finalQuestionsNumber;
            set
            {
                _finalQuestionsNumber = value;
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

        private string gameType; //bind to combobox/radiobutton
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

        private Language _gameLanguage = Language.Polski; //bind to combobox/radiobutton
        public Language GameLanguage
        {
            get => _gameLanguage;
            set
            {
                if (_gameLanguage != value)
                {
                    _gameLanguage = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private YesOrNo _oneManArmy = YesOrNo.Yes; 
        public YesOrNo OneManArmy
        {
            get => _oneManArmy;
            set
            {
                if (_oneManArmy != value)
                {
                    _oneManArmy = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private YesOrNo _speechRecognition = YesOrNo.No;
        public YesOrNo SpeechRecognition
        {
            get => _speechRecognition;
            set
            {
                if (_speechRecognition != value)
                {
                    _speechRecognition = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private YesOrNo _controllerReader = YesOrNo.No;
        public YesOrNo ControllerReader
        {
            get => _controllerReader;
            set
            {
                if (_controllerReader != value)
                {
                    _controllerReader = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private YesOrNo _presenterJoke = YesOrNo.No;
        public YesOrNo PresenterJoke
        {
            get => _presenterJoke;
            set
            {
                if (_presenterJoke != value)
                {
                    _presenterJoke = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private Voice _presenterVoice = Voice.Michał;
        public Voice PresenterVoice
        {
            get => _presenterVoice;
            set
            {
                if (_presenterVoice != value)
                {
                    _presenterVoice = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public enum YesOrNo
        {
            Yes,
            No,
        }

        private ViewType _currentViewType;
        public ViewType CurrentViewType
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

        public ICommand OnAssignPointsClickCommand { get; protected set; }
        public ICommand OnWrongResetClickCommand { get; protected set; }
        public ICommand OnWrongAnswerClickCommand { get; protected set; }
        public ICommand OnAnswerClickCommand { get; protected set; }
        public ICommand OnResetQuestionsClickCommand { get; protected set; }
        public ICommand SwitchViewCommand { get; private set; }
        public ICommand OnNextRoundClickCommand { get; private set; }
        public ICommand OnPrevRoundClickCommand { get; private set; }
        public ICommand OnQuestionsNumberChangedCommand { get; protected set; }
        public ICommand OnFinishGameCommand { get; protected set; }
        public ICommand OnSkipQuestionClickCommand { get; protected set; }

        private void NextRound(object obj)
        {
            if (activeGame.CurrentRound < activeGame.MaxRounds)
            {
                activeGame.CurrentRound++;
                SetCurrentQuestion();
                AddPointsToSum = true;
            }
        }

        private void PrevRound(object obj)
        {
            if (activeGame.CurrentRound > 1)
            {
                activeGame.CurrentRound--;
                SetCurrentQuestion();
            }
        }

        private void SwitchView(object obj)
        {
            // Implement logic to switch between views
            // For example, toggle between ViewA and ViewB
            if (obj is string viewType)
            {
                if (viewType == ViewType.Settings.ToString())
                {
                    CurrentView = new SettingsView();
                }
                else if(viewType == ViewType.Game.ToString())
                {
                    CurrentView = new RoundControlView();
                }
            }
        }

        private bool _oneOnOne = false;
        public bool OneOnOne
        {
            get => _oneOnOne;
            set
            {
                _oneOnOne = value;
                NotifyPropertyChanged();
            }
        }

        private void OnAssignPointsClick(object obj)
        {
            if (IsFirstTeamAnswering)
            {
                FirstTeamScore += PointsToWin;
                ActiveGame.GameRounds[ActiveGame.CurrentRound-1].FirstTeamPoints += PointsToWin;
                AddPointsToSum = false;
                PointsToWin = 0;
            }
            else if (IsSecondTeamAnswering)
            {
                SecondTeamScore += PointsToWin;
                ActiveGame.GameRounds[ActiveGame.CurrentRound-1].SecondTeamPoints += PointsToWin;
                AddPointsToSum = false;
                PointsToWin = 0;
            }
        }


        private void OnWrongResetClick(object obj)
        {
            ActiveGame.GameRounds[ActiveGame.CurrentRound-1].WrongAnswers = 0;
            IsSymbolVisible1 = IsSymbolVisible11 = IsSymbolVisible12 = IsSymbolVisible13 =
                IsSymbolVisible2 = IsSymbolVisible21 = IsSymbolVisible22 = IsSymbolVisible23 = false;
        }

        private void OnWrongAnswerClick(object obj)
        {
            if (OneOnOne && IsFirstTeamAnswering)
            {
                IsSymbolVisible1 = true;
                PlayAnswerSound("WrongAnswer");
            }
            else if (OneOnOne && IsSecondTeamAnswering)
            {
                IsSymbolVisible2 = true;
                PlayAnswerSound("WrongAnswer");
            }
            else if (IsFirstTeamAnswering)
            {
                switch (ActiveGame.GameRounds[ActiveGame.CurrentRound-1].WrongAnswers)
                {
                    case 0:
                        ActiveGame.GameRounds[ActiveGame.CurrentRound-1].AddWrongAnswer();
                        IsSymbolVisible11 = true;
                        PlayAnswerSound("WrongAnswer");
                        break;
                    case 1:
                        ActiveGame.GameRounds[ActiveGame.CurrentRound-1].AddWrongAnswer();
                        IsSymbolVisible12 = true;
                        PlayAnswerSound("WrongAnswer");
                        break;
                    case 2:
                        ActiveGame.GameRounds[ActiveGame.CurrentRound-1].AddWrongAnswer();
                        IsSymbolVisible13 = true;
                        PlayAnswerSound("WrongAnswer");
                        break;
                    case 3:
                        ActiveGame.GameRounds[ActiveGame.CurrentRound-1].AddWrongAnswer();
                        IsSymbolVisible2 = true;
                        PlayAnswerSound("WrongAnswer");
                        break;
                    default:
                        IsSymbolVisible11 = IsSymbolVisible12 =
                            IsSymbolVisible13 = IsSymbolVisible2 = false;
                        ActiveGame.GameRounds[ActiveGame.CurrentRound-1].AddWrongAnswer();
                        break;
                }
            }
            else if (IsSecondTeamAnswering)
            {
                switch (ActiveGame.GameRounds[ActiveGame.CurrentRound-1].WrongAnswers)
                {
                    case 0:
                        ActiveGame.GameRounds[ActiveGame.CurrentRound-1].AddWrongAnswer();
                        IsSymbolVisible21 = true;
                        PlayAnswerSound("WrongAnswer");
                        break;
                    case 1:
                        ActiveGame.GameRounds[ActiveGame.CurrentRound-1].AddWrongAnswer();
                        IsSymbolVisible22 = true;
                        PlayAnswerSound("WrongAnswer");
                        break;
                    case 2:
                        ActiveGame.GameRounds[ActiveGame.CurrentRound-1].AddWrongAnswer();
                        IsSymbolVisible23 = true;
                        PlayAnswerSound("WrongAnswer");
                        break;
                    case 3:
                        ActiveGame.GameRounds[ActiveGame.CurrentRound-1].AddWrongAnswer();
                        IsSymbolVisible1 = true;
                        PlayAnswerSound("WrongAnswer");
                        break;
                    default:
                        IsSymbolVisible21 = IsSymbolVisible22 =
                            IsSymbolVisible23 = IsSymbolVisible1 = false;
                        ActiveGame.GameRounds[ActiveGame.CurrentRound-1].ResetWrongAnswers();
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
                            if (AddPointsToSum)
                            {
                                PointsToWin += answerObject.Points;
                                PlayAnswerSound("CorrectAnswer");
                            }
                            return;
                            // if team1 currently answering assign points to temporary container (consider creating two separate lists for each team and separate buttons)

                        }
                        else if (answer.IsVisible)
                        {
                            answerObject.IsVisible = false;
                            AnswersObjectCollection[index] = answerObject;
                            if (AddPointsToSum)
                            {
                                PointsToWin -= answerObject.Points;
                            }
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

        private void OnFinishGame(object obj)
        {
            var database = GameType == "gamePL" ? databasePL : databaseEN;
            var questionsToRemove = ActiveGame.GameRounds.Where(x=>!x.RoundQuestion.IsReusable).Select(x => x.RoundQuestion).ToList();
            if (database == databasePL)
            {
                databasePL = gameService.UpdateDatabaseAfterGameEnd(database, questionsToRemove, GameType);
            }
            else if (database == databaseEN)
            {
                databaseEN = gameService.UpdateDatabaseAfterGameEnd(database, questionsToRemove, GameType);
            }
        }

        private void OnQuestionsNumberChanged(object obj)
        {
            if (obj is int questionsNumber)
            {
                ActiveGame.MaxRounds = questionsNumber;
                ActiveGame.CurrentRound = 1;
                OnWrongResetClick(this);
                SetQuestions();
                SetCurrentQuestion();
            }
        }

        //need to save to file, consider whether IsReusable is required
        private void OnSkipQuestionClick(object obj)
        {
            if (gameType == "gamePL")
            {
                var index = databasePL.FindIndex(x => x.QuestionString == CurrentQuestion.QuestionString);
                databasePL[index].IsAvailable = false;
                databasePL[index].IsReusable = false;
                ActiveGame.GameRounds[ActiveGame.CurrentRound-1].RoundQuestion = databasePL.FirstOrDefault(x =>
                    x.IsAvailable && ActiveGame.GameRounds.Select(y => y.RoundQuestion)
                        .All(y => y.QuestionString != x.QuestionString));
            }
            if (gameType == "gameEN")
            {
                var index = databaseEN.FindIndex(x => x.QuestionString == CurrentQuestion.QuestionString);
                databaseEN[index].IsAvailable = false;
                databaseEN[index].IsReusable = false;
                ActiveGame.GameRounds[ActiveGame.CurrentRound -1].RoundQuestion = databaseEN.FirstOrDefault(x =>
                    x.IsAvailable && ActiveGame.GameRounds.Select(y => y.RoundQuestion)
                        .All(y => y.QuestionString != x.QuestionString));
            }
            SetCurrentQuestion();
        }
    }
}
