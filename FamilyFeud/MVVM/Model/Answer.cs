using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyFeud.MVVM.Model
{
    internal class Answer
    {
        public int Points { get; set; }
        public string AnswerString { get; set; }
        public bool IsVisible { get; set; }

        public Answer()
        {
            IsVisible = false;
        }

        public Answer(int points, string answer)
        {
            Points = points;
            AnswerString = answer;
            IsVisible = false;
        }
    }
}
