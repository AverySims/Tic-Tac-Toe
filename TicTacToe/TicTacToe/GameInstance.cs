namespace TicTacToe;

public class GameInstance
{
    static int xWins = 0;
    static int oWins = 0;
    static int draws = 0;
    
    public void PrintResults(char[,] board)
    {
        char result = CheckForWin(board);
        
        SaveWinner(result);
        
        if (result == 'X')
        {
            Console.WriteLine("Player X wins!");
        }
        else if (result == 'O')
        {
            Console.WriteLine("Player O wins!");
        }
        else
        {
            Console.WriteLine("It's a draw!");
        }
    }

    public static char CheckForWin(char[,] board)
    {
        char[] players = { 'X', 'O' };

        foreach (char player in players)
        {
            // Check rows and columns for a win
            for (int i = 0; i < 3; i++)
            {
                if ((board[i, 0] == player && board[i, 1] == player && board[i, 2] == player) ||
                    (board[0, i] == player && board[1, i] == player && board[2, i] == player))
                {
                    return player;
                }
            }

            // Check diagonals for a win
            if ((board[0, 0] == player && board[1, 1] == player && board[2, 2] == player) ||
                (board[0, 2] == player && board[1, 1] == player && board[2, 0] == player))
            {
                return player;
            }
        }

        // Check for a draw
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                if (board[row, col] != 'X' && board[row, col] != 'O')
                {
                    return ' '; // Game is still ongoing
                }
            }
        }

        return 'D'; // Draw
    }
    
    public static void SaveWinner(char winner)
    {
        switch (winner)
        {
            case 'X':
                xWins++;
                break;
            case 'O':
                oWins++;
                break;
            case 'D':
                draws++;
                break;
            default:
                break;
        }
    } 
    
    public static void PrintScore()
    {
        Console.WriteLine($"X wins: {xWins}");
        Console.WriteLine($"O wins: {oWins}");
        Console.WriteLine($"Draws: {draws}");
    }
}