using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FamilyFeud.MVVM.Model;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;


namespace FamilyFeud.MVVM.Service
{
    public class GameService
    {
        public Excel.Workbook ExcelWorkbook { get; set; }
        public Excel.Application ExcelApplication { get; set; } = new Excel.Application();

        public List<Question> ConvertExcelToJson() //temporary returning List<question>
        {
            string path = @"MVVM\\DataBase\\QuestionsPL.xlsx";
            path = System.IO.Path.GetFullPath(path);
            ConnectToExcel(path);
            var questionsList = ReadExcelToJson(1, path);
            CloseExcelConnection();
            return questionsList;
        }

        private void ConnectToExcel(string path)
        {
            ExcelWorkbook = ExcelApplication.Workbooks.Open(path); // should be path from parameters
        }

        public void CloseExcelConnection()
        {
            ExcelWorkbook.Close();
            ExcelApplication.Quit();
        }

        public List<Question> ReadExcelToJson(int roundNumber, string source)
        {
            List<Question> questionsList = new List<Question>();
            var path = System.IO.Path.GetFullPath(source);
            if (source == path)
            {
                Excel.Worksheet excelWorkSheet = ExcelWorkbook.Sheets[roundNumber];
                Excel.Range excelRange = excelWorkSheet.UsedRange;
                var question = excelRange.Cells[1, 1].Value.ToString();
                var answersCountString = Int32.TryParse(excelRange.Cells[1, 2].Value.ToString(), out int answersCount);
                if (!answersCountString)
                {
                    return questionsList;
                }
                var lastRow = excelRange.Row + excelRange.Rows.Count - 1;
                List<Answer> answersList = new List<Answer>();
                for (int j = 1; j < lastRow; j += answersCount)
                {
                    answersList.Clear();
                    var questionObject = new Question
                    {
                        QuestionString = question,
                        IsAvailable = true,
                    };
                    question = excelRange.Cells[j,1].Value.ToString();
                    for (int i = 1; i < answersCount; i++)
                    {
                        var pointsToAssign = Int32.Parse(excelRange.Cells[i + j, 2].Value.ToString());
                        var answerStringToAssign = excelRange.Cells[i + j, 1].Value.ToString();
                        Answer answerToQuestion = new Answer
                        {
                            AnswerString = answerStringToAssign,
                            Points = pointsToAssign,
                            IsVisible = false,
                        };
                        answersList.Add(answerToQuestion);
                    }

                    questionObject.Answers = answersList;
                    var counter = answersCount + j;
                    answersCount = Int32.Parse(excelRange.Cells[1 + counter, 2].Value.ToString());
                }
            }

            return questionsList;
        }

        public void SetQuestionAndAnswers(int roundNumber)
        {
            List<Answer> newList = new List<Answer>();
            Excel._Worksheet excelWorksheet = ExcelWorkbook.Sheets[roundNumber];
            Excel.Range excelRange = excelWorksheet.UsedRange;
            var question = excelRange.Cells[1, 1].Value.ToString();
            for (int i = 1; i <= GetNumberOfAnswers(roundNumber); i++)
            {
                var pointsToAssign = Int32.Parse(excelRange.Cells[i + 1, 2].Value.ToString());
                var answerStringToAssign = excelRange.Cells[i + 1, 1].Value.ToString();
                newList.Add(new Answer(pointsToAssign, answerStringToAssign));
            }
            CloseExcelConnection();
        }

        public string CheckGameHistory()
        {
            return "s";
        }

        public int GetNumberOfAnswers(int roundNumber)
        {
            switch (roundNumber)
            {
                case 1:
                case 2:
                    return 5;
                case 3:
                    return 3;
                case 4:
                    return 4;
                default:
                    return 5;
            }
        }
    }
}
