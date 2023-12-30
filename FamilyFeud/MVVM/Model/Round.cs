using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FamilyFeud.MVVM.ViewModel;

namespace FamilyFeud.MVVM.Model
{
    public class Round : ObservableObject
    {
        private Question roundQuestion;
        public Question RoundQuestion
        {
            get => roundQuestion;
            set
            {
                roundQuestion = value;
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

        private int roundNumber;

        public int RoundNumber
        {
            get => roundNumber;
            set
            {
                roundNumber = value;
                NotifyPropertyChanged();
            }
        }

        private int firstTeamPoints;

        public int FirstTeamPoints
        {
            get => firstTeamPoints;
            set
            {
                firstTeamPoints = value;
                NotifyPropertyChanged();
            }
        }

        private int secondTeamPoints;

        public int SecondTeamPoints
        {
            get => secondTeamPoints;
            set
            {
                secondTeamPoints = value;
                NotifyPropertyChanged();
            }
        }

        private int winningTeam;

        public int WinningTeam
        {
            get => winningTeam;
            set
            {
                winningTeam = value;
                NotifyPropertyChanged();
            }
        }

        private int pointsMultiplier;

        public int PointsMultiplier
        {
            get => pointsMultiplier;
            set
            {
                pointsMultiplier = value;
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
    }
}
