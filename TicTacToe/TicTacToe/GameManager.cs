namespace TicTacToe;

/// <summary>
/// A static class that manages the game state
/// </summary>
public static class GameManager
{
    public static int P1Wins {get; private set;} = 0;
    public static int P2Wins {get; private set;} = 0;
    public static int Draws {get; private set;} = 0;
    
    /// <summary>
    /// Check the board for a win condition
    /// </summary>
    /// <param name="board">2D array representing board state</param>
    /// <param name="player">Reference to the player that just played</param>>
    /// <returns>The player who met the win condition OR returns 'D' if draw condition is met</returns>
    public static Player CheckWinCondition(char[,] board, Player player)
    {
        // Check rows and columns for a win
        for (int i = 0; i < 3; i++)
        {
            if ((board[i, 0] == player.Symbol && board[i, 1] == player.Symbol && board[i, 2] == player.Symbol) ||
                (board[0, i] == player.Symbol && board[1, i] == player.Symbol && board[2, i] == player.Symbol))
            {
                return player;
            }
        }

        // Check diagonals for a win
        if ((board[0, 0] == player.Symbol && board[1, 1] == player.Symbol && board[2, 2] == player.Symbol) ||
            (board[0, 2] == player.Symbol && board[1, 1] == player.Symbol && board[2, 0] == player.Symbol))
        {
            return player;
        }
        
        // Check for a draw
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                if (board[row, col] != ' ')
                {
                    return null; // Game is still ongoing
                }
            }
        }
        
        return new Player('D'); // Draw
    }
    
    /// <summary>
    /// Add a win or draw to the appropriate counter
    /// </summary>
    /// <param name="winner">The player that is assigned the win</param>
    public static void SaveWinner(Player winner)
    {
        winner.wins++;
    } 
    
    /// <summary>
    /// Prints the total number of wins and draws
    /// </summary>
    /// <param name="p1">Representation of player 1</param>
    /// <param name="p2">Representation of player 2</param>
    public static void PrintScores(char p1, char p2)
    {
        Console.WriteLine($"{p1} wins: {P1Wins}");
        Console.WriteLine($"{p2} wins: {P2Wins}");
        Console.WriteLine($"Draws: {Draws}");
    }
}