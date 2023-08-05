using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyFeud.MVVM.Model
{
    internal class Game
    {
        public string FirstTeamName { get; set; }
        public string SecondTeamName { get; set; }
        public List<Question> QuestionList { get; }
        public int CurrentRound { get; set; }

        public Game(string firstTeam, string secondTeam, List<Question> questionList)
        {
            FirstTeamName = firstTeam;
            SecondTeamName = secondTeam;
            QuestionList = questionList;
            CurrentRound = 0;
        }

        public void NextRound()
        {
            CurrentRound++;
        }

        public void PreviousRound()
        {
            CurrentRound--;
        }
    }
}
