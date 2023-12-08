using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FamilyFeud.MVVM.ViewModel;
using Excel = Microsoft.Office.Interop.Excel;

namespace FamilyFeud.MVVM.Model
{
    public class Question : ObservableObject
    {
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

        private List<Answer> answers;
        public List<Answer> Answers
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

        public Excel.Workbook ExcelWorkbook { get; set; }

        public Question()
        {
        }

        public Question(int roundNumber)
        {
            Answers = new List<Answer>();
            RoundNumber = roundNumber;
            ConnectToWorkbook();
            SetQuestionAndAnswers(RoundNumber);
        }

        public void ConnectToWorkbook()
        {
            Excel.Application excelApp = new Excel.Application();
            string excelPath = "c:/KatI.xlsx";
            Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(excelPath);
            ExcelWorkbook = excelWorkbook;
        }

        public int GetNumberOfAnswers()
        {
            switch (RoundNumber)
            {
                case 1:
                case 2:
                    return 5;
                case 3:
                    return 3;
                default:
                    return 5;
            }
        }

        public void SetQuestionAndAnswers(int roundNumber)
        {
            Excel._Worksheet excelWorksheet = ExcelWorkbook.Sheets[RoundNumber];
            Excel.Range excelRange = excelWorksheet.UsedRange;
            var question = excelRange.Cells[1, 1].Value.ToString();
            QuestionString = question;
            for (int i = 1; i <= GetNumberOfAnswers(); i++)
            {
                var pointsToAssign = Int32.Parse(excelRange.Cells[i + 1, 2].Value.ToString());
                var answerStringToAssign = excelRange.Cells[i + 1, 1].Value.ToString();
                Answers.Add(new Answer(pointsToAssign, answerStringToAssign));
            }
        }

    }
}
