using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FamilyFeud.MVVM.Service;
using FamilyFeud.MVVM.ViewModel;

namespace FamilyFeud.MVVM.Model
{
    public class Game : ObservableObject
    {
        public Game()
        {
            GameHistoryPath = "/GameHistory.xml";
            CurrentRound = 1;
        }

        public Game(string firstTeam, string secondTeam)
        {
            FirstTeam = new Team{ Name = firstTeam, Points = 0, Lives = 3};
            SecondTeam = new Team { Name = secondTeam, Points = 0, Lives = 3 };
            CurrentRound = 1;
            MaxRounds = 5;
            GameRounds = new ObservableCollection<Round>();
        }

        private ObservableCollection<Round> gameRounds;
        public ObservableCollection<Round> GameRounds
        {
            get { return gameRounds;}
            set
            {
                gameRounds = value;
                NotifyPropertyChanged();
            }
        }

        private Team firstTeam;
        public Team FirstTeam
        {
            get { return firstTeam; }
            set
            {
                firstTeam = value;
                NotifyPropertyChanged();
            }
        }

        private Team secondTeam;
        public Team SecondTeam
        {
            get { return secondTeam; }
            set
            {
                secondTeam = value;
                NotifyPropertyChanged();
            }
        }

        //private ObservableCollection<Question> questionList;
        //public ObservableCollection<Question> QuestionList
        //{
        //    get { return questionList; }
        //    set
        //    {
        //        questionList = value;
        //        NotifyPropertyChanged();
        //    }
        //}

        private int currentRound;
        public int CurrentRound
        {
            get => currentRound;
            set
            {
                currentRound = value;
                NotifyPropertyChanged();
            }
        }

        private int _maxRounds = 5;
        public int MaxRounds
        {
            get => _maxRounds;
            set
            {
                _maxRounds = value;
                NotifyPropertyChanged();
            }
        }

        private int pointsToWinGame = 600;
        public int PointsToWinGame
        {
            get => pointsToWinGame;
            set
            {
                pointsToWinGame = value;
                NotifyPropertyChanged();
            }
        }

        private int wrongAnswers;
        public int WrongAnswers
        {
            get => wrongAnswers;
            set
            {
                wrongAnswers = value;
                NotifyPropertyChanged();
            }
        }

        private string teamStartingRound;
        public string TeamStartingRound
        {
            get => teamStartingRound;
            set
            {
                teamStartingRound = value;
                NotifyPropertyChanged();
            }
        }

        private string gameHistoryPath;

        public string GameHistoryPath
        {
            get => gameHistoryPath;
            set
            {
                gameHistoryPath = value;
                NotifyPropertyChanged();
            }
        }
    }
}
