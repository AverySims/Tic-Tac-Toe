namespace TicTacToe;

internal class Program
{
	public char PlayerChar { get; } = 'X';
	public char OpponentChar { get; } = 'O';

	private static char[,] _currentBoard = new char[3,3];

	static bool _loopMain = true;
	static bool _gameLoop = true;
	static void	Main(string[] args)
	{
		BoardManager ascii = new BoardManager();

		while (_loopMain)
		{
			while (_gameLoop)
			{
				// Reset the board to empty spaces, then randomize it
				ascii.ResetBoard(ref _currentBoard);
				ascii.PrintAsciiBoard(_currentBoard);
			
				// Print the result of the game
				Console.WriteLine();
				GameManager.PrintMatchResult(_currentBoard);
			
				Console.WriteLine();
				Console.WriteLine("Use your number pad to play.");
				if (int.TryParse(Console.ReadLine(), out int input))
				{
					Console.Clear();
				}
				else
				{
					Console.Clear();
				}
			}
		}
	}

	/// <summary>
	/// Runs a number of random matches and prints the results
	/// </summary>
	/// <param name="board">The active BoardManager instance</param>
	/// <param name="matchCount">Number of matches to run</param>
	private static void RunMatches(BoardManager board, int matchCount = 100)
	{
		// Create a dummy board to test with
		char[,] dummyBoard = new char[3,3];
		
		// Run the matches
		for (int i = 0; i < matchCount; i++)
		{
			// Reset the board to empty spaces, then randomize it
			board.ResetBoard(ref dummyBoard);
			board.RandomizeBoard(ref dummyBoard);

			GameManager.SaveWinner(GameManager.CheckWinCondition(dummyBoard));
		}
		GameManager.PrintScores();
	}
}