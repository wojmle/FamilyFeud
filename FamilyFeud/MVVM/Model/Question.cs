using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FamilyFeud.MVVM.ViewModel;
using Excel = Microsoft.Office.Interop.Excel;

namespace FamilyFeud.MVVM.Model
{
    public class Question : ObservableObject
    {
        private ObservableCollection<Answer> answers;
        public ObservableCollection<Answer> Answers
        {
            get => answers;
            set
            {
                answers = value;
                NotifyPropertyChanged();
            }
        }

        private string questionString;
        public string QuestionString
        {
            get => questionString;
            set
            {
                questionString = value;
                NotifyPropertyChanged();
            }
        }

        private bool isAvailable;
        public bool IsAvailable
        {
            get => isAvailable;
            set
            {
                isAvailable = value;
                NotifyPropertyChanged();
            }
        }

        private bool isReusable = true;

        public bool IsReusable
        {
            get => isReusable;
            set
            {
                isReusable = value;
                NotifyPropertyChanged();
            }
        }

        public Question()
        {
        }
    }
}
