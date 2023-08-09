using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FamilyFeud.MVVM.Commands;
using FamilyFeud.MVVM.Model;
using FamilyFeud.MVVM.ViewModel;

namespace FamilyFeud.Commands
{
    public class WrongAnswerClick : CommandBase
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly Game _game;

        public WrongAnswerClick(MainWindowViewModel mainWindowViewModel, Game _game)
        {
            _mainWindowViewModel = mainWindowViewModel;
        }

        public override void Execute(object parameter)
        {
            if (_mainWindowViewModel.IsFirstTeamAnswering)
            {
                switch (_game.WrongAnswers)
                {
                    case 0: 
                        _game.AddWrongAnswer();
                        _mainWindowViewModel.IsSymbolVisible11 = true;
                        break;
                    case 1:
                        _game.AddWrongAnswer();
                        _mainWindowViewModel.IsSymbolVisible12 = true;
                        break;
                    case 2:
                        _game.AddWrongAnswer();
                        _mainWindowViewModel.IsSymbolVisible13 = true;
                        break;
                    case 3:
                        _game.AddWrongAnswer();
                        _mainWindowViewModel.IsSymbolVisible2 = true;
                        break;
                    default:
                        _mainWindowViewModel.IsSymbolVisible11 = _mainWindowViewModel.IsSymbolVisible12 =
                            _mainWindowViewModel.IsSymbolVisible13 = _mainWindowViewModel.IsSymbolVisible2 = false;
                        _game.ResetWrongAnswers();
                        break;
                }
            }
            else if (_mainWindowViewModel.IsSecondTeamAnswering)
            {
                switch (_game.WrongAnswers)
                {
                    case 0:
                        _game.AddWrongAnswer();
                        _mainWindowViewModel.IsSymbolVisible21 = true;
                        break;
                    case 1:
                        _game.AddWrongAnswer();
                        _mainWindowViewModel.IsSymbolVisible22 = true;
                        break;
                    case 2:
                        _game.AddWrongAnswer();
                        _mainWindowViewModel.IsSymbolVisible23 = true;
                        break;
                    case 3:
                        _game.AddWrongAnswer();
                        _mainWindowViewModel.IsSymbolVisible1 = true;
                        break;
                    default:
                        _mainWindowViewModel.IsSymbolVisible21 = _mainWindowViewModel.IsSymbolVisible22 =
                            _mainWindowViewModel.IsSymbolVisible23 = _mainWindowViewModel.IsSymbolVisible1 = false;
                        _game.ResetWrongAnswers();
                        break;
                }
            }
        }
    }
}
