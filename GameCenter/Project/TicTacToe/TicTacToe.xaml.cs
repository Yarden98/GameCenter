using GameCenter.Project.TicTacToe.Models;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GameCenter.Project.TicTacToe
{
    public partial class TicTacToe : Window
    {
        TicTacToeModel GameModel;

        /*       private char _player;
               private char _currentPlayer = 'X';
               private char[,] _board = new char[3, 3];
       */
        public TicTacToe()
        {
            InitializeComponent();
            GameModel = new TicTacToeModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.Content == null)
            {
                btn.Content = GameModel.CurrentPlayer;
                int row = Grid.GetRow(btn);
                int col = Grid.GetColumn(btn);

                GameModel.Board[row, col] = GameModel.CurrentPlayer;

                if (!IsGameEnded())
                {
                    GameModel.ToggleCurrentPlayer();
                    ComputerMove();
                }
            }
        }

        private async void ComputerMove()
        {
            await Task.Delay(1000);

            Random random = new Random();
            int row, col;
            do
            {
                row = random.Next(3);
                col = random.Next(3);
            } while (GameModel.Board[row, col] != '\0');

            Button computerButton = FindButton(row, col);

            if (computerButton != null)
            {
                computerButton.Content = GameModel.CurrentPlayer;
                GameModel.Board[row, col] = GameModel.CurrentPlayer;


                if (!IsGameEnded())
                {
                    GameModel.CurrentPlayer = 'X';

                }
            }
        }

        private Button FindButton(int row, int col)
        {
            foreach (UIElement element in gameGrid.Children)

            {
                if (element is Button button && Grid.GetRow(button) == row && Grid.GetColumn(button) == col)
                {

                    return button;
                }
            }
            return null;
        }

        public void ClearGame()
        {
            GameModel.CurrentPlayer = 'X';
            GameModel.Board = new char[3, 3];

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    GameModel.Board[i, j] = '\0';
                    Button button = FindButton(i, j);
                    if (button != null)
                    {
                        button.Content = null;
                    }
                }
            }
        }

        public bool IsGameEnded()
        {
            string title = "TicTacToe";

            if (GameModel.CheckForWin())
            {
                ShowMessage($"{GameModel.CurrentPlayer} Won! \n Do you want to play again?", title);
                return true;
            }
            else if (GameModel.IsBoardFull())
            {
                ShowMessage("It's a tie!", title);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ShowMessage(string message, string title)
        {
            MessageBoxResult result = MessageBox.Show(message, title, MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                ClearGame();
            }
            else
            {
                Close();
            }
        }
    }
}
