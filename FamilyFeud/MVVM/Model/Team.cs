using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FamilyFeud.MVVM.ViewModel;
using Excel = Microsoft.Office.Interop.Excel;

namespace FamilyFeud.MVVM.Model
{
    public class Team : ObservableObject
    {
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

        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                NotifyPropertyChanged();
            }
        }

        private bool isAnswering;
        public bool IsAnswering
        {
            get => isAnswering;
            set
            {
                isAnswering = value;
                NotifyPropertyChanged();
            }
        }

        private int lives;
        public int Lives
        {
            get => lives;
            set
            {
                lives = value;
                NotifyPropertyChanged();
            }
        }
    }
}
