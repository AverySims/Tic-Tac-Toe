namespace TicTacToe;

/// <summary>
/// A static class that manages the game state
/// </summary>
public static class GameManager
{
    public static int XWins {get; private set;} = 0;
    public static int OWins {get; private set;} = 0;
    public static int Draws {get; private set;} = 0;
    
    /// <summary>
    /// Check the board for a win condition
    /// </summary>
    /// <param name="board">2D array representing board state</param>
    /// <returns>The player who met the win condition OR returns 'D' if draw condition is met</returns>
    public static char CheckWinCondition(char[,] board)
    {
        char[] players = { 'X', 'O' };
        
        // Individual players are checked for a win
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
    
    /// <summary>
    /// Handles printing the result of the match
    /// </summary>
    /// <param name="board">2D array representing board state</param>
    public static void PrintMatchResult(char[,] board)
    {
        // Check the board for a win condition
        char result = CheckWinCondition(board);
        switch (result)
        {
            case 'X':
                Console.WriteLine("Player X wins!");
                break;
            case 'O':
                Console.WriteLine("Player O wins!");
                break;
            case 'D':
                Console.WriteLine("It's a draw!");
                break;
            default:
                break;
        }
        SaveWinner(result);
    }
    
    /// <summary>
    /// Add a win or draw to the appropriate counter
    /// </summary>
    /// <param name="winner">The win condition of the match</param>
    public static void SaveWinner(char winner)
    {
        switch (winner)
        {
            case 'X':
                XWins++;
                break;
            case 'O':
                OWins++;
                break;
            case 'D':
                Draws++;
                break;
            default:
                break;
        }
    } 
    
    /// <summary>
    /// Prints the total number of wins and draws
    /// </summary>
    public static void PrintScores()
    {
        Console.WriteLine($"X wins: {XWins}");
        Console.WriteLine($"O wins: {OWins}");
        Console.WriteLine($"Draws: {Draws}");
    }
}