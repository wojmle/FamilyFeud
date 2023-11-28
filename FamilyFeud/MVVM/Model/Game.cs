using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyFeud.MVVM.Model
{
    public class Game
    {
        public string FirstTeamName { get; set; }
        public string SecondTeamName { get; set; }
        public List<Question> QuestionList { get; }
        public int CurrentRound { get; set; }
        public int WrongAnswers { get; set; }
        public string TeamStartingRound { get; set; }
        public string GameHistoryPath { get; set; }

        public Game()
        {
            GameHistoryPath = "/GameHistory.xml";
            CurrentRound = 1;
        }

        public Game(string firstTeam, string secondTeam)
        {
            QuestionList = new List<Question>();
            FirstTeamName = firstTeam;
            SecondTeamName = secondTeam;
            CurrentRound = 1;
            SetQuestion();
        }

        public void NextRound()
        {
            CurrentRound++;
        }

        public void PreviousRound()
        {
            CurrentRound--;
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
