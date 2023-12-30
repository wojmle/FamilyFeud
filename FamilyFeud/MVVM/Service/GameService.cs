using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FamilyFeud.MVVM.Model;
using FamilyFeud.Properties;
using Microsoft.Office.Interop.Excel;
using Newtonsoft.Json;
using Excel = Microsoft.Office.Interop.Excel;


namespace FamilyFeud.MVVM.Service
{
    public class GameService
    {
        public const string FolderPath = @"MVVM\\DataBase\\";
        public const string PathEN = @"QuestionsEN";
        public const string PathPL = @"QuestionsPL";
        public Excel.Workbook ExcelWorkbook { get; set; }
        public Excel.Application ExcelApplication { get; set; } = new Excel.Application();
        private DataService dataService = new DataService();

        public List<Question> GetQuestionsList(string path)
        {
            if (path.Equals(PathEN))
            {
                var jsonPath = Path.Combine(FolderPath, PathEN + ".json");
                dataService.GetObjectFromFile(jsonPath, out List<Question> questionsList);
                return questionsList;
            }
            if (path.Equals(PathPL))
            {
                var jsonPath = Path.Combine(FolderPath, PathPL + ".json");
                dataService.GetObjectFromFile(jsonPath, out List<Question> questionsList);
                return questionsList;
            }

            return new List<Question>();
        }

        public bool SerializeObjectsToJson(string type)
        {
            int count = 1;
            if (type == PathPL)
            {
                var path = Path.Combine(FolderPath, PathPL + ".xlsx");
                var game = ReadExcel(path);
                
                var file = Path.Combine(FolderPath, "QuestionsPL.json");
                
                while (File.Exists(file))
                {
                    string tempFileName = string.Format("{0}({1})", PathPL, count++);
                    file = Path.Combine(FolderPath, tempFileName + ".json");
                }

                if (dataService.SaveObjectToFile(game, file) is false)
                {
                    var message = "Failed to save game database.";
                    return false;
                }

                return true;

            }
            if (type == PathEN)
            {
                var path = Path.Combine(FolderPath, PathEN + ".xlsx");
                var game = ReadExcel(path);

                var file = Path.Combine(FolderPath, "QuestionsEN.json");

                while (File.Exists(file))
                {
                    string tempFileName = string.Format("{0}({1})", PathEN, count++);
                    file = Path.Combine(FolderPath, tempFileName + ".json");
                }

                if (dataService.SaveObjectToFile(game, file) is false)
                {
                    var message = "Failed to save advanced options.";
                    return false;
                }

                return true;
            }

            return false;
        }

        public List<Question> UpdateDatabaseAfterGameEnd(List<Question> questionsDatabase, List<Question> gameQuestionsDatabase, string gameType)
        {
            for (int i = 0; i < gameQuestionsDatabase.Count; i++)
            {
                if (questionsDatabase.Any(x=>x.QuestionString == gameQuestionsDatabase[i].QuestionString && !gameQuestionsDatabase[i].IsReusable))
                {   
                    var index = questionsDatabase.FindIndex(x=>x.QuestionString == gameQuestionsDatabase[i].QuestionString);
                    questionsDatabase[index].IsAvailable = false;
                }
            }

            var path = gameType == "gamePL" ? PathPL : PathEN;
            var jsonPath = Path.Combine(FolderPath, path + ".json");
            dataService.SaveObjectToFile(questionsDatabase, jsonPath);
            return questionsDatabase;
        }

        public List<Question> ReadExcel(string path) //temporary returning List<question>
        {
            ConnectToExcel(path);
            var fileName = Path.GetFileNameWithoutExtension(path);
            var questionsList = SerializeExcelToObjects(fileName);
            CloseExcelConnection();
            return questionsList;
        }

        private void ConnectToExcel(string source)
        {
            var path = System.IO.Path.GetFullPath(source);
            ExcelWorkbook = ExcelApplication.Workbooks.Open(path); // should be path from parameters
        }

        public void CloseExcelConnection()
        {
            ExcelWorkbook.Close();
            ExcelApplication.Quit();
        }

        public List<Question> SerializeExcelToObjects(string source)
        {
            List<Question> questionsList = new List<Question>();
            if (source == PathPL)
            {
                Excel.Worksheet excelWorkSheet = ExcelWorkbook.Sheets[1];
                Excel.Range excelRange = excelWorkSheet.UsedRange;
                var question = excelRange.Cells[1, 1].Value.ToString();
                var answersCountString = Int32.TryParse(excelRange.Cells[1, 2].Value.ToString(), out int answersCount);
                if (!answersCountString)
                {
                    return questionsList;
                }
                var lastRow = excelRange.Row + excelRange.Rows.Count - 1;
                
                for (int j = 1; j < lastRow; j += answersCount + 1)
                {
                    answersCount = Int32.Parse(excelRange.Cells[j, 2].Value.ToString());
                    ObservableCollection<Answer> answersList = new ObservableCollection<Answer>();
                    question = excelRange.Cells[j, 1].Value.ToString();
                    var questionObject = new Question
                    {
                        QuestionString = question,
                        IsAvailable = true,
                    };
                    for (int i = 1; i <= answersCount; i++)
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
                    questionsList.Add(questionObject);
                }
            }
            else if (source == PathEN)
            {
                for (int i = 1; i <= 5; i++)
                {
                    Excel.Worksheet excelWorkSheet = ExcelWorkbook.Sheets[i];
                    Excel.Range excelRange = excelWorkSheet.UsedRange;
                    var lastRow = excelRange.Row + excelRange.Rows.Count - 1;
                    var answersCount = i + 3;

                    for (int j = 2; j <= lastRow; j++)
                    {
                        ObservableCollection<Answer> answersList = new ObservableCollection<Answer>();
                        var question = excelRange.Cells[j, 1].Value.ToString();
                        var questionObject = new Question
                        {
                            QuestionString = question,
                            IsAvailable = true,
                        };
                        for (int k = 1; k < answersCount; k++)
                        {
                            var pointsToAssign = Int32.Parse(excelRange.Cells[j, 2 * k + 1].Value.ToString());
                            var answerStringToAssign = excelRange.Cells[j, 2 * k].Value.ToString();
                            Answer answerToQuestion = new Answer
                            {
                                AnswerString = answerStringToAssign,
                                Points = pointsToAssign,
                                IsVisible = false,
                            };
                            answersList.Add(answerToQuestion);
                        }

                        questionObject.Answers = answersList;
                        questionsList.Add(questionObject);
                    }
                }
            }

            return questionsList;
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
