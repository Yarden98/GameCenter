namespace GameCenter.Project.TicTacToe.Models
{
    class TicTacToeModel
    {
        public char CurrentPlayer { get; set; }
        public char[,] Board { get; set; }

        public TicTacToeModel()
        {
            CurrentPlayer = 'X';
            Board = new char[3, 3];
        }

        public bool IsBoardFull()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (Board[i, j] == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }


        public bool CheckForWin()
        {
            for (int i = 0; i < 3; i++)
            {
                if (Board[i, 0] == CurrentPlayer && Board[i, 1] == CurrentPlayer && Board[i, 2] == CurrentPlayer)
                    return true;
            }

            for (int i = 0; i < 3; i++)
            {
                if (Board[0, i] == CurrentPlayer && Board[1, i] == CurrentPlayer && Board[2, i] == CurrentPlayer)
                    return true;
            }

            if (Board[0, 0] == CurrentPlayer && Board[1, 1] == CurrentPlayer && Board[2, 2] == CurrentPlayer)
                return true;

            if (Board[0, 2] == CurrentPlayer && Board[1, 1] == CurrentPlayer && Board[2, 0] == CurrentPlayer)
                return true;

            return false;
        }

        public void ToggleCurrentPlayer()
        {
            CurrentPlayer = CurrentPlayer == 'X' ? 'O' : 'X';
        }


    }
}

