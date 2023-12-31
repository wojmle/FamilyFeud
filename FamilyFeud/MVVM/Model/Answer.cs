﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FamilyFeud.MVVM.ViewModel;

namespace FamilyFeud.MVVM.Model
{
    public class Answer : ObservableObject
    {
        public Answer() { }

        public Answer(int points, string answerString)
        {
            this.points = points;
            this.AnswerString = answerString;
        }

        private int points;
        public int Points 
        { 
            get => points;
            set
            {
                points = value;
                NotifyPropertyChanged();
            }
        }

        private string answerString;
        public string AnswerString
        {
            get => answerString;
            set
            {
                answerString = value;
                NotifyPropertyChanged();
            }
        }

        private bool isVisible = false;

        public bool IsVisible
        {
            get => isVisible;
            set
            {
                isVisible = value;
                NotifyPropertyChanged();
            }
        }
    }
}
