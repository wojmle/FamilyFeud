using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FamilyFeud.MVVM.View;
using FamilyFeud.MVVM.ViewModel;

namespace FamilyFeud.MVVM.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string _firstTeamName { get; set; }
        public string FirstTeamName
        {
            get { return _firstTeamName; }
            set
            {
                _firstTeamName = value;
                OnPropertyChanged(nameof(FirstTeamName));
            }
        }
        
        public string _secondTeamName { get; set; }
        public string SecondTeamName
        {
            get { return _secondTeamName; }
            set
            {
                _secondTeamName = value;
                OnPropertyChanged(nameof(SecondTeamName));
            }
        }

        private int _firstTeamScore { get; set; }
        public int FirstTeamScore
        {
            get { return _firstTeamScore; }
            set
            {
                _firstTeamScore = value;
                OnPropertyChanged(nameof(FirstTeamScore));
            }
        }
        private int _secondTeamScore { get; set; }
        public int SecondTeamScore
        {
            get { return _secondTeamScore; }
            set
            {
                _secondTeamScore = value;
                OnPropertyChanged(nameof(SecondTeamScore));
            }
        }

        private int _pointsToWin { get; set; }
        public int PointsToWin
        {
            get { return _pointsToWin; }
            set
            {
                _pointsToWin = value;
                OnPropertyChanged(nameof(PointsToWin));
            }
        }

        private int _pointsSum { get; set; }
        public int PointsSum
        {
            get { return _pointsSum; }
            set
            {
                _pointsSum = value;
                OnPropertyChanged(nameof(PointsSum));
            }
        }

        private List<string> _questionList { get; set; }
        public List<string> QuestionList
        {
            get { return _questionList; }
            set
            {
                _questionList = value;
                OnPropertyChanged(nameof(QuestionList));
            }
        }

        
        public bool _isFirstTeamAnswering { get; set; }
        public bool IsFirstTeamAnswering
        {
            get { return _isFirstTeamAnswering; }
            set
            {
                _isFirstTeamAnswering = value;
                OnPropertyChanged(nameof(IsFirstTeamAnswering));
            }
        }

        public bool _isSecondTeamAnswering { get; set; }
        public bool IsSecondTeamAnswering
        {
            get { return _isSecondTeamAnswering; }
            set
            {
                _isSecondTeamAnswering = value;
                OnPropertyChanged(nameof(IsSecondTeamAnswering));
            }
        }


        public ICommand StartGame { get; }
        public ICommand ResetGame { get; }

    }
}
