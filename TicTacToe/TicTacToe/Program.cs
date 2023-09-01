namespace TicTacToe;

internal class Program
{
	public char PlayerChar { get; private set; } = ' ';
	public char OpponentChar { get; private set; } = 'O';

	private static char[,] currentBoard = new char[3,3];

	static bool _loopMain = true;
	static void	Main(string[] args)
	{
		GameInstance game = new GameInstance();
		AsciiClass ascii = new AsciiClass();
		
		RunMatches(ascii, 999999);
		Console.ReadLine();
		
		while (!_loopMain)
		{
			// Reset the board to empty spaces, then randomize it
			ascii.ResetBoard(ref currentBoard);
			ascii.RandomizeBoard(ref currentBoard);
			ascii.PrintAsciiBoard(currentBoard);
			
			// Print the result of the game
			Console.WriteLine();
			game.PrintResults(currentBoard);
			
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

	private static void RunMatches(AsciiClass ascii, int matches = 100)
	{
		for (int i = 0; i < matches; i++)
		{
			// Reset the board to empty spaces, then randomize it
			ascii.ResetBoard(ref currentBoard);
			ascii.RandomizeBoard(ref currentBoard);

			GameInstance.SaveWinner(GameInstance.CheckForWin(currentBoard));
		}
		GameInstance.PrintScore();
	}
}