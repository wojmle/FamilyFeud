using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyFeud.MVVM.Model
{
    internal class Question
    {
        public int RoundNumber { get; }
        public List<Answer> Answers { get; set; }
        public string QuestionString { get; set; }

        public Question(int roundNumber, List<Answer> answers, string questionString)
        {
            RoundNumber = roundNumber;
            Answers = answers;
            QuestionString = questionString;
        }
    }
}
