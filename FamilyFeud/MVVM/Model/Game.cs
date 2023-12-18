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
            QuestionList = new ObservableCollection<Question>();
            CurrentRound = 1;
            SetQuestion();
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

        private ObservableCollection<Question> questionList;
        public ObservableCollection<Question> QuestionList
        {
            get { return questionList; }
            set
            {
                questionList = value;
                NotifyPropertyChanged();
            }
        }

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

        private int maxRounds;
        public int MaxRounds
        {
            get => maxRounds;
            set
            {
                maxRounds = value;
                NotifyPropertyChanged();
            }
        }

        private int pointsToWinGame;
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

        public void AddWrongAnswer()
        {
            WrongAnswers++;
        }

        public void ResetWrongAnswers()
        {
            WrongAnswers = 0;
        }


        public void SetQuestion()
        {
            QuestionList.Add(new Question(CurrentRound));
        }
    }
}
