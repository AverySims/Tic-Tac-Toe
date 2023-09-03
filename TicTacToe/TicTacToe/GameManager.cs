namespace TicTacToe;

/// <summary>
/// A static class that manages the game state
/// </summary>
public static class GameManager
{
    private static Player _matchDraw = new Player('D');
    
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
                if (board[row, col] == ' ')
                {
                    return null; // Game is still ongoing
                }
            }
        }
        
        return _matchDraw; // Draw
    }
    
    /// <summary>
    /// Add a win or draw to the appropriate counter
    /// </summary>
    /// <param name="winner">The player that is assigned the win</param>
    public static void SaveWinner(ref Player winner)
    {
        winner.wins++;
    } 
    
    /// <summary>
    /// Prints the total number of wins and draws
    /// </summary>
    /// <param name="players">Representation of active players</param>
    public static void PrintScores(Player[] players)
    {
        foreach (var player in players)
        {
            Console.WriteLine($"{player.Symbol} wins: {player.wins}");
        }
        Console.WriteLine($"Draws: {_matchDraw.wins}");
    }
}